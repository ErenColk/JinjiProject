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

namespace JinjiProject.BusinessLayer.Managers.Concrete
{
    public class GenreManager : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreManager(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
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
            var genres = await _genreRepository.GetAllAsync();
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
                        Guid guid = Guid.NewGuid();
                        image.Save($"wwwroot/images/genrePhotos/{guid}{Path.GetExtension(updateGenreDto.UploadPath.FileName)}");
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
    }
}
