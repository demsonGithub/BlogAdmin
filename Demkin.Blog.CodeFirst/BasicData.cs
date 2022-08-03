using Demkin.Blog.DbAccess;
using Demkin.Blog.Entity;
using Demkin.Blog.Utils.Help;
using Demkin.Blog.Utils.SystemConfig;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Demkin.Blog.CodeFirst
{
    /// <summary>
    /// 初始化数据库
    /// </summary>
    public class BasicData
    {
        public static async Task InitDataAsync(MyDbContext myDbContext, string webRootPath)
        {
            try
            {
                // 创建数据库
                if (DbConfigInfo.Type == DataBaseType.Oracle)
                {
                    Console.WriteLine($"Oracle 数据库不支持该操作，可手动创建Oracle数据库!");
                    return;
                }
                var result = myDbContext.Db.DbMaintenance.CreateDatabase();
                if (!result)
                {
                    Console.WriteLine($"数据库创建失败!");
                    return;
                }
                Console.WriteLine($"数据库创建成功......");

                // 创建表结构
                var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
                string entityDllName = DbConfigInfo.EntityDllName;
                var referencedAssemblies = Directory.GetFiles(path, entityDllName + ".dll").Select(Assembly.LoadFrom).ToArray();
                var modelTypes = referencedAssemblies
                    .SelectMany(a => a.DefinedTypes)
                    .Select(type => type.AsType())
                    .Where(x => x.IsClass && x.Namespace != null && x.Namespace.Equals(entityDllName)).ToList();

                modelTypes.ForEach(item =>
                {
                    if (!myDbContext.Db.DbMaintenance.IsAnyTable(item.Name))
                    {
                        myDbContext.Db.CodeFirst.InitTables(item);
                        Console.WriteLine($"{item.Name} 表创建成功!");
                    }
                    else
                    {
                        Console.WriteLine($"{item.Name} 表已存在,如需生成，请手动删除!");
                    }
                });

                // 初始化数据
                if (DbConfigInfo.InitBasicData && !string.IsNullOrEmpty(DbConfigInfo.InitBasicDataFolder))
                {
                    string _initBasicDataFolder = Path.Combine(webRootPath, DbConfigInfo.InitBasicDataFolder);

                    #region SysUserInfo

                    if (!await myDbContext.Db.Queryable<SysUser>().AnyAsync())
                    {
                        var data = NPOIHelper.XlsToList<SysUser>(Path.Combine(_initBasicDataFolder, "SysUser.xls"));
                        myDbContext.GetEntityDB<SysUser>().InsertRange(data);
                        Console.WriteLine("表：SysUser 数据初始化成功......");
                    }
                    else
                    {
                        Console.WriteLine("表：SysUser 初始化数据失败!");
                    }

                    #endregion SysUserInfo
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}