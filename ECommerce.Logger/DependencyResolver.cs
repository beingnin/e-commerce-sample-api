using ECommerce.DL;
using ECommerce.DL.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infra
{
    public class DependencyResolver
    {
        IServiceCollection _services;
        public DependencyResolver(IServiceCollection services)
        {
            _services = services;
        }

        public void Execute(string dbConnectionString)
        {

            //infra
            _services.AddTransient<Abstracts.ICustomLogger, Logger.DbLogger>();
            //services
            _services.AddScoped<BL.Abstracts.IOrderService, BL.OrderService>();
            _services.AddScoped<BL.Abstracts.IAccountService, BL.AccountService>();
            //data access
            AddEFDependency(dbConnectionString);
            _services.AddScoped<DL.Abstracts.IOrderDL, OrderDL>();

        }
        public async Task EnsureDataBasePresence(IServiceCollection service)
        {
            var context = service.BuildServiceProvider().GetRequiredService<DbContext>();
            //await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }

        private void AddEFDependency(string dbConnectionString)
        {
            _services.AddScoped<DbContext,ECommerceContext>();
            _services.AddDbContext<ECommerceContext>(options =>
            {
                options.UseSqlServer(dbConnectionString);
                options.EnableSensitiveDataLogging();
            });

        }



    }
}
