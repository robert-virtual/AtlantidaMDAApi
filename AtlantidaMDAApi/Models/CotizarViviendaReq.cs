using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtlantidaMDAApi.Models
{
    public class CotizarViviendaReq
    {
        public Persona persona { get; set; }
        public Persona codeudor { get; set; }
        public Persona fiador { get; set; }
        public Solicitud solicitud { get; set; }


    }
}