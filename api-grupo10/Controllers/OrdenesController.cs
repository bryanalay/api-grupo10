using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Xml.Linq;
using TallerTecnico;

namespace api_grupo10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdenesController : Controller
    {
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<Orden>> OrdenesTr(string transaction)
        {
            var cadenaConexion = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("ConnectionStrings")["Conexion"];

            Orden inv = new Orden
            {
                Transaccion = transaction
            };

            XDocument xmlParam = Shared.DBXmlMethods.GetXml(inv);
            DataSet dbResult = await Shared.DBXmlMethods.EjecutaBase(Shared.StoredProcedures.getOrdenes, cadenaConexion, transaction, xmlParam.ToString());
            List<Orden> ordList = new List<Orden>();

            if (dbResult.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dbResult.Tables[0].Rows)
                    {
                        Console.WriteLine(dbResult.Tables[0].Rows.Count.ToString());
                        Orden invent = new Orden
                        {
                            Id = Convert.ToInt32(row["id"]),
                            Tarea = row["tarea"].ToString(),
                            Fecha = row["fecha"].ToString(),
                            Estado = row["estado"].ToString(),
                            Cliente = row["cliente"].ToString(),
                            EmpleadoAsignado = row["empleado_asignado"].ToString(),

                        };
                        ordList.Add(invent);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: ", ex);
                }
            }

            return Ok(ordList);
        }

        [Route("[action]")]
        [HttpDelete]
        public async Task<ActionResult<RespuestaLeyenda>> DeleteOrder(string transaction,int id)
        {
            var cadenaConexion = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("ConnectionStrings")["Conexion"];

            Orden inv = new Orden
            {
                Id = id,
                Transaccion = transaction
            };

            XDocument xmlParam = Shared.DBXmlMethods.GetXml(inv);
            DataSet dbResult = await Shared.DBXmlMethods.EjecutaBase(Shared.StoredProcedures.getOrdenes, cadenaConexion, transaction, xmlParam.ToString());
            List<RespuestaLeyenda> msgList = new List<RespuestaLeyenda>();

            if (dbResult.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dbResult.Tables[0].Rows)
                    {
                        Console.WriteLine(dbResult.Tables[0].Rows.Count.ToString());
                        RespuestaLeyenda invent = new RespuestaLeyenda
                        {
                            Respuesta = row["respuesta"].ToString(),
                            Leyenda = row["leyenda"].ToString()

                        };
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

        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<RespuestaLeyenda>> PostOrden(Orden ord)
        {
            var cadenaConexion = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("ConnectionStrings")["Conexion"];

            XDocument xmlParam = Shared.DBXmlMethods.GetXml(ord);
            DataSet dbResult = await Shared.DBXmlMethods.EjecutaBase(Shared.StoredProcedures.getOrdenes, cadenaConexion, ord.Transaccion, xmlParam.ToString());
            List<RespuestaLeyenda> msgList = new List<RespuestaLeyenda>();

            if (dbResult.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dbResult.Tables[0].Rows)
                    {
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
                    Console.WriteLine("Error en api: ", ex.ToString());
                }
            }
            return Ok(msgList);
        }

        [Route("[action]")]
        [HttpPatch]
        public async Task<ActionResult<RespuestaLeyenda>> UpdateOrden(Orden ord)
        {
            var cadenaConexion = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("ConnectionStrings")["Conexion"];

            XDocument xmlParam = Shared.DBXmlMethods.GetXml(ord);
            DataSet dbResult = await Shared.DBXmlMethods.EjecutaBase(Shared.StoredProcedures.getOrdenes, cadenaConexion, ord.Transaccion, xmlParam.ToString());
            List<RespuestaLeyenda> msgList = new List<RespuestaLeyenda>();


            if (dbResult.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dbResult.Tables[0].Rows)
                    {
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
                    Console.WriteLine("Error en api: ", ex.ToString());
                }
            }
            return Ok(msgList);
        }
    }
}
