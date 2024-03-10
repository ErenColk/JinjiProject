using JinjiProject.Core.Entities.Concrete;
using JinjiProject.DataAccess.Interface.Repositories;
using JinjiProject.DataAccessLayer.Context;
using JinjiProject.VMs.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.DataAccess.EFCore.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Category>> GetListCategoryIncludeOrderBy(Expression<Func<Category, bool>> expression)
        {
            IQueryable<Category> categories = _appDbContext.Categories.AsQueryable();
            categories = categories.Include(category => category.Genres).ThenInclude(genre => genre.Products);

            var orderedCategories = categories.Where(expression).OrderBy(category => category.Order);

            return await orderedCategories.ToListAsync();
        }

    }
}
