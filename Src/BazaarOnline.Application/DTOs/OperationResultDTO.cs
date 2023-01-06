namespace BazaarOnline.Application.DTOs
{
    public class OperationResultDTO
    {
        public bool IsSuccess { get; set; } = false;

        public string Message { get; set; } = string.Empty;
    }
}
