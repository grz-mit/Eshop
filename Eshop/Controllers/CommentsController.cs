using Eshop.Data;
using Eshop.DTO;
using Eshop.Models;
using Eshop.Services;
using Eshop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Controllers
{
    public class CommentsController : Controller
    {
        private readonly IOffersEndedService _offersEndedService;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        public CommentsController(IOffersEndedService offersEndedService, ICommentService commentService, IUserService userService)
        {
            _offersEndedService = offersEndedService;
            _commentService = commentService;
            _userService = userService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var offerBought = await _offersEndedService.GetBoughtOffer(id);
            var comments = await _commentService.GetReceivedComments(offerBought.User.Id);    

            var commentVM = new CommentViewModel()
            {
                LoggedUser = await _userService.GetUserById(), //Logged user
                User = await _userService.GetUserById(offerBought.User.Id), //Seller
                Comments = comments, //Seller comments
                OfferBought = offerBought, //Bought offer
                Avg = _commentService.CalculateAvgRating(comments), //Avg seller rating from comments
                ComCount = comments.Count().ToString() //Seller Comments Count
            };
            
            return View(commentVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCom(CreateCommentDTO createCommentDTO)
        {
            await _commentService.CreateComment(createCommentDTO);
            
            return RedirectToAction("Index", new { id = createCommentDTO.OfferEndedId });        
        }

    }
}
