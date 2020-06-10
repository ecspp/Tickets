using System.Collections.Immutable;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tickets.Domain;
using Microsoft.AspNetCore.Identity;

namespace Tickets.WebAPI.Data
{
    public class DataContext : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Followup> Followups { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}