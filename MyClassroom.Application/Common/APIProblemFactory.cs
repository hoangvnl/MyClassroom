using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using MyClassroom.Contracts;
using System.Globalization;
using System.Resources;

namespace MyClassroom.Application.Common
{
    public static class APIProblemFactory
    {
        private static ResourceManager s_resourceManager = new ResourceManager(typeof(Messages));
        private static CultureInfo s_cultureInfo = new CultureInfo("en-US");

        public static APIProblem InvalidAuthentication(string fieldName = "Authentication")
        {
            return new APIProblem(
                detail: Messages.InvalidAuthentication,
                errors: new Dictionary<string, string[]>() { { fieldName, new string[] { Messages.InvalidAuthentication } } }
                );
        }

        public static APIProblem CreateUserError(string fieldName = "User")
        {
            return new APIProblem(
                detail: Messages.CreateUserError,
                errors: new Dictionary<string, string[]>() { { fieldName, new string[] { Messages.CreateUserError } } }
                );
        }

        public static APIProblem UserNameAlreadyExist(string fieldName = "UserName")
        {
            return new APIProblem(
                detail: Messages.UserNameAlreadyExists,
                errors: new Dictionary<string, string[]>() { { fieldName, new string[] { Messages.UserNameAlreadyExists } } }
                );
        }

        public static APIProblem EmailAlreadyExist(string fieldName = "Email")
        {
            return new APIProblem(
                detail: Messages.EmailAlreadyExists,
                errors: new Dictionary<string, string[]>() { { fieldName, new string[] { Messages.EmailAlreadyExists } } }
                );
        }

        public static APIProblem ClassroomNotFound(string fieldName = "ClassroomId")
        {
            return new APIProblem(
                detail: Messages.ClassroomNotFound,
                errors: new Dictionary<string, string[]>() { { fieldName, new string[] { Messages.ClassroomNotFound } } }
                );
        }

        public static APIProblem UserNotFound(string fieldName = "UserId")
        {
            return new APIProblem(
                detail: Messages.UserNotFound,
                errors: new Dictionary<string, string[]>() { { fieldName, new string[] { Messages.UserNotFound } } }
                );
        }

        public static APIProblem UserAlreadyJoinedClassroom(string fieldName = "UserId")
        {
            return new APIProblem(
                detail: Messages.UserAlreadyJoinedClassroom,
                errors: new Dictionary<string, string[]>() { { fieldName, new string[] { Messages.UserAlreadyJoinedClassroom } } }
                );
        }
        public static APIProblem RoleNotFound(string fieldName = "Role")
        {
            return new APIProblem(
                detail: Messages.RoleNotFound,
                errors: new Dictionary<string, string[]>() { { fieldName, new string[] { Messages.RoleNotFound } } }
                );
        }

        public static APIProblem IdentityErrorsProblem(IEnumerable<IdentityError> identityErrors)
        {
            return new APIProblem(
                detail: "Identity Error",
                errors: new Dictionary<string, string[]>() { { "Identity Error", identityErrors.Select(i => i.Description).ToArray() } }
                );
        }
    }
}
