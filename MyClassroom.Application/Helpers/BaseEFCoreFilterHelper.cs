using MyClassroom.Contracts.EFCoreFilter;
using MyClassroom.Domain.SeedWork;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace MyClassroom.Application.Helpers
{
    public abstract class BaseEFCoreFilterHelper<TEntity> where TEntity : Entity
    {
        public abstract Expression<Func<TEntity, bool>> BuildFilterExpression();
        public static IEnumerable<Expression<Func<TEntity, object>>> BuildIncludeExpression(params Expression<Func<TEntity, object>>[] includeExpressions) => [.. includeExpressions];

        public static Dictionary<Expression<Func<TEntity, object>>, SortOrder> BuildSortExpression(string sortExpressionString)
        {
            if (string.IsNullOrEmpty(sortExpressionString)) return null;

            Dictionary<Expression<Func<TEntity, object>>, SortOrder> sortExpressions = new();

            var sortOptions = sortExpressionString.Split(EFCoreListFilter.SortedFieldSeparateChar);

            for (int i = 0; i < sortOptions.Length; i++)
            {
                var sort = sortOptions[i].Split(EFCoreListFilter.SortedSeparateChar);

                if (PropertyExists(sort[0]))
                {
                    sortExpressions.Add(
                        GetPropertyExpression(sort[0]),
                        SortOrder.Descending.GetDisplayName().Equals(sort[1], StringComparison.OrdinalIgnoreCase)
                            ? SortOrder.Descending
                            : SortOrder.Ascending
                        );
                }

            }

            return sortExpressions;
        }

        private static bool PropertyExists(string propertyName)
        {
            return typeof(TEntity).GetProperty(propertyName, BindingFlags.IgnoreCase |
              BindingFlags.Public | BindingFlags.Instance) != null;
        }

        private static Expression<Func<TEntity, object>> GetPropertyExpression(string propertyName)
        {
            if (!PropertyExists(propertyName)) return null;

            var parameterExpression = Expression.Parameter(typeof(TEntity));
            var expression = Expression.Property(parameterExpression, propertyName);
            var conversion = Expression.Convert(expression, typeof(object));

            return Expression.Lambda<Func<TEntity, object>>(conversion, parameterExpression);
        }
    }
}
