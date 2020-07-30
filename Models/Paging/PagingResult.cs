using System.Collections;
using System.ComponentModel;

namespace UniversityDemo.Models.Paging
{
    public class PagingResult
    {
        [DefaultValue(0)]
        public int MaxItemCount { get; set; }

        [DefaultValue(1)]
        public int CurrentPage { get; set; }

        [DefaultValue(10)]
        public int ItemsPerPage { get; set; }

        public IEnumerable Items { get; set; }

        [DefaultValue(true)]
        public bool HasNextPage { get { return MaxItemCount >= CurrentPage * ItemsPerPage; } }

        [DefaultValue(true)]
        public bool HasPreviousPage { get { return CurrentPage > 1; } }
    }
}