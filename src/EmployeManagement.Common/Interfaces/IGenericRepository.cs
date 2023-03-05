using EmployeeManagement.Common.Model;
using System.Linq.Expressions;

namespace EmployeeManagement.Common.Interfaces;

public  interface IGenericRepository<T> where T : BaseEntity
{
    Task<List<T>> GetFilteredAsync (Expression<Func<T, bool>>[] filters, int? skip, int? take, bool isTracking = true, params Expression<Func<T, object>>[] includes);
    Task<List<T>> GetAsync (int? skip, int? take, bool isTracking = true, params Expression<Func<T, object>>[] includes);
    Task<T?> GetByIdAsync (int id, bool isTracking = true, params Expression<Func<T, object>>[] includes);
    Task<int> InsertAsync (T entity);
    void Update (T entity);
    void Delete(T entity);
    Task SaveChangesAsync();
}
