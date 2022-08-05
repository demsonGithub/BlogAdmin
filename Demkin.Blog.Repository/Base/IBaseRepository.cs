using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Demkin.Blog.Repository.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        #region 查询

        Task<TEntity> GetEntityAsync(object id);

        Task<TEntity> GetEntityAsync(Expression<Func<TEntity, bool>> whereExpresision);

        Task<List<TEntity>> GetEntityListAsync();

        Task<List<TEntity>> GetEntityListAsync(string whereSql);

        Task<List<TEntity>> GetEntityListAsync(string whereSql, string orderByFiled, bool isAsc = true);

        Task<List<TEntity>> GetEntityListAsync(string whereSql, string orderByFiled, int topNum, bool isAsc = true);

        Task<List<TEntity>> GetEntityListAsync(string whereSql, string orderByFiled, int currentPage, int pageSize, bool isAsc = true);

        Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression);

        Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression, string orderByFiled, bool isAsc = true);

        Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression, string orderByFiled, int topNum, bool isAsc = true);

        Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression, string orderByFiled, int currentPage, int pageSize, bool isAsc = true);

        #endregion 查询

        #region 增加

        Task<TEntity> AddAsync(TEntity entity);

        Task<int> AddAsync(List<TEntity> entities);

        #endregion 增加

        #region 删除

        Task<bool> DeleteAsync(object id);

        Task<bool> DeleteAsync(TEntity entity);

        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> whereExpression);

        #endregion 删除

        #region 修改

        Task<bool> UpdateAsync(TEntity entity);

        Task<int> UpdateAsync(Expression<Func<TEntity, TEntity>> updateColumns, Expression<Func<TEntity, bool>> updateExpression);

        #endregion 修改

        #region Others

        Task<bool> IsExist(object id);

        Task<bool> IsExist(Expression<Func<TEntity, bool>> whereExpression);

        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression);

        #endregion Others
    }
}