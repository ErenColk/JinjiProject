using JinjiProject.Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.DataAccessLayer.Configuration
{
    public abstract class BaseEntityConfig<T> : IEntityTypeConfiguration<T> where T : class, IEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.CreatedDate).IsRequired(true);
            builder.Property(x => x.ModifiedDate).IsRequired(false);
            builder.Property(x => x.DeletedDate).IsRequired(false);
            builder.Property(x => x.Status).IsRequired(true);
        }
    }
}
