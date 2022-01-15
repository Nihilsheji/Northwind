using Northwind.DbContexts.Queries;
using Northwind.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Services.Abstractions
{
    public interface ICrudServiceBase<T, KeyType> where T : class, IIdentificable<KeyType>
    {
        Task<int> GetCount();
        Task<int> GetCount(GetQueryOptions<T> opt);
        Task<int> GetCount<TResult>(GetQueryOptions<T, TResult> opt);
        Task<T> Create(T s);
        Task<IEnumerable<T>> Create(IEnumerable<T> s);
        Task<IEnumerable<T>> Get();
        Task<T> Get(KeyType id);
        Task<IEnumerable<T>> Get(IEnumerable<KeyType> ids);
        Task<T> Get(GetSingleQueryOptions<T> opt);
        Task<TResult> Get<TResult>(GetSingleQueryOptions<T, TResult> opt);
        Task<IEnumerable<T>> Get(GetQueryOptions<T> opt);
        Task<IEnumerable<TResult>> Get<TResult>(GetQueryOptions<T, TResult> opt);
        Task<T> Update(T entity);
        Task<bool> Remove(KeyType id);
        Task<bool> Remove(IEnumerable<KeyType> ids);
    }
}
