namespace BancaApi.Models
{
    public class UsuarioModel : BaseModel
    {
        public int Pk_Tbl_Banca_Usuario { set; get; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Contrasena { get; set; }
        public UsuarioModel()
        {
            Pk_Tbl_Banca_Usuario = 0;
            Identificacion = string.Empty;
            Nombre = string.Empty;
            Contrasena = string.Empty;
        }
    }
}
