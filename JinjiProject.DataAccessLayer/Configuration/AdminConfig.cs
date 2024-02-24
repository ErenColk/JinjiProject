using JinjiProject.Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.DataAccessLayer.Configuration
{
    public class AdminConfig : BaseEntityConfig<Admin>
    {
        public override void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasOne(x => x.AppUser).WithOne(x => x.Admin).HasForeignKey<Admin>(x => x.AppUserId);
            base.Configure(builder);
        }
    }
}
