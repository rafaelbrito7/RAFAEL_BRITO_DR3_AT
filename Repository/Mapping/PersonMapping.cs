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
    public class PersonMapping : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Birthday).IsRequired();
            builder.Property(x => x.PhotoUrl).IsRequired();
            builder.Property(x => x.CountryId).IsRequired();
            builder.Property(x => x.StateId).IsRequired();

            builder.HasOne(x => x.State).WithMany(x => x.People).HasForeignKey(x => x.StateId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Country).WithMany(x => x.People).HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.Restrict);
            // Excluir países: Não permitir ou excluir todas as pessoas antes
            // Verificação de amizade dupla ou amizade consigo mesmo
            // 

            builder.HasMany(x => x.FriendshipsA).WithOne(x => x.APerson).HasForeignKey(x => x.APersonId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.FriendshipsB).WithOne(x => x.BPerson).HasForeignKey(x => x.BPersonId).OnDelete(DeleteBehavior.Restrict);
            // Verificar se devo colocar numero de países e pessoas na pagina inicial
        }
    }
}
