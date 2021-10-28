using Northwind.DbContexts;
using Northwind.DbContexts.Queries;
using Northwind.Models.Response;
using Northwind.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class CrudServiceBase<T> : ICrudServiceBase<T> where T : class
    {
        private readonly INorthwindDbContext _context;
        
        public CrudServiceBase(INorthwindDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetCount()
        {
            return await _context.GetCount<T>();
        }

        public async Task<int> GetCount(GetQueryOptions<T> opt)
        {
            return await _context.GetCount(opt);

        }

        public async Task<int> GetCount<TResult>(GetQueryOptions<T, TResult> opt)
        {
            return await _context.GetCount(opt);
        }

        public async Task<T> Create(T s)
        {
            var entity = _context.CreateEntity(s);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<T>> Create(IEnumerable<T> s)
        {
            var entities = _context.CreateEntities(s);
            await _context.SaveChangesAsync();

            return entities;
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await _context.GetEntities<T>();
        }

        public async Task<T> Get<KeyType>(KeyType id)
        {
            return await _context.GetEntity<T, KeyType>(id);
        }

        public async Task<T> Get<KeyType>(GetSingleQueryOptions<T> opt)
        {
            return await _context.GetEntity(opt);
        }

        public async Task<IEnumerable<T>> Get<KeyType>(IEnumerable<KeyType> ids)
        {
            return await _context.GetEntities<T, KeyType>(ids);
        }

        public async Task<T> Get(GetSingleQueryOptions<T> opt)
        {
            return await _context.GetEntity(opt);
        }

        public async Task<TResult> Get<TResult>(GetSingleQueryOptions<T, TResult> opt)
        {
            return await _context.GetEntity(opt);
        }

        public async Task<IEnumerable<DictionaryValue<TKey, TValue>>> GetDictionary<TKey, TValue>(Func<T, DictionaryValue<TKey, TValue>> dictMap)
        {
            return await _context.GetEntities(new GetQueryOptions<T, DictionaryValue<TKey, TValue>>()
            {
                Select = (IQueryable<T> q) => q.Select(e => dictMap(e))
            });
        }

        public async Task<IEnumerable<T>> Get(GetQueryOptions<T> opt)
        {
            return await _context.GetEntities(opt);
        }

        public async Task<IEnumerable<TResult>> Get<TResult>(GetQueryOptions<T, TResult> opt)
        {
            return await _context.GetEntities(opt);
        }


        public async Task<T> Update(T entity)
        {
            var updated = _context.UpdateEntity(entity);
            await _context.SaveChangesAsync();

            return updated;
        }

        public async Task<bool> Remove<KeyType>(KeyType id) {
            var entity = await _context.GetEntity<T, KeyType>(id);
            
            if(entity == null)
            {
                return false;
            }

            var deleted = _context.DeleteEntity<T>(entity);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Remove<KeyType>(IEnumerable<KeyType> ids)
        {
            var entities = await _context.GetEntities<T, KeyType>(ids);

            if (entities == null)
            {
                return false;
            }

            var deleted = _context.DeleteEntities(entities);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
