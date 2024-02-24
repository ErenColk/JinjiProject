using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Utilities.Results.Concrete;
using JinjiProject.Dtos.Brands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Managers.Abstract
{
    public interface IBrandService
    {
        public Task<DataResult<Brand>> CreateBrandAsync(CreateBrandDto createBrandDto);
        public Task<DataResult<Brand>> UpdateBrandAsync(UpdateBrandDto updateBrandDto);
        public Task<DataResult<Brand>> SoftDeleteBrandAsync(int id);
        public Task<DataResult<Brand>> HardDeleteBrandAsync(int id);
        public Task<DataResult<List<ListBrandDto>>> GetAllBrand();
        public Task<DataResult<GetBrandDto>> GetBrandById(int id);
        public Task<DataResult<GetBrandDto>> GetFilteredBrand(Expression<Func<Brand, bool>> expression);
    }
}
