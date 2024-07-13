namespace BazaarOnline.Application.DTOs.AuthDTOs
{
    public class CodeSentResultDTO
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
