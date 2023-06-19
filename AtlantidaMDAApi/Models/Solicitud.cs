using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtlantidaMDAApi.Models
{
    public class Solicitud
    {

        public string origenFondos { get; set; }
        public string programaFondos { get; set; }
        public string destino { get; set; }
        public string valorTerreno { get; set; }
        public string valorMejorasActuales { get; set; }
        public string precioVenta { get; set; }
        public string montoPresupuestoConstruccion { get; set; }
        public string montoPrima { get; set; }
        public string fechaPrimerPago { get; set; }
    }
}