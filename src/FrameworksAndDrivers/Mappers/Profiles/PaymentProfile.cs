using ApplicationBusinessRules;
using AutoMapper;
using EnterpriseBusinessRules.Entities;
using EnterpriseBusinessRules.Sns.Entities;
using FrameworksAndDrivers.Database.Models;

namespace FrameworksAndDrivers.Mappers.Profiles
{
    public class PaymentProfile : AutoMapper.Profile
    {
        public PaymentProfile()
        {
            CreateMap<PaymentModel, Payment>();
            CreateMap<Payment, PaymentModel>();

            CreateMap<OutboxMessage, SnsMessage>()
                .ForMember(dest => dest.Payload,
                    opt => opt.ConvertUsing(new JsonObjectConverter(), src => src.Payload));

        }
    }
}
