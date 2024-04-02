using System.Linq.Expressions;

namespace MyClassroom.Contracts.EFCoreFilter
{
    public class EFCoreFilter<TEntity>
    {
        public EFCoreFilter()
        {
            
        }
        public Expression<Func<TEntity, bool>> Filters { get; set; } = null;

        public Dictionary<Expression<Func<TEntity, object>>, SortOrder> Sorts { get; set; } = null;

        public IEnumerable<Expression<Func<TEntity, object>>> Includes { get; set; } = null;

        public int? Offset { get; set; } = null;
        public int? Limit { get; set; } = null;

    }
}
