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
using ApplicationBusinessRules.Services;

namespace ApplicationBusinessRules.UnitTests.UseCases 
{
    public class PaymentServiceTest 
    {
        private readonly Mock<IGetPaymentUseCase> _mockGetPaymentUseCase;
        private readonly Mock<ICreatePaymentUseCase> _mockCreatePaymentUseCase;
        private readonly Mock<IUpdatePaymentStatusUseCase> _mockUpdatePaymentStatusUseCase;

        public PaymentServiceTest() {
            this._mockGetPaymentUseCase = new Mock<IGetPaymentUseCase>(MockBehavior.Strict);
            this._mockCreatePaymentUseCase = new Mock<ICreatePaymentUseCase>(MockBehavior.Strict);
            this._mockUpdatePaymentStatusUseCase = new Mock<IUpdatePaymentStatusUseCase>(MockBehavior.Strict);
        }

        [Fact]
        public async void GetPayment_Should_BeCalledOnce() {
            // Arrange
            var expectedResponseFromUseCase = new Response<Payment>();
            
            _mockGetPaymentUseCase.Setup(s => s.GetPayment(It.IsAny<Guid>()))
                     .Returns(() => Task.Run( () => expectedResponseFromUseCase));
            var sut = new PaymentService(_mockGetPaymentUseCase.Object, _mockCreatePaymentUseCase.Object
                , _mockUpdatePaymentStatusUseCase.Object);

            // Act
            var result = await sut.GetPayment(Guid.NewGuid());

            // Assert
            _mockGetPaymentUseCase.Verify(x => x.GetPayment(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void GetPayment_Should_BeOk() {
            // Assert
            var expectedPayment = GetExpectedPayment();
            var expectedResponseFromUseCase = new Response<Payment>();
            expectedResponseFromUseCase.SetSuccess(true);
            expectedResponseFromUseCase.SetResponse(expectedPayment);

            this._mockGetPaymentUseCase.Setup(s => s.GetPayment(It.IsAny<Guid>()))
                    .Returns(() => Task.Run(() => expectedResponseFromUseCase));

            var sut = new PaymentService(_mockGetPaymentUseCase.Object, _mockCreatePaymentUseCase.Object
                , _mockUpdatePaymentStatusUseCase.Object);

            // Act
            var result = await sut.GetPayment(Guid.NewGuid());

            // Assert
            using(new AssertionScope()){
                result.IsOk().Should().BeTrue();
                result.HasErrors().Should().BeFalse();
                result.HasException().Should().BeFalse();
                result.GetResponse().Should().BeEquivalentTo(expectedResponseFromUseCase.GetResponse());
            }
        }

        private Payment GetExpectedPayment() { 
            return GetExpectedPayments().FirstOrDefault();
        }

        private List<Payment> GetExpectedPayments() {
            var payI = new Payment() {};
            var payII = new Payment() {};

            return new List<Payment>() {
                payI,
                payII
            };
        }
    }
}
