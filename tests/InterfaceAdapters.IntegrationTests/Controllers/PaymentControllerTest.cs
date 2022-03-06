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
    public class PaymentControllerTest
    {
        private TestServer _server;
        private HttpClient _client;

        public PaymentControllerTest()
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

        [Fact]
        [Trait("Controller", "GetPayments")]
        public async Task GetPayments_Should_BeOk()
        {
            // Arrange
            var clientId = "xxxxx";

            // Act
            var response = await _client.GetAsync($"v1/payments/{clientId}");

            // Assert
            var context = await response.Content.ReadAsStringAsync();
            var payments = JsonConvert.DeserializeObject<Payment>(context);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            payments.Should().BeOfType<Payment>();
            payments.Should().BeNull();
        }
    }
}
