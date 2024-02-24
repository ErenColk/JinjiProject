using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Utilities.Results.Concrete;
using JinjiProject.Dtos.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Managers.Abstract
{
    public interface IMaterialService
    {
        public Task<DataResult<Material>> CreateMaterialAsync(CreateMaterialDto createMaterialDto);
        public Task<DataResult<Material>> UpdateMaterialAsync(UpdateMaterialDto updateMaterialDto);
        public Task<DataResult<Material>> SoftDeleteMaterialAsync(int id);
        public Task<DataResult<Material>> HardDeleteMaterialAsync(int id);
        public Task<DataResult<List<ListMaterialDto>>> GetAllMaterial();
        public Task<DataResult<GetMaterialDto>> GetMaterialById(int id);
        public Task<DataResult<GetMaterialDto>> GetFilteredMaterial(Expression<Func<Material, bool>> expression);
    }
}
