using Microsoft.EntityFrameworkCore;
using SupportTicket.Domain.Entities;
using SupportTicket.Infrastructure.presistence.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Infrastructure.presistence.DbContexts
{
    public class SupportTicketDbContext : DbContext
    {
        public SupportTicketDbContext(DbContextOptions<SupportTicketDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketResponse> TicketResponses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SupportTicketDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
