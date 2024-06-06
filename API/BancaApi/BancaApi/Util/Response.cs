namespace BancaApi.Util
{
    public class Response<T>
    {
		public T? Datos { get; set; }
		public bool Exitoso { get; set; }
		public string? Mensaje { get; set; }
    }
}
