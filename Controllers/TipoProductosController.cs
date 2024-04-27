using ApisFlutter.Interfaces;
using ApisFlutter.Models.DTOS;
using ApisFlutter.Models.DTOS.DTOSUPDATE;
using Microsoft.AspNetCore.Mvc;

namespace ApisFlutter.Controllers
{
    [Route("api/tipoProductos")]
    [ApiController]
    public class TipoProductosController : Controller
    {
        private readonly ITipoProductoService _service;

        public TipoProductosController(ITipoProductoService service)
        {
            _service = service;
        }

        [Route("GetTipoProductos")]
        [HttpGet()]
        public async Task<IActionResult> GetTipoProductos()
        {
            var result = await _service.GetTipoProductosAsync();
            return Ok(result);
        }

        [Route("CreateTipoProducto")]
        [HttpPost]
        public async Task<ActionResult<TipoProductoDTO>> CreateTipoProducto(TipoProductoDTO tipoProductoDTO)
        {
            try
            {

                var result = await _service.CreateTipoProductoAsync(tipoProductoDTO);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Se produjo un error al crear el TipoProducto"
                });
            }

        }

        [HttpDelete("deleteTipoProducto/{tipoProductoId}")]
        public async Task<ActionResult> DeleteTipoProducto(int tipoProductoId)
        {
            try
            {
                await _service.DeleteTipoProductoAsync(tipoProductoId);
                return Ok(new
                {
                    Success = true,
                    Message = "TipoProducto eliminado con éxito"
                });
            }
            catch (InvalidOperationException ex)
            {

                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Se produjo un error al eliminar el TipoProducto"
                });
            }

        }

        [HttpPut("updateTipoProducto/{tipoProductoId}")]
        public async Task<ActionResult> UpdateTipoProducto(int tipoProductoId, [FromBody] TipoProductoUpdateDTO tipoProductoDTO)
        {
            try
            {
                await _service.UpdateTipoProductoAsync(tipoProductoId, tipoProductoDTO);
                return Ok(new
                {
                    Success = true,
                    Message = "TipoProducto Actualizado con éxito"
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Ocurrió un error al actualizar el TipoProducto"
                });
            }
        }
    }
}
