using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using ToolBox.Database;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConnection _connection;

        public AuthController(IConnection connection)
        {
            _connection = connection;
        }

        [Route("register")]
        public ActionResult Register([FromBody]RegisterInfo registerInfo)
        {
            try
            {
                if (!(registerInfo is null) && ModelState.IsValid)
                {
                    Command command = new Command("SP_RegisterUser", true);
                    command.AddParameter("Nom", registerInfo.Nom);
                    command.AddParameter("Prenom", registerInfo.Prenom);
                    command.AddParameter("Email", registerInfo.Email);
                    command.AddParameter("Passwd", registerInfo.Passwd);

                    int rowCount = _connection.ExecuteNonQuery(command);
                    return NoContent();
                }
            }
            catch (SqlException ex)
            {
                return BadRequest(new { ErrorMessage = "Error with Sql Server!!" });
            }            

            return BadRequest();
        }

        [Route("login")]
        public ActionResult Login([FromBody]LoginInfo loginInfo)
        {
            try
            { 
                if (!(loginInfo is null) && ModelState.IsValid)
                {
                    Command command = new Command("SP_LoginUser", true);
                    command.AddParameter("Email", loginInfo.Email);
                    command.AddParameter("Passwd", loginInfo.Passwd);

                    User user = _connection.ExecuteReader(command, dr => new User() { Id = (int)dr["Id"], Nom = (string)dr["Nom"], Prenom = (string)dr["Prenom"], Email = (string)dr["Email"], IsAdmin = (bool)dr["IsAdmin"] }).SingleOrDefault();

                    if (user is null)
                        return NoContent();

                    return Ok(user);
                }
            }
            catch (SqlException ex)
            {
                return BadRequest(new { ErrorMessage = "Error with Sql Server!!" });
            }  

            return BadRequest();
        }
    }
}