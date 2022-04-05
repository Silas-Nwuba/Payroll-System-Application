using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Enterprise_Application
{
    public class PaymentRecordListPagination<T> : List<T>
    {

        public int PageIndex { get; private set; }
        public int TotalPage { get; private set; }

        public PaymentRecordListPagination(IList<T> item, int count, int pageIndex, int Pagesize)
        {
            PageIndex = pageIndex;
            TotalPage = (int)Math.Ceiling(count / (double)Pagesize);
            this.AddRange(item);
        }
        //this field will handle the enable and disable of the next and prevoius
        public bool IsPreviousPageAvailabe => PageIndex > 1;
        public bool IsNextPageAvailable => PageIndex < TotalPage;

        public static PaymentRecordListPagination<T> Create(IList<T> Source,int PageSize ,int pageIndex)
        {
            var source = Source.Count();
            var item = Source.Skip((pageIndex - 1) * PageSize).Take(source).ToList();
            return new PaymentRecordListPagination<T>(item, pageIndex, PageSize, source);
           
        }
        
    }
}
