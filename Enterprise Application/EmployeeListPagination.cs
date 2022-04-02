using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Enterprise_Application
{
    public class EmployeeListPagination<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPage { get; private set; }

        public EmployeeListPagination(IList<T> item, int count, int pageIndex, int Pagesize)
        {
            PageIndex = pageIndex;
            TotalPage = (int)Math.Ceiling(count /(double)Pagesize);
            this.AddRange(item);
        }
        //this will enable and disable the next and previous like if the page move to other the prevoius will enable if 
        //the page is in the index of I which is the main page the prevoius will be disable
        public bool IsPreviousPageAvailabe => PageIndex > 1;
        public bool IsNextPageAvailabe => PageIndex < TotalPage;

        public static EmployeeListPagination<T> Create(IList<T> Source , int pageIndex,int pageSize)
        {
            var source = Source.Count();
            var item = Source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new EmployeeListPagination<T>(item , source, pageIndex,pageSize);
        }
            

    }

}
