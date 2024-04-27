using System.ComponentModel.DataAnnotations;

namespace ApisFlutter.Models.DTOS.DTOSUPDATE
{
    //este dto sirve solo para la parte de la actualizacion a si ya no pedira el isDelete
    public class ProveedorUpdateDTO
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Contacto { get; set; }
    }
}
