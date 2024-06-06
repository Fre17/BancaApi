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
	public class TransaccionControllerTest
	{
		//transaccion controller 
		private readonly TransaccionController _transaccionController;
		private readonly ITransaccionService _transaccionService;
		private readonly ITransaccionRepository _transaccionRepository;
		private readonly SqlContext _contexto;

		public TransaccionControllerTest()
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
		}
		//pruebas transaccion controller
		//consulta transacciones 
		[Fact]
		public async Task ConsultaTransacciones()
		{
			IActionResult result = await _transaccionController.ConsultaTransaccionBancaria(new BancaApi.Models.TransaccionesModel
			{
				Opcion = 0,
				Usuario = "Unit Test",
			});
			Assert.IsType<OkObjectResult>(result);
		}
		//consulta transacciones aplicando filtros 
		[Fact]
		public async Task ConsultaTransaccionesFiltros()
		{
			IActionResult result = await _transaccionController.ConsultaTransaccionBancaria(new BancaApi.Models.TransaccionesModel
			{
				Opcion = 0,
				Usuario = "Unit Test",
				Pk_Tbl_Banca_Transacciones = 0,
				Fk_Tbl_Banca_Tipo_Transaccion = new BancaApi.Models.TipoTransaccionModel
				{
					Pk_Tbl_Banca_Tipo_Transaccion = 1,
				},
				Fk_Tbl_Banca_Cuenta_Bancaria_Origen = new BancaApi.Models.CuentaBancariaModel
				{
					Pk_Tbl_Cuenta_Bancaria = 1,
				}
			});
			Assert.IsType<OkObjectResult>(result);
		}
		//crea una transaccion 
		[Fact]
		public async Task MantenimientoTransaccionCrea()
		{
			IActionResult result = await _transaccionController.MantenimientoTransaccionBancaria(new BancaApi.Models.TransaccionesModel
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
			Assert.IsType<OkObjectResult>(result);
		}
		//actualiza una transaccion 
		[Fact]
		public async Task MantenimientoTransaccionActualiza()
		{
			IActionResult result = await _transaccionController.MantenimientoTransaccionBancaria(new BancaApi.Models.TransaccionesModel
			{
				Opcion = 0,
				Usuario = "Unit Test",
				Pk_Tbl_Banca_Transacciones = 29,
				Fk_Tbl_Banca_Tipo_Transaccion = new BancaApi.Models.TipoTransaccionModel
				{
					Pk_Tbl_Banca_Tipo_Transaccion = 1,
				},
				Fk_Tbl_Banca_Cuenta_Bancaria_Origen = new BancaApi.Models.CuentaBancariaModel
				{
					Pk_Tbl_Cuenta_Bancaria = 1,
				},
				Monto = 1500,
			});
			Assert.IsType<OkObjectResult>(result);
		}
		//agrega fondos a una cuenta 
		[Fact]
		public async Task MantenimientoTransaccionAgregarFondos()
		{
			IActionResult result = await _transaccionController.MantenimientoTransaccionBancaria(new BancaApi.Models.TransaccionesModel
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
			Assert.IsType<OkObjectResult>(result);
		}
		//retira fondos a una cuenta 
		[Fact]
		public async Task MantenimientoTransaccionRetirarFondos()
		{
			IActionResult result = await _transaccionController.MantenimientoTransaccionBancaria(new BancaApi.Models.TransaccionesModel
			{
				Opcion = 2,
				Usuario = "Unit Test",
				Fk_Tbl_Banca_Tipo_Transaccion = new BancaApi.Models.TipoTransaccionModel
				{
					Pk_Tbl_Banca_Tipo_Transaccion = 2,
				},
				Fk_Tbl_Banca_Cuenta_Bancaria_Origen = new CuentaBancariaModel
				{
					Pk_Tbl_Cuenta_Bancaria = 1,
				},
				Monto = 500,
			});
			Assert.IsType<OkObjectResult>(result);
		}
		//transferir fondos de una cuenta a otra
		[Fact]
		public async Task MantenimientoTransaccionTransferirFondos()
		{
			IActionResult result = await _transaccionController.MantenimientoTransaccionBancaria(new BancaApi.Models.TransaccionesModel
			{
				Opcion = 3,
				Usuario = "Unit Test",
				Fk_Tbl_Banca_Tipo_Transaccion = new BancaApi.Models.TipoTransaccionModel
				{
					Pk_Tbl_Banca_Tipo_Transaccion = 3,
				},
				Fk_Tbl_Banca_Cuenta_Bancaria_Origen = new CuentaBancariaModel
				{
					Pk_Tbl_Cuenta_Bancaria = 1,
				},
				Fk_Tbl_Banca_Cuenta_Bancaria_Destino = new BancaApi.Models.CuentaBancariaModel
				{
					Pk_Tbl_Cuenta_Bancaria = 14,
				},
				Monto = 500,
			});
			Assert.IsType<OkObjectResult>(result);
		}
		//intereses ganados 
		[Fact]
		public async Task MantenimientoTransaccionInteresGanado()
		{
			IActionResult result = await _transaccionController.MantenimientoTransaccionBancaria(new BancaApi.Models.TransaccionesModel
			{
				Opcion = 4,
				Usuario = "Unit Test",
			});
			Assert.IsType<OkObjectResult>(result);
		}
	}
}
