using AtlantidaMDAApi.Helpers;
using AtlantidaMDAApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AtlantidaMDAApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ViviendaController : ApiController
    {

        // POST: api/Vivienda
        [HttpPost]
        public HttpResponseMessage cotizar([FromBody] CotizarViviendaReq req)
        {
            // TODO: encriptar peticion
            // TODO: enviar peticion a servicio SOAP

            // TODO: desencriptar respuesta
            // TODO: retornar resultado

            return Request.CreateResponse(HttpStatusCode.Created, req);
        }
        [HttpPost]
        public async Task<HttpResponseMessage> documentacionCotizacion()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorRes() { error = "Header 'content-type' erroneo el header debe tener el valor de 'multipart/form-data'" });
            }
            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            List<string> base64Files = new List<string>();
            foreach (var file in provider.Contents)
            {
                //var filename = file.Headers.ContentDisposition.FileName;
                var binaryFileData = await file.ReadAsByteArrayAsync();
                base64Files.Add(Convert.ToBase64String(binaryFileData));

            }
            return Request.CreateResponse(HttpStatusCode.OK, base64Files);

        }
        // POST: api/Vivienda
        [HttpPost]
        public HttpResponseMessage cotizarEncrypted([FromBody] string encryptedData)
        {
            string decryptedData = Crypto.decrypt(encryptedData);
            // TODO: encriptar peticion
            // TODO: enviar peticion a servicio SOAP

            // TODO: desencriptar respuesta
            // TODO: retornar resultado
            return Request.CreateResponse(HttpStatusCode.Created, decryptedData);
        }
        [HttpGet]
        public HttpResponseMessage GetSolicitudesCotizadas()
        {
            List<SolicitudCotizada> solicitudesCotizadas = new List<SolicitudCotizada>();
            for (int i = 0; i <= 10; i++)
            {
                solicitudesCotizadas.Add(new SolicitudCotizada()
                {
                    NumeroCotizacion = $"{1 + i}",
                    Producto = $"Producto {1 + i}",
                    Identificacion = $"070320010123{i}",
                    NombreSolicitante = i % 2 == 0 ? "Emerson Castillo" : "Daniel Valle",
                    MontoSolicitado = $"{1000 + i}",
                    Plazo = $"{i + 1} meses",
                    TasaInteres = $"{10 + i}%",
                    EjecutivoEncargado = "Roberto Castillo",
                    EstadoCotizacion = "Cotizado",
                    ResultadoEvaluacion = "Empty",
                    FechaCreacion = DateTime.UtcNow.AddDays(-i)
                });
            }
            return Request.CreateResponse(HttpStatusCode.OK, solicitudesCotizadas);
        }
        [HttpGet]
        public HttpResponseMessage GetSolicitudesIngresadas()
        {
            List<SolicitudCotizada> solicitudesIngresadas = new List<SolicitudCotizada>();
            for (int i = 0; i <= 10; i++)
            {
                solicitudesIngresadas.Add(new SolicitudCotizada()
                {
                    NumeroCotizacion = $"{1 + i}",
                    Producto = $"Producto {1 + i}",
                    Identificacion = $"070320010123{i}",
                    NombreSolicitante = i % 2 == 0 ? "Daysi Castellanos" : "Iris Castellanos",
                    MontoSolicitado = $"{1000 + i}",
                    Plazo = $"{i + 1} meses",
                    TasaInteres = $"{10 + i}%",
                    EjecutivoEncargado = "Roberto Castillo",
                    EstadoCotizacion = "Ingresada",
                    ResultadoEvaluacion = "Empty",
                    FechaCreacion = DateTime.UtcNow.AddDays(-i)
                });
            }
            return Request.CreateResponse(HttpStatusCode.OK, solicitudesIngresadas);
        }
        [HttpGet]
        public HttpResponseMessage GetNotificacionesCotizaciones()
        {
            List<Notificacion> historialCotizadas = new List<Notificacion>();
            for (int i = 0; i <= 10; i++)
            {
                historialCotizadas.Add(new Notificacion()
                {
                    Cotizacion = $"{1 + i}",
                    TituloNotificacion = $"Prueba {1 + i}",
                    Fecha = DateTime.UtcNow,
                    Detalle = $"Detalle {1 + i}",
                    MensajesPendientes = $"{1 + i}",
                });
            }
            return Request.CreateResponse(HttpStatusCode.OK, historialCotizadas);
        }
        [HttpGet]
        public HttpResponseMessage GetNotificacionesGenerales()
        {
            List<Notificacion> historialCotizadas = new List<Notificacion>();
            for (int i = 0; i <= 10; i++)
            {
                historialCotizadas.Add(new Notificacion()
                {
                    TituloNotificacion = $"Prueba {1 + i}",
                    Fecha = DateTime.UtcNow
                });
            }
            return Request.CreateResponse(HttpStatusCode.OK, historialCotizadas);
        }
        [HttpGet]
        public HttpResponseMessage GetHistorialCotizaciones()
        {
            List<SolicitudCotizada> historialCotizadas = new List<SolicitudCotizada>();
            for (int i = 0; i <= 10; i++)
            {
                historialCotizadas.Add(new SolicitudCotizada()
                {
                    NumeroCotizacion = $"{1 + i}",
                    Producto = $"Producto {1 + i}",
                    Identificacion = $"070320010123{i}",
                    NombreSolicitante = i % 2 == 0 ? "Ivis Valle" : "Angela Valle",
                    MontoSolicitado = $"{1000 + i}",
                    Plazo = $"{i + 1} meses",
                    TasaInteres = $"{10 + i}%",
                    EjecutivoEncargado = "Roberto Castillo",
                    EstadoCotizacion = "Concluida",
                    ResultadoEvaluacion = "Empty",
                    FechaCreacion = DateTime.UtcNow
                });
            }
            return Request.CreateResponse(HttpStatusCode.OK, historialCotizadas);
        }

        [HttpGet]
        public HttpResponseMessage mensajeria()
        {
            List<Mensaje> solicitudesCotizadas = new List<Mensaje>();
            for (int i = 0; i <= 10; i++)
            {
                solicitudesCotizadas.Add(new Mensaje()
                {
                    Cotizacion = $"{1 + i}",
                    Usuario = i % 2 == 0 ? "Roberto Castillo" : "Daniel Valle",
                    Titulo = $"Titulo {1 + i}",
                    Fecha = DateTime.UtcNow,
                    Estado = $"Estado {1 + i}",
                    Detalle = $"Detalle {1 + i}",
                    Nuevo = i % 2 == 0,
                });
            }
            return Request.CreateResponse(HttpStatusCode.OK, solicitudesCotizadas);
        }

        [HttpGet]
        public HttpResponseMessage ping()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "ping");
        }
    }
}
