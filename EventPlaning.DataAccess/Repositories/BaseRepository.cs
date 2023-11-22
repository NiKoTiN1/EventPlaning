using EventPlanning.DataAccess.Interfaces;
using EventPlanning.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanning.DataAccess.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class,  IBaseModel
    {
        public BaseRepository(DatabaseContext context)
        {
            this.context = context;
        }

        private DbSet<T> Set => context.Set<T>();

        protected readonly DatabaseContext context;

        public async Task Add(T item)
        {
            Set.Add(item);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> filter = null, string[] children = null)
        {
            try
            {
                IQueryable<T> query = Set;

                if (children != null)
                {
                    foreach (string entity in children)
                    {
                        query = query.Include(entity);
                    }
                }

                if (filter == null)
                {
                    return query;
                }
                return query.Where(filter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task Remove(T item)
        {
            context.Remove(item);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Update(T item)
        {
            context.Update(item);
            await context.SaveChangesAsync();
        }
    }
}
