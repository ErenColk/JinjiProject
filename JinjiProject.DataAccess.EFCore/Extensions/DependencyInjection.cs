using JinjiProject.DataAccess.EFCore.Repositories;
using JinjiProject.DataAccess.EFCore.Seeds;
using JinjiProject.DataAccess.Interface.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.DataAccess.EFCore.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEFCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            AdminSeed.SeedAsync(configuration).GetAwaiter().GetResult();

            return services;
        }
    }
}
