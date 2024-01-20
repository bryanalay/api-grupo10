using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Xml.Linq;
using TallerTecnico;

namespace api_grupo10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : Controller
    {
        private readonly IConfiguration Configuration;
        public UsuariosController(IConfiguration configuracion)
        {
            Configuration = configuracion;
        }


        [Route("[action]")]
        [HttpGet]
        [Authorize]
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
                    Console.WriteLine("Error: ", ex.ToString());
                }
            }

            return Ok(userList);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<RespuestaLeyenda>> UsuarioLogin(Usuario usr)
        {
            var cadenaConexion = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("ConnectionStrings")["Conexion"];

            //Usuario inv = new Usuario
            //{
            //    Nombre = nombre,
            //    Password = password,
            //    Transaccion = transaction,
            //};

            XDocument xmlParam = Shared.DBXmlMethods.GetXml(usr);
            DataSet dbResult = await Shared.DBXmlMethods.EjecutaBase("GetUsuario", cadenaConexion, usr.Transaccion, xmlParam.ToString());
            List<RespuestaLeyenda> msgList = new List<RespuestaLeyenda>();


            if (dbResult.Tables.Count > 0)
            {
                try
                {
                    if (dbResult.Tables[0].Rows.Count > 0)
                    {
                        Console.WriteLine(usr.Nombre);
                        Console.WriteLine(usr.Password);
                        string tk = CrearToken(usr);
                        Console.WriteLine("este es el token",tk);
                        return Ok(JsonConvert.SerializeObject(tk));
                    }
                    else
                    {
                        RespuestaLeyenda objRes = new RespuestaLeyenda();
                        objRes.Leyenda = "Error en las credenciales de acceso";
                        objRes.Respuesta = "Error";
                    }
                    //Usuario user = new Usuario
                    //{

                    //};
                    //foreach (DataRow row in dbResult.Tables[0].Rows)
                    //{
                    //    Console.WriteLine(dbResult.Tables[0].Rows.Count.ToString());
                    //    RespuestaLeyenda invent = new()
                    //    {
                    //        Respuesta = row["respuesta"].ToString(),
                    //        Leyenda = row["leyenda"].ToString(),
                    //    };
                    //    Console.WriteLine(invent);
                    //    msgList.Add(invent);
                    //}
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: "+ ex.ToString());
                }
            }

            return Ok();
        }

        private string CrearToken(Usuario usuario)
        {
            var claims = new List<Claim>();

            if (usuario.Id != null)
                claims.Add(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));

            if (!string.IsNullOrEmpty(usuario.Cedula))
                claims.Add(new Claim(ClaimTypes.Name, usuario.Cedula));

            // Agrega más reclamaciones según sea necesario

            if (claims.Count == 0)
            {
                // No hay reclamaciones válidas, puedes manejar esto según tus necesidades
                return string.Empty;
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
