using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApisFlutter.Models
{
    [Table("Producto")]
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        [ForeignKey("TipoProductoId")]
        public int TipoProductoId { get; set; }
        [Required]
        [ForeignKey("ProveedorId")]
        public int ProveedorId { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

    }
}
