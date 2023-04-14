using Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Repository.Mapping
{
    public class FriendshipMap : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.APersonId).IsRequired();
            builder.Property(x => x.BPersonId).IsRequired();

            builder.HasOne(x => x.APerson)
            .WithMany(x => x.FriendshipsA)
            .HasForeignKey(x => x.APersonId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.BPerson)
            .WithMany(x => x.FriendshipsB)
            .HasForeignKey(x => x.BPersonId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
