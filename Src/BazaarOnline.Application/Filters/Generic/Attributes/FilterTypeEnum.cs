namespace BazaarOnline.Application.Filters.Generic.Attributes
{
    public enum FilterTypeEnum
    {
        /// <summary>
        /// Both values should equal. it's like `==`
        /// </summary>
        Equals,

        /// <summary>
        /// Both values should equal. it's like `==`. Regardless of case of words
        /// </summary>
        EqualsIgnoreCase,

        /// <summary>
        /// This value should exists inside of model value.
        /// for example, used when model value is string, and this value is a substring
        /// and you want to filter where model value contains this string
        /// </summary>
        ModelContainsThis,

        /// <summary>
        /// Model value should exists in this value.
        /// for example, used when this value is a list of items and model value is an item,
        /// and  you want to filter where model value item exists in this list 
        /// </summary>
        ThisContainsModel,

        ModelGreaterThanEqualThis,

        ModelSmallerThanEqualThis,
    }
}
