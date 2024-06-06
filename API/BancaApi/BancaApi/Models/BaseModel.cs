namespace BancaApi.Models
{
    public class BaseModel
    {
        public int Opcion { set; get; }
        public string Usuario { set; get; }
        public BaseModel()
        {
            Opcion = 0;
            Usuario = string.Empty;
        }
    }
}
