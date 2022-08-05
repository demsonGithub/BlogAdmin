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

        public async Task<int> AddAsync(List<TEntity> entities)
        {
            return await _db.Insertable(entities).ExecuteCommandAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _db.Queryable<TEntity>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).CountAsync();
        }

        public async Task<bool> DeleteAsync(object id)
        {
            return await _db.Deleteable<TEntity>(id).ExecuteCommandHasChangeAsync();
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            return await _db.Deleteable(entity).ExecuteCommandHasChangeAsync();
        }

        public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            var resultNum = await _db.Deleteable(whereExpression).ExecuteCommandAsync();
            return resultNum;
        }

        public async Task<TEntity> GetEntityAsync(object id)
        {
            return await _db.Queryable<TEntity>().InSingleAsync(id);
        }

        public Task<TEntity> GetEntityAsync(Expression<Func<TEntity, bool>> whereExpresision)
        {
            var selectEntity = _db.Queryable<TEntity>().SingleAsync(whereExpresision);

            return selectEntity;
        }

        public async Task<List<TEntity>> GetEntityListAsync()
        {
            return await _db.Queryable<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> GetEntityListAsync(string whereSql)
        {
            return await _db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(whereSql), whereSql).ToListAsync();
        }

        public async Task<List<TEntity>> GetEntityListAsync(string whereSql, string orderByFiled, bool isAsc = true)
        {
            return await _db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(whereSql), whereSql).OrderByIF(!string.IsNullOrEmpty(orderByFiled), orderByFiled + (isAsc ? " asc" : " desc")).ToListAsync();
        }

        public async Task<List<TEntity>> GetEntityListAsync(string whereSql, string orderByFiled, int topNum, bool isAsc = true)
        {
            return await _db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(whereSql), whereSql).OrderByIF(!string.IsNullOrEmpty(orderByFiled), orderByFiled + (isAsc ? " asc" : " desc")).Take(topNum).ToListAsync();
        }

        public async Task<List<TEntity>> GetEntityListAsync(string whereSql, string orderByFiled, int currentPage, int pageSize, bool isAsc = true)
        {
            return await _db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(whereSql), whereSql).OrderByIF(!string.IsNullOrEmpty(orderByFiled), orderByFiled + (isAsc ? " asc" : " desc")).ToPageListAsync(currentPage, pageSize);
        }

        public async Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).ToListAsync();
        }

        public async Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression, string orderByFiled, bool isAsc = true)
        {
            return await _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).OrderByIF(!string.IsNullOrEmpty(orderByFiled), orderByFiled + (isAsc ? " asc" : " desc")).ToListAsync();
        }

        public async Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression, string orderByFiled, int topNum, bool isAsc = true)
        {
            return await _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).OrderByIF(!string.IsNullOrEmpty(orderByFiled), orderByFiled + (isAsc ? " asc" : " desc")).Take(topNum).ToListAsync();
        }

        public async Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression, string orderByFiled, int currentPage, int pageSize, bool isAsc = true)
        {
            return await _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).OrderByIF(!string.IsNullOrEmpty(orderByFiled), orderByFiled + (isAsc ? " asc" : " desc")).ToPageListAsync(currentPage, pageSize);
        }

        public async Task<bool> IsExist(object id)
        {
            return await _db.Queryable<TEntity>().InSingleAsync(id) != null;
        }

        public async Task<bool> IsExist(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).CountAsync() > 0;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            return await _db.Updateable(entity).ExecuteCommandHasChangeAsync();
        }

        public async Task<int> UpdateAsync(Expression<Func<TEntity, TEntity>> updateColumns, Expression<Func<TEntity, bool>> updateExpression)
        {
            return await _db.Updateable<TEntity>().SetColumns(updateColumns).Where(updateExpression).ExecuteCommandAsync();
        }
    }
}