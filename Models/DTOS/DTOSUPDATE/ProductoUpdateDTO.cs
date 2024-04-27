using System.ComponentModel.DataAnnotations;

namespace ApisFlutter.Models.DTOS.DTOSUPDATE
{
    //este dto sirve solo para la parte de la actualizacion a si ya no pedira el isDelete
    public class ProductoUpdateDTO
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
    }
}
