using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiServices.Domain.Entities.OrderSchema;

namespace AiServices.Infrastructure.DatabaseContext.ApplicationDbs.Configurations.OrderSchema
{
    public class PurchaseOrderSnapshotConfiguration : IEntityTypeConfiguration<PurchaseOrderSnapshot>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderSnapshot> entity)
        {
            entity.HasKey(e => e.PurchaseOrderSnapshotId)
                  .HasName("PK__PurchaseOrderSnapshot__5026B698A94EFE60");

            entity.ToTable("PurchaseOrderSnapshot", "Orders");

            entity.Property(e => e.PurchaseOrderSnapshotId)
                  .HasDefaultValueSql("gen_random_uuid()")
                  .HasColumnName("PurchaseOrderSnapshotId");

            entity.Property(e => e.TotalPrice).HasPrecision(18, 2);
        }
    }
}
