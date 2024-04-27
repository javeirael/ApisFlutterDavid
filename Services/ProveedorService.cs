using ApisFlutter.Context;
using ApisFlutter.Interfaces;
using ApisFlutter.Models;
using ApisFlutter.Models.DTOS;
using ApisFlutter.Models.DTOS.DTOSUPDATE;
using Microsoft.EntityFrameworkCore;

namespace ApisFlutter.Services
{
    //hereda de la interfaz para consumir los servicios 
    public class ProveedorService : IProveedorService
    {
        // Variable para acceder al contexto de la base de datos
        private readonly ApplicationDbContext _dbContext;
        // Constructor que recibe el contexto de la base de datos como parámetro
        public ProveedorService(ApplicationDbContext dbContext)
        {
            // Inicializa el contexto de la base de datos
            _dbContext = dbContext; 
        }

        public async Task<ProveedorDTO> CreateProveedorAsync(ProveedorDTO proveedorDTO)
        {
            // Verificar si ya existe un Proveedor con la misma información   
            bool existeProveedor = await _dbContext.proveedor.AnyAsync(p =>
             p.Nombre == proveedorDTO.Nombre &&
             p.Contacto == proveedorDTO.Contacto
             );
            // Si el Proveedor ya existe, lanzar una excepción
            if (existeProveedor)
            {
                throw new InvalidOperationException("Ya existe un Proveedor.");
            }
            // Si no existe, crear y guardar el nuevo Proveedor
            Proveedor proveedor = new()
            {
                Nombre = proveedorDTO.Nombre,
                Contacto = proveedorDTO.Contacto,
                IsDeleted = proveedorDTO.IsDeleted
            };
            // Agregar el nuevo Proveedor al contexto de base de datos
            _dbContext.proveedor.Add(proveedor);
            // Guardar los cambios en la base de datos
            await _dbContext.SaveChangesAsync();
            // Crear un objeto ProveedorDTO con los datos del Proveedor creado
            ProveedorDTO result = new()
            {
                Nombre = proveedor.Nombre,
                Contacto = proveedor.Contacto,
                IsDeleted = proveedor.IsDeleted
            };
            // Devolver el objeto ProveedorDTO creado
            return result;
        }
        //Trae una lista de todos los Proveedores que sean igual a cero es decir todos aquellos que esta actvos
        public async Task<List<Proveedor>> GetProveedorAsync()
        {
            // Lista para almacenar los Proveedores
            List<Proveedor> Proveedores = new List<Proveedor>();
            // Consulta SQL para seleccionar todos los Proveedores que no han sido eliminados
            string query =
                "SELECT * FROM proveedor WHERE IsDeleted = 0";
            // Ejecutar la consulta SQL, hace a la entidad 
            var result = await _dbContext.proveedor.FromSqlRaw(query).ToListAsync();
            // Agregar los resultados a la lista de Proveedores
            Proveedores.AddRange(result);
            // Devolver la lista de Proveedores
            return Proveedores;
        }

        public async Task DeleteProveedorAsync(int ProveedorId)
        {
            // Buscar el Proveedor en la base de datos por su ID
            var Proveedor = await _dbContext.Set<Proveedor>().FindAsync(ProveedorId);
            // Verificar si el Proveedor existe
            if (Proveedor == null)
            {
                // Lanzar una excepción si el Proveedor no se encuentra en la base de datos
                throw new InvalidOperationException($"No se encontró un Proveedor con ID: {ProveedorId}");
            }
            // Verificar si el Proveedor ya está marcado como eliminado
            if (Proveedor.IsDeleted == true)
            {
                // Lanzar una excepción si el Proveedor ya está marcado como eliminado
                throw new InvalidOperationException("No se puede eliminado un Proveedor marcado como eliminado.");
            }
            // Marcar el Proveedor como eliminado
            Proveedor.IsDeleted = true;
            // Guardar los cambios en la base de datos
            await _dbContext.SaveChangesAsync();
        }
        //este metodo los datos lo obtiene de un dto que esta en la carpeta dtoUpdate que ya no trae el isDelete para evitar que el usuario lo deshabilite 
        public async Task UpdateProveedorAsync(int ProveedorId, ProveedorUpdateDTO proveedorDTO)
        {
            // Buscar el Proveedor en la base de datos por su ID
            var proveedor = await _dbContext.Set<Proveedor>().FindAsync(ProveedorId);
            // Verificar si el Proveedor existe
            if (proveedor == null)
            {
                // Lanzar una excepción si el Proveedor no se encuentra en la base de datos
                throw new InvalidOperationException($"No se encontró un Proveedor con ID: {ProveedorId}");
            }
            // Verificar si el Proveedor está marcado como eliminado
            if (proveedor.IsDeleted == true)
            {
                // Lanzar una excepción si el Proveedor está marcado como eliminado
                throw new InvalidOperationException("No se puede actualizar un proveedor marcado como eliminado.");
            }
            // Actualizar los campos del Proveedor con los datos proporcionados en el DTO
            proveedor.Nombre = proveedorDTO.Nombre;
            proveedor.Contacto = proveedorDTO.Contacto;
            // Guardar los cambios en la base de datos
            await _dbContext.SaveChangesAsync();  
        }
    }
}
