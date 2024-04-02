using FluentValidation;
using MyClassroom.Application.Commands;

namespace MyClassroom.Application.Validations
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
        }
    }
}
