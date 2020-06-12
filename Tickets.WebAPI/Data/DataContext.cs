using System.Collections.Immutable;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tickets.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tickets.WebAPI.Data
{
    public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Followup> Followups { get; set; }
        
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ConfigureContactModel(builder);
            ConfigureFollowupModel(builder);
            ConfigureTicketModel(builder);
            ConfigureUserModel(builder);
        }

        private void ConfigureContactModel(ModelBuilder builder)
        {
            builder
                .Entity<Contact>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder
                .Entity<Contact>()
                .Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(255);
        }

        private void ConfigureTicketModel(ModelBuilder builder)
        {
            builder
                .Entity<Ticket>()
                .Property(t => t.CreatedAt)
                .IsRequired()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder
                .Entity<Ticket>()
                .Property(t => t.CompanyId)
                .IsRequired()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder
                .Entity<Ticket>()
                .Property(t => t.AuthorId)
                .IsRequired()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder
                .Entity<Ticket>()
                .Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(150);
            builder
                .Entity<Ticket>()
                .Property(t => t.AuthorId)
                .IsRequired()
                .HasMaxLength(5000);
        }

        private void ConfigureUserModel(ModelBuilder builder)
        {
            builder
                .Entity<User>()
                .Property(u => u.CreatedAt)
                .IsRequired()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder
                .Entity<User>()
                .Property(u => u.CompanyId)
                .IsRequired()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        }
        
        private void ConfigureFollowupModel(ModelBuilder builder)
        {
            builder
               .Entity<Followup>()
               .Property(t => t.CreatedAt)
               .IsRequired()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder
                .Entity<Followup>()
                .Property(t => t.CompanyId)
                .IsRequired()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder
                .Entity<Followup>()
                .Property(t => t.TicketId)
                .IsRequired()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder
                .Entity<Followup>()
                .Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(150);
            builder
                .Entity<Followup>()
                .Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(5000);
            builder
                .Entity<Followup>()
                .Property(t => t.AuthorId)
                .IsRequired()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        }
    }
}