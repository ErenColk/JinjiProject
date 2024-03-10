using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Utilities.Results.Concrete;
using JinjiProject.Dtos.Brands;
using JinjiProject.Dtos.Genres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Managers.Abstract
{
    public interface IGenreService
    {
        public Task<DataResult<Genre>> CreateGenreAsync(CreateGenreDto createGenreDto);
        public Task<DataResult<Genre>> UpdateGenreAsync(UpdateGenreDto updateGenreDto);
        public Task<DataResult<Genre>> SoftDeleteGenreAsync(int id);
        public Task<DataResult<List<ListGenreDto>>> GetAllByExpression(Expression<Func<Genre, bool>> expression);
        public Task<DataResult<Genre>> HardDeleteGenreAsync(int id);
        public Task<DataResult<List<ListGenreDto>>> GetAllGenre();
        public Task<DataResult<GetGenreDto>> GetGenreById(int id);
        public Task<DataResult<GetGenreDto>> GetFilteredGenre(Expression<Func<Genre, bool>> expression);
    }
}
