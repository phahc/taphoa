using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSys.WebApp
{

    public class PageHelper<T> : List<T>
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PageHelper(IQueryable<T> dataSource, int pageIndex, int pageSize, int totalCount)
        {
            TotalCount = totalCount;
            CurrentPage = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            this.AddRange(dataSource);
        }
    }
}
