using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace api_grupo10.Shared
{
    public class DBXmlMethods
    {
        public static XDocument GetXml<T>(T criterio)
        {
            XDocument xml = new XDocument(new XDeclaration("1.0", "utf-8", "true"));
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                using XmlWriter xw = xml.CreateWriter();
                xs.Serialize(xw, criterio);
                return xml;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<DataSet> EjecutaBase(string nombreProcedimiento, string cadenaConexion, string transaccion, string dataXML)
        {
            DataSet dtSet = new DataSet();
            SqlConnection cnn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter adapter = new SqlDataAdapter();
                cmd.CommandText = nombreProcedimiento;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cnn;
                cmd.CommandTimeout = 120;
                cmd.Parameters.Add("@Transaccion", SqlDbType.VarChar).Value = transaccion;
                cmd.Parameters.Add("@XML", SqlDbType.Xml).Value = dataXML.ToString();
                await cnn.OpenAsync().ConfigureAwait(false);
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dtSet);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Logs", "Ejecuta base", ex.ToString());
                cnn.Close();
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return dtSet;
        }
    }
}
