using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;
using EnterpriseBusinessRules.Entities;
using FrameworksAndDrivers.Database.Contexts;
using FrameworksAndDrivers.Database.Models;
using FrameworksAndDrivers.Database.Repositories;
using FrameworksAndDrivers.UnitTests.Helpers;
using ApplicationBusinessRules.Interfaces;
using FrameworksAndDrivers.UnitTests.Mocks.Database.Contexts;
using AutoMapper;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace FrameworksAndDrivers.UnitTests.Repositories
{
    public class PaymentRepositoryTest
    {
        private readonly ITestOutputHelper _output;
        private Mock<ApplicationDbContext> _mockDbContext;
        private readonly IMapper _mapper;

        public PaymentRepositoryTest(ITestOutputHelper output, IMapper mapper)
        {
            _output = output;
            _mapper = mapper;
            MockDbContext();
        }

        private PaymentRepositoryTest MockDbContext()
        {        
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
            _mockDbContext = new Mock<ApplicationDbContext>(options);
            _mockDbContext.Setup(c => c.Payments)
                .Returns(MoqExtensions.DbSetMock<PaymentModel>(MockPaymentModel.Data).Object);
            return this;
        }

        [Fact]
        [Trait("Repository", "Payment")]
        public async void GetPayments_Should_ReturnPayments()
        {
            // Assert
            var repo = new PaymentRepository(_mockDbContext.Object, _mapper); //ToDo: Use a mock mapper
            var payment = new Payment
            {
                MerchantId = 1
            };

            // Act
            var response = await repo.GetPayments(payment);

            // Assert
            var payments = response.GetResponse();
            payments.Should().NotBeEmpty();
            payments.Count().Should().BeGreaterThan(1);
            payments[0].Id.Should().Be(MockPaymentModel.Data[0].Id);
            payments[1].Id.Should().Be(MockPaymentModel.Data[1].Id);
        }
    }
}
