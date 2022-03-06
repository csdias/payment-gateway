using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using InterfaceAdapters.Interfaces;
using EnterpriseBusinessRules.Entities;
using ApplicationBusinessRules.Interfaces;
using System;
using Microsoft.AspNetCore.Authorization;
using EnterpriseBusinessRules.Entities.ResourceParameters;
using System.Collections.Generic;
using System.Text.Json;
using EnterpriseBusinessRules.Entities.ResouceParameters;
using AutoMapper;

//ToDo: Revisit the getPayments repo used directly from controller
//ToDo: Revisit ResourceParameters
//ToDo: Revisit Roles and JWT
//ToDo: See the correct places of resourceparameters entitys and interfaces should be
//ToDo: Verify the neeed of the mapper in ther controller for the get payments with pagination
//ToDo: Apply Response in the get payments with pagination
//ToDo: Remove Swagger First


namespace InterfaceAdapters.Controllers
{
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IClientSession _session;
        private readonly IPaymentService _paymentService;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IPropertyCheckerService _propertyCheckerService;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;


        public PaymentController(IClientSession session, IPaymentService paymentService,
            IPropertyMappingService propertyMappingService, IPropertyCheckerService propertyCheckerService,
            IPaymentRepository paymentRepository, IMapper mapper)
        {
            _session = session;
            _paymentService = paymentService;
            _propertyMappingService = propertyMappingService;
            _propertyCheckerService = propertyCheckerService;
            _paymentRepository = paymentRepository;
            _mapper = mapper;
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

        public ActionResult<IEnumerable<Payment>> GetPayments([FromQuery] PaymentResourceParameters paymentResourceParameters)
        {
            if (!_propertyMappingService.ValidMappingExistsFor<PaymentFilter, Payment>(paymentResourceParameters.OrderBy))
            {
                return BadRequest();
            }

            if (!_propertyCheckerService.TypeHasProperties<PaymentFilter>
              (paymentResourceParameters.Fields))
            {
                return BadRequest();
            }

            var payments = _paymentRepository.GetPayments(paymentResourceParameters);

            var previousPageLink = payments.HasPrevious ?
                CreatePaymentsResourceUri(paymentResourceParameters,
                ResourceUriType.PreviousPage) : null;

            var nextPageLink = payments.HasNext ?
                CreatePaymentsResourceUri(paymentResourceParameters,
                ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = payments.TotalCount,
                pageSize = payments.PageSize,
                currentPage = payments.CurrentPage,
                totalPages = payments.TotalPages,
                previousPageLink,
                nextPageLink
            };

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<Payment>>(payments));
        }

        //[HttpGet]
        //[Route("v1/payments")]
        //[Authorize]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ApiExplorerSettings(GroupName = "v1")]        
        //public async Task<IActionResult> GetPayments([FromQuery] Payment payment)
        //{
        //    payment.ClientId = _session.GetClientId();

        //    var response = await this
        //        ._paymentService
        //        .GetPayments(payment);

        //    if (response.HasException())
        //    {
        //        throw response.GetException();
        //    }

        //    if (response.HasErrors())
        //    {
        //        return UnprocessableEntity(response.GetMessages());
        //    }

        //    return Ok(response.GetResponse());
        //}

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


        private string CreatePaymentsResourceUri(PaymentResourceParameters userResourceParameters,
                    ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link(nameof(GetPayments),
                      new
                      {
                          fields = userResourceParameters.Fields,
                          orderBy = userResourceParameters.OrderBy,
                          pageNumber = userResourceParameters.PageNumber - 1,
                          pageSize = userResourceParameters.PageSize,
                          searchQuery = userResourceParameters.SearchQuery
                      });
                case ResourceUriType.NextPage:
                    return Url.Link(nameof(GetPayments),
                      new
                      {
                          fields = userResourceParameters.Fields,
                          orderBy = userResourceParameters.OrderBy,
                          pageNumber = userResourceParameters.PageNumber + 1,
                          pageSize = userResourceParameters.PageSize,
                          searchQuery = userResourceParameters.SearchQuery
                      });

                default:
                    return Url.Link(nameof(GetPayments),
                    new
                    {
                        fields = userResourceParameters.Fields,
                        orderBy = userResourceParameters.OrderBy,
                        pageNumber = userResourceParameters.PageNumber,
                        pageSize = userResourceParameters.PageSize,
                        searchQuery = userResourceParameters.SearchQuery
                    });
            }
        }
    }
}
