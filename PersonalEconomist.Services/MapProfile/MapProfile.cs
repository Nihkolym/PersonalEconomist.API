using AutoMapper;
using PersonalEconomist.Domain.Models.Activity;
using PersonalEconomist.Domain.Models.Counter;
using PersonalEconomist.Domain.Models.CreditCard;
using PersonalEconomist.Domain.Models.Goal;
using PersonalEconomist.Domain.Models.Indication;
using PersonalEconomist.Domain.Models.Item;
using PersonalEconomist.Domain.Models.Transaction;
using PersonalEconomist.Domain.Models.User;
using PersonalEconomist.Entities.Models.Activity;
using PersonalEconomist.Entities.Models.Counter;
using PersonalEconomist.Entities.Models.CreditCard;
using PersonalEconomist.Entities.Models.Goal;
using PersonalEconomist.Entities.Models.Indication;
using PersonalEconomist.Entities.Models.Item;
using PersonalEconomist.Entities.Models.Transaction;
using PersonalEconomist.Entities.Models.User;
using System.Collections.Generic;
using System.Linq;

namespace PersonalEconomist.Services.MapProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<GoalDTO, Goal>().ReverseMap();
            CreateMap<ItemDTO, Item>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<CreditCardDTO, CreditCard>().ReverseMap();
            CreateMap<ActivityDTO, Activity>().ReverseMap();
            CreateMap<IndicationDTO, Indication>().ReverseMap();
            CreateMap<CounterDTO, Counter>().ReverseMap();
            CreateMap<Transaction, TransactionDTO>()
                .ForMember(t=> t.Items, opt => opt
                    .MapFrom(t => t.TransactionItems.Select(ti => ti.Item).ToList())).ReverseMap();
        }
    }
}