using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CosturasCrisApi.Models
{
    [Table("Product_Service")]
    public class ProductService
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Description { get; set; }
        [Required]
        public decimal SuggestedPrice { get; set; }
        [Required]
        public int AssociateId { get; set; }
    }
}
