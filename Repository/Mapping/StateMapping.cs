using Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Repository.Mapping
{
    public class StateMapping : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.PhotoUrl).IsRequired();

            builder.HasOne(x => x.Country)
                .WithMany(x => x.States)
                .HasForeignKey(s => s.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.People)
                .WithOne(x => x.State)
                .HasForeignKey(x => x.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Navigation(x => x.People)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
        }
    }
}
