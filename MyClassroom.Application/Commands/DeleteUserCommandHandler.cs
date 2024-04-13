using MediatR;
using Microsoft.AspNetCore.Identity;
using MyClassroom.Application.Common;
using MyClassroom.Contracts;
using MyClassroom.Domain.AggregatesModel.UserAggregate;
using MyClassroom.Infrastructure;
using MyClassroom.Infrastructure.Services;
using Serilog;

namespace MyClassroom.Application.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, BaseResponse<bool>>
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;


        private User _user = null;

        public DeleteUserCommandHandler(UserManager<User> userManager, ApplicationDbContext dbContext, ILogger logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger.ForContext<DeleteUserCommandHandler>() ?? throw new ArgumentNullException(nameof(logger));
        }

        async Task<BaseResponse<bool>> IRequestHandler<DeleteUserCommand, BaseResponse<bool>>.Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            BaseResponse<bool> returnValue = await Validate(command);

            if (returnValue != null)
            {
                return returnValue;
            }

            
            var deleteResult = await _userManager.UpdateAsync(_user);

            if (deleteResult.Succeeded == true)
            {
                return new BaseResponse<bool>(true);
            }
            else
            {
                return new(APIProblemFactory.IdentityErrorsProblem(deleteResult.Errors));
            }
        }

        private async Task<BaseResponse<bool>> Validate(DeleteUserCommand command)
        {
            _user = await _userManager.FindByIdAsync(command.UserId.ToString());

            if (_user != null)
            {
                return new(APIProblemFactory.UserNotFound());
            }

            return null;
        }
    }
}
