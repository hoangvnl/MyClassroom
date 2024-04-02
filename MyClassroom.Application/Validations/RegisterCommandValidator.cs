// Ignore Spelling: Validator

using FluentValidation;
using MyClassroom.Application.Commands;

namespace MyClassroom.Application.Validations
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(command => command.UserName).NotEmpty();
            RuleFor(command => command.Email).NotEmpty().EmailAddress();
            RuleFor(command => command.FirstName).NotEmpty();
            RuleFor(command => command.LastName).NotEmpty();
            RuleFor(command => command.Password).NotEmpty();
        }
    }
}
