using FluentValidation;
using MyClassroom.Application.Commands;

namespace MyClassroom.Application.Validations
{
    public class CreateClassroomCommandValidator : AbstractValidator<CreateClassroomCommand>
    {
        public CreateClassroomCommandValidator()
        {
            RuleFor(command => command.CreateClassroomRequest).NotNull().ChildRules(c =>
            {
                c.RuleFor(r => r.Title).NotEmpty();
                c.RuleFor(r => r.Description).NotEmpty();
            });
        }
    }
}
