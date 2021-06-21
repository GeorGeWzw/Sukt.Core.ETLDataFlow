using Microsoft.Extensions.DependencyInjection;
using Sukt.EntityFrameworkCore;
using Sukt.Module.Core.AppOption;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sukt.EtlCore.WorkNode.EntityFrameworkCore
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
            var settings = services.GetAppSettings();
            services.AddSuktDbContext<SuktContext>(x=> {
                x.ConnectionString = settings.DbContexts.Values.First().ConnectionString;
                x.DatabaseType = settings.DbContexts.Values.First().DatabaseType;
                x.MigrationsAssemblyName = settings.DbContexts.Values.First().MigrationsAssemblyName;
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
