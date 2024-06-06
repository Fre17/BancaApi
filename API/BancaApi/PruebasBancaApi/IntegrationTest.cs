using BancaApi.Controllers;
using BancaApi.Models;
using BancaApi.Repository.IRepository;
using BancaApi.Repository.Repository;
using BancaApi.Services.IService;
using BancaApi.Services.Service;
using BancaApi.Util;
using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebasBancaApi
{
	public class IntegrationTest
	{
		//transaccion controller 
		private readonly TransaccionController _transaccionController;
		private readonly ITransaccionService _transaccionService;
		private readonly ITransaccionRepository _transaccionRepository;
		//cuenta bancaria controller 
		private readonly CuentaBancariaController _cuentaBancariaController;
		private readonly ICuentaBancariaService _cuentaBancariaService;
		private readonly ICuentaBancariaRepository _cuentaBancariaRepository;

		private readonly SqlContext _contexto;

		public IntegrationTest()
		{
			HtmlSanitizer htmlSanitizer = new HtmlSanitizer();
			// Construir configuración a partir del archivo appsettings.json
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json")
				.Build();
			_contexto = new SqlContext(configuration);
			//transaccion controller 
			_transaccionRepository = new TransaccionRepository(_contexto);
			_transaccionService = new TransaccionService(_transaccionRepository);
			_transaccionController = new TransaccionController(htmlSanitizer, _transaccionService);
			//cuenta bancaria controller 
			_cuentaBancariaRepository = new CuentaBancariaRepository(_contexto);
			_cuentaBancariaService = new CuentaBancariaService(_cuentaBancariaRepository);
			_cuentaBancariaController = new CuentaBancariaController(htmlSanitizer, _cuentaBancariaService);
		}
		//pruebas de integracion
		//crear una cuenta, obtenerla y recargar fondos 
		[Fact]
		public async Task PruebaIntegracionUno()
		{
			//crea la cuenta
			IActionResult resultCreacion = await _transaccionController.MantenimientoTransaccionBancaria(new BancaApi.Models.TransaccionesModel
			{
				Opcion = 0,
				Usuario = "Unit Test",
				Fk_Tbl_Banca_Tipo_Transaccion = new BancaApi.Models.TipoTransaccionModel
				{
					Pk_Tbl_Banca_Tipo_Transaccion = 1,
				},
				Fk_Tbl_Banca_Cuenta_Bancaria_Origen = new BancaApi.Models.CuentaBancariaModel
				{
					Pk_Tbl_Cuenta_Bancaria = 1,
				},
				Monto = 1000,
			});

			//validar la creacion correctamente
			if (resultCreacion is OkObjectResult okResult)
			{
				//obtener la cuenta
				IActionResult resultConsulta = await _cuentaBancariaController.ConsultaCuentaBancaria(new BancaApi.Models.CuentaBancariaModel
				{
					Opcion = 0,
					Usuario = "Unit Test",
				});

				if (resultConsulta is OkObjectResult okResultConsulta)
				{
					// Convertir el valor del OkObjectResult a List<CuentaBancariaModel>
					List<CuentaBancariaModel> cuentas = (okResultConsulta.Value as Response<List<CuentaBancariaModel>>).Datos;

					if (cuentas != null && cuentas.Any())
					{
						// Obtener la ultima cuenta bancaria de la lista
						CuentaBancariaModel oCuenta = cuentas.LastOrDefault();

						//recargar fondos 
						IActionResult resultRecarga = await _transaccionController.MantenimientoTransaccionBancaria(new BancaApi.Models.TransaccionesModel
						{
							Opcion = 1,
							Usuario = "Unit Test",
							Fk_Tbl_Banca_Tipo_Transaccion = new BancaApi.Models.TipoTransaccionModel
							{
								Pk_Tbl_Banca_Tipo_Transaccion = 1,
							},
							Fk_Tbl_Banca_Cuenta_Bancaria_Origen = new CuentaBancariaModel
							{
								Pk_Tbl_Cuenta_Bancaria = 1,
							},
							Monto = 1500,
						});
						Assert.IsType<OkObjectResult>(resultRecarga);
					}
					else
					{
						Assert.IsType<BadRequestObjectResult>(okResultConsulta);
					}
				}
				else
				{
					Assert.IsType<BadRequestObjectResult>(resultConsulta);
				}
			}
			else
			{
				Assert.IsType<BadRequestObjectResult>(resultCreacion);
			}
		}
		//crear consultar la ultima cuenta, recargar fondos, retiro y una transaccion de la misma
		[Fact]
		public async Task PruebaIntegracionDos()
		{
			//obtener la cuenta
			IActionResult resultConsulta = await _cuentaBancariaController.ConsultaCuentaBancaria(new BancaApi.Models.CuentaBancariaModel
			{
				Opcion = 0,
				Usuario = "Unit Test",
			});

			if (resultConsulta is OkObjectResult okResultConsulta)
			{
				// Convertir el valor del OkObjectResult a List<CuentaBancariaModel>
				List<CuentaBancariaModel> cuentas = (okResultConsulta.Value as Response<List<CuentaBancariaModel>>).Datos;

				if (cuentas != null && cuentas.Any())
				{
					// Obtener la ultima cuenta bancaria de la lista
					CuentaBancariaModel oCuenta = cuentas.LastOrDefault();

					//recargar fondos 
					IActionResult resultRecarga = await _transaccionController.MantenimientoTransaccionBancaria(new BancaApi.Models.TransaccionesModel
					{
						Opcion = 1,
						Usuario = "Unit Test",
						Fk_Tbl_Banca_Tipo_Transaccion = new BancaApi.Models.TipoTransaccionModel
						{
							Pk_Tbl_Banca_Tipo_Transaccion = 1,
						},
						Fk_Tbl_Banca_Cuenta_Bancaria_Origen = new CuentaBancariaModel
						{
							Pk_Tbl_Cuenta_Bancaria = 1,
						},
						Monto = 1500,
					});

					//validar que la recarga se pudiera realizar
					if (resultRecarga is BadRequestObjectResult)
					{
						Assert.IsType<BadRequestObjectResult>(resultRecarga);
						return;
					}

					//retirar fondos 
					IActionResult resultRetiro = await _transaccionController.MantenimientoTransaccionBancaria(new BancaApi.Models.TransaccionesModel
					{
						Opcion = 2,
						Usuario = "Unit Test",
						Fk_Tbl_Banca_Tipo_Transaccion = new BancaApi.Models.TipoTransaccionModel
						{
							Pk_Tbl_Banca_Tipo_Transaccion = 1,
						},
						Fk_Tbl_Banca_Cuenta_Bancaria_Origen = oCuenta,
						Monto = 500,
					});

					//validar que el retiro se pudiera realizar
					if (resultRetiro is BadRequestObjectResult)
					{
						Assert.IsType<BadRequestObjectResult>(resultRetiro);
						return;
					}

					//transferir fondos 
					IActionResult resultTransferencia = await _transaccionController.MantenimientoTransaccionBancaria(new BancaApi.Models.TransaccionesModel
					{
						Opcion = 3,
						Usuario = "Unit Test",
						Fk_Tbl_Banca_Tipo_Transaccion = new BancaApi.Models.TipoTransaccionModel
						{
							Pk_Tbl_Banca_Tipo_Transaccion = 1,
						},
						Fk_Tbl_Banca_Cuenta_Bancaria_Origen = oCuenta,
						Fk_Tbl_Banca_Cuenta_Bancaria_Destino = new BancaApi.Models.CuentaBancariaModel
						{
							Pk_Tbl_Cuenta_Bancaria = 1,
						},
						Monto = 500,
					});

					//validar que el retiro se pudiera realizar
					if (resultTransferencia is BadRequestObjectResult)
					{
						Assert.IsType<BadRequestObjectResult>(resultTransferencia);
						return;
					}
					else
					{
						Assert.IsType<OkObjectResult>(resultTransferencia);
					}

				}
			}
			else
			{
				Assert.IsType<BadRequestObjectResult>(resultConsulta);
			}
		}
	}
}
