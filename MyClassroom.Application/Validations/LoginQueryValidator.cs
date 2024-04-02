// Ignore Spelling: Validator

using FluentValidation;
using MyClassroom.Application.Queries;

namespace MyClassroom.Application.Validations
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(query => query.UserName).NotEmpty();
            RuleFor(query => query.Password).NotEmpty();
        }
    }
}
