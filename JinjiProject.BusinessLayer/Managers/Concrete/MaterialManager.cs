using AutoMapper;
using JinjiProject.BusinessLayer.Constants;
using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Utilities.Results.Concrete;
using JinjiProject.DataAccess.EFCore.Repositories;
using JinjiProject.DataAccess.Interface.Repositories;
using JinjiProject.Dtos.Brands;
using JinjiProject.Dtos.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Managers.Concrete
{
    public class MaterialManager : IMaterialService
    {
        private readonly IMaterialRepository materialRepository;
        private readonly IMapper mapper;

        public MaterialManager(IMaterialRepository materialRepository, IMapper mapper)
        {
            this.materialRepository = materialRepository;
            this.mapper = mapper;
        }
        public async Task<DataResult<Material>> CreateMaterialAsync(CreateMaterialDto createMaterialDto)
        {
            if (createMaterialDto == null)
            {
                return new ErrorDataResult<Material>(Messages.CreateBrandError);
            }
            else
            {
                Material material = mapper.Map<Material>(createMaterialDto);
                bool result = await materialRepository.Create(material);
                if (result)
                    return new SuccessDataResult<Material>(material, Messages.CreateBrandSuccess);
                else
                    return new ErrorDataResult<Material>(material, Messages.CreateBrandRepoError);

            }
        }

        public async Task<DataResult<List<ListMaterialDto>>> GetAllMaterial()
        {
            var materials = await materialRepository.GetAllAsync();
            return new SuccessDataResult<List<ListMaterialDto>>(mapper.Map<List<ListMaterialDto>>(materials), Messages.MaterialListedSuccess);
        }

        public async Task<DataResult<GetMaterialDto>> GetFilteredMaterial(Expression<Func<Material, bool>> expression)
        {
            var materialDto = await materialRepository.GetFilteredFirstOrDefault(expression);
            if (materialDto == null)
            {
                return new ErrorDataResult<GetMaterialDto>(Messages.MaterialFilteredError);
            }
            else
            {
                GetMaterialDto getMaterialDto = mapper.Map<GetMaterialDto>(materialDto);
                return new SuccessDataResult<GetMaterialDto>(getMaterialDto, Messages.MaterialFilteredSuccess);
            }
        }

        public async Task<DataResult<GetMaterialDto>> GetMaterialById(int id)
        {

            if (id <= 0)
                return new ErrorDataResult<GetMaterialDto>(Messages.MaterialNotFound);
            else
            {
                GetMaterialDto getMaterialDto = mapper.Map<GetMaterialDto>(await materialRepository.GetByIdAsync(id));
                return new SuccessDataResult<GetMaterialDto>(getMaterialDto, Messages.MaterialFoundSuccess);
            }
        }

        public async Task<DataResult<Material>> HardDeleteMaterialAsync(int id)
        {
            var materialDto = await materialRepository.GetByIdAsync(id);
            if (materialDto == null)
            {
                return new ErrorDataResult<Material>(Messages.MaterialNotFound);
            }
            else
            {
                bool result = await materialRepository.HardDelete(materialDto);
                if (result)
                    return new SuccessDataResult<Material>(Messages.MaterialDeletedSuccess);
                else
                    return new ErrorDataResult<Material>(Messages.MaterialDeletedRepoError);
            }
        }

        public async Task<DataResult<Material>> SoftDeleteMaterialAsync(int id)
        {
            var materialDto = await materialRepository.GetByIdAsync(id);
            if (materialDto == null)
            {
                return new ErrorDataResult<Material>(Messages.MaterialNotFound);
            }
            else
            {
                bool result = await materialRepository.SoftDelete(materialDto);
                if (result)
                    return new SuccessDataResult<Material>(Messages.MaterialDeletedSuccess);
                else
                    return new ErrorDataResult<Material>(Messages.MaterialDeletedRepoError);
            }
        }

        public async Task<DataResult<Material>> UpdateMaterialAsync(UpdateMaterialDto updateMaterialDto)
        {
            if (updateMaterialDto == null)
            {
                return new ErrorDataResult<Material>(Messages.UpdateMaterialError);
            }
            else
            {
                Material material = mapper.Map<Material>(updateMaterialDto);
                bool result = await materialRepository.Update(material);
                if (result)
                    return new SuccessDataResult<Material>(material, Messages.UpdateMaterialSuccess);
                else
                    return new ErrorDataResult<Material>(material, Messages.UpdateMaterialRepoError);
            }
        }
    }
}
