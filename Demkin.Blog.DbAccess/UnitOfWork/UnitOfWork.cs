using Demkin.Blog.Utils.SystemConfig;
using Serilog;
using SqlSugar;
using System;
using System.Linq;

namespace Demkin.Blog.DbAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISqlSugarClient _sqlSugarClient;

        private int _tranCount { get; set; }

        public UnitOfWork(ISqlSugarClient sqlSugarClient)
        {
            if (SiteInfo.IsDebugSql)
            {
                sqlSugarClient.Aop.OnLogExecuting = (sql, p) =>
                {
                    Log.Information("Sql语句：" + sql);
                    Log.Debug("参数：" + string.Join(',', p?.Select(item => item.ParameterName + ":" + item.Value)));
                };
            }

            _sqlSugarClient = sqlSugarClient;
            _tranCount = 0;
        }

        public SqlSugarScope GetDbClient()
        {
            return _sqlSugarClient as SqlSugarScope;
        }

        public void BeginTran()
        {
            lock (this)
            {
                _tranCount++;
                GetDbClient().BeginTran();
            }
        }

        public void CommitTran()
        {
            lock (this)
            {
                _tranCount--;
                if (_tranCount == 0)
                {
                    try
                    {
                        GetDbClient().CommitTran();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        GetDbClient().RollbackTran();
                    }
                }
            }
        }

        public void RollbackTran()
        {
            lock (this)
            {
                _tranCount--;
                GetDbClient().RollbackTran();
            }
        }
    }
}