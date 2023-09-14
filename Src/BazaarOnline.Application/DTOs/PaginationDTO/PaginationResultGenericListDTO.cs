namespace BazaarOnline.Application.DTOs.PaginationDTO
{
    public class PaginationResultGenericListDTO<T> where T : class
    {
        public int AllCount { get; set; } = 0;
        public IEnumerable<T> Content { get; set; } = new List<T>();
    }

    public class PaginationResultGenericTypeDTO<T> where T : class
    {
        public int AllCount { get; set; } = 0;
        public T Content { get; set; } = default(T);
    }
}