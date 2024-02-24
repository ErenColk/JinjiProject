using JinjiProject.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.Dtos.Admins
{
    public class CreateAdminDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string ImagePath { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public IFormFile UploadPath { get; set; }
        public string AppUserId { get; set; }
    }
}
