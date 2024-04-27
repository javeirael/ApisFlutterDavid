using ApisFlutter.Context;
using ApisFlutter.Interfaces;
using ApisFlutter.Models;
using ApisFlutter.Models.DTOS;
using ApisFlutter.Models.DTOS.DTOSUPDATE;
using Microsoft.EntityFrameworkCore;

namespace ApisFlutter.Services
{
    //hereda de la interfaz para consumir los servicios 
    public class ProductoService : IProductoService
    {
        // Variable para acceder al contexto de la base de datos
        private readonly ApplicationDbContext _dbContext;
        // Constructor que recibe el contexto de la base de datos como parámetro
        public ProductoService(ApplicationDbContext dbContext)
        {
            // Inicializa el contexto de la base de datos
            _dbContext = dbContext;
        }

        //Trae una lista de todos los productos que sean igual a cero es decir todos aquellos que esta actvos
        public async Task<List<Producto>> GetProductoAsync()
        {
            // Lista para almacenar los productos
            List<Producto> productos = new List<Producto>();
            // Consulta SQL para seleccionar todos los productos que no han sido eliminados
            string query =
                "SELECT * FROM producto WHERE IsDeleted = 0";
            // Ejecutar la consulta SQL, hace a la entidad 
            var result = await _dbContext.producto.FromSqlRaw(query).ToListAsync();
            // Agregar los resultados a la lista de productos
            productos.AddRange(result);
            // Devolver la lista de productos
            return productos;
        }

        public async Task<ProductoDTO> CreateProductoAsync(ProductoDTO productoDTO)
        {
            // Verificar si ya existe un producto con la misma información   
            bool existeProducto = await _dbContext.producto.AnyAsync(p =>
                p.Nombre == productoDTO.Nombre &&
                p.Descripcion == productoDTO.Descripcion &&
                p.Precio == productoDTO.Precio &&
                p.TipoProductoId == productoDTO.TipoProductoId &&
                p.ProveedorId == productoDTO.ProveedorId
                );
            // Si el producto ya existe, lanzar una excepción
            if (existeProducto)
            {
                throw new InvalidOperationException("Ya existe un producto.");
            }

            // Si no existe, crear y guardar el nuevo producto
            Producto producto = new()
            {
                Nombre = productoDTO.Nombre,
                Descripcion = productoDTO.Descripcion,
                Precio = productoDTO.Precio,
                TipoProductoId = productoDTO.TipoProductoId,
                ProveedorId = productoDTO.ProveedorId,
                IsDeleted = productoDTO.IsDeleted
            };
            // Agregar el nuevo producto al contexto de base de datos
            _dbContext.producto.Add(producto);
            // Guardar los cambios en la base de datos
            await _dbContext.SaveChangesAsync();
            // Crear un objeto ProductoDTO con los datos del producto creado
            ProductoDTO result = new()
            {
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                TipoProductoId = producto.TipoProductoId,
                ProveedorId = producto.ProveedorId,
                IsDeleted = producto.IsDeleted
            };
            // Devolver el objeto ProductoDTO creado
            return result;
        }

        public async Task DeleteProductoAsync(int productoId)
        {

            // Buscar el producto en la base de datos por su ID
            var producto = await _dbContext.Set<Producto>().FindAsync(productoId);
            // Verificar si el producto existe
            if (producto == null)
            {
                // Lanzar una excepción si el producto no se encuentra en la base de datos
                throw new InvalidOperationException($"No se encontró un producto con ID: {productoId}");
            }
            // Verificar si el producto ya está marcado como eliminado
            if (producto.IsDeleted == true)
            {
                // Lanzar una excepción si el producto ya está marcado como eliminado
                throw new InvalidOperationException("No se puede eliminado un producto marcado como eliminado.");
            }
            // Marcar el producto como eliminado
            producto.IsDeleted = true;
            // Guardar los cambios en la base de datos
            await _dbContext.SaveChangesAsync();
        }

        //este metodo los datos lo obtiene de un dto que esta en la carpeta dtoUpdate que ya no trae el isDelete para evitar que el usuario lo deshabilite 
        public async Task UpdateProductoAsync(int productoId, ProductoUpdateDTO productoDTO)
        {
            // Buscar el producto en la base de datos por su ID
            var producto = await _dbContext.Set<Producto>().FindAsync(productoId);
            // Verificar si el producto existe
            if (producto == null)
            {
                // Lanzar una excepción si el producto no se encuentra en la base de datos
                throw new InvalidOperationException($"No se encontró un producto con ID: {productoId}");
            }
            // Verificar si el producto está marcado como eliminado
            if (producto.IsDeleted == true) 
            {
                // Lanzar una excepción si el producto está marcado como eliminado
                throw new InvalidOperationException("No se puede actualizar un producto marcado como eliminado.");
            }
            // Actualizar los campos del producto con los datos proporcionados en el DTO
            producto.Nombre = productoDTO.Nombre;
            producto.Descripcion = productoDTO.Descripcion;
            producto.Precio = productoDTO.Precio;
            producto.TipoProductoId = productoDTO.TipoProductoId;
            producto.ProveedorId = productoDTO.ProveedorId;

            // Guardar los cambios en la base de datos
            await _dbContext.SaveChangesAsync();
        }
    }
    
}
