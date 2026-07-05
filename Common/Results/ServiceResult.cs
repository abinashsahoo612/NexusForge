namespace TaskManagementSystem.Common.Results
{
    public class ServiceResult
    {
        public bool Success { get; init; }

        public string Message { get; init; } = string.Empty;

        public static ServiceResult Ok(string message = "")
        {
            return new ServiceResult
            {
                Success = true,
                Message = message
            };
        }

        public static ServiceResult Fail(string message)
        {
            return new ServiceResult
            {
                Success = false,
                Message = message
            };
        }
    }
}