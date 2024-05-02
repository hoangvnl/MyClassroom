namespace MyConfigurationServer.gRPC.Contracts
{
    public class BaseResponse<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public T? Value { get; set; }
        public ErrorDetails? ErrorDetails { get; set; } = null;

        public BaseResponse(T value)
        {
            Value = value;
            IsSuccess = true;
        }

        public BaseResponse(ErrorDetails error)
        {
            Value = null;
            IsSuccess = false;
            ErrorDetails = error;
        }

        public BaseResponse()
        {
            
        }
    }
}
