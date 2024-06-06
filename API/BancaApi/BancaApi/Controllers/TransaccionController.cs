using BancaApi.Models;
using BancaApi.Services.IService;
using BancaApi.Services.Service;
using BancaApi.Util;
using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BancaApi.Controllers
{
    [Route("BancaApi/transaccion")]
    [ApiController]
    public class TransaccionController : ControllerBase
    {
        private readonly HtmlSanitizer _htmlSanitizer;
        private readonly ITransaccionService _transaccionService;
        public TransaccionController(HtmlSanitizer htmlSanitizer, ITransaccionService transaccionService)
        {
            _htmlSanitizer = htmlSanitizer;
            _transaccionService = transaccionService;
        }
        // EndPoint para obtener información de una cuenta bancaria aplicando filtros
        [HttpPost]
		[Route("ConsultaTransaccionBancaria")]
		public async Task<IActionResult> ConsultaTransaccionBancaria(TransaccionesModel pTransaccion)
        {
            try
            {
                // Sanitizar el parametro de la cuenta bancaria para evitar ataques XSS
                TransaccionesModel oTransaccion = await ComprobarParametro(pTransaccion);

                if (oTransaccion != null)
                {
                    // Consultar la información de la cuenta bancaria por los filtros
                    var response = await _transaccionService.ConsultaTransaccionBancaria(oTransaccion);

                    // Retornar la respuesta de la consulta
                    if (response.Exitoso)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        return BadRequest(response);
                    }
                }
                else
                {
                    return base.BadRequest(new Util.Response<TransaccionesModel>{ Datos = null, Mensaje = "Error en la validación del parámetro", Exitoso = false });
                }
            }
            catch (Exception ex)
            {
                return base.BadRequest(new Util.Response<TransaccionesModel> { Datos = null, Mensaje = $"Error: {ex.Message}", Exitoso = false });
            }
        }
        // EndPoint para obtener información de una cuenta bancaria aplicando filtros
        [HttpPost]
		[Route("MantenimientoTransaccionBancaria")]
		public async Task<IActionResult> MantenimientoTransaccionBancaria(TransaccionesModel pTransaccion)
        {
            try
            {
                // Sanitizar el parametro de la cuenta bancaria para evitar ataques XSS
                TransaccionesModel oTransaccion = await ComprobarParametro(pTransaccion);

                if (oTransaccion != null)
                {
                    // Consultar la información de la cuenta bancaria por los filtros
                    var response = await _transaccionService.MantenimientoTransaccionBancaria(pTransaccion);

                    // Retornar la respuesta de la consulta
                    if (response.Exitoso)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        return BadRequest(response);
                    }
                }
                else
                {
                    return base.BadRequest(new Util.Response<TransaccionesModel> { Datos = null, Mensaje = "Error en la validación del parámetro", Exitoso = false });
                }
            }
            catch (Exception ex)
            {
                return base.BadRequest(new Util.Response<CuentaBancariaModel> { Datos = null, Mensaje = $"Error: {ex.Message}", Exitoso = false });
            }
        }
        // Método que valida y sanitiza la entidad de parámetro para evitar ataques XSS
        private async Task<TransaccionesModel> ComprobarParametro(TransaccionesModel pTransaccion)
        {
            try
            {
                return new TransaccionesModel
                {
                    //base model
                    Usuario = _htmlSanitizer.Sanitize(pTransaccion.Usuario),
                    Opcion = Convert.ToInt32(_htmlSanitizer.Sanitize(pTransaccion.Opcion.ToString())),
                    //fk tipo transaccion
                    Fk_Tbl_Banca_Tipo_Transaccion = new TipoTransaccionModel
                    {
                        Pk_Tbl_Banca_Tipo_Transaccion = Convert.ToInt32(_htmlSanitizer.Sanitize(pTransaccion.Fk_Tbl_Banca_Tipo_Transaccion.Pk_Tbl_Banca_Tipo_Transaccion.ToString())),
                        Descripcion = _htmlSanitizer.Sanitize(pTransaccion.Fk_Tbl_Banca_Tipo_Transaccion.Descripcion),
                    },
                    //cuenta bancaria origen
                    Fk_Tbl_Banca_Cuenta_Bancaria_Origen = new CuentaBancariaModel
                    {
                        Pk_Tbl_Cuenta_Bancaria = Convert.ToInt32(_htmlSanitizer.Sanitize(pTransaccion.Fk_Tbl_Banca_Cuenta_Bancaria_Origen.Pk_Tbl_Cuenta_Bancaria.ToString())),
                        Fk_Tbl_Usuario = new UsuarioModel
                        {
                            Pk_Tbl_Banca_Usuario = Convert.ToInt32(_htmlSanitizer.Sanitize(pTransaccion.Fk_Tbl_Banca_Cuenta_Bancaria_Origen.Fk_Tbl_Usuario.Pk_Tbl_Banca_Usuario.ToString())),
                            Usuario = _htmlSanitizer.Sanitize(pTransaccion.Fk_Tbl_Banca_Cuenta_Bancaria_Origen.Fk_Tbl_Usuario.Usuario),
                            Nombre = _htmlSanitizer.Sanitize(pTransaccion.Fk_Tbl_Banca_Cuenta_Bancaria_Origen.Fk_Tbl_Usuario.Nombre),
                            Identificacion = _htmlSanitizer.Sanitize(pTransaccion.Fk_Tbl_Banca_Cuenta_Bancaria_Origen.Fk_Tbl_Usuario.Identificacion),
                            Contrasena = new Util.Crypto().Encrypt(_htmlSanitizer.Sanitize(pTransaccion.Fk_Tbl_Banca_Cuenta_Bancaria_Origen.Fk_Tbl_Usuario.Contrasena)),
                        },
                        Saldo = Convert.ToDecimal(_htmlSanitizer.Sanitize(pTransaccion.Fk_Tbl_Banca_Cuenta_Bancaria_Origen.Saldo.ToString())),
                    },
                    //cuenta bancaria destino 
                    Fk_Tbl_Banca_Cuenta_Bancaria_Destino = new CuentaBancariaModel
                    {
                        Pk_Tbl_Cuenta_Bancaria = Convert.ToInt32(_htmlSanitizer.Sanitize(pTransaccion.Fk_Tbl_Banca_Cuenta_Bancaria_Destino.Pk_Tbl_Cuenta_Bancaria.ToString())),
                        Fk_Tbl_Usuario = new UsuarioModel
                        {
                            Pk_Tbl_Banca_Usuario = Convert.ToInt32(_htmlSanitizer.Sanitize(pTransaccion.Fk_Tbl_Banca_Cuenta_Bancaria_Destino.Fk_Tbl_Usuario.Pk_Tbl_Banca_Usuario.ToString())),
                            Usuario = _htmlSanitizer.Sanitize(pTransaccion.Fk_Tbl_Banca_Cuenta_Bancaria_Destino.Fk_Tbl_Usuario.Usuario),
                            Nombre = _htmlSanitizer.Sanitize(pTransaccion.Fk_Tbl_Banca_Cuenta_Bancaria_Destino.Fk_Tbl_Usuario.Nombre),
                            Identificacion = _htmlSanitizer.Sanitize(pTransaccion.Fk_Tbl_Banca_Cuenta_Bancaria_Destino.Fk_Tbl_Usuario.Identificacion),
                            Contrasena = new Util.Crypto().Encrypt(_htmlSanitizer.Sanitize(pTransaccion.Fk_Tbl_Banca_Cuenta_Bancaria_Destino.Fk_Tbl_Usuario.Contrasena)),
                        },
                        Saldo = Convert.ToDecimal(_htmlSanitizer.Sanitize(pTransaccion.Fk_Tbl_Banca_Cuenta_Bancaria_Destino.Saldo.ToString())),
                    },
                    //montos
                    Monto = Convert.ToDecimal(_htmlSanitizer.Sanitize(pTransaccion.Monto.ToString())),
                    FechaMovimiento = Convert.ToDateTime(_htmlSanitizer.Sanitize(pTransaccion.FechaMovimiento.ToString())),
                    Detalle = _htmlSanitizer.Sanitize(pTransaccion.Detalle),
                };
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error en la validación del parámetro: {ex.Message}");
            }
        }
    }
}
