using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.Dtos.SendMails
{
    public class RenewPasswordDto
    {
        public string Email { get; set; }
        public string Link { get; set; }
    }
}
