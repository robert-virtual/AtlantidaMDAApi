using AtlantidaMDAApi.Helpers;
using AtlantidaMDAApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
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
        public HttpResponseMessage login([FromBody] Credentials credentials)
        {
            if (credentials.username != "rober" || credentials.password != "pass")
            {
                return Request.CreateResponse(
                    HttpStatusCode.Unauthorized,
                    new LoginRes() { error = "Credenciales incorrectas" }
                );
            }
            return Request.CreateResponse(
                HttpStatusCode.OK,
                new LoginRes()
                {
                    user = new User()
                    {
                        username = credentials.username
                    },
                    token = JWT.generateToken(credentials.username)
                }
            );
        }

        [HttpGet]
        [Route("check-token")]
        public bool checkToken()
        {
            return true;
        }

    }
}
