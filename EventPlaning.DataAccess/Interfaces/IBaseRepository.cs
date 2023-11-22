using EventPlanning.Domain.Models;
using System.Linq.Expressions;

namespace EventPlanning.DataAccess.Interfaces
{
    public interface IBaseRepository<T>
    {
        public Task Add(T item);
        public IQueryable<T> Get(Expression<Func<T, bool>> filter = null, string[] children = null);
        public Task Update(T item);
        public Task Remove(T item);
    }
}
