using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupportTicket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Infrastructure.presistence.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(t => t.Description)
                   .IsRequired();

            builder.Property(t => t.Status)
                   .IsRequired();

            builder.Property(t => t.CreatedAt)
                   .IsRequired();

            builder.Property(t => t.UpdatedAt)
                   .IsRequired();


            builder.HasOne(t => t.CreatedByUser)
               .WithMany(u => u.CreatedTickets)
               .HasForeignKey(t => t.CreatedByUserId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t=>t.AssignedByUser)
                .WithMany(u=>u.AssignedTickets)
                .HasForeignKey(t=>t.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.Responses)
                .WithOne(r => r.Ticket)
                .HasForeignKey(r => r.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
