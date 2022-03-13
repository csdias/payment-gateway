using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworksAndDrivers.Database.Attributes;

namespace FrameworksAndDrivers.Database.Models
{
    [Auditable]
    [Table("credit-card-status", Schema = "payment")]
    public class CreditStatusModel
    {
        [Key]
        [Required]
        [Column("id")]
        public short Id { get; set; }

        [Required]
        [Column("status")]
        public string Status { get; set; } 
    }
}
