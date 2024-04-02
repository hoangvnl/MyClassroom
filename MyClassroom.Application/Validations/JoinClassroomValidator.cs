using FluentValidation;
using MyClassroom.Application.Commands;

namespace MyClassroom.Application.Validations
{
    public class JoinClassroomValidator : AbstractValidator<JoinClassroomCommand>
    {
        public JoinClassroomValidator() 
        {
            RuleFor(command => command.JoinClassroomRequest).NotNull().ChildRules(c =>
            {
                c.RuleFor(r => r.UserId).NotEmpty();
                c.RuleFor(r => r.ClassroomId).NotEmpty();
                c.RuleFor(r => r.UserClassroomJoinType).NotEmpty();
            });
        }
    }
}
