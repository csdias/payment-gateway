using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using EnterpriseBusinessRules.Entities;
using ApplicationBusinessRules.Helpers;
using ApplicationBusinessRules.Interfaces;
using FrameworksAndDrivers.Database.Models;
using FrameworksAndDrivers.Database.Contexts;
using System.Collections.Generic;

namespace FrameworksAndDrivers.Database.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public PaymentRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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
