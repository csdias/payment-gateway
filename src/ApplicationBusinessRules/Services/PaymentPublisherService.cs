using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using AutoMapper;
using Microsoft.Extensions.Logging;
using EnterpriseBusinessRules.Sns.Entities;

namespace ApplicationBusinessRules.Services
{
    public class PaymentPublisherService : IPaymentPublisherService
    {
        private readonly IAmazonSimpleNotificationService _sns;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentPublisherService> _logger;

        public PaymentPublisherService(IAmazonSimpleNotificationService sns, IMapper mapper,
            ILogger<PaymentPublisherService> logger)
        {
            _sns = sns;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<bool> PublishMessage(OutboxMessage entry)
        {
            var request = new PublishRequest
            {
                TopicArn = entry.TopicArn.Trim(),
                Message = JsonSerializer.Serialize(_mapper.Map<SnsMessage>(entry)),
                MessageAttributes = entry.MessageFilters.ToDictionary(x => x.FilterKey, y => new MessageAttributeValue
                {
                    StringValue = y.FilterValue,
                    DataType = nameof(String)
                })
            };

            try
            {
                var response = await _sns.PublishAsync(request);
                return !string.IsNullOrEmpty(response.MessageId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to send message id: {MessageId}", entry.MessageId);
                return false;
            }
        }
    }
}
