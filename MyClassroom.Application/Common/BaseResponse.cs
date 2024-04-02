namespace MyClassroom.Application.Common
{
    public class BaseResponse<T>
    {
        public T? Value { get; set; }
        public bool IsSuccess { get; set; }
        public APIProblem Problem { get; set; }

        public BaseResponse(T value)
        {
            Value = value;
            IsSuccess = true;
        }

        public BaseResponse(APIProblem problem)
        {
            IsSuccess = false;
            Problem = problem;
        }
    }
}
