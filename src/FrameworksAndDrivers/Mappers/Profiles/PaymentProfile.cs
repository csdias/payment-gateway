using EnterpriseBusinessRules.Entities;
using FrameworksAndDrivers.Database.Models;

namespace FrameworksAndDrivers.Mappers.Profiles
{
    public class PaymentProfile : AutoMapper.Profile
    {
        public PaymentProfile()
        {
            CreateMap<PaymentModel, Payment>();
            CreateMap<Payment, PaymentModel>();
        }
    }
}
