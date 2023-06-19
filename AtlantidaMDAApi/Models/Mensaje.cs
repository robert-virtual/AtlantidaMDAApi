using System;

namespace AtlantidaMDAApi.Models
{
    internal class Mensaje
    {
        public Mensaje()
        {
        }

        public string Cotizacion { get; set; }
        public string Usuario { get; set; }
        public string Titulo { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public string Detalle { get; set; }
        public bool Nuevo { get; set; }
    }
}