using ApisFlutter.Context;
using ApisFlutter.Interfaces;
using ApisFlutter.Models.DTOS;
using ApisFlutter.Models;
using Microsoft.EntityFrameworkCore;
using ApisFlutter.Models.DTOS.DTOSUPDATE;

namespace ApisFlutter.Services
{
    public class TipoProductoService : ITipoProductoService
    {
        private readonly ApplicationDbContext _dbContext;

        public TipoProductoService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TipoProductoDTO> CreateTipoProductoAsync(TipoProductoDTO tipoProductoDTO)
        {
            bool existeTipoProducto = await _dbContext.tipoProducto.AnyAsync(p =>
            p.Nombre == tipoProductoDTO.Nombre 
            );

            if (existeTipoProducto)
            {
                throw new InvalidOperationException("Ya existe este tipoProducto.");
            }

            TipoProducto tipoProducto = new()
            {
                Nombre = tipoProductoDTO.Nombre,
                IsDeleted = tipoProductoDTO.IsDeleted,
            };

            _dbContext.tipoProducto.Add(tipoProducto);
            await _dbContext.SaveChangesAsync();

            TipoProductoDTO resultadoDTO = new()
            {
                Nombre = tipoProducto.Nombre,
                IsDeleted = tipoProducto.IsDeleted
            };
            return resultadoDTO;
        }

        public async Task<List<TipoProducto>> GetTipoProductosAsync()
        {
            List<TipoProducto> tiposDeProductos = new List<TipoProducto>();

            string query =
               "SELECT * FROM TipoProducto WHERE IsDeleted = 0";

            var result= await _dbContext.tipoProducto.FromSqlRaw(query).ToListAsync();

            tiposDeProductos.AddRange(result);

            return tiposDeProductos;
        }

        public async Task DeleteTipoProductoAsync(int tipoProductoId)
        {
          var tipo = await _dbContext.Set<TipoProducto>().FindAsync(tipoProductoId);

            if (tipo == null)
            {
                throw new InvalidOperationException($"No se encontró un TipoProducto con ID: {tipoProductoId}");
            }
            if (tipo.IsDeleted == true)
            {
                throw new InvalidOperationException("No se puede eliminado un TipoProducto marcado como eliminado.");
            }
            tipo.IsDeleted = true;
            await  _dbContext.SaveChangesAsync();
        }

        public async Task UpdateTipoProductoAsync(int tipoProductoId, TipoProductoUpdateDTO tipoProductoDTO)
        {
            var tipo = await _dbContext.Set<TipoProducto>().FindAsync(tipoProductoId);

            if(tipo == null)
            {
                throw new InvalidOperationException($"No se encontró un TipoProducto con ID: {tipoProductoId}");
            }
            if (tipo.IsDeleted == true)
            {
                throw new InvalidOperationException("No se puede actualizar un TipoProducto marcado como eliminado.");
            }

            tipo.Nombre = tipoProductoDTO.Nombre;

            await _dbContext.SaveChangesAsync();
        }
    }
}
