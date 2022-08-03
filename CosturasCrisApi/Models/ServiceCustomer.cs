using CosturasCrisApi.Models.Enum;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CosturasCrisApi.Models
{
    [Table("Service_Customer")]
    public class ServiceCustomer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int ProductServiceId { get; set; }
        [Required]
        public decimal RealPrice { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
        [Required]
        [Column(TypeName = "varchar(15)")]
        [DefaultValue(StatusServiceCustomer.P)]
        public StatusServiceCustomer Status { get; set; }
        [Required]
        [Column(TypeName = "varchar(150)")]
        public string DescriptionService { get; set; }

        [Column(TypeName = "varchar(100)")]
        public StatusPaid StatusPaid { get; set; }
        
        [Column(TypeName = "varchar(150)")]
        public string CodeClothing { get; set; }
    }
}
