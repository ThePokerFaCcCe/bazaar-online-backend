namespace BazaarOnline.Application.DTOs.PaginationDTO
{
    /// <summary>
    /// Properties Can set in 3 types:
    /// 1. Take, Offset.
    /// 2. Take, Page.
    /// 3. Page.
    /// </summary>
    public class PaginationFilterDTO
    {
        private int _offset = 0;
        
        public int Take { get; set; } = 10;

        public int Offset
        {
            get => Page > 0 ? (Page - 1) * Take : _offset;
            set => _offset = value;
        }

        public int Page { get; set; } = -1;
    }
}
