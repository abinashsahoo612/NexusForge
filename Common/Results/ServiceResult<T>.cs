namespace TaskManagementSystem.Common.Results
{
    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; init; }

        public static ServiceResult<T> Ok(T data, string message = "")
        {
            return new ServiceResult<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public new static ServiceResult<T> Fail(string message)
        {
            return new ServiceResult<T>
            {
                Success = false,
                Message = message
            };
        }
    }
}