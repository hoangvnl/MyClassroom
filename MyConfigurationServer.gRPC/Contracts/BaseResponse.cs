namespace MyConfigurationServer.gRPC.Contracts
{
    public class BaseResponse<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Value { get; set; }

        public BaseResponse(T value)
        {
            Value = value;
            IsSuccess = true;
        }

        public BaseResponse(string message)
        {
            Value = null;
            IsSuccess = false;
            Message = message;
        }

        public BaseResponse()
        {
            
        }
    }
}
