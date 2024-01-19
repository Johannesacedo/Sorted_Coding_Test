namespace Sorted_Coding_Test.Model
{

    public class InvalidRequestException : Exception
    {
        public List<ErrorDetail> ErrorDetails { get; }

        public InvalidRequestException(string message, List<ErrorDetail> errorDetails = null) : base(message)
        {
            ErrorDetails = errorDetails ?? new List<ErrorDetail>();
        }
    }

    public class NotFoundException : Exception
    {
        public List<ErrorDetail> ErrorDetails { get; }

        public NotFoundException(string message, List<ErrorDetail> errorDetails = null) : base(message)
        {
            ErrorDetails = errorDetails ?? new List<ErrorDetail>();
        }
    }
    public class ErrorDetail
    {
        public string PropertyName { get; set; }
        public string Message { get; set; }
    }
}
