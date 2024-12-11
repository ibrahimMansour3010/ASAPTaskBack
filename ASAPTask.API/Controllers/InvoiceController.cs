using ASAPTask.Applications.Common.Interfaces;
using ASAPTask.Applications.Item.Queries.ItemList.Dtos;
using ASAPTask.Applications.Item.Queries.ItemList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASAPTask.Applications.Invoice.Queries.InvoiceLookup;
using ASAPTask.Applications.Invoice.Queries.InvoiceLookup.Dtos;
using ASAPTask.Applications.Invoice.Commands.CreateInvoice;
using ASAPTask.Applications.Invoice.Commands.CreateInvoice.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ASAPTask.Applications.Invoice.Queries.InvoiceList;
using ASAPTask.Applications.Invoice.Queries.InvoiceList.Dtos;
using ASAPTask.Applications.Invoice.Queries.GetInvoiceForEdit;
using ASAPTask.Applications.Invoice.Queries.GetInvoiceForEdit.Dtos;
using ASAPTask.Applications.Invoice.Commands.DeleteInvoice;
using ASAPTask.Applications.Invoice.Commands.DeleteInvoice.Dtos;
using ASAPTask.Applications.Invoice.Commands.UpdateInvoice;
using ASAPTask.Applications.Invoice.Commands.UpdateInvoice.Dtos;

namespace ASAPTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InvoiceController : BaseController
    {
        [HttpGet("GetInvoiceLookup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(contentType: "application/json", Type = typeof(IResult<InvoiceLookupOutput>))]
        public async Task<IActionResult> GetInvoiceLookup([FromQuery] InvoiceLookupQuery query)
                => Ok(await Mediator.Send(query));


        [HttpPost("CreateInvoice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(contentType: "application/json", Type = typeof(IResult<CreateInvoiceOutput>))]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceCommand command)
                => Ok(await Mediator.Send(command));
        


        [HttpGet("GetInvoiceList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(contentType: "application/json", Type = typeof(IResult<InvoiceListOutput>))]
        public async Task<IActionResult> GetInvoiceList([FromQuery] InvoiceListQuery query)
                => Ok(await Mediator.Send(query));

        [HttpGet("GetInvoiceForEdit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(contentType: "application/json", Type = typeof(IResult<GetInvoiceForEditOutput>))]
        public async Task<IActionResult> GetInvoiceForEdit([FromQuery] GetInvoiceForEditQuery query)
                => Ok(await Mediator.Send(query));


        [HttpDelete("DeleteInvoice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(contentType: "application/json", Type = typeof(IResult<DeleteInvoiceOutput>))]
        public async Task<IActionResult> DeleteInvoice([FromBody] DeleteInvoiceCommand command)
                => Ok(await Mediator.Send(command));


        [HttpPut("UpdateInvoice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(contentType: "application/json", Type = typeof(IResult<UpdateInvoiceOutput>))]
        public async Task<IActionResult> UpdateInvoice([FromBody] UpdateInvoiceCommand command)
                => Ok(await Mediator.Send(command));

    }
}
