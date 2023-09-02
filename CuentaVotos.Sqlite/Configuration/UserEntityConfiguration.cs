using CuentaVotos.Entiies.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Sqlite.Configuration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x=>x.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(x=>x.Email).IsUnique();

            builder.Property(x=>x.FirstName)
                .HasMaxLength(50)
                .IsRequired();


            builder.Property(x => x.LastName)
                .HasMaxLength(50)
                .IsRequired();


            builder.Property(x => x.PasswordHash)
                .HasMaxLength(150)
                .IsRequired();


            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(50);

            builder.Property(x => x.Codigo)
                .IsRequired()
                .ValueGeneratedOnAdd();


        }
    }
}
