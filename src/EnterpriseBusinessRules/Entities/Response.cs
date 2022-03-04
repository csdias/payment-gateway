using System;
using System.Linq;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace EnterpriseBusinessRules.Entities
{
    public class Response<T>
    {
        public Response<T> SetSuccess(bool success)
        {
            this.Success = success;
            this.Exception = success ? null : this.Exception;
            return this;
        }

        public Response<T> SetStatus(int status)
        {
            this.Status = status;
            return this;
        }

        public bool CheckStatus(int status)
        {
            return this.Status == status;
        }

        public Response<T> SetException(Exception exception)
        {
            this.Exception = exception;
            this.Success = exception == null ? this.Success : false;
            return this;
        }

        public Response<T> SetResponse(T response)
        {
            this.Content = response;
            return this;
        }

        public Response<T> ClearMessages()
        {
            this.Messages = new List<string>();
            return this;
        }

        public Response<T> SetMessages(List<string> messages)
        {
            this.Messages = messages;
            return this;
        }

        public Response<T> SetMessages(ValidationResult validate)
        {
            return this.SetMessages(
                validate
                    .Errors
                    .Select(x => x.ErrorMessage)
                    .ToList()
            );
        }

        public Response<T> AddMessage(string message)
        {
            Messages.Add(message);
            return this;
        }

        public bool IsOk()
        {
            return this.Success;
        }

        public bool HasErrors()
        {
            return !this.Success;
        }

        public bool HasException()
        {
            return this.Exception != null;
        }

        public int GetStatus()
        {
            return this.Status;
        }

        public T GetResponse()
        {
            return this.Content;
        }

        public List<string> GetMessages()
        {
            return this.Messages;
        }

        public Exception GetException()
        {
            return this.Exception;
        }

        [JsonIgnore]
        private bool Success { get; set; } = true;
        private Exception Exception { get; set; } = null;
        private T Content { get; set; }
        private int Status { get; set; } = 200;
        private List<string> Messages { get; set; } = new List<string>();
    }
}
