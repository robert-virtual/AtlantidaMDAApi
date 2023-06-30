using AtlantidaMDAApi.Helpers;
using AtlantidaMDAApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.Json;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Routing;
using System.Web.WebPages;

namespace AtlantidaMDAApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthController : ApiController
    {
        // POST: auth/login
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public HttpResponseMessage login([FromBody] string encryptedData)
        {

            string decryptedData = Crypto.decrypt(encryptedData);
            Credentials credentials = JsonSerializer.Deserialize<Credentials>(decryptedData);
            if (credentials.username != "rober" || credentials.password != "pass")
            {
                return Request.CreateResponse(
                    HttpStatusCode.Unauthorized,
                    new LoginRes() { error = "Credenciales incorrectas" }
                );
            }

            //CotizarViviendaReq
            string json = JsonSerializer.Serialize(new LoginRes()
            {
                user = new User()
                {
                    username = credentials.username
                },
                token = JWT.generateToken(credentials.username)
            });
            return Request.CreateResponse(
                HttpStatusCode.OK,
                Crypto.encrypt(json)

            );
        }

        [HttpGet]
        [Route("check-token")]
        public string checkToken()
        {
            return Crypto.encrypt("true");
        }

    }
}
