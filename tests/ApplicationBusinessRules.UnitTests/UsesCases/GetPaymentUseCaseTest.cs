using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using FluentAssertions;
using FluentAssertions.Execution;
using EnterpriseBusinessRules.Entities;
using ApplicationBusinessRules.Interfaces;
using ApplicationBusinessRules.UseCases;

namespace ApplicationBusinessRules.UnitTests.UseCases 
{
    public class GetPaymentUseCaseTest 
    {
        private readonly Mock<IPaymentRepository> _mockPaymentRepository;
        public GetPaymentUseCaseTest() {
            this._mockPaymentRepository = new Mock<IPaymentRepository>(MockBehavior.Strict);
        }

        [Fact]
        public async void GetPayment_Should_BeCalledOnce() {
            var expectedResponseFromRepository = new Response<Payment>();
            
            this._mockPaymentRepository.Setup(s => s.GetPayment(It.IsAny<Guid>()))
                     .Returns(() => Task.Run( () => expectedResponseFromRepository));

            var sut = new GetPaymentUseCase(this._mockPaymentRepository.Object);
            var result = await sut.GetPayment(Guid.NewGuid());

            this._mockPaymentRepository.Verify(x => x.GetPayment(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void GetPayment_Should_BeOk() {
            var expectedPayment = GetExpectedPayment();
            var expectedResponseFromRepository = new Response<Payment>();
            expectedResponseFromRepository.SetSuccess(true);
            expectedResponseFromRepository.SetResponse(expectedPayment);

            this._mockPaymentRepository.Setup(s => s.GetPayment(It.IsAny<Guid>()))
                    .Returns(() => Task.Run(() => expectedResponseFromRepository));
            
            var sut = new GetPaymentUseCase(this._mockPaymentRepository.Object);
            var result = await sut.GetPayment(Guid.NewGuid());

            using(new AssertionScope()){
                result.IsOk().Should().BeTrue();
                result.HasErrors().Should().BeFalse();
                result.HasException().Should().BeFalse();
                result.GetResponse().Should().Be(expectedResponseFromRepository.GetResponse());
            }
        }

        private Payment GetExpectedPayment() { 
            return GetExpectedPayments().FirstOrDefault();
        }
    
        private List<Payment> GetExpectedPayments() {
            var payI = new Payment() { };
            var payII = new Payment() { };

            return new List<Payment>() {
                payI,
                payII
            };
        }
    }
}
