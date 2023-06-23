namespace BazaarOnline.Application.DTOs
{
    public class ValidationResultDTO
    {
        public bool IsValid { get; set; } = false;

        public string Message { get; set; } = string.Empty;

        public object? Data { get; set; } = null;
    }
}