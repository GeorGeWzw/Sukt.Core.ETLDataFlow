using Microsoft.Extensions.DependencyInjection;
using Sukt.EntityFrameworkCore;
using Sukt.Module.Core.AppOption;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sukt.EtlCore.EntityFrameworkCore
{
    public class EntityFrameworkCoreModule: EntityFrameworkCoreBaseModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            this.AddDbDriven(context.Services);
            var configuration = context.Services.GetConfiguration();
            context.Services.Configure<AppOptionSettings>(configuration.GetSection("SuktCore"));
            var settings = context.Services.GetAppSettings();
            context.Services.AddSuktDbContext<SuktContext>(x=> {
                x.ConnectionString = settings.DbContexts.Values.First().ConnectionString;
                x.DatabaseType = settings.DbContexts.Values.First().DatabaseType;
                x.MigrationsAssemblyName = settings.DbContexts.Values.First().MigrationsAssemblyName;
            });
            context.Services.AddUnitOfWork<SuktContext>();
            context.Services.AddRepository();
            //AddAddSuktDbContextWnitUnitOfWork(context.Services);
            //AddRepository(context.Services);
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
