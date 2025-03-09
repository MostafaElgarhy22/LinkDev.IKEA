using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.IKEA.DAL.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.IKEA.DAL.Persistence.Data.Configurations.Common
{
    internal class BaseEntityConfigurations<Tkey, TEntity> : IEntityTypeConfiguration<TEntity>
        where Tkey : IEquatable<Tkey>
        where TEntity : BaseEntity<Tkey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(E => E.Id);
        }
    }
}
