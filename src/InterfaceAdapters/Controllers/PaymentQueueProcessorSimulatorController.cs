using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using EnterpriseBusinessRules.Entities;
using ApplicationBusinessRules.Interfaces;

namespace InterfaceAdapters.Controllers
{
    [ApiController]
    public class PaymentQueueProcessorSimulatorController : ControllerBase
    {
        private readonly IPaymentQueueProcessorService _queueProcessor;

        public PaymentQueueProcessorSimulatorController(IPaymentQueueProcessorService queueProcessor)
        {
            _queueProcessor = queueProcessor;
        }

        [HttpPost]
        [Route("v1/payment-processor-queue/{merchantId}/payment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "v1")]
        public async Task<IActionResult> OrderPayment(string merchantId, [FromBody] Payment payment)
        {
            var response = await this
                ._queueProcessor
                .PublishPaymentOrder(payment);

            if (response.HasException())
            {
                throw response.GetException();
            }

            if (response.HasErrors())
            {
                return NotFound(response.GetMessages());
            }

            return Ok(response.GetResponse());
        }
    }
}
