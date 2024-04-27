using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApisFlutter.Models
{

    [Table("Proveedor")]
    public class Proveedor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Contacto { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    }
}
