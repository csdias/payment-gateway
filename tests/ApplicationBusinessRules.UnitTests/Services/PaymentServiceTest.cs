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
        private readonly Mock<IValidateCreditCardUseCase> _validateCreditCardUseCase;
        private readonly Mock<IPaymentQueueProcessorService> _paymentQueueProcessorService;

        public PaymentServiceTest() {
            _mockGetPaymentUseCase = new Mock<IGetPaymentUseCase>(MockBehavior.Strict);
            _mockCreatePaymentUseCase = new Mock<ICreatePaymentUseCase>(MockBehavior.Strict);
            _mockUpdatePaymentStatusUseCase = new Mock<IUpdatePaymentStatusUseCase>(MockBehavior.Strict);
            _validateCreditCardUseCase = new Mock<IValidateCreditCardUseCase>(MockBehavior.Strict);
            _paymentQueueProcessorService = new Mock<IPaymentQueueProcessorService>(MockBehavior.Strict);
        }

        [Fact]
        public async void GetPayment_Should_BeCalledOnce() {
            // Arrange
            var expectedResponseFromUseCase = new Response<Payment>();
            
            _mockGetPaymentUseCase.Setup(s => s.GetPayment(It.IsAny<Guid>()))
                    .Returns(() => Task.Run( () => expectedResponseFromUseCase));
            var sut = new PaymentService(_mockGetPaymentUseCase.Object, _mockCreatePaymentUseCase.Object,
                    _mockUpdatePaymentStatusUseCase.Object, _paymentQueueProcessorService.Object,
                    _validateCreditCardUseCase.Object);

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

            var sut = new PaymentService(_mockGetPaymentUseCase.Object, _mockCreatePaymentUseCase.Object,
                    _mockUpdatePaymentStatusUseCase.Object, _paymentQueueProcessorService.Object,
                    _validateCreditCardUseCase.Object);

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
            return new List<Payment>() {
                new Payment {
                    Id = Guid.Parse("fc782e65-0117-4c5e-b6d0-afa845effa3e"),
                    MerchantId = 1,
                    CreditCardId = 1,
                    CreditCard = new CreditCard()
                    {
                        Id = 1,
                        Number = "379354508162306",
                        HolderName = "Antônio J. Penteado",
                        HolderAddress = "Heroic St. 195",
                        ExpirationMonth = "12",
                        ExpirationYear = "2025",
                        Cvv = "323",
                        StatusId = 1
                    },
                    Amount = 15.99m,
                    Currency = "EUR",
                    SaleDescription = "Final soccer match",
                    StatusId = 1
                }
            };
        }
    }
}
