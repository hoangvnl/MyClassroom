using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MyClassroom.Application.Commands;
using MyClassroom.Application.Queries;
using MyClassroom.Contracts;
using MyClassroom.Infrastructure.Services;
using System.Security.Claims;

namespace MyClassroom.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController(IMediator mediator,
        ProblemDetailsFactory problemDetailsFactory,
        IUserContextBuilder userContextBuilder) : Controller
    {
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory ?? throw new ArgumentNullException(nameof(problemDetailsFactory));
        private readonly IUserContextBuilder _userContextBuilder = userContextBuilder ?? throw new ArgumentNullException(nameof(userContextBuilder));

        [HttpPost("/Login")]
        public async Task<IActionResult> LoginAsync([FromBody] Contracts.LoginRequest loginRequest)
        {
            var response = await _mediator.Send(new LoginQuery(loginRequest.UserName, loginRequest.Password));

            if (response.IsSuccess == true)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.Problem.Errors);
            }
        }

        [HttpPost("/Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] Contracts.RegisterRequest registerRequest)
        {
            var response = await _mediator.Send(new RegisterCommand(
                registerRequest.Email, 
                registerRequest.Password,
                registerRequest.FirstName, 
                registerRequest.LastName,
                registerRequest.UserName,
                registerRequest.Role));
            
            if (response.IsSuccess == true)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.Problem.Errors);
            }
        }

        [HttpDelete("/Delete")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteAsync([FromBody] Guid userId)
        {
            var userContext = _userContextBuilder.BuildUserContext(User.Identity as ClaimsIdentity);
            var response = await _mediator.Send(new DeleteUserCommand(userId, userContext));

            if (response.IsSuccess == true)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.Problem.Errors);
            }
        }
    }
}
