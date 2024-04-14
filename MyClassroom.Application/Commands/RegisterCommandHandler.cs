using MyClassroom.Application.Common;
using MyClassroom.Contracts;
using MyClassroom.Domain.AggregatesModel.UserAggregate;
using MediatR;
using Serilog;
using MyClassroom.Domain.AggregatesModel.RoleAggregate;

namespace MyClassroom.Application.Commands
{
    public class RegisterCommandHandler(ILogger logger, IUserRepository userRepository, IRoleRepository roleRepository) : IRequestHandler<RegisterCommand, BaseResponse<RegisterResponse>>
    {
        private readonly ILogger _logger = logger.ForContext<RegisterCommandHandler>() ?? throw new ArgumentNullException(nameof(logger));
        private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        private readonly IRoleRepository _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));

        private Role _role { get; set; }

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
                UserName = request.UserName,
                RoleId = _role.Id
            };

            try
            {
                await _userRepository.CreateAsync(user, request.Password);

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

            _role = await _roleRepository.GetByNameAsync(request.Role.ToString());

            if (_role == null)
            {
                return new(APIProblemFactory.RoleNotFound());
            }

            return null;
        }
    }
}
