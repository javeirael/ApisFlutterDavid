using System.ComponentModel.DataAnnotations;

namespace ApisFlutter.Models.DTOS.DTOSUPDATE
{
    //este dto sirve solo para la parte de la actualizacion a si ya no pedira el isDelete
    public class TipoProductoUpdateDTO
    {
        [Required]
        public string Nombre { get; set; }
    }
}
