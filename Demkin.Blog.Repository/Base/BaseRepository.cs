using Demkin.Blog.DbAccess.UnitOfWork;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Demkin.Blog.Repository.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly IUnitOfWork _unitOfWork;

        private ISqlSugarClient _db;

        public ISqlSugarClient Db
        {
            get { return _db; }
        }

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _db = unitOfWork.GetDbClient();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var insertEntity = _db.Insertable(entity);

            return await insertEntity.ExecuteReturnEntityAsync();
        }

        public Task<int> AddAsync(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetEntityAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetEntityAsync(Expression<Func<TEntity, bool>> whereExpresision)
        {
            var selectEntity = _db.Queryable<TEntity>().SingleAsync(whereExpresision);

            return selectEntity;
        }

        public Task<List<TEntity>> GetEntityListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetEntityListAsync(string whereSql)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetEntityListAsync(string whereSql, string orderByFiled)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetEntityListAsync(string whereSql, string orderByFiled, int topNum)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetEntityListAsync(string whereSql, string orderByFiled, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression, string orderByFiled)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression, string orderByFiled, int topNum)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression, string orderByFiled, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExist(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExist(Expression<Func<TEntity, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync(object id, Func<TEntity, Task> updateAction)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Expression<Func<TEntity, bool>> whereExpression, Func<TEntity, Task> updateAction)
        {
            throw new NotImplementedException();
        }
    }
}