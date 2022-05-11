using AutoMapper;
using Eshop.DTO;
using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop
{
    public class EshopMappingProfile : Profile
    {
        public EshopMappingProfile()
        {
            CreateMap<PostModel, SoldPostModel>();
            
            CreateMap<CreatePostDTO, PostModel>();

            CreateMap<MessageDTO, ReceivedMessageModel>()
                        .ForMember(r => r.ReplyId, m => m.MapFrom(dto => dto.UserId))
                        .ForMember(r => r.PostName, m => m.MapFrom(dto => dto.OfferTitle))
                        .ForMember(r => r.UserId, m => m.MapFrom(dto => dto.OfferOwnerId));

            CreateMap<MessageDTO, SentMessageModel>()
                        .ForMember(s => s.PostName, m => m.MapFrom(dto => dto.OfferTitle));

            CreateMap<CreateCommentDTO, CommentModel>()
                        .ForMember(c => c.PostUserId, m => m.MapFrom(dto => dto.ReceiverUserId));                                                                                                  
        }
    }
}
