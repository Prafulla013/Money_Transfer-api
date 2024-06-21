using Inficare.Application.Admin.User.Commands;
using Inficare.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Inficare.Api.Controllers.AdminArea
{
    public class BalanceController : BaseAdminController
    {
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [HttpPost("{userid}")]
        public async Task<IActionResult> Create([FromRoute] int userid, [FromBody] DepositBalanceCommand command, CancellationToken cancellationToken)
        {
            try
            {
                command.UserId = userid;
                var response = await Mediator.Send(command, cancellationToken);
                return Ok(response);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
