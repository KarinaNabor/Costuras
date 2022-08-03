using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CosturasCrisApi.Models
{
    [Table("Customers")]
    public class Customers
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(25)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string PaternalSurname { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string MaternalSurname { get; set; }
        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Phone { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }
        [Required]
        public int AssociateId { get; set; }
    }
}
