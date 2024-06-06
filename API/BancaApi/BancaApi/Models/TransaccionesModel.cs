using System.Globalization;

namespace BancaApi.Models
{
    public class TransaccionesModel : BaseModel
    {
        public int Pk_Tbl_Banca_Transacciones { get; set; }
        public TipoTransaccionModel Fk_Tbl_Banca_Tipo_Transaccion { set; get; }
        public CuentaBancariaModel Fk_Tbl_Banca_Cuenta_Bancaria_Origen { set; get; }
        public CuentaBancariaModel Fk_Tbl_Banca_Cuenta_Bancaria_Destino { set; get; }
        public decimal Monto { set; get; }
        public DateTime FechaMovimiento { set; get; }
        public string Detalle { set; get; }
        public TransaccionesModel()
        {
            Pk_Tbl_Banca_Transacciones = 0;
            Fk_Tbl_Banca_Tipo_Transaccion = new TipoTransaccionModel();
            Fk_Tbl_Banca_Cuenta_Bancaria_Origen = new CuentaBancariaModel();
            Fk_Tbl_Banca_Cuenta_Bancaria_Destino = new CuentaBancariaModel();
            Monto = 0;
            FechaMovimiento = new DateTime();
            Detalle = string.Empty;
        }
    }
}
