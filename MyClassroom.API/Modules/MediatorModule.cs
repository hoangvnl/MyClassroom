using Autofac;
using FluentValidation;
using MediatR;
using MyClassroom.Application.Behaviors;
using MyClassroom.Application.Commands;
using MyClassroom.Application.Queries;
using MyClassroom.Application.Validations;
using System.Reflection;

namespace MyClassroom.API.Modules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(
                                  typeof(LoginQuery).GetTypeInfo().Assembly).
                                       AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(
                                  typeof(RegisterCommand).GetTypeInfo().Assembly).
                                       AsClosedTypesOf(typeof(IRequestHandler<,>));            
            builder.RegisterAssemblyTypes(
                                  typeof(CreateClassroomCommand).GetTypeInfo().Assembly).
                                       AsClosedTypesOf(typeof(IRequestHandler<,>)); 
            
            builder.RegisterAssemblyTypes(
                                  typeof(GetAllClassroomQuery).GetTypeInfo().Assembly).
                                       AsClosedTypesOf(typeof(IRequestHandler<,>));         
            
            builder.RegisterAssemblyTypes(
                                  typeof(JoinClassroomCommand).GetTypeInfo().Assembly).
                                       AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterAssemblyTypes(
                                  typeof(DeleteUserCommand).GetTypeInfo().Assembly).
                                       AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterAssemblyTypes(typeof(LoginQueryValidator).GetTypeInfo().Assembly)
                            .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                            .AsImplementedInterfaces();           
            builder.RegisterAssemblyTypes(typeof(RegisterCommandValidator).GetTypeInfo().Assembly)
                            .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                            .AsImplementedInterfaces();            
            builder.RegisterAssemblyTypes(typeof(CreateClassroomCommandValidator).GetTypeInfo().Assembly)
                            .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                            .AsImplementedInterfaces();            
            builder.RegisterAssemblyTypes(typeof(JoinClassroomValidator).GetTypeInfo().Assembly)
                            .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                            .AsImplementedInterfaces();            
            builder.RegisterAssemblyTypes(typeof(DeleteUserCommandValidator).GetTypeInfo().Assembly)
                            .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                            .AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).
                                           As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).
                                           As(typeof(IPipelineBehavior<,>));
        }
    }
}
