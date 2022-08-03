using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CosturasCrisApi.Models
{
    [Table("Associate")]
    public class Associate
    {
        [Key]
        public int Id { get; set; }
        
        [Column(TypeName = "varchar(25)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string PaternalSurname { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string MaternalSurname { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Adress { get; set; }
        
        [Column(TypeName = "varchar(10)")]
        public string Phone { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "varchar(16)")]
        public string Password { get; set; }
        [Required]
        [Column(TypeName = "varchar(16)")]
        public string UserAccount { get; set; }
    }
}
