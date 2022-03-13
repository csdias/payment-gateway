using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworksAndDrivers.Database.Attributes;

namespace FrameworksAndDrivers.Database.Models
{
    [Auditable]
    [Table("merchant", Schema = "payment")]
    public class MerchantModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("bank-account-details")]
        public string BankAccountDetails { get; set; }
    }
}
