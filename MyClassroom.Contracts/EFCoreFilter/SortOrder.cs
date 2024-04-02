using System.ComponentModel.DataAnnotations;

namespace MyClassroom.Contracts.EFCoreFilter
{
    public enum SortOrder
    {
        [Display(Name = "ASC")]
        Ascending,
        [Display(Name = "DESC")]
        Descending,
    }
}
