namespace BazaarOnline.Application.Utils.Extensions
{
    public static class ModelHelper
    {
        /// <summary>
        /// Update model properties That exists in filler object
        /// </summary>
        /// <typeparam name="TModel">Model Class</typeparam>
        /// <typeparam name="TFiller">Filler Class</typeparam>
        /// <param name="model">Model object that it's properties should be filled</param>
        /// <param name="filler">Filler object that should be used for filling model</param>
        /// <param name="ignoreNulls">Don't fill properties with `null` values in filler</param>
        /// <returns>`model` that filled from `filler`</returns>
        public static TModel? FillFromObject<TModel, TFiller>(this TModel model, TFiller filler,
            bool ignoreNulls = false)
        {
            if (model == null || filler == null)
                return default(TModel);

            var modelType = model.GetType();
            var fillerType = filler.GetType();

            fillerType.GetProperties().ToList().ForEach(
                p =>
                {
                    var value = p.GetValue(filler);
                    if (!ignoreNulls || value != null)
                    {
                        var prop = modelType.GetProperty(p.Name);
                        if (prop != null && prop.PropertyType == p.PropertyType &&
                            (prop.PropertyType.IsValueType || prop?.PropertyType == typeof(string)))
                            prop?.SetValue(model, value);
                    }
                }
            );

            return model;
        }

        /// <summary>
        /// Create new instance of `TModel` and fill model properties That exists in `filler` object
        /// </summary>
        /// <typeparam name="TModel">Model Class To Create And Fill</typeparam>
        /// <typeparam name="TFiller">Filler Class</typeparam>
        /// <param name="filler">Filler object that should be used for filling model</param>
        /// <param name="ignoreNulls">Don't fill properties with `null` values in filler</param>
        /// <returns>New instance of `TModel` that is filled from `filler`</returns>
        public static TModel CreateAndFillFromObject<TModel, TFiller>(TFiller filler, bool ignoreNulls = false)
            where TModel : new()
        {
            var model = Activator.CreateInstance<TModel>();
            var modelType = model.GetType();
            var fillerType = filler.GetType();

            fillerType.GetProperties().ToList().ForEach(
                p =>
                {
                    var value = p.GetValue(filler);
                    if (!ignoreNulls || value != null)
                    {
                        var prop = modelType.GetProperty(p.Name);
                        if (prop != null && (prop.PropertyType.IsValueType || prop?.PropertyType == typeof(string)))
                            prop?.SetValue(model, value);
                    }
                }
            );

            return model;
        }

        /// <summary>
        /// Trim Value of all string properties in given model
        /// </summary>
        /// <typeparam name="T">model class</typeparam>
        /// <param name="model">a model instance for trim</param>
        public static void TrimStrings<T>(this T model) where T : class
        {
            model.GetType().GetProperties()
                .Where(p => p.PropertyType == typeof(string))
                .ToList()
                .ForEach(
                    p =>
                    {
                        var value = p.GetValue(model)?.ToString()?.Trim();
                        p.SetValue(model, value);
                    }
                );
        }
    }
}