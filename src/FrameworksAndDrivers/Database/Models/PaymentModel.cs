using System;
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
        [Column("merchant_id")]
        public long MerchantId { get; set; }

        public MerchantModel Merchant { get; set; }

        [Required]
        [MaxLength(16)]
        [Column("card_id")]
        public long CreditCardId { get; set; }

        public CreditCardModel CreditCard { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("sale_description")]
        public string SaleDescription { get; set; }

        [Required]
        [Column("amount")]
        public decimal Amount { get; set; } = 0;

        [Required]
        [MaxLength(3)]
        [Column("currency")]
        public string Currency { get; set; }

        [Required]
        [Column("status_id")]
        public short StatusId { get; set; }

        public PaymentStatusModel Status { get; set; }
    }
}
