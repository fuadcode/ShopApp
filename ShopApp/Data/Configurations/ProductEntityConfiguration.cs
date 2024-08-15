﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApp.Entities;

namespace ShopApp.Data.Configurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired(true).HasMaxLength(50);
            builder.Property(p => p.SalePrice).IsRequired(true).HasColumnType("decimal(18,2)");
            builder.Property(p => p.CostPrice).IsRequired(true).HasColumnType("decimal(18,2)");
            builder.Property(p => p.isDelete).HasDefaultValue(false);
            builder.Property(p => p.CreatedDate).HasDefaultValueSql("getdate()");
            builder.Property(p => p.UpdateDate).HasDefaultValueSql("getdate()");

        }
    }
}