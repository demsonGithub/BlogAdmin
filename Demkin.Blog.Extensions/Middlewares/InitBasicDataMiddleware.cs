using Demkin.Blog.CodeFirst;
using Demkin.Blog.DbAccess;
using Demkin.Blog.Utils.SystemConfig;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demkin.Blog.Extensions.Middlewares
{
    /// <summary>
    /// 初始化基本表结构和数据
    /// </summary>
    public static class InitBasicDataMiddleware
    {
        public static void UseInitBasicDataMiddleware(this IApplicationBuilder app, MyDbContext myDbContext, string webRootPath)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            try
            {
                if (DbConfigInfo.InitTables)
                {
                    BasicData.InitDataAsync(myDbContext, webRootPath).Wait();
                }
            }
            catch
            {
                throw new Exception("数据库初始化出错");
            }
        }
    }
}