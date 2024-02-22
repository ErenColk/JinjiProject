using JinjiProject.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.Core.Entities.Abstract
{
    public interface IBaseEntity : IEntity
    {
        public int Id { get; set; }
       
    }
}
