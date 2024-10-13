using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Wrapper
{
    public static class QuerableExtentions
    {
        public static async Task<PagintedResult<T>> ToPagintedListAsync<T>
         (this IQueryable<T> Source, int PageNumber, int PageSize)where T : class
        {

            if (Source == null)
            { 
                throw new Exception("Empty");
            }
            //number of pages
            PageNumber = PageNumber == 0 ? 1 : PageNumber;
            //number of record 
            PageSize = PageSize == 0 ? 10 :PageSize;
            // sourse = 100  page number = 1 page size = 10 count = (100/10=10)
            int count =  Source.AsNoTracking().Count();
            if (count == 0)
            {
                var Result = PagintedResult<T>
                              .Success(new List<T>(), count,
                               PageNumber, PageSize);
                return Result;
            }
            PageNumber = PageNumber <= 0 ? 1 : PageNumber;
            var items =  Source.Skip((PageNumber -1)*PageSize)
                                     .Take(PageSize).ToList();
            var result = PagintedResult<T>
                            .Success(items, count,
                             PageNumber, PageSize);
            return result;












            /* Pagination
            int NumberofRecordPerPage = 10;
            int NoOfPages =
                Convert.ToInt32(
                    Math.Ceiling(Convert.ToDouble(T.Count)) / 
                    Convert.ToDouble(NumberofRecordPerPage));
            int NoOfRecordToSkip= (PageNumber-1) * NumberofRecordPerPage;
                   T = T.Skip(NoOfRecordToSkip).Take(NumberofRecordPerPage).ToList();
            */

        }
    }
}
