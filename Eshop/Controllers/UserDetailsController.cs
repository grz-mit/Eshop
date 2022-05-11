using Eshop.Data;
using Eshop.Models;
using Eshop.Services;
using Eshop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Eshop.Controllers
{
    public class UserDetailsController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;

        public UserDetailsController(ICommentService commentService, IUserService userService)
        {
            _commentService = commentService;
            _userService = userService;
        }

        public async Task<IActionResult> UserDetails()
        {
            var comments = await _commentService.GetReceivedComments();
            var commentsVM = new CommentsViewModel()
            {
                User = await _userService.GetUserById(),
                Comments = comments,
                Avg = _commentService.CalculateAvgRating(comments),
                ComCount = comments.Count().ToString()
            };

            return View(commentsVM);           
        }
        public async Task<IActionResult> UserDetailsOffer(string offerOwnerId, int offerId)
        {
            var comments = await _commentService.GetReceivedComments(offerOwnerId);
            var commentsVM = new CommentsViewModel()
            {
                LoggedUser = await _userService.GetUserById(),
                User = await _userService.GetUserById(offerOwnerId),
                OfferId = offerId,
                OfferOwnerId = offerOwnerId,
                Comments = comments,
                Avg = _commentService.CalculateAvgRating(comments),
                ComCount = comments.Count().ToString()
            };

            return View(commentsVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReply(ReplyComModel reply)
        {
            await _commentService.CreateReply(reply);
            
            return RedirectToAction(nameof(UserDetails));           
        }
    }
}