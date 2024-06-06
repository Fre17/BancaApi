using System.Data;
using System.Globalization;
using System.Text;

namespace BancaApi.Util
{
    public class BDUtilitario
    {
        public static string ObtieneString(IDataReader reader, string columnName)
        {
            try
            {
                return reader[columnName].ToString().Trim();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static string ObtieneNumeroString(IDataReader reader, string columnName)
        {
            try
            {
                return reader[columnName].ToString();
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public static int ObtieneInt(IDataReader reader, string columnName)
        {
            try
            {
                return Convert.ToInt32(reader[columnName].ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static long ObtieneInt64(IDataReader reader, string columnName)
        {
            try
            {
                return Convert.ToInt64(reader[columnName].ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static bool ObtieneBool(IDataReader reader, string columnName)
        {
            try
            {
                return Convert.ToBoolean(Convert.ToInt32(reader[columnName].ToString()));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ObtieneBoolBit(IDataReader reader, string columnName)
        {
            try
            {
                return Convert.ToBoolean((reader[columnName].ToString()));
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static decimal ObtieneDecimal(IDataReader reader, string columnName)
        {
            try
            {
                CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
                cultureInfo.NumberFormat = new NumberFormatInfo()
                {
                    NumberDecimalSeparator = ",",
                    CurrencyDecimalSeparator = ",",
                    NumberGroupSeparator = ".",
                    CurrencyGroupSeparator = ".",
                    NumberDecimalDigits = 2,
                    CurrencyDecimalDigits = 2,
                    PercentDecimalDigits = 2
                };
                string numero = reader[columnName].ToString().Replace('.', ',');
                return Math.Round(Convert.ToDecimal(numero, cultureInfo), 2);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static decimal ObtieneDecimal(string numero)
        {
            try
            {
                CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
                cultureInfo.NumberFormat = new NumberFormatInfo()
                {
                    NumberDecimalSeparator = ",",
                    CurrencyDecimalSeparator = ",",
                    NumberGroupSeparator = ".",
                    CurrencyGroupSeparator = ".",
                    NumberDecimalDigits = 2,
                    CurrencyDecimalDigits = 2,
                    PercentDecimalDigits = 2
                };

                // Intenta convertir con el separador de coma primero
                try
                {
                    return Convert.ToDecimal(numero.Replace(".", ","), cultureInfo);
                }
                catch (Exception)
                {
                    // Si falla, intenta convertir con el separador de punto
                    return Convert.ToDecimal(numero.Replace(",", "."), cultureInfo);
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static double ObtieneDouble(IDataReader reader, string columnName)
        {
            try
            {
                CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
                cultureInfo.NumberFormat = new NumberFormatInfo()
                {
                    NumberDecimalSeparator = ",",
                    CurrencyDecimalSeparator = ",",
                    NumberGroupSeparator = ".",
                    CurrencyGroupSeparator = ".",
                    NumberDecimalDigits = 2,
                    CurrencyDecimalDigits = 2,
                    PercentDecimalDigits = 2
                };
                string numero = reader[columnName].ToString().Replace('.', ',');
                return Convert.ToDouble(numero, cultureInfo);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static double ObtieneDouble(string numero)
        {
            try
            {
                CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
                cultureInfo.NumberFormat = new NumberFormatInfo()
                {
                    NumberDecimalSeparator = ".",
                    CurrencyDecimalSeparator = ".",
                    NumberGroupSeparator = ".",
                    CurrencyGroupSeparator = ".",
                    NumberDecimalDigits = 2,
                    CurrencyDecimalDigits = 2,
                    PercentDecimalDigits = 2
                };
                return Convert.ToDouble(numero, cultureInfo);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static DateTime ObtieneDateTime(IDataReader reader, string columnName)
        {
            try
            {
                return Convert.ToDateTime(reader[columnName].ToString());
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }
        public static DateTime ObtieneDateTime(string pFecha)
        {
            try
            {
                DateTime dateTime;
                if (DateTime.TryParseExact(pFecha, "dd/M/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out dateTime))
                {
                    return dateTime;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }
        public static byte[] ObtieneByteArray(IDataReader reader, string columnName)
        {
            try
            {
                string value = reader[columnName].ToString().Trim();
                // Convertir la cadena en un arreglo de bytes usando la codificación adecuada
                return Encoding.UTF8.GetBytes(value);
            }
            catch (Exception)
            {
                // En caso de error, devolver un arreglo de bytes vacío
                return new byte[0];
            }
        }
    }
}
