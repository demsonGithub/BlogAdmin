using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Demkin.Blog.IService.Base
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        #region 查询

        Task<TEntity> GetEntityAsync(object id);

        Task<TEntity> GetEntityAsync(Expression<Func<TEntity, bool>> whereExpresision);

        Task<List<TEntity>> GetEntityListAsync();

        Task<List<TEntity>> GetEntityListAsync(string whereSql);

        Task<List<TEntity>> GetEntityListAsync(string whereSql, string orderByFiled);

        Task<List<TEntity>> GetEntityListAsync(string whereSql, string orderByFiled, int topNum);

        Task<List<TEntity>> GetEntityListAsync(string whereSql, string orderByFiled, int currentPage, int pageSize);

        Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression);

        Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression, string orderByFiled);

        Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression, string orderByFiled, int topNum);

        Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression, string orderByFiled, int currentPage, int pageSize);

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

        Task<int> UpdateAsync(TEntity entity, Expression<Func<TEntity, object>> updateColumns, Expression<Func<TEntity, bool>> updateExpression);

        //Task<int> UpdateAsync(Expression<Func<TEntity, bool>> whereExpression, Func<TEntity, Task> updateAction);

        #endregion 修改

        #region Others

        Task<bool> IsExist(object id);

        Task<bool> IsExist(Expression<Func<TEntity, bool>> whereExpression);

        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression);

        #endregion Others
    }
}