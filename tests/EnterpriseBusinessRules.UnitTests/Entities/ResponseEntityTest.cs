using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FluentValidation.Results;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;
using System.Linq;

namespace EnterpriseBusinessRules.UnitTests.Entities
{
    public class ResponseEntityTests
    {
        [Fact]
        public void SetSuccessMethodIsOk()
        {
            // Arrange
            var responseEntity = new Response<bool>();
            
            // Act
            var response = responseEntity.SetSuccess(true);

            // Assert
            response.IsOk().Should().BeTrue();
        }

        [Fact]
        public void SetSuccessMethodIsNotOk()
        {
            // Arrange
            var responseEntity = new Response<bool>();
            
            // Act
            var response = responseEntity.SetSuccess(false);

            // Assert
            response.IsOk().Should().BeFalse();
        }

        [Fact]
        public void SetStatusMethodIsOk()
        {
            // Arrange
            int status = TestHelpers.RandomInt(3);
            var responseEntity = new Response<int>();

            // Act
            var response = responseEntity.SetStatus(status);
            
            // Assert
            response.GetStatus().Should().Be(status);
        }

        [Fact]
        public void CheckStatusMethodIsOk()
        {
            // Arrange
            var status = TestHelpers.RandomInt(3);
            var responseEntity = new Response<bool>();
            responseEntity.SetStatus(status);

            // Act            
            var response = responseEntity.CheckStatus(status);
            
            // Assert
            response.Should().BeTrue();
        }

        [Fact]
        public void CheckStatusMethodIsNotOk()
        {
            // Arrange
            var status = TestHelpers.RandomInt(3);
            var responseEntity = new Response<bool>();
            responseEntity.SetStatus(status);

            // Act            
            var response = responseEntity.CheckStatus(TestHelpers.RandomInt(3));
            
            // Assert
            response.Should().BeFalse();
        }        

        [Fact]
        public void SetExceptionMethodIsOk()
        {
            // Arrange
            var responseEntity = new Response<bool>();

            // Act
            var response = responseEntity.SetException(null);

            // Assert
            response.HasException().Should().BeFalse();
            response.IsOk().Should().BeTrue();
        }

        [Fact]
        public void SetExceptionMethodIsNotOk()
        {
            // Arrange
            var exception = new System.Exception();

            var responseEntity = new Response<bool>();
            
            // Act
            var response = responseEntity.SetException(exception);

            // Assert
            response.HasException().Should().BeTrue();
            response.IsOk().Should().BeFalse();
        }

        [Fact]
        public void SetResponseMethodIsOk()
        {
            // Arrange
            var responseEntity = new Response<bool>();
            
            // Act
            var response = responseEntity.SetResponse(true);
            
            // Assert
            response.GetResponse().Should().BeTrue();
        }

        [Fact]
        public void SetMessagesMethodIsOk()
        {
            // Arrange
            var messagesError = new List<string>();
            messagesError.Add("Error 1");
            messagesError.Add("Error 2");

            var responseEntity = new Response<bool>();
            
            // Act            
            var response = responseEntity.SetMessages(messagesError);
            
            // Assert
            response.GetMessages().Count().Should().Be(2);
            response.GetMessages().FirstOrDefault().Should().Be("Error 1");
        }

        [Fact]
        public void SetMessagesWithValidationResultMethodIsOk()
        {
            // Arrange
            var validationResult = new ValidationResult();

            var responseEntity = new Response<bool>();
            
            // Act            
            var response = responseEntity.SetMessages(validationResult);

            // Assert
            response.Should().NotBeNull();
        }

        [Fact]
        public void ClearMessagesMethodIsOk()
        {
            // Arrange
            var messagesError = new List<string>();
            messagesError.Add("Error 1");
            messagesError.Add("Error 2");

            var responseEntity = new Response<bool>();
            responseEntity.SetMessages(messagesError);

            // Act
            var response = responseEntity.ClearMessages();

            // Assert
            response.GetMessages().Count().Should().Be(0);
        }

        [Fact]
        public void AddMessagesMethodIsOk()
        {
            // Arrange
            var messageError = TestHelpers.RandomString(10);
            var responseEntity = new Response<string>();

            // Act
            var response = responseEntity.AddMessage(messageError);

            // Assert
            response.GetMessages().Count().Should().Be(1);
            response.GetMessages().FirstOrDefault().Should().Be(messageError);
        }

        [Fact]
        public void IsOkMethodIsOk()
        {
            // Arrange
            var responseEntity = new Response<string>();
            responseEntity.SetSuccess(true);

            // Act
            var response = responseEntity.IsOk();

            // Arrange
            response.Should().BeTrue();
        }

        [Fact]
        public void IsOkMethodIsNotOk()
        {
            // Arrange
            var responseEntity = new Response<string>();
            responseEntity.SetSuccess(false);

            // Act
            var response = responseEntity.IsOk();

            // Arrange
            response.Should().BeFalse();
        }

        [Fact]
        public void HasErrorsMethodIsOk()
        {
            // Arrange
            var responseEntity = new Response<string>();
            responseEntity.SetSuccess(false);

            // Act
            var response = responseEntity.HasErrors();

            // Assert
            response.Should().BeTrue();
        }

        [Fact]
        public void HasErrorsMethodIsNotOk()
        {
            // Arrange
            var responseEntity = new Response<string>();
            responseEntity.SetSuccess(true);

            // Act
            var response = responseEntity.HasErrors();

            // Assert
            response.Should().BeFalse();
        }

        [Fact]
        public void HasExceptionMethodIsOk()
        {
            // Arrange
            var exception = new System.Exception();

            var responseEntity = new Response<bool>();
            responseEntity.SetException(exception);
            
            // Act
            var response = responseEntity.HasException();

            // Assert
            response.Should().BeTrue();
        }

        [Fact]
        public void GetStatusMethodIsOk()
        {
            // Arrange
            int status = TestHelpers.RandomInt(3);
            var responseEntity = new Response<int>();
            responseEntity.SetStatus(status);

            // Act
            var response = responseEntity.GetStatus();
            
            // Assert
            response.Should().Be(status);
        }

        [Fact]
        public void GetResponseMethodIsOk()
        {
            // Arrange
            var responseEntity = new Response<bool>();
            responseEntity.SetResponse(true);

            // Act
            var response = responseEntity.GetResponse();

            // Assert
            response.Should().BeTrue();
        }

        [Fact]
        public void GetMessagesMethodIsOk()
        {
            var responseEntity = new Response<string>();
            var response = responseEntity.GetMessages();
            response.Should().NotBeNull();


            var responseEntityList = new Response<List<string>>();
            var responseList = responseEntityList.GetMessages();
            response.Should().NotBeNull();
        }

        [Fact]
        public void GetExceptionMethodIsOk()
        {
            var responseEntity = new Response<string>();
            var response = responseEntity.GetException();
            response.Should().BeNull();
        }

        




    }
}
