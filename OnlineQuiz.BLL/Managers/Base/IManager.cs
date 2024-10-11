using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Managers.Base
{
    public interface IManager<T, U> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(U id);
        void Add(T entity);
        void Update(T entity);
        void DeleteById(U id);
    }
}
