using ApisFlutter.Models.DTOS;
using ApisFlutter.Models;
using ApisFlutter.Models.DTOS.DTOSUPDATE;

namespace ApisFlutter.Interfaces
{
    // Interfaz que define los servicios disponibles para la gestión de productos.estos Interfaces de servicios son los que hereda en servicios
    public interface IProductoService
    {
        // Obtiene una lista de todos los productos disponibles.
        //Una tarea que representa la operación asincrónica.La tarea devuelve una lista de productos.
        Task<List<Producto>> GetProductoAsync();
        // Crea un nuevo producto con la información proporcionada.Una tarea que representa la operación asincrónica.
        // La tarea devuelve el producto creado.
        Task<ProductoDTO> CreateProductoAsync(ProductoDTO productoDTO);
        // Elimina un producto con el ID especificado.Una tarea que representa la operación asincrónica.
        Task DeleteProductoAsync(int productoId);
        //Actualiza la información de un producto existente con el ID especificado.
       Task UpdateProductoAsync(int productoId, ProductoUpdateDTO productoDTO);
    }
}
