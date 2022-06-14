using Infrastructure.ErrorMessage;
using Infrastructure.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        IQueryable<T> GetAll();
        Task<List<T>> GetAllWithPaging(PageParamenters page);
        ErrorMessage<T> BadRequestMessage(T entity, string msg);
        ErrorMessage<T> NotFoundMessage(T entity);
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate);
    }
}
