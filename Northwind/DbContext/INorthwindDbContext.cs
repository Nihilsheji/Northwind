using Microsoft.EntityFrameworkCore;
using Northwind.DbContexts.Queries;
using Northwind.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.DbContexts
{
    public interface INorthwindDbContext
    {

        Task<int> GetCount<T>() where T : class;
        Task<int> GetCount<T>(GetQueryOptions<T> opt) where T : class;
        Task<int> GetCount<T, TResult>(GetQueryOptions<T, TResult> opt) where T : class;


        Task<T> GetEntity<T, KeyType>(KeyType id) where T : class;
        Task<T> GetEntity<T>(GetSingleQueryOptions<T> opt) where T : class;
        Task<TResult> GetEntity<T, TResult>(GetSingleQueryOptions<T, TResult> opt) where T : class;

        Task<IEnumerable<T>> GetEntities<T>() where T : class;
        Task<IEnumerable<T>> GetEntities<T, KeyType>(IEnumerable<KeyType> ids) where T : class;
        Task<IEnumerable<T>> GetEntities<T>(GetQueryOptions<T> opt) where T : class;
        Task<IEnumerable<TResult>> GetEntities<T, TResult>(GetQueryOptions<T, TResult> opt) where T : class;

        T CreateEntity<T>(T entity) where T : class;
        IEnumerable<T> CreateEntities<T>(IEnumerable<T> entities) where T : class;

        T UpdateEntity<T>(T entity) where T : class;

        T DeleteEntity<T>(T entity) where T : class;
        IEnumerable<T> DeleteEntities<T>(IEnumerable<T> entity) where T : class;

        Task SaveChangesAsync();
        
    }
}
