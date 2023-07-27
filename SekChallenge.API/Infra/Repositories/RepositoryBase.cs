﻿using Microsoft.EntityFrameworkCore;
using SekChallenge.API.Entities;
using SekChallenge.API.Infra.Repositories.Interfaces;

namespace SekChallenge.API.Infra.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : EntityBase, IEntity, new()
    {
        private static readonly IEnumerable<EntityState> EntityMaintainedStatus = new[] { EntityState.Modified, EntityState.Added, EntityState.Deleted };

        private readonly SekContext _sekContext;
        protected DbSet<TEntity> Set { get; private set; }

        protected RepositoryBase(SekContext sekContext)
        {
            _sekContext = sekContext;
            Set = sekContext.Set<TEntity>();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var entry = await Set.AddAsync(entity);
            return entry.Entity;
        }

        public Task DeleteAsync(TEntity entity)
        {
            Set.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await Set.ToListAsync();
        }

        public async Task<TEntity?> GetAsync(Guid id)
        {
            var entry = await Set.FindAsync(id);
            return entry;
        }

        public Task UpdateAsync(TEntity entity)
        {
            var entry = _sekContext.Entry(entity);
            if (!EntityMaintainedStatus.Contains(entry.State))
            {
                entry.State = EntityState.Modified;
            }

            return Task.CompletedTask;
        }
    }
}
