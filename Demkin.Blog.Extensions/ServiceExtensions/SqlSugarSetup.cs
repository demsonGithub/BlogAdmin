using Demkin.Blog.Utils.SystemConfig;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;

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
                    DbType = (DbType)DbConfigInfo.Type,
                    ConnectionString = DbConfigInfo.ConnectionString,
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.Attribute
                };

                return new SqlSugarScope(connectionConfig);
            });
        }
    }
}