using Microsoft.Extensions.DependencyInjection;
using Sukt.EntityFrameworkCore;
using Sukt.Module.Core.AppOption;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Sukt.EtlCore.EntityFrameworkCore
{
    public class EntityFrameworkCoreModule: EntityFrameworkCoreBaseModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            var configuration = context.Services.GetConfiguration();
            context.Services.Configure<AppOptionSettings>(configuration.GetSection("SuktCore"));
            base.ConfigureServices(context);
            //AddAddSuktDbContextWnitUnitOfWork(context.Services);
            //AddRepository(context.Services);
        }

        public override void AddDbContextWithUnitOfWork(IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var settings = services.GetAppSettings();
            var connection = services.GetConfiguration()["SuktCore:DbContexts:MySql:ConnectionString"];
            //var connection = services.GetFileByConfiguration("SuktCore:DbContext:MongoDBConnectionString", "未找到存放MongoDB数据库链接的文件");
            if (Path.GetExtension(connection).ToLower() == ".txt") //txt文件
            {
                connection = provider.GetFileText(connection, $"未找到存放MySql数据库链接的文件");
            }

            services.AddSuktDbContext<SuktContext>(options =>
            {
                options.ConnectionString = connection;
                options.DatabaseType = settings.DbContexts["MySql"].DatabaseType;
            });
            services.AddUnitOfWork<SuktContext>();
        }
        //protected virtual IServiceCollection AddRepository(IServiceCollection services)
        //{
        //    services.AddScoped(typeof(IEFCoreRepository<,>), typeof(BaseRepository<,>));
        //    services.AddScoped(typeof(IAggregateRootRepository<,>), typeof(AggregateRootBaseRepository<,>));
        //    return services;
        //}

        //protected virtual IServiceCollection AddAddSuktDbContextWnitUnitOfWork(IServiceCollection services)
        //{
        //    services.AddSuktDbContext<DefaultDbContext>();
        //    services.AddUnitOfWork<DefaultDbContext>();
        //    return services;
        //}
    }
}
