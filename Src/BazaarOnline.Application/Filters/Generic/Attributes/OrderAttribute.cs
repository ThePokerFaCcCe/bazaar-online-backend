namespace BazaarOnline.Application.Filters.Generic.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public sealed class OrderAttribute : Attribute

    {
        /// <summary>
        /// Order attribute that detected by GenericFilterExtension for ordering query result
        /// </summary>
        /// <param name="title"> The value that should send by user</param>
        public OrderAttribute(string title)
        {
            Title = title;
        }
        /// <summary>
        /// The value that should send by user
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The property name that exists in model. if `null`, then
        /// `Title` value will be used as property name
        /// </summary>
        public string? Property { get; set; }

        /// <summary>
        /// Returns property name that should be used for ordering
        /// </summary>
        public string? PropertyName => Property ?? Title;

    }
}
