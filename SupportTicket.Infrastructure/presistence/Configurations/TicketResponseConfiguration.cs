using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SupportTicket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Infrastructure.presistence.Configurations
{
    public class TicketResponseConfiguration : IEntityTypeConfiguration<TicketResponse>
    {
        public void Configure(EntityTypeBuilder<TicketResponse> builder)
        {
            builder.ToTable("TicketResponses");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.ResponseText)
                   .IsRequired();

            builder.Property(r => r.CreatedAt)
                   .IsRequired();

            builder.Property(r => r.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasOne(r => r.Ticket)
                   .WithMany(t => t.Responses)
                   .HasForeignKey(r => r.TicketId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.AdminUser)
                   .WithMany() 
                   .HasForeignKey(r => r.AdminUserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
