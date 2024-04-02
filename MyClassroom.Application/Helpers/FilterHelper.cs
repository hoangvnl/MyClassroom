using System.Linq.Expressions;

namespace MyClassroom.Application.Helpers
{
    public static class FilterHelper
    {
        public static Expression<Func<T, bool>> CombineExpressions<T>(
          Expression<Func<T, bool>> expr1,
          Expression<Func<T, bool>> expr2)
        {
            var param = Expression.Parameter(typeof(T));

            var body = Expression.AndAlso(
                Expression.Invoke(expr1, param),
                Expression.Invoke(expr2, param)
            );

            return Expression.Lambda<Func<T, bool>>(body, param);
        }
    }
}
