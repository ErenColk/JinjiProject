using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.BusinessLayer.Managers.Concrete;
using JinjiProject.DataAccess.Interface.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Extensions
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IAdminService, AdminManager>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IBrandService, BrandManager>();
            services.AddScoped<IMaterialService, MaterialManager>();
            services.AddScoped<IGenreService, GenreManager>();


            return services;
        }
    }
}
