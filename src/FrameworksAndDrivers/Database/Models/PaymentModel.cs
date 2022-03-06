using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworksAndDrivers.Database.Attributes;

namespace FrameworksAndDrivers.Database.Models
{
    [Auditable]
    [Table("payment", Schema = "payment")]
    public class PaymentModel
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("client_id")]
        public string ClientId { get; set; }

        [Required]
        [Column("credit_card", TypeName = "jsonb")]
        public Dictionary<string, object> CreditCard { get; set; }

        [Required]
        [Column("ammount")]
        public decimal Ammount { get; set; } = 0;

        [Required]
        [MaxLength(255)]
        [Column("status_id")]
        public short StatusId { get; set; }
        //TODO: set the fk

        //[Required]
        //[MaxLength(8)]
        //[ForeignKey("PaymentStatus")]
        //[Column("payment-status-id")]
        //public PaymentStatusModel Status { get; set; }
    }
}
