using AutoMapper;
using JinjiProject.BusinessLayer.Constants;
using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Utilities.Results.Concrete;
using JinjiProject.DataAccess.EFCore.Repositories;
using JinjiProject.DataAccess.Interface.Repositories;
using SixLabors.ImageSharp;
using JinjiProject.Dtos.Brands;
using JinjiProject.Dtos.Genres;
using JinjiProject.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JinjiProject.Dtos.Categories;
using SixLabors.ImageSharp.Processing;
using JinjiProject.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace JinjiProject.BusinessLayer.Managers.Concrete
{
    public class GenreManager : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public GenreManager(IGenreRepository genreRepository, IMapper mapper , ICategoryRepository categoryRepository)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task<DataResult<Genre>> CreateGenreAsync(CreateGenreDto createGenreDto)
        {
            if (createGenreDto == null)
            {
                return new ErrorDataResult<Genre>(Messages.CreateGenreError);
            }
            else
            {
                if (createGenreDto.UploadPath != null)
                {
                    using (var image = Image.Load(createGenreDto.UploadPath.OpenReadStream()))
                    {
                        image.Mutate(x => x.Resize(385, 330));
                        image.Mutate(x => x.Brightness(0.6F));
                        Guid guid = Guid.NewGuid();
                        image.Save($"wwwroot/images/genrePhotos/{guid}{Path.GetExtension(createGenreDto.UploadPath.FileName)}");
                        createGenreDto.ImagePath = $"/images/genrePhotos/{guid}{Path.GetExtension(createGenreDto.UploadPath.FileName)}";
                    }                 
                }

                Genre genre = _mapper.Map<Genre>(createGenreDto);
                bool result = await _genreRepository.Create(genre);
                if (result)
                    return new SuccessDataResult<Genre>(genre, Messages.CreateGenreSuccess);
                else
                    return new ErrorDataResult<Genre>(genre, Messages.CreateGenreRepoError);

            }
        }

        public async Task<DataResult<List<ListGenreDto>>> GetAllByExpression(Expression<Func<Genre, bool>> expression)
        {
            var genres = await _genreRepository.GetAllByExpression(expression);
            return new SuccessDataResult<List<ListGenreDto>>(_mapper.Map<List<ListGenreDto>>(genres), Messages.GenreListedSuccess);
        }

        public async Task<DataResult<List<ListGenreDto>>> GetAllGenre()
        {
            var genres = await _genreRepository.GetAllAsync(false);
            return new SuccessDataResult<List<ListGenreDto>>(_mapper.Map<List<ListGenreDto>>(genres), Messages.GenreListedSuccess);
        }

        public async Task<DataResult<GetGenreDto>> GetFilteredGenre(Expression<Func<Genre, bool>> expression)
        {
            var genreDto = await _genreRepository.GetFilteredFirstOrDefault(expression);
            if (genreDto == null)
            {
                return new ErrorDataResult<GetGenreDto>(Messages.GenreFilteredError);
            }
            else
            {
                GetGenreDto getGenreDto = _mapper.Map<GetGenreDto>(genreDto);
                return new SuccessDataResult<GetGenreDto>(getGenreDto, Messages.GenreFilteredSuccess);
            }
        }

        public async Task<DataResult<GetGenreDto>> GetGenreById(int id)
        {
            if (id <= 0)
                return new ErrorDataResult<GetGenreDto>(Messages.GenreNotFound);
            else
            {
                GetGenreDto getGenreDto = _mapper.Map<GetGenreDto>(await _genreRepository.GetByIdAsync(id));
                return new SuccessDataResult<GetGenreDto>(getGenreDto, Messages.GenreFoundSuccess);
            }
        }

        public async Task<DataResult<Genre>> HardDeleteGenreAsync(int id)
        {
            var genreDto = await _genreRepository.GetByIdAsync(id);
            if (genreDto == null)
            {
                return new ErrorDataResult<Genre>(Messages.GenreNotFound);
            }
            else
            {
                bool result = await _genreRepository.HardDelete(genreDto);
                if (result)
                {
                    if (File.Exists($"wwwroot/{genreDto.ImagePath}"))
                    {
                        File.Delete($"wwwroot/{genreDto.ImagePath}");
                    }
                    return new SuccessDataResult<Genre>(Messages.GenreDeletedSuccess);
                }
                else
                    return new ErrorDataResult<Genre>(Messages.GenreDeletedRepoError);
            }
        }

        public async Task<DataResult<Genre>> SoftDeleteGenreAsync(int id)
        {
            var genreDto = await _genreRepository.GetByIdAsync(id);
            if (genreDto == null)
            {
                return new ErrorDataResult<Genre>(Messages.GenreNotFound);
            }
            else
            {
                genreDto.IsOnHomePage = null;
                genreDto.Order = null;
                bool result = await _genreRepository.SoftDelete(genreDto);
                if (result)
                    return new SuccessDataResult<Genre>(Messages.GenreDeletedSuccess);
                else
                    return new ErrorDataResult<Genre>(Messages.GenreDeletedRepoError);
            }
        }

        public async Task<DataResult<Genre>> UpdateGenreAsync(UpdateGenreDto updateGenreDto)
        {
            if (updateGenreDto == null)
            {
                return new ErrorDataResult<Genre>(Messages.UpdateGenreError);
            }
            else
            {
                Genre genre = await _genreRepository.GetByIdAsync(updateGenreDto.Id);
                            
                if (updateGenreDto.UploadPath != null)
                {
                    using (var image = Image.Load(updateGenreDto.UploadPath.OpenReadStream()))
                    {
                        image.Mutate(x => x.Resize(385, 330));
                        image.Mutate(x => x.Brightness(0.6F));
                        Guid guid = Guid.NewGuid();
                        image.Save($"wwwroot/images/genrePhotos/{guid}{Path.GetExtension(updateGenreDto.UploadPath.FileName)}");
                        if (File.Exists($"wwwroot/{updateGenreDto.ImagePath}"))
                        {
                            File.Delete($"wwwroot/{updateGenreDto.ImagePath}");
                        }
                        updateGenreDto.ImagePath = $"/images/genrePhotos/{guid}{Path.GetExtension(updateGenreDto.UploadPath.FileName)}";
                    }                  
                }

                genre = _mapper.Map(updateGenreDto, genre);
                bool result = await _genreRepository.Update(genre);
                if (result)
                    return new SuccessDataResult<Genre>(genre, Messages.UpdateGenreSuccess);
                else
                    return new ErrorDataResult<Genre>(genre, Messages.UpdateGenreRepoError);
            }
        }

        public async Task<DataResult<IEnumerable<Genre>>> UpdateAllGenreAsync(List<UpdateHomePageGenreDto> updateGenreDto)
        {
            if (updateGenreDto == null)
            {
                return new ErrorDataResult<IEnumerable<Genre>>(Messages.UpdateGenreError);
            }

            var updatedGenreIds = updateGenreDto.Select(dto => dto.Id).ToList();
            var genresToUpdate = await _genreRepository.GetAllByExpression(genre => updatedGenreIds.Contains(genre.Id));


            if (genresToUpdate == null || !genresToUpdate.Any())
            {
                return new ErrorDataResult<IEnumerable<Genre>>(Messages.NoGenresToUpdateError);
            }

            foreach (var item in updateGenreDto)
            {
                var genreToUpdate = genresToUpdate.FirstOrDefault(c => c.Id == item.Id);
                if (genreToUpdate != null)
                {
                    _mapper.Map(item, genreToUpdate);
                    bool result = await _genreRepository.Update(genreToUpdate);
                    if (!result)
                    {
                        return new ErrorDataResult<IEnumerable<Genre>>(Messages.UpdateListGenreRepoError);
                    }
                }
            }

            var genresToResetUpdate = await _genreRepository.GetAllByExpression(genre => !updatedGenreIds.Contains(genre.Id));

            foreach (var item in genresToResetUpdate)
            {
                Genre genreToUpdate = new();
                if (genreToUpdate != null)
                {
                    item.Order = null;
                    item.IsOnHomePage = false;
                    _mapper.Map(item, genreToUpdate);
                    bool result = await _genreRepository.Update(genreToUpdate);
                    if (!result)
                    {
                        return new ErrorDataResult<IEnumerable<Genre>>(Messages.UpdateListGenreRepoError);
                    }
                }


            }

            return new SuccessDataResult<IEnumerable<Genre>>(genresToUpdate, Messages.UpdateListGenreSuccess);
        }

        public async Task<DataResult<List<ListGenreDto>>> GetGenreBySearchValues(string? name, string? categoryId, string? createdDate)
        {
            int nullParamCount = new[] { name, categoryId }.Count(param => param != null);
            if (DateTime.Parse(createdDate).Year.ToString() != "1")
            {
                nullParamCount++;
            }

            var genresByName = await _genreRepository.GetAllByExpression(genre => genre.Status != Status.Deleted && genre.Name.Contains(name));
            var genresByCategory = await _genreRepository.GetAllByExpression(genre => genre.Status != Status.Deleted && genre.CategoryId == Convert.ToInt32(categoryId));
            var genresByCreatedYear = await _genreRepository.GetAllByExpression(genre => genre.Status != Status.Deleted && EF.Functions.DateDiffDay(genre.CreatedDate, DateTime.Parse(createdDate)) == 0);

            var filteredGenres = IntersectNonEmpty(nullParamCount, genresByName, genresByCategory, genresByCreatedYear);

            return filteredGenres.Any()
            ? new SuccessDataResult<List<ListGenreDto>>(_mapper.Map<List<ListGenreDto>>(filteredGenres), Messages.GenreListedSuccess)
            : new ErrorDataResult<List<ListGenreDto>>(Messages.GenreNotFound);
        }

        private static IEnumerable<T> IntersectNonEmpty<T>(int _nullParamCount, params IEnumerable<T>[] lists)
        {
            var nonEmptyLists = lists.Where(list => list != null && list.Any()).ToList();

            if (nonEmptyLists.Count == 0)
            {
                return new List<T>();
            }

            IEnumerable<T> result = nonEmptyLists[0];
            if (_nullParamCount == nonEmptyLists.Count)
            {
                for (int i = 1; i < nonEmptyLists.Count; i++)
                {
                    result = result.Intersect(nonEmptyLists[i]).ToList();

                    if (!result.Any())
                    {
                        break;
                    }
                }
                return result;
            }
            else
            {

                return result = new List<T>();
            }
        }
    }
}
