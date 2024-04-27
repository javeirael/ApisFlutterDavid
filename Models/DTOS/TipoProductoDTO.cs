using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApisFlutter.Models.DTOS
{
    public class TipoProductoDTO
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    }
}
