using Microsoft.Data.SqlClient;

namespace BancaApi.Util
{
    public class SqlContext
    {
        string _cadenaDeConexion { get; set; }
		public SqlContext(IConfiguration configuracion) => _cadenaDeConexion = $"Data Source={configuracion["Sql:Servidor"]};Initial Catalog={configuracion["Sql:BD"]};Persist Security Info=True;User ID={configuracion["Sql:Usuario"]};TrustServerCertificate=True; Password={configuracion["Sql:Contrasena"]}";
		public string ObtenerCadenaDeConexion() => _cadenaDeConexion;
        public SqlConnection ObtenerConexionBaseDeDatos()
        {
            SqlConnection _conexionSql = new SqlConnection(ObtenerCadenaDeConexion());
            _conexionSql.Open();
            return _conexionSql;
        }
        public bool ExisteColumnaEnSqlDataReader(SqlDataReader pSqlReader, string pNombreColumna) => pSqlReader.GetSchemaTable().Columns.Contains(pNombreColumna);
    }
}
