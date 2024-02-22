using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.Core.Entities.Abstract
{
    public interface IBaseUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string ImagePath { get; set; }

        [NotMapped]
        public IFormFile UploadPath { get; set; }
    }
}
