using AutoMapper;
using Eshop.DTO;
using Eshop.Models;
using Eshop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IReplyRepository _replyRepository;
        private readonly IMapper _mapper;
        public CommentService(ICommentRepository commentRepository, IUserRepository userRepository, IReplyRepository replyRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _replyRepository = replyRepository;
            _mapper = mapper;
        }

        public async Task<List<CommentModel>> GetReceivedComments()
        {
            var userComments = await _commentRepository.GetReceivedComments();
            userComments.Reverse();

            return userComments;
        }

        public async Task<List<CommentModel>> GetReceivedComments(string userId)
        {
            var userComments = await _commentRepository.GetReceivedComments(userId);
            userComments.Reverse();

            return userComments;
        }

        public string CalculateAvgRating(List<CommentModel> userComments)
        {
            decimal avg = 0;
            int commentsCount = 0;

            if (userComments.Any() == true)
            {
                foreach (var item in userComments)
                {
                    avg = avg + item.Rate;
                    commentsCount++;
                }
                avg = avg / commentsCount;

                return Math.Round(avg, 2).ToString();
            }
            else
            {
                return "Brak Ocen";
            }
        }

        public async Task CreateReply (ReplyComModel reply)
        {
            var user = await _userRepository.GetById();

            reply.NickName = user.NickName;          
            await _replyRepository.CreateReply(reply);
        }

        public async Task CreateComment(CreateCommentDTO createCommentDTO)
        {
            var user = await _userRepository.GetById();
            var comment = _mapper.Map<CommentModel>(createCommentDTO);
            comment.UserId = user.Id;
            comment.NickName = user.NickName;

            await _commentRepository.CreateComment(comment);            
        }
    }
}
