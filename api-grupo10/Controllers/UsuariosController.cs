using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Xml.Linq;
using TallerTecnico;

namespace api_grupo10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : Controller
    {
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<Usuario>> UsuarioTr(string transaction)
        {
            var cadenaConexion = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("ConnectionStrings")["Conexion"];

            Usuario inv = new Usuario
            {
                Transaccion = transaction
            };

            XDocument xmlParam = Shared.DBXmlMethods.GetXml(inv);
            DataSet dbResult = await Shared.DBXmlMethods.EjecutaBase("GetUsuario", cadenaConexion, transaction, xmlParam.ToString());
            List<Usuario> userList = new List<Usuario>();

            if (dbResult.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dbResult.Tables[0].Rows)
                    {
                        Console.WriteLine(dbResult.Tables[0].Rows.Count.ToString());
                        Usuario invent = new Usuario
                        {
                            Id = Convert.ToInt32(row["id"]),
                            Nombre = row["nombre"].ToString(),
                            Cedula = row["cedula"].ToString(),
                            Celular = row["celular"].ToString(),
                            Correo = row["correo"].ToString(),
                            Password = row["password"].ToString(),

                        };
                        userList.Add(invent);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: ", ex);
                }
            }

            return Ok(userList);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<RespuestaLeyenda>> UsuarioLogin(string username, string password, string transaction)
        {
            var cadenaConexion = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("ConnectionStrings")["Conexion"];

            Usuario inv = new Usuario
            {
                Nombre = username,
                Password = password,
                Transaccion = transaction,
            };

            XDocument xmlParam = Shared.DBXmlMethods.GetXml(inv);
            DataSet dbResult = await Shared.DBXmlMethods.EjecutaBase("GetUsuario", cadenaConexion, transaction, xmlParam.ToString());
            List<RespuestaLeyenda> msgList = new List<RespuestaLeyenda>();

            if (dbResult.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dbResult.Tables[0].Rows)
                    {
                        Console.WriteLine(dbResult.Tables[0].Rows.Count.ToString());
                        RespuestaLeyenda invent = new()
                        {
                            Respuesta = row["respuesta"].ToString(),
                            Leyenda = row["leyenda"].ToString(),
                        };
                        Console.WriteLine(invent);
                        msgList.Add(invent);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: ", ex);
                }
            }

            return Ok(msgList);
        }
    }
}
