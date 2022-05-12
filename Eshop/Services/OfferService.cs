using AutoMapper;
using Eshop.DTO;
using Eshop.Exceptions;
using Eshop.Models;
using Eshop.Repositories;
using Eshop.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Services
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IUserContextService _userContextService;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IUserRepository _userRepository;
        private readonly ISoldPostRepository _soldPostRepository;
        private readonly IMapper _mapper;

        public OfferService(IMapper mapper, IOfferRepository offerRepository, IUserContextService userContextService,
            IShoppingCartRepository shoppingCartRepository, IWebHostEnvironment hostEnvironment, 
            IUserRepository userRepository, ISoldPostRepository soldPostRepository)
        {
            _mapper = mapper;
            _offerRepository = offerRepository;
            _userContextService = userContextService;
            _shoppingCartRepository = shoppingCartRepository;
            _hostEnvironment = hostEnvironment;
            _userRepository = userRepository;
            _soldPostRepository = soldPostRepository;
        }

        public async Task<List<OfferModel>> FilteredOffers(string searchString, string offerCategory, decimal offerPriceFrom, decimal offerPriceTo)
        {
            var offers = _offerRepository.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                offers = offers.Where(s => s.Title.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(offerCategory))
            {
                offers = offers.Where(x => x.Category == offerCategory);
            }
            if (offerPriceFrom != 0)
            {
                offers = offers.Where(x => x.Price >= offerPriceFrom);
            }
            if (offerPriceTo != 0)
            {
                offers = offers.Where(x => x.Price <= offerPriceTo);
            }

            return await offers.ToListAsync();
        }

        public async Task<SelectList> Categories()
        {
            var offers = _offerRepository.GetAll();

            List<string> category = await offers.OrderBy(p => p.Category)
                                               .Select(p => p.Category)
                                               .Distinct()
                                               .ToListAsync();

            var genres = new SelectList(category);

            return genres;
        }
 
        public async Task<OfferDetailsViewModel> GetDetailsOfferVM(int? id)
        {
            var inCart = false;
            var offers = _offerRepository.GetAll();          
            var offer = await offers.Include(p=>p.User)
                                  .Include(p=>p.GalleryModel.ImageModel)
                                  .FirstOrDefaultAsync(p => p.Id == id);

            var userCartItems = await _shoppingCartRepository.GetUserCartItems();
            
            if (offer == null)
                throw new NotFoundException();

            if (userCartItems.Any(p => p.OfferId == id))
                inCart = true;

            var otherOffers = await offers.Where(p => p.UserId == offer.UserId && p.Id != id).ToListAsync();

            var offerDetailsVM = new OfferDetailsViewModel
            {
                Offer = offer,
                OtherOffers = otherOffers,
                UserId = _userContextService.UserId,
                InCart = inCart
            };
            
            return offerDetailsVM;
        }

        public async Task<OfferModel> OfferToDelete(int? id)
        {
            var offer = await _offerRepository.GetById(id);

            if (offer == null)
                throw new NotFoundException();

            if (offer.UserId != _userContextService.UserId)
                throw new UnauthorizedAccessException();

            return offer;
        }

        public async Task DeleteOffer(int id)
        {
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images");
            var offer = await _offerRepository.GetById(id);

            foreach(var image in offer.GalleryModel.ImageModel)
            {
                if(image.Name != "noimage.png")
                {
                    File.Delete(imagePath + "\\" + image.Name);
                }
            }

            await _offerRepository.DeleteOffer(offer);
        }

        public async Task<BuyViewModel> OfferToBuy(int? id)
        {

            var buyer = await _userRepository.GetById(_userContextService.UserId);
            var offer = await _offerRepository.GetById(id);

            if (offer == null)
                throw new NotFoundException();

            if (offer.UserId == buyer.Id)
                throw new UnauthorizedAccessException();

            var buyVM = new BuyViewModel()
            {
                Offer = offer,
                BuyerWallet = buyer.Wallet,
                WalletAfterBuy = buyer.Wallet - offer.Price
            };

            return buyVM;
        }

        public async Task BuyOffer(int id, SoldPostModel soldPost)
        {
            var index = 0;
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images");
            var buyer = await _userRepository.GetById(_userContextService.UserId);
            var offer = await _offerRepository.GetById(id);
            
            var seller = offer.User;

            var sellerWalletBallance = seller.Wallet + offer.Price;

            if (sellerWalletBallance > 999999.99m)
                throw new BalanceExceededException();

            if(buyer.Wallet - offer.Price >= 0)
            {
                seller.Wallet = seller.Wallet + offer.Price;
                buyer.Wallet = buyer.Wallet - offer.Price;

                soldPost.UserWhoBought = _userContextService.UserId;
                soldPost.SoldDate = DateTime.Now;

                await _soldPostRepository.CreateSoldPost(soldPost);
                await _userRepository.UpdateUser(seller);
                await _userRepository.UpdateUser(buyer);
                await _offerRepository.DeleteOffer(offer);

                foreach (var image in offer.GalleryModel.ImageModel)
                {
                    if (image.Name != "noimage.png" && index != 0)
                    {
                        File.Delete(imagePath + "\\" + image.Name);
                    }

                    index++;
                } 
            }
            else
            {
                throw new NegativeBalanceException();
            }
        }

        public async Task CreateOffer (CreateOfferDTO createOfferDTO)
        {      
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string path;
            string fileName;
            string extension;

            var offer = _mapper.Map<OfferModel>(createOfferDTO);
            offer.GalleryModel = new GalleryModel();
            offer.GalleryModel.ImageModel = new List<ImageModel>();
            offer.UserId = _userContextService.UserId;
            offer.CreateDate = DateTime.Now;
            
            if(createOfferDTO.ImageFile != null)
            {
                foreach(IFormFile image in createOfferDTO.ImageFile)
                {
                    fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    extension = Path.GetExtension(image.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                    var imageModel = new ImageModel()
                    {
                        Name = fileName
                    };                   

                    offer.GalleryModel.ImageModel.Add(imageModel);
                    path = Path.Combine(wwwRootPath + "/Images/", fileName);
                    
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }
                }
            }
            else
            {
                var imageModel = new ImageModel()
                {
                    Name = "noimage.png"
                };

                offer.GalleryModel.ImageModel.Add(imageModel);
            }

            await _offerRepository.CreateOffer(offer);

        }

        public async Task<List<OfferModel>> OffersOwnedByUser()
        {
            var offers = await _offerRepository.OwnedByUser();
            return offers;
        }
    }
}
