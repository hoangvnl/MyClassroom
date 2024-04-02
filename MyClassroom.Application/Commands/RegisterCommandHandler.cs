using MyClassroom.Application.Common;
using MyClassroom.Contracts;
using MyClassroom.Domain.AggregatesModel.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Serilog;
using MyClassroom.Infrastructure;

namespace MyClassroom.Application.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, BaseResponse<RegisterResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, ILogger logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger.ForContext<RegisterCommandHandler>() ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<BaseResponse<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            BaseResponse<RegisterResponse> returnValue = null;

            returnValue = await Validate(request);

            if (returnValue != null)
            {
                return returnValue;
            }

            var user = new ApplicationUser()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName
            };

            using (var identityDbContextTransaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var userNameFound = await _userManager.FindByNameAsync(request.UserName);


                    var createResult = await _userManager.CreateAsync(user, request.Password);

                    if (createResult.Succeeded)
                    {
                        var roleAddResult = await _userManager.AddToRoleAsync(user, request.Role.ToString());

                        if (roleAddResult.Succeeded)
                        {
                            returnValue = new(new RegisterResponse() { IsRegisterSuccess = true });
                            identityDbContextTransaction.Commit();
                        }
                        else
                        {
                            returnValue = new(APIProblemFactory.IdentityErrorsProblem(roleAddResult.Errors));
                        }
                    }
                    else
                    {
                        returnValue = new(APIProblemFactory.IdentityErrorsProblem(createResult.Errors));
                    }

                    return returnValue;
                }
                catch (Exception ex)
                {
                    identityDbContextTransaction.Rollback();
                    _logger.Error(ex, "[RegisterCommandHandler] Unhandle error occurs.");
                    throw;
                }
            }
        }

        private async Task<BaseResponse<RegisterResponse>> Validate(RegisterCommand request)
        {

            if (await _userManager.FindByNameAsync(request.UserName) != null)
            {
                return new(APIProblemFactory.UserNameAlreadyExist());
            }

            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new(APIProblemFactory.EmailAlreadyExist());
            }

            return null;
        }
    }
}
