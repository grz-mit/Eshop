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
            CreateMap<OfferModel, OfferEndedModel>();

            CreateMap<CreateOfferDTO, OfferModel>();

            CreateMap<MessageDTO, ReceivedMessageModel>()
                        .ForMember(r => r.ReplyId, m => m.MapFrom(dto => dto.UserId))
                        .ForMember(r => r.OfferTitle, m => m.MapFrom(dto => dto.OfferTitle))
                        .ForMember(r => r.UserId, m => m.MapFrom(dto => dto.OfferOwnerId));

            CreateMap<MessageDTO, SentMessageModel>()
                        .ForMember(s => s.OfferTitle, m => m.MapFrom(dto => dto.OfferTitle));

            CreateMap<CreateCommentDTO, CommentModel>()
                        .ForMember(c => c.OfferUserId, m => m.MapFrom(dto => dto.ReceiverUserId));                                                                                                  
        }
    }
}
