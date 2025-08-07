using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.AdminControl.DTOs
{
    public class UserDto
    {
        public string? Name {  get; set; }
        public string? Email { get; set; }
        public string? Role {  get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? IsActive { get; set; } 
    }
}
