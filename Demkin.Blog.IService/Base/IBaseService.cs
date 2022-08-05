using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Demkin.Blog.IService.Base
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        #region 查询

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetEntityAsync(object id);

        /// <summary>
        /// 根据条件获取匹配的第一个实体
        /// </summary>
        /// <param name="whereExpresision"></param>
        /// <returns></returns>
        Task<TEntity> GetEntityAsync(Expression<Func<TEntity, bool>> whereExpresision);

        /// <summary>
        /// 获取所有的实体集合
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetEntityListAsync();

        /// <summary>
        /// 根据sql查询匹配的实体集合
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetEntityListAsync(string whereSql);

        /// <summary>
        /// 根据sql查询匹配的实体集合，并根据字段排序
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="orderByFiled"></param>
        /// <param name="isAsc">是否升序，默认Asc</param>
        /// <returns></returns>
        Task<List<TEntity>> GetEntityListAsync(string whereSql, string orderByFiled, bool isAsc = true);

        /// <summary>
        /// 根据sql查询匹配的实体集合，获取前多少行，并根据字段排序
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="orderByFiled"></param>
        /// <param name="topNum"></param>
        /// <param name="isAsc">是否升序，默认Asc</param>
        /// <returns></returns>
        Task<List<TEntity>> GetEntityListAsync(string whereSql, string orderByFiled, int topNum, bool isAsc = true);

        /// <summary>
        /// 根据sql查询分页的实体集合，并根据字段排序
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="orderByFiled"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="isAsc">是否升序，默认Asc</param>
        /// <returns></returns>
        Task<List<TEntity>> GetEntityListAsync(string whereSql, string orderByFiled, int currentPage, int pageSize, bool isAsc = true);

        /// <summary>
        /// 根据条件查询匹配的实体集合
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 根据条件查询匹配的实体集合,并根据字段排序
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderByFiled"></param>
        /// <param name="isAsc">是否升序，默认Asc</param>
        /// <returns></returns>
        Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression, string orderByFiled, bool isAsc = true);

        /// <summary>
        /// 根据条件查询匹配的实体集合,并根据字段排序,获取前多少行
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderByFiled"></param>
        /// <param name="topNum"></param>
        /// <param name="isAsc">是否升序，默认Asc</param>
        /// <returns></returns>
        Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression, string orderByFiled, int topNum, bool isAsc = true);

        /// <summary>
        /// 根据条件查询分页的实体集合，并根据字段分页
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderByFiled"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="isAsc">是否升序，默认Asc</param>
        /// <returns></returns>
        Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, bool>> whereExpression, string orderByFiled, int currentPage, int pageSize, bool isAsc = true);

        #endregion 查询

        #region 增加

        /// <summary>
        /// 增加一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// 批量增加实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> AddAsync(List<TEntity> entities);

        #endregion 增加

        #region 删除

        /// <summary>
        /// 根据Id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(object id);

        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(TEntity entity);

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> whereExpression);

        #endregion 删除

        #region 修改

        /// <summary>
        /// 根据实体更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity entity);

        /// <summary>
        /// 根据表达式更新
        /// </summary>
        /// <param name="updateColumns"></param>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(Expression<Func<TEntity, TEntity>> updateColumns, Expression<Func<TEntity, bool>> updateExpression);

        #endregion 修改

        #region Others

        /// <summary>
        /// 判断Id是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExist(object id);

        /// <summary>
        /// 根据条件判断是否存在实体
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        Task<bool> IsExist(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 统计实体的数量
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        /// <summary>
        /// 根据条件统计实体的数量
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression);

        #endregion Others
    }
}