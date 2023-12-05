using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Invoice.RecordModels
{
    public class LoginRecordModel
    {
        [Key]
        [Required]
        public string LoginId { get; set; } = string.Empty;
        [Required]
        public string LoginName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public Guid? Token { get; set; }
        public DateTime? TokenExpiry { get; set; }

    }
}
