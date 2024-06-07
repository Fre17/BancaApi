using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace WorkerInteres
{
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> _logger;

		public Worker(ILogger<Worker> logger)
		{
			_logger = logger;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				if (_logger.IsEnabled(LogLevel.Information))
				{
					//consulta al api, transaccion controller opcio 4 registro de intereses en todas las cuentas
					using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(900) })
					{
						var uri = new Uri("https://localhost:44319/BancaApi/transaccion/MantenimientoTransaccionBancaria");
						var json = JsonConvert.SerializeObject(
																new
																{
																	Opcion = 4,
																	Usuario = "Worker",
																});
						var content = new StringContent(json, Encoding.UTF8, "application/json");
						HttpResponseMessage response = client.PostAsync(uri, content).Result;
						string resultado = await response.Content.ReadAsStringAsync();
					}
				}
				//duerme el worker por las proximas 24 horas 
				await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
			}
		}
	}
}
