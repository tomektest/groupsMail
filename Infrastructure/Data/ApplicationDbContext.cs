using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*modelBuilder.Entity<GroupType>()
              .HasKey(m => m.Id);

            modelBuilder.Entity<MailsType>()
             .HasKey(m => m.Id);
            */

            modelBuilder.Entity<MailsType>(ConfigureMail);
            modelBuilder.Entity<GroupType>(ConfigureGroup);
            modelBuilder.Entity<ApplicationUser>(ConfigureApplicationUser);
        }

        public DbSet<GroupType> Groups { get; set; }

        public DbSet<MailsType> Mails { get; set; }

        private void ConfigureApplicationUser(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("AspNetUsers");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd().IsRequired();
        }

        private void ConfigureGroup(EntityTypeBuilder<GroupType> builder)
        {
            builder.ToTable("Groups");
            builder.HasKey(a => a.Id);
        }

        private void ConfigureMail(EntityTypeBuilder<MailsType> builder)
        {
            builder.ToTable("Mails");
            builder.HasKey(c => c.Id);
        }
    }
}
