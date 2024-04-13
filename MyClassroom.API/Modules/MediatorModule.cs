using Autofac;
using AutoMapper.Internal;
using FluentValidation;
using MediatR;
using MyClassroom.Application.Behaviors;
using System.Reflection;

namespace MyClassroom.API.Modules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IRequest).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));


            builder.RegisterAssemblyTypes(typeof(AbstractValidator<>).GetTypeInfo().Assembly)
                            .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                            .AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).
                                           As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).
                                           As(typeof(IPipelineBehavior<,>));

        }
    }
}
