﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.IKEA.DAL.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.IKEA.DAL.Persistence.Data.Configurations.Common
{
    internal class BaseAuditableEntityConfiguration <Tkey, TEntity> : BaseEntityConfigurations<Tkey, TEntity>
        where Tkey : IEquatable<Tkey>
        where TEntity : BaseAuditableEntity<Tkey>
    {

        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            builder.Property(E => E.CreatedBy).HasColumnType("varchar(50)");
            builder.Property(E => E.LastModifiedBy).HasColumnType("varchar(50)");
            builder.Property(E => E.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(E => E.LastModifiedOn).HasComputedColumnSql("GETUTCDATE()");

        }
    }
}
