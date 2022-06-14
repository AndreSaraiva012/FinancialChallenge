using Infrastructure.BaseClass;
using Infrastructure.ErrorMessage;
using Infrastructure.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        private readonly DataContext _context;
        protected readonly DbSet<T> _dbSet;
        protected Func<IQueryable<T>, IIncludableQueryable<T, object>> _include;

        public GenericRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual void SetInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            _include = include;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<T>> GetAllWithPaging(PageParamenters page)
        {
            var query = _dbSet
                .Skip((page.PageNumber - 1) * page.PageSize)
                .Take(page.PageSize)
                .AsNoTracking();

            if (_include != null)
            {
                query = _include(query);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var query = _dbSet
                .Where(e => e.Id == id).AsNoTracking();

            if (_include != null)
            {
                query = _include(query);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            var query = _dbSet
                .Where(predicate);

            if (_include != null)
            {
                query = _include(query);
            }
            return await query.FirstOrDefaultAsync();
        }

        public ErrorMessage<T> BadRequestMessage(T entity, string msg)
        {
            List<string> errorList = new List<string>();
            errorList.Add(msg);
            var error = new ErrorMessage<T>(HttpStatusCode.BadRequest.GetHashCode().ToString(), errorList, entity);
            return error;
        }

        public ErrorMessage<T> NotFoundMessage(T entity)
        {
            List<string> errorList = new List<string>();
            errorList.Add("In database doesn´t contain the data you want....");
            var error = new ErrorMessage<T>(HttpStatusCode.NoContent.GetHashCode().ToString(), errorList, entity);
            return error;
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate)
        {
            var query = _dbSet
                .Where(predicate);

            if (_include != null)
            {
                query = _include(query);
            }
            return await query.ToListAsync();
        }
    }
}