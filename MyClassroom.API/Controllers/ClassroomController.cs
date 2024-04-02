using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyClassroom.Contracts;
using MyClassroom.Infrastructure.Services;
using Serilog;
using System.Security.Claims;
using GradeStructureDto = MyClassroom.Contracts.GradeStructureDto;
using ILogger = Serilog.ILogger;

namespace MyClassroom.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ClassroomController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IUserContextBuilder _userContextBuilder;
        private readonly ILogger _logger;

        public ClassroomController(IMediator mediator, IUserContextBuilder userContextBuilder, ILogger logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _userContextBuilder = userContextBuilder ?? throw new ArgumentNullException(nameof(userContextBuilder));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var userContext = _userContextBuilder.BuildUserContext(User.Identity as ClaimsIdentity);
            var response = await _mediator.Send(new Application.Queries.GetAllClassroomQuery(userContext));

            if (response?.IsSuccess == true)
            {
                return Ok(response.Value);
            }
            else
            {
                return BadRequest(response.Problem.Errors);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateClassroomRequest request)
        {
            var userContext = _userContextBuilder.BuildUserContext(User.Identity as ClaimsIdentity);
            var response = await _mediator.Send(new Application.Commands.CreateClassroomCommand(request, userContext));

            if (response?.IsSuccess == true)
            {
                return Ok(response.Value);
            }
            else
            {
                return BadRequest(response.Problem.Errors);
            }

        }

        [HttpPost("JoinClassroom")]
        public async Task<IActionResult> JoinClassroomAsync([FromBody] UserJoinClassroomRequest request)
        {
            var userContext = _userContextBuilder.BuildUserContext(User.Identity as ClaimsIdentity);
            var response = await _mediator.Send(new Application.Commands.JoinClassroomCommand(request, userContext));

            if (response?.IsSuccess == true)
            {
                return Ok(response.Value);
            }
            else
            {
                return BadRequest(response.Problem.Errors);
            }

        }

        //[HttpPost("{ClassroomId:guid:required}/UpdateGrades")]
        //public async Task<IActionResult> UpdateGradesAsync([FromRoute] Guid ClassroomId, [FromBody] List<GradeStructureDto> request)
        //{
        //    var userContext = _userContextBuilder.BuildUserContext(User.Identity as ClaimsIdentity);
        //    var response = await _mediator.Send(new Application.Commands.UpdateGradesCommand(request, ClassroomId, userContext));

        //    if (response?.IsSuccess == true)
        //    {
        //        return Ok(response.Value);
        //    }
        //    else
        //    {
        //        return BadRequest(response.Problem.Errors);
        //    }
        //}

        //[HttpPost("{ClassroomId:guid:required}/JoinClass")]
        //public async Task<IActionResult> JoinClassAsync([FromRoute] Guid ClassroomId)
        //{
        //    var userContext = _userContextBuilder.BuildUserContext(User.Identity as ClaimsIdentity);
        //    var response = await _mediator.Send(new Application.Commands.UpdateGradesCommand(request, ClassroomId, userContext));

        //    if (response?.IsSuccess == true)
        //    {
        //        return Ok(response.Value);
        //    }
        //    else
        //    {
        //        return BadRequest(response.Problem.Errors);
        //    }
        //    return Ok();
        //}
    }
}
