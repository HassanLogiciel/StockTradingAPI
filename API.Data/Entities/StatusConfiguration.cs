using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Data.Entities
{
    class StatusConfiguration : IEntityTypeConfiguration<StatusDb>
    {
        public void Configure(EntityTypeBuilder<StatusDb> builder)
        {
            builder.ToTable("Status");
        }
    }
}
