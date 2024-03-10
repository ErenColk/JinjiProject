using JinjiProject.Core.Entities.Concrete;
using JinjiProject.VMs.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.DataAccess.Interface.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {

        Task<List<Category>> GetListCategoryIncludeOrderBy(Expression<Func<Category, bool>> expression); 

    }
}
