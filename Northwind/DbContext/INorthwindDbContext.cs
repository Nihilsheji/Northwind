﻿using Microsoft.EntityFrameworkCore;
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

        Task<int> GetCount<T>(DbSet<T> set) where T : class;
        Task<int> GetCount<T>(DbSet<T> set, GetQueryOptions<T> opt) where T : class;
        Task<int> GetCount<T, TResult>(DbSet<T> set, GetQueryOptions<T, TResult> opt) where T : class;


        Task<T> GetEntity<T, KeyType>(DbSet<T> set, KeyType id) where T : class;
        Task<T> GetEntity<T>(DbSet<T> set, GetSingleQueryOptions<T> opt) where T : class;
        Task<TResult> GetEntity<T, TResult>(DbSet<T> set, GetSingleQueryOptions<T, TResult> opt) where T : class;

        Task<IEnumerable<T>> GetEntities<T>(DbSet<T> set) where T : class;
        Task<IEnumerable<T>> GetEntities<T, KeyType>(DbSet<T> set, IEnumerable<KeyType> ids) where T : class;
        Task<IEnumerable<T>> GetEntities<T>(DbSet<T> set, GetQueryOptions<T> opt) where T : class;
        Task<IEnumerable<TResult>> GetEntities<T, TResult>(DbSet<T> set, GetQueryOptions<T, TResult> opt) where T : class;

        T CreateEntity<T>(DbSet<T> set, T entity) where T : class;
        IEnumerable<T> CreateEntities<T>(DbSet<T> set, IEnumerable<T> entities) where T : class;

        T UpdateEntity<T>(DbSet<T> set, T entity) where T : class;

        T DeleteEntity<T>(DbSet<T> set, T entity) where T : class;
        IEnumerable<T> DeleteEntities<T>(DbSet<T> set, IEnumerable<T> entity) where T : class;

        Task SaveChangesAsync();
        DbSet<T> GetDbSet<T>() where T : class;
        
    }
}
