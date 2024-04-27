using ApisFlutter.Models.DTOS;
using ApisFlutter.Models;
using ApisFlutter.Models.DTOS.DTOSUPDATE;

namespace ApisFlutter.Interfaces
{
    public interface ITipoProductoService
    {
        Task<List<TipoProducto>> GetTipoProductosAsync();
        Task<TipoProductoDTO> CreateTipoProductoAsync(TipoProductoDTO tipoProductoDTO);
        Task DeleteTipoProductoAsync(int tipoProductoId);
        Task UpdateTipoProductoAsync(int tipoProductoId, TipoProductoUpdateDTO tipoProductoDTO);

    }
}
