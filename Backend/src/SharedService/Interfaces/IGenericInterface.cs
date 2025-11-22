using SharedService.Response;
using System.Linq.Expressions;

namespace SharedService.Interfaces
{
    public interface IGenericInterface<T> where T:class
    {
        Task<CustomResponse> CraeteAsync(T entity);
        Task<CustomResponse> UpdateAsync(T entity);
        Task<CustomResponse> DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindByIdAsync(int id);
        Task<T> GetByAsync(Expression<Func<T, bool>> predicate);

    }
}
