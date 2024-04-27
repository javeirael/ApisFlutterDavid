using System.ComponentModel.DataAnnotations;

namespace ApisFlutter.Models.DTOS
{
    public class ProductoDTO
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public int TipoProductoId { get; set; }
        [Required]
        public int ProveedorId { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    }
}
