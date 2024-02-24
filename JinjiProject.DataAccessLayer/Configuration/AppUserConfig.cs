using JinjiProject.Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.DataAccessLayer.Configuration
{
    public class AppUserConfig : BaseEntityConfig<AppUser>
    {
        public async override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            base.Configure(builder);
        }

    }
}
