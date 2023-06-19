using AtlantidaMDAApi.Helpers;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace AtlantidaMDAApi.Filters
{
    public class JwtValidationHandler:DelegatingHandler
    {
        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> values;
            if (!request.Headers.TryGetValues("Authorization", out values) || values.Count() > 1)
                return false;
            string str = values.ElementAt(0);
            token = str.StartsWith("Bearer ") ? str.Substring(7) : str;
            return true;
        }

        protected override Task<HttpResponseMessage> SendAsync(
          HttpRequestMessage request,
          CancellationToken cancellationToken)
        {
            string token;
            HttpStatusCode statusCode;
            if (!TryRetrieveToken(request, out token))
            {
                statusCode = HttpStatusCode.Unauthorized;
                return base.SendAsync(request, cancellationToken);
            }
            try
            {
                string jwtSecretKey = ConfigurationManager.AppSettings["JWT_SECRET"];
                string jwtAudience = ConfigurationManager.AppSettings["JWT_AUDIENCE"];
                string jetIssuer = ConfigurationManager.AppSettings["JWT_ISSUER"];
                SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Convert.FromBase64String(jwtSecretKey));
                JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = jwtAudience,
                    ValidIssuer = jetIssuer,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = symmetricSecurityKey
                };
                SecurityToken validatedToken;
                Thread.CurrentPrincipal = securityTokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                HttpContext.Current.User = securityTokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                return base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException ex)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            catch (Exception ex)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(statusCode));
        }

        public bool LifetimeValidator(
          DateTime? notBefore,
          DateTime? expires,
          SecurityToken securityToken,
          TokenValidationParameters validationParameters)
        {
            if (expires.HasValue)
            {
                DateTime utcNow = DateTime.UtcNow;
                DateTime? nullable = expires;
                if ((nullable.HasValue ? (utcNow < nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
                    return true;
            }
            return false;
        }

    }
}