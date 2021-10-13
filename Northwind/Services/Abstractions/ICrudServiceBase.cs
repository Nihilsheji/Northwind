using Northwind.DbContexts.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Services.Abstractions
{
    public interface ICrudServiceBase<T> where T : class
    {
        Task<T> Create(T s);
        Task<IEnumerable<T>> Create(IEnumerable<T> s);
        Task<T> Get(int id);
        Task<IEnumerable<T>> Get(IEnumerable<int> ids);
        Task<IEnumerable<T>> Get(GetQueryOptions<T> opt);
        Task<T> Update(T entity);
        Task<bool> Remove(int id);
        Task<bool> Remove(IEnumerable<int> ids);
    }
}
