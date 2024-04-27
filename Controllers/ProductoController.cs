using ApisFlutter.Interfaces;
using ApisFlutter.Models.DTOS;
using ApisFlutter.Models.DTOS.DTOSUPDATE;
using Azure;
using Microsoft.AspNetCore.Mvc;

namespace ApisFlutter.Controllers
{
    // Controlador para la gestión de productos
    [Route("api/producto")]
    [ApiController]
    public class ProductoController : Controller
    {
        private readonly IProductoService _service;

        // Constructor que recibe el servicio de productos
        public ProductoController(IProductoService service)
        {
            _service = service;
        }

        // Método para obtener todos los productos
        [Route("GetProducto")]
        [HttpGet()]
        public async Task<IActionResult> GetProducto()
        {
            try
            {
                // Obtener todos los productos del servicio
                var result = await _service.GetProductoAsync();
                // Devolver los productos obtenidos
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Manejar cualquier error y devolver un código de estado 500 con un mensaje de error
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Se produjo un error al obtener los productos"
                });
            }
        }

        // Método para crear un nuevo producto
        [Route("CreateProducto")]
        [HttpPost]
        public async Task<ActionResult<ProductoDTO>> CreateProducto(ProductoDTO productoDTO)
        {
            try
            {
                // Intentar crear un nuevo producto utilizando el servicio
                var result = await _service.CreateProductoAsync(productoDTO);
                // Devolver el producto creado
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                // Manejar la excepción si ya existe un producto con la misma información y devolver un código de estado BadRequest con un mensaje de error
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
                    Message = "Se produjo un error al crear el producto"
                });
            }
        }

        // Método para eliminar un producto por su ID
        [HttpDelete("deleteProducto/{productoId}")]
        public async Task<ActionResult> DeleteProducto(int productoId)
        {
            try
            {
                // Intentar eliminar el producto utilizando el servicio
                await _service.DeleteProductoAsync(productoId);
                // Devolver un mensaje de éxito
                return Ok(new
                {
                    Success = true,
                    Message = "Producto eliminado con éxito"
                });
            }
            catch (InvalidOperationException ex)
            {
                // Manejar la excepción si no se encuentra el producto y devolver un código de estado BadRequest con un mensaje de error
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
                    Message = "Se produjo un error al eliminar el producto"
                });
            }
        }

        // Método para actualizar la información de un producto por su ID
        [HttpPut("updateProducto/{productoId}")]
        public async Task<ActionResult> UpdateProducto(int productoId, [FromBody] ProductoUpdateDTO productoDTO)
        {
            try
            {
                // Intentar actualizar la información del producto utilizando el servicio
                await _service.UpdateProductoAsync(productoId, productoDTO);
                // Devolver un mensaje de éxito
                return Ok(new
                {
                    Success = true,
                    Message = "Producto actualizado con éxito"
                });
            }
            catch (InvalidOperationException ex)
            {
                // Manejar la excepción si no se encuentra el producto y devolver un código de estado BadRequest con un mensaje de error
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
                    Message = "Ocurrió un error al actualizar el producto"
                });
            }
        }
    }
}

