﻿using JinjiProject.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.Dtos.Genres
{
    public class ListHomePageGenreDto
    {
        public int Id { get; set; }        
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Title { get; set; }
        public bool? IsOnHomePage { get; set; }
        public int? Order { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
