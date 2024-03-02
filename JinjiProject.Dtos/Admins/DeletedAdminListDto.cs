using JinjiProject.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.Dtos.Admins
{
    public class DeletedAdminListDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string ImagePath { get; set; }
        public string Email { get; set; }
        public DateTime DeletedDate { get; set; }
        public Status Status { get; set; }
    }
}
