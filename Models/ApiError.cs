namespace CompucorVtas.Models
{
    public class ApiError
    {
        public int Status { get; set; }
        public string Error { get; set; }
        public string? StackTrace { get; set; }

        public ApiError(int status, string error, string? stackTrace = null)
        {
            Status = status;
            Error = error;
            StackTrace = stackTrace;
        }
    }
}
