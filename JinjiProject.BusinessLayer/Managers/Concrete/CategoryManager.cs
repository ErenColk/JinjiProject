using AutoMapper;
using Castle.Core.Internal;
using JinjiProject.BusinessLayer.Constants;
using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Utilities.Results.Concrete;
using JinjiProject.DataAccess.EFCore.Repositories;
using JinjiProject.DataAccess.Interface.Repositories;
using JinjiProject.Dtos.Admins;
using JinjiProject.Dtos.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Managers.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryManager(ICategoryRepository CategoryRepository, IMapper mapper)
        {
            _categoryRepository = CategoryRepository;
            _mapper = mapper;

        }

        public async Task<DataResult<Category>> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            if (createCategoryDto == null)
            {
                return new ErrorDataResult<Category>(Messages.CreateCategoryError);
            }
            else
            {
                Category Category = _mapper.Map<Category>(createCategoryDto);
                bool result = await _categoryRepository.Create(Category);
                if (result)
                    return new SuccessDataResult<Category>(Category, Messages.CreateCategorySuccess);
                else
                    return new ErrorDataResult<Category>(Category, Messages.CreateCategoryRepoError);

            }
        }

        public async Task<DataResult<GetCategoryDto>> GetCategoryById(int id)
        {
            if (id <= 0)
                return new ErrorDataResult<GetCategoryDto>(Messages.CategoryNotFound);
            else
            {
                GetCategoryDto getCategoryDto = _mapper.Map<GetCategoryDto>(await _categoryRepository.GetByIdAsync(id));
                if (getCategoryDto == null)
                {
                    return new ErrorDataResult<GetCategoryDto>(Messages.CategoryNotFound);
                }
                return new SuccessDataResult<GetCategoryDto>(getCategoryDto, Messages.CategoryFoundSuccess);
            }


        }

        public async Task<DataResult<List<ListCategoryDto>>> GetAllCategory()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return new SuccessDataResult<List<ListCategoryDto>>(_mapper.Map<List<ListCategoryDto>>(categories), Messages.CategoryListedSuccess);
        }

        public async Task<DataResult<GetCategoryDto>> GetFilteredCategory(Expression<Func<Category, bool>> expression)
        {
            var CategoryDto = await _categoryRepository.GetFilteredFirstOrDefault(expression);
            if (CategoryDto == null)
            {
                return new ErrorDataResult<GetCategoryDto>(Messages.CategoryFilteredError);
            }
            else
            {
                GetCategoryDto getCategoryDto = _mapper.Map<GetCategoryDto>(CategoryDto);
                return new SuccessDataResult<GetCategoryDto>(getCategoryDto, Messages.CategoryFilteredSuccess);
            }
        }

        public async Task<DataResult<Category>> HardDeleteCategoryAsync(int id)
        {
            var CategoryDto = await _categoryRepository.GetByIdAsync(id);
            if (CategoryDto == null)
            {
                return new ErrorDataResult<Category>(Messages.CategoryNotFound);
            }
            else
            {
                bool result = await _categoryRepository.HardDelete(CategoryDto);
                if (result)
                    return new SuccessDataResult<Category>(Messages.CategoryDeletedSuccess);
                else
                    return new ErrorDataResult<Category>(Messages.CategoryDeletedRepoError);
            }
        }

        public async Task<DataResult<Category>> SoftDeleteCategoryAsync(int id)
        {
            var CategoryDto = await _categoryRepository.GetByIdAsync(id);
            if (CategoryDto == null)
            {
                return new ErrorDataResult<Category>(Messages.CategoryNotFound);
            }
            else
            {
                bool result = await _categoryRepository.SoftDelete(CategoryDto);
                if (result)
                    return new SuccessDataResult<Category>(Messages.CategoryDeletedSuccess);
                else
                    return new ErrorDataResult<Category>(Messages.CategoryDeletedRepoError);
            }
        }

        public async Task<DataResult<Category>> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            if (updateCategoryDto == null)
            {
                return new ErrorDataResult<Category>(Messages.UpdateCategoryError);
            }
            else
            {
                Category category = await _categoryRepository.GetByIdAsync(updateCategoryDto.Id);
                _mapper.Map(updateCategoryDto, category);
                bool result = await _categoryRepository.Update(category);
                if (result)
                    return new SuccessDataResult<Category>(category, Messages.UpdateCategorySuccess);
                else
                    return new ErrorDataResult<Category>(category, Messages.UpdateCategoryRepoError);
            }
        }

        public async Task<DataResult<List<ListCategoryDto>>> GetAllByExpression(Expression<Func<Category, bool>> expression)
        {

            var categories = await _categoryRepository.GetAllByExpression(expression);

            if (categories.Count() <= 0 || categories == null)
            {
                return new ErrorDataResult<List<ListCategoryDto>>(Messages.CategoryListedEmpty);

            }
            else
            {
                return new SuccessDataResult<List<ListCategoryDto>>(_mapper.Map<List<ListCategoryDto>>(categories), Messages.CategoryListedSuccess);

            }
        }

        public async Task<DataResult<IEnumerable<Category>>> UpdateAllCategoryAsync(List<UpdateCategoryDto> updateCategoryDto)
        {
            if (updateCategoryDto == null)
            {
                return new ErrorDataResult<IEnumerable<Category>>(Messages.UpdateCategoryError);
            }

            var updatedCategoryIds = updateCategoryDto.Select(dto => dto.Id).ToList();
            var categoriesToUpdate = await _categoryRepository.GetAllByExpression(category => updatedCategoryIds.Contains(category.Id));


            if (categoriesToUpdate == null || !categoriesToUpdate.Any())
            {
                return new ErrorDataResult<IEnumerable<Category>>(Messages.NoCategoriesToUpdateError);
            }

            foreach (var item in updateCategoryDto)
            {
                var categoryToUpdate = categoriesToUpdate.FirstOrDefault(c => c.Id == item.Id);
                if (categoryToUpdate != null)
                {
                    _mapper.Map(item, categoryToUpdate);
                    bool result = await _categoryRepository.Update(categoryToUpdate);
                    if (!result)
                    {
                        return new ErrorDataResult<IEnumerable<Category>>(Messages.UpdateListCategoryRepoError);
                    }
                }
            }


            var categoriesToResetUpdate = await _categoryRepository.GetAllByExpression(category => !updatedCategoryIds.Contains(category.Id));

            foreach(var item in categoriesToResetUpdate)
            {
                Category categoryToUpdate = new();
                if (categoryToUpdate != null)
                {
                    item.Order = null;
                    item.IsOnHomePage = false;
                    _mapper.Map(item, categoryToUpdate);
                    bool result = await _categoryRepository.Update(categoryToUpdate);
                    if (!result)
                    {
                        return new ErrorDataResult<IEnumerable<Category>>(Messages.UpdateListCategoryRepoError);
                    }
                }


            }

            return new SuccessDataResult<IEnumerable<Category>>(categoriesToUpdate, Messages.UpdateListCategorySuccess);
        }






    }
}
