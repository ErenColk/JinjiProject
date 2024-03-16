using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.Core.Entities.Concrete
{
    public class Subscriber : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
