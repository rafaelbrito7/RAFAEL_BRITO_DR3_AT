using Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Mapping
{
    public class CountryMapping : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.PhotoUrl).IsRequired();

            builder.HasMany(x => x.States)
                .WithOne(x => x.Country)
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.People)
                .WithOne(x => x.Country)
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Navigation(x => x.States)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            builder.Navigation(x => x.People)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
        }
    }
}
