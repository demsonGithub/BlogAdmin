using Demkin.Blog.Utils.SystemConfig;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SqlSugar;
using System;
using System.Linq;

namespace Demkin.Blog.Extensions.ServiceExtensions
{
    public static class SqlSugarSetup
    {
        public static void AddSqlSugarSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddScoped<ISqlSugarClient>(o =>
            {
                var connectionConfig = new ConnectionConfig
                {
                    DbType = (DbType)ConfigSetting.DbConfigInfo.Type,
                    ConnectionString = ConfigSetting.DbConfigInfo.ConnectionString,
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.Attribute,
                };

                return new SqlSugarScope(connectionConfig);
            });
        }
    }
}