using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.Dtos.Genres
{
    public class UpdateGenreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Title { get; set; }
        public string ImagePath { get; set; }
        public IFormFile UploadPath { get; set; }
        public string CategoryId { get; set; }
        public bool? IsOnHomePage { get; set; }
        public int? Order { get; set; }
    }
}
