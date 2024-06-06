namespace BancaApi.Models
{
    public class TipoTransaccionModel : BaseModel
    {
        public int Pk_Tbl_Banca_Tipo_Transaccion { get; set; }
        public string Descripcion { get; set; }
        public TipoTransaccionModel()
        {
            Pk_Tbl_Banca_Tipo_Transaccion = 0;
            Descripcion = string.Empty;
        }
    }
}
