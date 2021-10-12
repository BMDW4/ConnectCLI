using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WmdaConnect.Models
{
    public class Registry
    {
        [Key]
        public int RegistryId { get; set; }

        [Column(TypeName = "varchar(256)")]
        public string RegistryName { get; set; }

        [Column(TypeName = "varchar(256)")]
        public string ClientId { get; set; }

        [Column(TypeName = "varchar(256)")]
        public string DisplayName { get; set; }

        [Column(TypeName = "varchar(256)")]
        public string QueueName { get; set; }

        public override string ToString()
        {
            return $" Registry: {RegistryName} ";
        }
    }
}
