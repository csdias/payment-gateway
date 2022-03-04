using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworksAndDrivers.Database.Attributes;

namespace FrameworksAndDrivers.Database.Models
{
    [Auditable]
    [Table("payment-status", Schema = "payment")]
    public class PaymentStatusModel
    {
        [Key]
        [Required]
        [Column("id")]
        public short Id { get; set; }

        [Required]
        [MaxLength(2)]
        [Column("status")]
        public string Status { get; set; } 
    }
}
