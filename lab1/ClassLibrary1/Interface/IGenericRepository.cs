using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        public IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int? pageIndex = null, // Optional parameter for pagination (page number)
            int? pageSize = null);


        public T GetByID(object id);

        public void Insert(T entity);

        public void Delete(object id);

        public void Delete(T entityToDelete);

        public void Update(T entityToUpdate);
    }
}
