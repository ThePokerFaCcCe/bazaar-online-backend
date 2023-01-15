using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BazaarOnline.Application.Utils.Extensions
{
    public static class Sorter
    {
        /// <summary>
        /// Order query result by property name
        /// </summary>
        /// <param name="propertyName">property for ordering(Use dots for nested prop)</param>
        /// <param name="allowedProperties">available property names for ordering</param>
        /// <returns>Ordered query if `propertyName` is valid, else the self query</returns>
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source,
            string propertyName,
            string[] allowedProperties)
        {
            string command = propertyName[0] == '-' ? "OrderByDescending" : "OrderBy";
            propertyName = _ValidateOrderProp(propertyName, allowedProperties.ToList());
            if (propertyName == null)
                return source;

            var type = typeof(TEntity);
            var parameter = Expression.Parameter(type, "p");

            MemberExpression propertyAccess;
            var property = GetProperty(parameter, propertyName, out propertyAccess);

            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command,
                new Type[] { type, property.PropertyType },
                source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }

        /// <summary>
        /// Get property of parameter that supports nested properties
        /// </summary>
        /// <param name="parameter">parameter that the property name should exists inside of it</param>
        /// <param name="propertyNameDotted">property name that splitted with dots(if it's nested)</param>
        /// <param name="memberAccess">variable that member access expression sets to it</param>
        /// <returns>the property that found</returns>
        private static PropertyInfo? GetProperty(ParameterExpression parameter, string propertyNameDotted,
            out MemberExpression memberAccess)
        {
            var propNames = propertyNameDotted.Split('.');
            PropertyInfo property = parameter.Type.GetProperty(propNames[0]);
            memberAccess = Expression.MakeMemberAccess(parameter, property);

            foreach (var propName in propNames.Skip(1))
            {
                property = property.PropertyType.GetProperty(propName);
                memberAccess = Expression.MakeMemberAccess(memberAccess, property);
            }

            return property;
        }

        /// <summary>
        /// Validate that the property is available for ordering
        /// </summary>
        /// <param name="orderByProperty">property for ordering</param>
        /// <param name="allowedProperties">available property names for ordering</param>
        /// <returns>Validated property name</returns>
        private static string? _ValidateOrderProp(string orderByProperty,
            List<string> allowedProperties)
        {
            orderByProperty = orderByProperty.Replace("-", "").Trim().ToLower();
            return allowedProperties.Find(p => p.ToLower() == orderByProperty);
        }
    }
}