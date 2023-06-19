using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtlantidaMDAApi.Models
{
    public class SolicitudCotizada
    {
        public string NumeroCotizacion { get; set; }
        public string Producto { get; set; }
        public string Identificacion { get; set; }
        public string NombreSolicitante { get; set; }
        public string MontoSolicitado { get; set; }
        public string Plazo { get; set; }
        public string TasaInteres { get; set; }
        public string EjecutivoEncargado { get; set; }
        public string EstadoCotizacion { get; set; }
        public string ResultadoEvaluacion { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}