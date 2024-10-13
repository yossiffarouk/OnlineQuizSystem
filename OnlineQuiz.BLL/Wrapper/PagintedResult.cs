using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Wrapper
{
    public class PagintedResult<T>
    {
        public List<T > Data { get; set; }
        //public PagintedResult(List<T> data) 
        //{
        //   Data = data;
        //}
        public PagintedResult(bool succeeded, List<T> data = default,
                                 List<string> Messages = null, int count = 0,
                                 int pagenumber = 1, int pagesize = 10)
               { 
                 Suceeded = succeeded;
                 Data = data;
                 Messages = Messages ; 
                 TotalCount = count;
                 CurerntPage = pagenumber;
                 TotalPages = (int)Math.Ceiling(count / (double)pagesize);
                 PageSize = pagesize;
                }
        public static PagintedResult<T> Success(List<T> data, int count,
                                              int pagenumber, int pagesize)
        {
            return new (true,data ,null , count,pagenumber,pagesize);
        }

        public int CurerntPage {  get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public List<string> Massages {  get; set; } 
        public bool  Suceeded { get; set; }
        public bool HasPreviousPage => CurerntPage > 1;
        public bool HasNextPage => CurerntPage < TotalPages;





    }
}
