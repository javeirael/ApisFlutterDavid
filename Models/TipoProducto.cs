using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApisFlutter.Models
{
    [Table("TipoProducto")]
    public class TipoProducto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    }

}
