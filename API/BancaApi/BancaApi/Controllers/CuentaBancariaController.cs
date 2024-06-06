using BancaApi.Models;
using BancaApi.Services.IService;
using BancaApi.Util;
using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BancaApi.Controllers
{
    [Route("BancaApi/CuentaBancaria")]
    [ApiController]
    public class CuentaBancariaController : ControllerBase
    {
        private readonly HtmlSanitizer _htmlSanitizer;
        private readonly ICuentaBancariaService _cuentaBancariaService;
        public CuentaBancariaController(HtmlSanitizer htmlSanitizer, ICuentaBancariaService cuentaBancariaService)
        {
            _htmlSanitizer = htmlSanitizer;
            _cuentaBancariaService = cuentaBancariaService;
        }
        // EndPoint para obtener información de una cuenta bancaria aplicando filtros
        [HttpPost]
        [Route("ConsultaCuentaBancaria")]
        public async Task<IActionResult> ConsultaCuentaBancaria(CuentaBancariaModel pCuentaBancaria)
        {
            try
            {
                // Sanitizar el parametro de la cuenta bancaria para evitar ataques XSS
                CuentaBancariaModel oCuentaBancaria = await ComprobarParametro(pCuentaBancaria);

                if (oCuentaBancaria != null)
                {
                    // Consultar la información de la cuenta bancaria por los filtros
                    var response = await _cuentaBancariaService.ConsultaCuentaBancaria(oCuentaBancaria);

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
                    return base.BadRequest(new Util.Response<CuentaBancariaModel>{ Datos = null, Mensaje = "Error en la validación del parámetro", Exitoso = false });
                }
            }
            catch (Exception ex)
            {
                return base.BadRequest(new Util.Response<CuentaBancariaModel> { Datos = null, Mensaje = $"Error: {ex.Message}", Exitoso = false });
            }
        }
        // EndPoint para obtener mantenimiento de una cuenta bancaria 
        [HttpPost]
		[Route("MantenimientoCuentaBancaria")]
		public async Task<IActionResult> MantenimientoCuentaBancaria(CuentaBancariaModel pCuentaBancaria)
        {
            try
            {
                // Sanitizar el parametro de la cuenta bancaria para evitar ataques XSS
                CuentaBancariaModel oCuentaBancaria = await ComprobarParametro(pCuentaBancaria);

                if (oCuentaBancaria != null)
                {
                    // Consultar la información de la cuenta bancaria por los filtros
                    var response = await _cuentaBancariaService.MantenimientoCuentaBancaria(oCuentaBancaria);

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
                    return base.BadRequest(new Util.Response<CuentaBancariaModel> { Datos = null, Mensaje = "Error en la validación del parámetro", Exitoso = false });
                }
            }
            catch (Exception ex)
            {
                return base.BadRequest(new Util.Response<CuentaBancariaModel> { Datos = null, Mensaje = $"Error: {ex.Message}", Exitoso = false });
            }
        }
        // Método que valida y sanitiza la entidad de parámetro para evitar ataques XSS
        private async Task<CuentaBancariaModel> ComprobarParametro(CuentaBancariaModel pCuentaBancaria)
        {
            try
            {
                return new CuentaBancariaModel
                {
                    Usuario = _htmlSanitizer.Sanitize(pCuentaBancaria.Usuario),
                    Opcion = Convert.ToInt32(_htmlSanitizer.Sanitize(pCuentaBancaria.Opcion.ToString())),
                    Fk_Tbl_Usuario = new UsuarioModel
                    {
                        Pk_Tbl_Banca_Usuario = Convert.ToInt32(_htmlSanitizer.Sanitize(pCuentaBancaria.Fk_Tbl_Usuario.Pk_Tbl_Banca_Usuario.ToString())),
                        Usuario = _htmlSanitizer.Sanitize(pCuentaBancaria.Fk_Tbl_Usuario.Usuario),
                        Nombre = _htmlSanitizer.Sanitize(pCuentaBancaria.Fk_Tbl_Usuario.Nombre),
                        Identificacion = _htmlSanitizer.Sanitize(pCuentaBancaria.Fk_Tbl_Usuario.Identificacion),
                        Contrasena = new Util.Crypto().Encrypt(_htmlSanitizer.Sanitize(pCuentaBancaria.Fk_Tbl_Usuario.Contrasena)),
                    },
                    Saldo = Convert.ToDecimal(_htmlSanitizer.Sanitize(pCuentaBancaria.Saldo.ToString())),
                };
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error en la validación del parámetro: {ex.Message}");
            }
        }
    }
}
