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

        Task<List<TEntity>> GetEntityListAsync(string whereSql, string orderByFiled);

        Task<List<TEntity>> GetEntityListAsync(string whereSql, string orderByFiled, int topNum);

        Task<List<TEntity>> GetEntityListAsync(string whereSql, string orderByFiled, int pageIndex, int pageSize);

        Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression);

        Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression, string orderByFiled);

        Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression, string orderByFiled, int topNum);

        Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression, string orderByFiled, int pageIndex, int pageSize);

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

        Task<TEntity> UpdateAsync(object id, Func<TEntity, Task> updateAction);

        Task<int> UpdateAsync(Expression<Func<TEntity, bool>> whereExpression, Func<TEntity, Task> updateAction);

        #endregion 修改

        #region Others

        Task<bool> IsExist(object id);

        Task<bool> IsExist(Expression<Func<TEntity, bool>> whereExpression);

        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression);

        #endregion Others
    }
}