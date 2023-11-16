using ApplicationCore_BilgeAdam.Entities.Abstract;
using Infrastructure_BilgeAdam.Context;
using Infrastructure_BilgeAdam.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_BilgeAdam.Services.Concrete
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _table;


        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }



        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = Status.Modified;
            _table.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            entity.DeletedDate = DateTime.Now;
            entity.Status = Status.Passive;
            _table.Update(entity);
            await _context.SaveChangesAsync();
        }


        //READ Operations
        public async Task<T> GetById(int id) => await _table.FirstOrDefaultAsync(x => x.Id == id && x.Status != Status.Passive);

        public async Task<T> GetByDefault(Expression<Func<T, bool>> expression) => await _table.FirstOrDefaultAsync(expression);

        public async Task<List<T>> GetByDefaults(Expression<Func<T, bool>> expression) => await _table.Where(expression).ToListAsync();


        public async Task<bool> Any(Expression<Func<T, bool>> expression) => await _table.AnyAsync(expression);


        public async Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> join = null)
        {
            IQueryable<T> query = _table;

            if (join != null)
            {
                query = join(query);
            }

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderBy != null)
            {
                return await orderBy(query).Select(select).ToListAsync();
            }
            else
            {
                return await query.Select(select).ToListAsync();
            }
        }

        
    }
}
