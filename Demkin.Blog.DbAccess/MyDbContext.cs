using Demkin.Blog.Utils.SystemConfig;
using SqlSugar;
using System;

namespace Demkin.Blog.DbAccess
{
    public class MyDbContext
    {
        private SqlSugarScope _db;
        private string _connectionString = DbConfigInfo.ConnectionString;
        private DbType _dbType = (DbType)DbConfigInfo.Type;

        /// <summary>
        /// 数据库链接对象
        /// </summary>
        public SqlSugarScope Db { get => _db; private set => _db = value; }

        public MyDbContext(ISqlSugarClient sqlSugarClient)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentNullException("数据库字符串未配置");
            }
            _db = sqlSugarClient as SqlSugarScope;
        }

        #region 实例方法

        /// <summary>
        /// 功能描述:获取数据库处理对象
        /// </summary>
        /// <returns>返回值</returns>
        public SimpleClient<T> GetEntityDB<T>() where T : class, new()
        {
            return new SimpleClient<T>(_db);
        }

        /// <summary>
        /// 功能描述:获取数据库处理对象
        /// </summary>
        /// <param name="db">db</param>
        /// <returns>返回值</returns>
        public SimpleClient<T> GetEntityDB<T>(SqlSugarClient db) where T : class, new()
        {
            return new SimpleClient<T>(db);
        }

        #endregion 实例方法
    }
}