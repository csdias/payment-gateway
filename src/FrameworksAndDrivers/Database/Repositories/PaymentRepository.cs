using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using EnterpriseBusinessRules.Entities;
using ApplicationBusinessRules.Helpers;
using InterfaceAdapters.Interfaces;
using ApplicationBusinessRules.Interfaces;
using FrameworksAndDrivers.Database.Models;
using FrameworksAndDrivers.Database.Contexts;
using System.Collections.Generic;
using EnterpriseBusinessRules.Entities.ResourceParameters;

namespace FrameworksAndDrivers.Database.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public PaymentRepository(ApplicationDbContext dbContext, IMapper mapper,
            IPropertyMappingService propertyMappingService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _propertyMappingService = propertyMappingService;
        }

        public async Task<Response<Payment>> GetPayment(Guid id)
        {
            try
            {
                var pay = await this._dbContext.Payments.AsNoTracking()
                    .Where(item => item.Id == id)
                    .FirstOrDefaultAsync();
                if (pay == null)
                {
                    return new Response<Payment>()
                        .SetStatus(404)
                        .SetSuccess(false)
                        .AddMessage("Payment not found");
                }
                return new Response<Payment>()
                    .SetSuccess(true)
                    .SetResponse(_mapper.Map<Payment>(pay));
            }
            catch (Exception exception)
            {
                return new Response<Payment>()
                    .SetException(exception);
            }
        }

        public PagedList<Payment> GetPayments(PaymentResourceParameters paymentResourceParameters)
        {
            if (paymentResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(paymentResourceParameters));
            }

            var collection = _dbContext.Payments as IQueryable<Payment>;

            if (!string.IsNullOrWhiteSpace(paymentResourceParameters.SearchQuery))
            {

                var searchQuery = paymentResourceParameters.SearchQuery.Trim();
                collection = collection.Where(a => a.ClientId == searchQuery
                    || (a.Id != null && a.Id.ToString().Contains(searchQuery)));
            }

            if (!string.IsNullOrWhiteSpace(paymentResourceParameters.OrderBy))
            {
                // get property mapping dictionary
                var authorPropertyMappingDictionary =
                    _propertyMappingService.GetPropertyMapping<Payment, PaymentModel>();

                collection = collection.ApplySort(paymentResourceParameters.OrderBy,
                    authorPropertyMappingDictionary);
            }

            return PagedList<Payment>.Create(collection,
                paymentResourceParameters.PageNumber,
                paymentResourceParameters.PageSize);
        }

        public async Task<Response<List<Payment>>> GetPayments(Payment payment)
        {
            try
            {
                var pays = await this._dbContext.Payments.AsNoTracking()
                    .Where(item => item.ClientId == payment.ClientId)
                    .FirstOrDefaultAsync();
                if (pays == null)
                {
                    return new Response<List<Payment>>()
                        .SetStatus(404)
                        .SetSuccess(false)
                        .AddMessage("Payments not found");
                }
                return new Response<List<Payment>>()
                    .SetSuccess(true)
                    .SetResponse(_mapper.Map<List<Payment>>(pays));
            }
            catch (Exception exception)
            {
                return new Response<List<Payment>>()
                    .SetException(exception);
            }
        }

        public async Task<Response<bool>> CreatePayment(Payment payment)
        {
            try
            {  
                var validate = ValidatorHelper.ValidateEntity<bool>(payment);
                if(validate.HasErrors()) {
                    return validate;
                }
                await _dbContext.Payments.AddAsync(
                    this._mapper.Map<PaymentModel>(payment)
                );
                await _dbContext.SaveChangesAsync();
                return new Response<bool>()
                    .SetSuccess(true)
                    .SetResponse(true);
            }
            catch (Exception exception)
            {
                return new Response<bool>()
                    .SetException(exception);
            }
        }
   
        public async Task<Response<Payment>> UpdatePaymentStatus(Payment payment)
        {
            try
            {
                var validate = ValidatorHelper.ValidateEntity<Payment>(payment);
                if(validate.HasErrors()) {
                    return validate;
                }
                var pay = await this._dbContext.Payments
                    .Where(item => item.Id == payment.Id)
                    .FirstOrDefaultAsync();              
                if (pay != null) {
                    await this._dbContext.SaveChangesAsync();
                } else {
                     return new Response<Payment>()
                    .SetSuccess(false)
                    .AddMessage("Payment not found");
                }
                return new Response<Payment>()
                    .SetSuccess(true)
                    .SetResponse(_mapper.Map<Payment>(pay));
            }
            catch (Exception exception)
            {
                return new Response<Payment>()
                    .SetException(exception);
            }
        }

    }
}
