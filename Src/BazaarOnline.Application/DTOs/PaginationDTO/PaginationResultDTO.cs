namespace BazaarOnline.Application.DTOs.PaginationDTO
{
    public class PaginationResultDTO<T> where T : class
    {
        public int AllCount { get; set; } = 0;
        public IEnumerable<T> Content { get; set; } = new List<T>();
    }
}