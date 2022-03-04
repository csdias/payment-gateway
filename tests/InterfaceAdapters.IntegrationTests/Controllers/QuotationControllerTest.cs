using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;
using FrameworksAndDrivers;
using EnterpriseBusinessRules.Entities;
using System.Text;
using InterfaceAdapters.IntegrationTests.Helpers;

namespace InterfaceAdapters.IntegrationTests.Controllers
{
    public class QuotationControllerTest
    {
        private TestServer _server;
        private HttpClient _client;

        public QuotationControllerTest()
        {
            var rootPath = Path.GetFullPath("../../../../../");
            var webHostBuilder = new WebHostBuilder()
                .UseContentRoot(rootPath) //CalculateRelativeContentRootPath()
                .UseEnvironment("Testing")
                .UseStartup<Startup>()
                .UseConfiguration(new ConfigurationBuilder()
                    .SetBasePath(rootPath)
                    .AddJsonFile("src/FrameworksAndDrivers/appsettings.testing.json")
                    .Build()
                );

            _server = new TestServer(webHostBuilder);
            _client = _server.CreateClient();
        }

        // [Fact]
        // [Trait("Quotations", "FindQuotation")]
        // public async Task FindQuotation_WithoutParams_ReturnEmptyList()
        // {
        //     // Arrange && Act
        //     var response = await _client.GetAsync("v1/quotations");

        //     // Assert
        //     var context = await response.Content.ReadAsStringAsync();
        //     var quotationList = JsonConvert.DeserializeObject<List<QuotationEntity>>(context);

        //     response.StatusCode.Should().Be(HttpStatusCode.OK);
        //     response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
        //     quotationList.Should().BeOfType<List<QuotationEntity>>();
        //     quotationList.Count().Should().Be(0);
        //     //quotationList.FirstOrDefault().Id.Should().Be("12345");
        // }

        // [Fact]
        // [Trait("Quotations", "FindOneQuotation")]
        // public async Task FindOneQuotation_WithValidParams_ReturnEmptyQuotation()
        // {
        //     // Arrange
        //     var quotationId = "xxxxx";

        //     // Act
        //     var response = await _client.GetAsync($"v1/quotations/{quotationId}");

        //     // Assert
        //     var context = await response.Content.ReadAsStringAsync();
        //     var quotation = JsonConvert.DeserializeObject<QuotationEntity>(context);

        //     response.StatusCode.Should().Be(HttpStatusCode.OK);
        //     response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
        //     quotation.Should().BeOfType<QuotationEntity>();
        //     quotation.Should().BeNull();
        //     //quotation.Id.Should().Be(productId);
        // }

        // [Fact]
        // [Trait("Quotations", "InsuranceQuote")]
        // public async Task InsuranceQuote_WithValidParams_ReturnSuccess()
        // {
        //     // Arrange
        //     var productId = "AB777F";

        //     var information = new Dictionary<string, object>();
        //     information.Add("param1", "test");

        //     var parameters = new Dictionary<string, object>();
        //     parameters.Add("age", 30);
        //     parameters.Add("gender", "M");

        //     var items = new List<InsuranceQuoteInputItemEntity>();
        //     items.Add(
        //         new InsuranceQuoteInputItemEntity()
        //         {
        //             Amount = new InsuranceQuoteInputAmountEntity()
        //             {
        //                 Type = "monthly",
        //                 Amount = 1000F
        //             },
        //             Product = "AB777F"
        //         }
        //     );

        //     var quotation = new InsuranceQuoteInputEntity()
        //     {
        //         Code = TestHelpers.RandomString(8),
        //         Name = TestHelpers.RandomString(8),
        //         Description = TestHelpers.RandomString(10),
        //         Client = TestHelpers.RandomString(10),
        //         Product = TestHelpers.RandomString(8),
        //         Information = information,
        //         Rate = 10F,
        //         Amount = new InsuranceQuoteInputAmountEntity()
        //         {
        //             Type = "monthly",
        //             Amount = 1000F
        //         },
        //         Parameters = parameters,
        //         Items = items
        //     };
            
        //     string strQuotation = JsonConvert.SerializeObject(quotation);
        //     HttpContent content = new StringContent(strQuotation, Encoding.UTF8, "application/json");

        //     // Act
        //     var response = await _client.PostAsync($"v1/insurance-quote/{productId}", content);

        //     // Assert
        //     response.StatusCode.Should().Be(HttpStatusCode.OK);
        //     response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
        // }
    }
}