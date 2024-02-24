using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.Core.Enums
{
    public enum Gender
    {
        [Display(Name = "Erkek")]
        Man = 1,
        [Display(Name = "Kadın")]
        Woman
    }
}
