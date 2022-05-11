using Eshop.DTO;
using Eshop.Models;
using Eshop.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Eshop.Services
{
    public interface IPostService
    {
        Task<List<PostModel>> FilteredPosts(string searchString, string postCategory, decimal postPriceFrom, decimal postPriceTo);
        Task<SelectList> Categories();
        Task<PostDetailsViewModel> GetDetailPostVM(int? id);
        Task<PostModel> PostToDelete(int? id);
        Task DeletePost(int id);
        Task<BuyViewModel> PostToBuy(int? id);
        Task BuyPost(int id, SoldPostModel soldPost);
        Task CreatePost(CreatePostDTO createPostDTO);
        Task<List<PostModel>> OffersOwnedByUser();
    }
}