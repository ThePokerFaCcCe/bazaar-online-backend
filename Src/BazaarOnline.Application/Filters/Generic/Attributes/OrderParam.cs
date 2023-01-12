namespace BazaarOnline.Application.Filters.Generic.Attributes
{
    public class OrderParam
    {
        /// <summary>
        /// The value that should send by user
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The property name that exists in model. if `null`, then
        /// `Title` value will be used as property name
        /// </summary>
        public string? Property { get; set; }
    }
}
