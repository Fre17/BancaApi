using System.ComponentModel.DataAnnotations;

namespace BancaApi.Models
{
    public class CuentaBancariaModel : BaseModel
    {
        public int Pk_Tbl_Cuenta_Bancaria { get; set; }
        public UsuarioModel Fk_Tbl_Usuario { get; set; }
        public decimal Saldo { get; set; }
        public CuentaBancariaModel()
        {
            Pk_Tbl_Cuenta_Bancaria = 0;
            Fk_Tbl_Usuario = new UsuarioModel();
            Saldo = 0;
        }
    }
}
