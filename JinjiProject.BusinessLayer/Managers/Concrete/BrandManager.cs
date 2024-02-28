using AutoMapper;
using JinjiProject.BusinessLayer.Constants;
using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Utilities.Results.Concrete;
using JinjiProject.DataAccess.EFCore.Repositories;
using JinjiProject.DataAccess.Interface.Repositories;
using JinjiProject.Dtos.Admins;
using JinjiProject.Dtos.Brands;
using JinjiProject.Dtos.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Managers.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandRepository brandRepository;
        private readonly IMapper mapper;

        public BrandManager(IBrandRepository brandRepository,IMapper mapper)
        {
            this.brandRepository = brandRepository;
            this.mapper = mapper;
        }
        public async Task<DataResult<Brand>> CreateBrandAsync(CreateBrandDto createBrandDto)
        {
            if (createBrandDto == null)
            {
                return new ErrorDataResult<Brand>(Messages.CreateBrandError);
            }
            else
            {
                Brand brand = mapper.Map<Brand>(createBrandDto);
                bool result = await brandRepository.Create(brand);
                if (result)
                    return new SuccessDataResult<Brand>(brand, Messages.CreateBrandSuccess);
                else
                    return new ErrorDataResult<Brand>(brand, Messages.CreateBrandRepoError);

            }
        }

        public async Task<DataResult<List<ListBrandDto>>> GetAllBrand()
        {
            var brands = await brandRepository.GetAllAsync();
            return new SuccessDataResult<List<ListBrandDto>>(mapper.Map<List<ListBrandDto>>(brands), Messages.BrandListedSuccess);
        }

        public async Task<DataResult<GetBrandDto>> GetBrandById(int id)
        {
            if (id <= 0)
                return new ErrorDataResult<GetBrandDto>(Messages.BrandNotFound);
            else
            {
                GetBrandDto getBrandDto = mapper.Map<GetBrandDto>(await brandRepository.GetByIdAsync(id));
                return new SuccessDataResult<GetBrandDto>(getBrandDto, Messages.BrandFoundSuccess);
            }
        }

        public async Task<DataResult<GetBrandDto>> GetFilteredBrand(Expression<Func<Brand, bool>> expression)
        {
            var brandDto = await brandRepository.GetFilteredFirstOrDefault(expression);
            if (brandDto == null)
            {
                return new ErrorDataResult<GetBrandDto>(Messages.AdminFilteredError);
            }
            else
            {
                GetBrandDto getBrandDto = mapper.Map<GetBrandDto>(brandDto);
                return new SuccessDataResult<GetBrandDto>(getBrandDto, Messages.AdminFilteredSuccess);
            }
        }

        public async Task<DataResult<Brand>> HardDeleteBrandAsync(int id)
        {
            var brandDto = await brandRepository.GetByIdAsync(id);
            if (brandDto == null)
            {
                return new ErrorDataResult<Brand>(Messages.BrandNotFound);
            }
            else
            {
                bool result = await brandRepository.HardDelete(brandDto);
                if (result)
                    return new SuccessDataResult<Brand>(Messages.BrandDeletedSuccess);
                else
                    return new ErrorDataResult<Brand>(Messages.BrandDeletedRepoError);
            }
        }

        public async Task<DataResult<Brand>> SoftDeleteBrandAsync(int id)
        {
            var brandDto = await brandRepository.GetByIdAsync(id);
            if (brandDto == null)
            {
                return new ErrorDataResult<Brand>(Messages.BrandNotFound);
            }
            else
            {
                bool result = await brandRepository.SoftDelete(brandDto);
                if (result)
                    return new SuccessDataResult<Brand>(Messages.BrandDeletedSuccess);
                else
                    return new ErrorDataResult<Brand>(Messages.BrandDeletedRepoError);
            }
        }

        public async Task<DataResult<Brand>> UpdateBrandAsync(UpdateBrandDto updateBrandDto)
        {
            if (updateBrandDto == null)
            {
                return new ErrorDataResult<Brand>(Messages.UpdateBrandError);
            }
            else
            {
                Brand brand = await brandRepository.GetByIdAsync(updateBrandDto.Id);
                mapper.Map(updateBrandDto, brand);
                bool result = await brandRepository.Update(brand);
                if (result)
                    return new SuccessDataResult<Brand>(brand, Messages.UpdateBrandSuccess);
                else
                    return new ErrorDataResult<Brand>(brand, Messages.UpdateBrandRepoError);
            }
        }

		public async Task<DataResult<List<ListBrandDto>>> GetAllByExpression(Expression<Func<Brand, bool>> expression)
		{
			var brands = await brandRepository.GetAllByExpression(expression);
			return new SuccessDataResult<List<ListBrandDto>>(mapper.Map<List<ListBrandDto>>(brands), Messages.BrandListedSuccess);
		}

        
    }
}
