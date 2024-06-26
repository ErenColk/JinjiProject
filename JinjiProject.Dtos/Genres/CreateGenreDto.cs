﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.Dtos.Genres
{
    public class CreateGenreDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Title { get; set; }
        public string ImagePath { get; set; }
        public IFormFile UploadPath { get; set; }
        public string CategoryId { get; set; }
    }
}
