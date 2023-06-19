using System;

namespace AtlantidaMDAApi.Models
{
    internal class Notificacion
    {
        public Notificacion()
        {
        }

        public string TituloNotificacion { get; set; }
        public string Cotizacion { get; set; }
        public string Detalle { get; set; }
        public string MensajesPendientes { get; set; }
        public DateTime Fecha { get; set; }
    }
}