using ApisFlutter.Interfaces;
using ApisFlutter.Models.DTOS;
using ApisFlutter.Models.DTOS.DTOSUPDATE;
using Microsoft.AspNetCore.Mvc;

namespace ApisFlutter.Controllers
{
    // Controlador para la gestión de proveedor
    [Route("api/proveedor")]
    [ApiController]
    public class ProveedorController : Controller
    {
        private readonly IProveedorService _service;
        // Constructor que recibe el servicio de proveedor
        public ProveedorController(IProveedorService service)
        {
            _service = service;
        }
        // Método para obtener todos los proveedor
        [Route("GetProveedor")]
        [HttpGet()]
        public async Task<IActionResult> GetProveedor()
        {
            // Obtener todos los proveedor del servicio
            var result = await _service.GetProveedorAsync();
            // Devolver los proveedor obtenidos
            return Ok(result);
        }
        // Método para crear un nuevo proveedor
        [Route("Createproveedor")]
        [HttpPost]
        public async Task<ActionResult<ProveedorDTO>> Createproveedor(ProveedorDTO proveedorDTO)
        {
            try
            {
                // Llama al servicio para crear el proveedor
                var result = await _service.CreateProveedorAsync(proveedorDTO);
                // Devolver el  proveedor
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                // Manejar la excepción si ya existe un proveedor con la misma información y devolver un código de estado BadRequest con un mensaje de error
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                // Manejar cualquier otro error y devolver un código de estado 500 con un mensaje de error
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Se produjo un error al crear al proveedor"
                });
            }

        }
        // Método para eliminar un proveedor por su ID
        [HttpDelete("deleteProveedor/{ProveedorId}")]
        public async Task<ActionResult> DeleteProveedor(int ProveedorId)
        {
            try
            {

                // Intentar eliminar el proveedor utilizando el servicio
                await _service.DeleteProveedorAsync(ProveedorId);
                // Devolver un mensaje de éxito
                return Ok(new
                {
                    Success = true,
                    Message = "Proveedor eliminado con éxito"
                });
            }
            catch (InvalidOperationException ex)
            {
                // Manejar la excepción si no se encuentra el Proveedor y devolver un código de estado BadRequest con un mensaje de error
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                // Manejar cualquier otro error y devolver un código de estado 500 con un mensaje de error
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Se produjo un error al eliminar al Proveedor"
                });
            }

        }
        // Método para actualizar la información de un Proveedor por su ID
        [HttpPut ("updateProveedor/{ProveedorId}")]
        public async Task<ActionResult> UpdateProveedor(int ProveedorId, [FromBody] ProveedorUpdateDTO proveedorDTO)
        {
            try
            {  // Intentar actualizar la información del Proveedor utilizando el servicio
                await _service.UpdateProveedorAsync(ProveedorId, proveedorDTO);
                return Ok(new
                {
                    Success = true,
                    Message = "Proveedor Actualizado con éxito"
                });
            }
            catch (InvalidOperationException ex)
            {
                // Manejar la excepción si no se encuentra el Proveedor y devolver un código de estado BadRequest con un mensaje de error
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                // Manejar cualquier otro error y devolver un código de estado 500 con un mensaje de error
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Ocurrió un error al actualizar el Proveedor"
                });
            }
        }
    }
}
