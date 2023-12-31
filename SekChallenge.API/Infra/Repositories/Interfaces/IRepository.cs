﻿using SekChallenge.API.Entities;

namespace SekChallenge.API.Infra.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity?> GetAsync(Guid id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
