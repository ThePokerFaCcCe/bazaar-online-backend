using System.Linq.Expressions;
using System.Reflection;
using BazaarOnline.Application.Filters.Generic.Attributes;
using BazaarOnline.Application.Utils.Extentions;

#pragma warning disable

namespace BazaarOnline.Application.Filters
{
    public static class GenericFilterExtention
    {
        public static IQueryable<TEntity> Filter<TEntity, TFilter>(this IQueryable<TEntity> query, TFilter filter)
        {
            var modelType = typeof(TEntity);
            var filterType = typeof(TFilter);

            var properties = filterType.GetProperties()
                .Where(p =>
                    p.CustomAttributes
                        .Any(ca => ca.AttributeType == typeof(FilterAttribute))
                );

            foreach (var property in properties)
            {
                var filterValue = property.GetValue(filter);

                if (filterValue == null) continue;

                var filterattr = property.GetCustomAttribute<FilterAttribute>();

                string propName = filterattr.ModelPropertyName ?? property.Name;

                var modelParam = Expression.Parameter(modelType, "model");
                Expression modelProp = GetProperty(modelParam, propName);
                Expression value = Expression.Constant(filterValue);

                Expression expression = null;

                switch (filterattr.FilterType)
                {
                    case FilterTypeEnum.Equals:
                        expression = Expression.Equal(modelProp, value);
                        break;

                    case FilterTypeEnum.EqualsIgnoreCase:
                        modelProp = Expression.Call(
                            modelProp,
                            modelProp.Type.GetMethod("ToLower", new Type[] { }));

                        value = Expression.Call(
                            value,
                            value.Type.GetMethod("ToLower", new Type[] { }));

                        expression = Expression.Equal(modelProp, value);
                        break;

                    case FilterTypeEnum.ModelGreaterThanEqualThis:
                        expression = Expression.GreaterThanOrEqual(
                            Expression.Convert(modelProp, typeof(double)),
                            Expression.Convert(value, typeof(double)));
                        break;

                    case FilterTypeEnum.ModelSmallerThanEqualThis:
                        expression = Expression.LessThanOrEqual(
                            Expression.Convert(modelProp, typeof(double)),
                            Expression.Convert(value, typeof(double)));
                        break;

                    case FilterTypeEnum.ModelContainsThis:
                        expression = Expression.Call(
                            modelProp,
                            modelProp.Type.GetMethod("Contains", new[] { property.PropertyType }),
                            value);
                        break;

                    case FilterTypeEnum.ThisContainsModel:
                        expression = Expression.Call(
                            value,
                            property.PropertyType.GetMethod("Contains", new[] { modelProp.Type }),
                            modelProp);
                        break;

                }

                var lambda = Expression.Lambda<Func<TEntity, bool>>(expression, modelParam);
                query = query.Where(lambda);
            }

            return _OrderQuery(query, filter);
        }

        private static IQueryable<TEntity> _OrderQuery<TEntity, TFilter>(IQueryable<TEntity> query, TFilter filter)
        {
            var filterType = typeof(TFilter);
            var orderAttributeProps = filterType.GetProperties()
                .Where(p =>
                    p.CustomAttributes
                        .Any(ca => ca.AttributeType == typeof(OrderAttribute))
                );

            if (!orderAttributeProps.Any())
                return query;

            if (orderAttributeProps.DistinctBy(p => p.GetHashCode()).Count() > 1)
                throw new AmbiguousMatchException("There's more than one Property that has OrderAttribute");

            var orderAttributeProp = orderAttributeProps.First();
            var allowedProps = orderAttributeProp
                .GetCustomAttributes<OrderAttribute>()
                .Select(attr => new OrderParam
                {
                    Title = attr.Title,
                    Property = attr.PropertyName,
                });
            string orderPropertyName = orderAttributeProp.GetValue(filter)?.ToString();
            if (string.IsNullOrEmpty(orderPropertyName))
                return query;

            string cleanedPropertyName = orderPropertyName.Replace("-", "").Trim().ToLower();
            var orderProperty = allowedProps
                .FirstOrDefault(p => p.Title.ToLower() == cleanedPropertyName)
                ?.Property;

            if (orderProperty == null)
                return query;

            return query.OrderBy(
                orderPropertyName[0] == '-' ? $"-{orderProperty}" : orderProperty,
                allowedProps.Select(p => p.Property).ToArray());
        }

        private static MemberExpression GetProperty(ParameterExpression param, string propertyNameDotted)
        {
            var propNames = propertyNameDotted.Split('.');
            MemberExpression property = Expression.PropertyOrField(param, propNames.First());

            foreach (var propName in propNames.Skip(1))
                property = Expression.PropertyOrField(property, propName);

            return property;
        }
    }
}

#pragma warning restore 
