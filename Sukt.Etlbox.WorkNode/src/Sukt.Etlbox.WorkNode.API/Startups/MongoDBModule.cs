using Microsoft.Extensions.DependencyInjection;
using SuktCore.MongoDB;
using SuktCore.MongoDB.DbContexts;
using SuktCore.Shared.Extensions;
using System.IO;

namespace Sukt.Etlbox.WorkNode.API.Startups
{
    public class MongoDBModule : MongoDBModuleBase
    {
        protected override void AddDbContext(IServiceCollection services)
        {
            //var dbpath = services.GetConfiguration()["SuktCore:DbContext:MongoDBConnectionString"];
            //var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath; //获取项目路径
            //var dbcontext = Path.Combine(basePath, dbpath);
            //if (!File.Exists(dbcontext))
            //{
            //    throw new Exception("未找到存放数据库链接的文件");
            //}
            var provider = services.BuildServiceProvider();
            var connection = services.GetConfiguration()["SuktCore:MongoDBs:MongoDBConnectionString"];
            //var connection = services.GetFileByConfiguration("SuktCore:DbContext:MongoDBConnectionString", "未找到存放MongoDB数据库链接的文件");
            if (Path.GetExtension(connection).ToLower() == ".txt") //txt文件
            {
                connection = provider.GetFileText(connection, $"未找到存放MongoDB数据库链接的文件");
            }
            //var connection = services.GetFileByConfiguration("SuktCore:MongoDBs:MongoDBConnectionString", "未找到存放MongoDB数据库链接的文件"); //File.ReadAllText(dbcontext).Trim();
            services.AddMongoDbContext<DefaultMongoDbContext>(options =>
            {
                options.ConnectionString = connection;
            });
        }
    }
}
