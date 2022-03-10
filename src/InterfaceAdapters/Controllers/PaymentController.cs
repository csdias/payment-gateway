using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using InterfaceAdapters.Interfaces;
using EnterpriseBusinessRules.Entities;
using ApplicationBusinessRules.Interfaces;
using System;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace InterfaceAdapters.Controllers
{
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IClientSession _session;
        private readonly IPaymentService _paymentService;

        public PaymentController(IClientSession session, IPaymentService paymentService)
        {
            _session = session;
            _paymentService = paymentService;
        }

        [HttpGet]
        [Route("v1/payments/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "v1")]
        public async Task<IActionResult> GetPayment(Guid id)
        {
            var response = await this
                ._paymentService
                .GetPayment(id);

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

        [HttpGet]
        [Route("v1/payments")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "v1")]
        public async Task<IActionResult> GetPayments([FromQuery] Payment payment)
        {
            payment.ClientId = _session.GetClientId();

            var response = await this
                ._paymentService
                .GetPayments(payment);

            if (response.HasException())
            {
                throw response.GetException();
            }

            if (response.HasErrors())
            {
                return UnprocessableEntity(response.GetMessages());
            }

            return Ok(response.GetResponse());
        }

        [HttpPost]
        [Route("v1/payments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "v1")]
        public async Task<IActionResult> CreatePayment([FromBody] Payment payment)
        {
            payment.ClientId = this._session.GetClientId(); // Embryo of a simulation of a client id

            var response = await this
                ._paymentService
                .CreatePayment(payment);

            if (response.HasException())
            {
                throw response.GetException();
            }

            if (response.HasErrors())
            {
                return UnprocessableEntity(response.GetMessages());
            }

            return Ok(response.GetResponse());
        }

        [HttpPut]
        [Route("v1/payments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "v1")]
        public async Task<IActionResult> UpdatePaymentStatus([FromBody] Payment payment)
        {
            var response = await this
                ._paymentService
                .UpdatePaymentStatus(payment);

            if (response.HasException())
            {
                throw response.GetException();
            }

            if (response.HasErrors())
            {
                return UnprocessableEntity(response.GetMessages());
            }

            return Ok(response.GetResponse());
        }
    }
}
