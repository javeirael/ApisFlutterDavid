using ApisFlutter.Models.DTOS;
using ApisFlutter.Models;
using ApisFlutter.Models.DTOS.DTOSUPDATE;
using Microsoft.VisualBasic;

namespace ApisFlutter.Interfaces
{
    // Interfaz para definir servicios relacionados con proveedores.estos Interfaces de servicios son los que hereda en servicios

    public interface IProveedorService
    {
        // Obtiene una lista de proveedores.
        Task<List<Proveedor>> GetProveedorAsync();
        // Crea un nuevo proveedor con la información proporcionada.Usando un DTO
        Task<ProveedorDTO> CreateProveedorAsync(ProveedorDTO proveedorDTO);
        // Elimina un proveedor con el ID especificado.
        Task DeleteProveedorAsync(int ProveedorId);
        // Actualiza la información de un proveedor existente con el ID especificado.
        Task UpdateProveedorAsync(int ProveedorId, ProveedorUpdateDTO proveedorDTO);
    }
}
