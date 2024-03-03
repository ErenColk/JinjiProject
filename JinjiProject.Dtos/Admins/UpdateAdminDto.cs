using JinjiProject.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.Dtos.Admins
{
    public class UpdateAdminDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? ImagePath { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public IFormFile UploadPath { get; set; }
    }
}
