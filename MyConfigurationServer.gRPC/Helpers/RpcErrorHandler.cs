using Grpc.Core;
using MyConfigurationServer.gRPC.Contracts;

namespace MyConfigurationServer.gRPC.Helpers
{
    public static class RpcErrorHandler
    {
        private static readonly Dictionary<StatusCode, string> _errorMessages = new()
        {
            { StatusCode.NotFound, "The requested configuration was not found." },
            { StatusCode.InvalidArgument, "One or more parameters were invalid." },
            { StatusCode.Internal, "Internal Error" },
            { StatusCode.AlreadyExists, "Classroom Configuration already exists" }
            // ... add more mappings
        };

        public static ErrorDetails GetErrorDetail(StatusCode statusCode)
        {
            return new(statusCode.ToString(), _errorMessages.TryGetValue(statusCode, out var message) ? message : "An error occurred.");
        }

        public static string GetErrorMessage(StatusCode statusCode)
        {
            return _errorMessages.TryGetValue(statusCode, out var message) ? message : "An error occurred.";
        }

        public static Status Internal()
        {
            return new(StatusCode.Internal, GetErrorMessage(StatusCode.Internal));
        }

        public static Status AlreadyExists()
        {
            return new(StatusCode.AlreadyExists, GetErrorMessage(StatusCode.AlreadyExists));
        }

        public static Status NotFound()
        {
            return new(StatusCode.NotFound, GetErrorMessage(StatusCode.NotFound));
        }

        public static Status InvalidArgument()
        {
            return new(StatusCode.InvalidArgument, GetErrorMessage(StatusCode.InvalidArgument));
        }
    }
}
