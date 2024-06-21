using Inficare.Application.Admin.ExchangeRate.Queries;
using Inficare.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Inficare.Api.Controllers.AdminArea
{
    public class ExchangeRateController : BaseAdminController
    {
        [Produces("application/json")]
        [ProducesResponseType(typeof(CurrencyExchangeModel), 200)]
        [ProducesResponseType(404)]
        [HttpGet("page={page}&per_page={per_page}&from_date={from_date}&to_date={to_date}")]
        public async Task<IActionResult> GetAgents([FromRoute] int page, [FromRoute] int per_page, [FromRoute] string from_date, [FromRoute] string to_date, CancellationToken cancellationToken)
        {
            try
            {
                var query = new ListExchangeRateQuery { Page=page,PerPage=per_page, FromDate = from_date,ToDate=to_date };
                var response = await Mediator.Send(query, cancellationToken);
                return Ok(response);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (BadRequestException)
            {
                return BadRequest();
            }
        }
    }
}
