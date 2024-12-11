using ASAPTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Infrastructure.Configurations
{
    public class InvoiceConfigurations : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {

                builder.Property(e => e.InvoiceNumber)
                    .HasMaxLength(20)
                    .IsRequired()
                    .HasDefaultValueSql("(FORMAT(NEXT VALUE FOR InvoiceNumberSequence, '000000'))"); // Numeric string format

                builder.HasIndex(e => e.InvoiceNumber)
                    .IsUnique(); // Ensure uniqueness

        }
    }
}
