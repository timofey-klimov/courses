﻿using DataAccess.Interfaces;
using Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Implementation
{
    public class FilterProvider : IFilterProvider
    {
        public async Task<int> GetCountQuery<TEntity>(IQueryable<TEntity> query, ISpecification<TEntity> spec) where TEntity : BaseEntity
        {
            var exp = spec.CreateCriteria();

            return exp == null ? (await query.CountAsync()) : (await query.Where(exp).CountAsync());
        }

        public IQueryable<TEntity> GetQuery<TEntity>(IQueryable<TEntity> query, ISpecification<TEntity> spec) where TEntity : BaseEntity
        {
            var exp = spec.CreateCriteria();

            return exp == null ? query : query.Where(exp);
        }
    }
}
