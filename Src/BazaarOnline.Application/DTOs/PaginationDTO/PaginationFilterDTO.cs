namespace BazaarOnline.Application.DTOs.PaginationDTO
{
    public class PaginationFilterDTO
    {
        public int Take { get; set; } = 10;
        public int Offset { get; set; } = 0;
    }
}
