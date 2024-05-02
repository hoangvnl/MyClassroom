namespace MyConfigurationServer.gRPC.Contracts
{
    public class ErrorDetails(string errorCode, string message)
    {
        public string ErrorCode { get; set; } = errorCode;
        public string Message { get; set; } = message;
    }


}
