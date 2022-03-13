using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworksAndDrivers.Database.Attributes;

namespace FrameworksAndDrivers.Database.Models
{
    [Auditable]
    [Table("credit-card", Schema = "payment")]
    public class CreditCardModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [MaxLength(16)]
        [Column("number")]
        public string Number { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("holder_name")]
        public string HolderName { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("holder_address")]
        public string HolderAddress { get; set; }

        [Required]
        [StringLength(2)]
        [Column("expiration_month")]
        public string ExpirationMonth { get; set; }

        [Required]
        [StringLength(4)]
        [Column("expiration_year")]
        public string ExpirationYear { get; set; }

        [Required]
        [StringLength(3)]
        [Column("cvv")]
        public string Cvv { get; set; }

        [Column("status_id")]
        public short StatusId { get; set; }

        public CreditStatusModel Status { get; set; }
    }
}
