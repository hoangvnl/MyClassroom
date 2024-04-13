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
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(ApplicationDbContext dbContext, ILogger logger, IUserRepository userRepository)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger.ForContext<RegisterCommandHandler>() ?? throw new ArgumentNullException(nameof(logger));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<BaseResponse<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            BaseResponse<RegisterResponse> returnValue = null;

            returnValue = await Validate(request);

            if (returnValue != null)
            {
                return returnValue;
            }

            var user = new User()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName
            };

            try
            {
                await _userRepository.CreateAsync(user, request.Password);

                //if (createResult != null)
                //{
                //    returnValue = new(new RegisterResponse() { IsRegisterSuccess = true });
                //}

                return new(new RegisterResponse() { IsRegisterSuccess = true });
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "[RegisterCommandHandler] Unhandle error occurs.");
                throw;
            }

        }

        private async Task<BaseResponse<RegisterResponse>> Validate(RegisterCommand request)
        {

            if (await _userRepository.GetByUserNameAsync(request.UserName) != null)
            {
                return new(APIProblemFactory.UserNameAlreadyExist());
            }

            if (await _userRepository.GetByEmailAsync(request.Email) != null)
            {
                return new(APIProblemFactory.EmailAlreadyExist());
            }

            return null;
        }
    }
}
