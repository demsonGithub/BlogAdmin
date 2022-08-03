using Demkin.Blog.IService.Base;
using Demkin.Blog.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Demkin.Blog.Service.Base
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        private readonly IBaseRepository<TEntity> _baseRepository;

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            return await _baseRepository.AddAsync(entity);
        }

        public async Task<int> AddAsync(List<TEntity> entities)
        {
            return await _baseRepository.AddAsync(entities);
        }

        public async Task<int> CountAsync()
        {
            return await _baseRepository.CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _baseRepository.CountAsync(whereExpression);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            return await _baseRepository.DeleteAsync(id);
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            return await _baseRepository.DeleteAsync(entity);
        }

        public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _baseRepository.DeleteAsync(whereExpression);
        }

        public async Task<TEntity> GetEntityAsync(object id)
        {
            return await _baseRepository.GetEntityAsync(id);
        }

        public async Task<TEntity> GetEntityAsync(Expression<Func<TEntity, bool>> whereExpresision)
        {
            return await _baseRepository.GetEntityAsync(whereExpresision);
        }

        public async Task<List<TEntity>> GetEntityListAsync()
        {
            return await _baseRepository.GetEntityListAsync();
        }

        public async Task<List<TEntity>> GetEntityListAsync(string whereSql)
        {
            return await _baseRepository.GetEntityListAsync(whereSql);
        }

        public async Task<List<TEntity>> GetEntityListAsync(string whereSql, string orderByFiled)
        {
            return await _baseRepository.GetEntityListAsync(whereSql, orderByFiled);
        }

        public async Task<List<TEntity>> GetEntityListAsync(string whereSql, string orderByFiled, int topNum)
        {
            return await _baseRepository.GetEntityListAsync(whereSql, orderByFiled, topNum);
        }

        public async Task<List<TEntity>> GetEntityListAsync(string whereSql, string orderByFiled, int pageIndex, int pageSize)
        {
            return await _baseRepository.GetEntityListAsync(whereSql, orderByFiled, pageIndex, pageSize);
        }

        public async Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _baseRepository.GetEntityListAsync(whereExpression);
        }

        public async Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression, string orderByFiled)
        {
            return await _baseRepository.GetEntityListAsync(whereExpression, orderByFiled);
        }

        public async Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression, string orderByFiled, int topNum)
        {
            return await _baseRepository.GetEntityListAsync(whereExpression, orderByFiled, topNum);
        }

        public async Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression, string orderByFiled, int pageIndex, int pageSize)
        {
            return await _baseRepository.GetEntityListAsync(whereExpression, orderByFiled, pageIndex, pageSize);
        }

        public async Task<bool> IsExist(object id)
        {
            return await _baseRepository.IsExist(id);
        }

        public async Task<bool> IsExist(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _baseRepository.IsExist(whereExpression);
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            return await _baseRepository.UpdateAsync(entity);
        }

        public async Task<TEntity> UpdateAsync(object id, Func<TEntity, Task> updateAction)
        {
            return await _baseRepository.UpdateAsync(id, updateAction);
        }

        public async Task<int> UpdateAsync(Expression<Func<TEntity, bool>> whereExpression, Func<TEntity, Task> updateAction)
        {
            return await _baseRepository.UpdateAsync(whereExpression, updateAction);
        }
    }
}