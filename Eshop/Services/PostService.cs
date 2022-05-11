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
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserContextService _userContextService;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IUserRepository _userRepository;
        private readonly ISoldPostRepository _soldPostRepository;
        private readonly IMapper _mapper;

        public PostService(IMapper mapper, IPostRepository postRepository, IUserContextService userContextService,
            IShoppingCartRepository shoppingCartRepository, IWebHostEnvironment hostEnvironment, 
            IUserRepository userRepository, ISoldPostRepository soldPostRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
            _userContextService = userContextService;
            _shoppingCartRepository = shoppingCartRepository;
            _hostEnvironment = hostEnvironment;
            _userRepository = userRepository;
            _soldPostRepository = soldPostRepository;
        }

        public async Task<List<PostModel>> FilteredPosts(string searchString, string postCategory, decimal postPriceFrom, decimal postPriceTo)
        {
            var posts = _postRepository.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(s => s.Title.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(postCategory))
            {
                posts = posts.Where(x => x.Category == postCategory);
            }
            if (postPriceFrom != 0)
            {
                posts = posts.Where(x => x.Price >= postPriceFrom);
            }
            if (postPriceTo != 0)
            {
                posts = posts.Where(x => x.Price <= postPriceTo);
            }

            return await posts.ToListAsync();
        }

        public async Task<SelectList> Categories()
        {
            var posts = _postRepository.GetAll();

            List<string> category = await posts.OrderBy(p => p.Category)
                                               .Select(p => p.Category)
                                               .Distinct()
                                               .ToListAsync();

            var genres = new SelectList(category);

            return genres;
        }
 
        public async Task<PostDetailsViewModel> GetDetailPostVM(int? id)
        {
            var inCart = false;
            var posts = _postRepository.GetAll();          
            var post = await posts.Include(p=>p.User)
                                  .Include(p=>p.GalleryModel.ImageModel)
                                  .FirstOrDefaultAsync(p => p.Id == id);

            var userCartItems = await _shoppingCartRepository.GetUserCartItems();
            
            if (post == null)
                throw new NotFoundException();

            if (userCartItems.Any(p => p.PostId == id))
                inCart = true;

            var otherPosts = await posts.Where(p => p.UserId == post.UserId && p.Id != id).ToListAsync();

            var postDetailsVM = new PostDetailsViewModel
            {
                Post = post,
                OtherPosts = otherPosts,
                UserId = _userContextService.UserId,
                InCart = inCart
            };
            
            return postDetailsVM;
        }

        public async Task<PostModel> PostToDelete(int? id)
        {
            var post = await _postRepository.GetById(id);

            if (post == null)
                throw new NotFoundException();

            if (post.UserId != _userContextService.UserId)
                throw new UnauthorizedAccessException();

            return post;
        }

        public async Task DeletePost(int id)
        {
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images");
            var post = await _postRepository.GetById(id);

            foreach(var image in post.GalleryModel.ImageModel)
            {
                if(image.Name != "noimage.png")
                {
                    File.Delete(imagePath + "\\" + image.Name);
                }
            }

            await _postRepository.DeletePost(post);
        }

        public async Task<BuyViewModel> PostToBuy(int? id)
        {

            var buyer = await _userRepository.GetById(_userContextService.UserId);
            var post = await _postRepository.GetById(id);

            if (post == null)
                throw new NotFoundException();

            if (post.UserId == buyer.Id)
                throw new UnauthorizedAccessException();

            var buyVM = new BuyViewModel()
            {
                Post = post,
                BuyerWallet = buyer.Wallet,
                WalletAfterBuy = buyer.Wallet - post.Price
            };

            return buyVM;
        }

        public async Task BuyPost(int id, SoldPostModel soldPost)
        {
            var index = 0;
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images");
            var buyer = await _userRepository.GetById(_userContextService.UserId);
            var post = await _postRepository.GetById(id);
            
            var seller = post.User;

            var sellerWalletBallance = seller.Wallet + post.Price;

            if (sellerWalletBallance > 999999.99m)
                throw new BalanceExceededException();

            if(buyer.Wallet - post.Price >= 0)
            {
                seller.Wallet = seller.Wallet + post.Price;
                buyer.Wallet = buyer.Wallet - post.Price;

                soldPost.UserWhoBought = _userContextService.UserId;
                soldPost.SoldDate = DateTime.Now;

                await _soldPostRepository.CreateSoldPost(soldPost);
                await _userRepository.UpdateUser(seller);
                await _userRepository.UpdateUser(buyer);
                await _postRepository.DeletePost(post);

                foreach (var image in post.GalleryModel.ImageModel)
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

        public async Task CreatePost (CreatePostDTO createPostDTO)
        {      
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string path;
            string fileName;
            string extension;

            var post = _mapper.Map<PostModel>(createPostDTO);
            post.GalleryModel = new GalleryModel();
            post.GalleryModel.ImageModel = new List<ImageModel>();
            post.UserId = _userContextService.UserId;
            post.CreateDate = DateTime.Now;
            
            if(createPostDTO.ImageFile != null)
            {
                foreach(IFormFile image in createPostDTO.ImageFile)
                {
                    fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    extension = Path.GetExtension(image.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                    var imageModel = new ImageModel()
                    {
                        Name = fileName
                    };                   

                    post.GalleryModel.ImageModel.Add(imageModel);
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

                post.GalleryModel.ImageModel.Add(imageModel);
            }

            await _postRepository.CreatePost(post);

        }

        public async Task<List<PostModel>> OffersOwnedByUser()
        {
            var offers = await _postRepository.OwnedByUser();
            return offers;
        }
    }
}
