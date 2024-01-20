using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Xml.Linq;
using TallerTecnico;

namespace api_grupo10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventarioController : Controller
    {
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<InventarioPiezas>> InventarioTr(string transaction)
        {
            var cadenaConexion = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("ConnectionStrings")["Conexion"];

            InventarioPiezas inv = new InventarioPiezas
            {
                Transaccion = transaction
            };

            XDocument xmlParam = Shared.DBXmlMethods.GetXml(inv);
            DataSet dbResult = await Shared.DBXmlMethods.EjecutaBase("GetInventario", cadenaConexion,transaction,xmlParam.ToString());
            List<InventarioPiezas> invList = new List<InventarioPiezas>();

            if(dbResult.Tables.Count > 0)
            {
                try
                {
                    foreach(DataRow row in dbResult.Tables[0].Rows)
                    {
                        Console.WriteLine(dbResult.Tables[0].Rows.Count.ToString());
                        InventarioPiezas invent = new InventarioPiezas
                        {
                            Id = Convert.ToInt32(row["id"]),
                            Nombre = row["nombre"].ToString(),
                            Descripcion = row["descripcion"].ToString(),
                            Cantidad = Convert.ToInt32(row["cantidad"])
                        };
                        invList.Add(invent);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error: ",ex);
                }
            }

            return Ok(invList);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<RespuestaLeyenda>> PostInventario(InventarioPiezas inv)
        {
            var cadenaConexion = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("ConnectionStrings")["Conexion"];

            XDocument xmlParam = Shared.DBXmlMethods.GetXml(inv);
            DataSet dbResult = await Shared.DBXmlMethods.EjecutaBase("GetInventario", cadenaConexion, inv.Transaccion, xmlParam.ToString());
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
