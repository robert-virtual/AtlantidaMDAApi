using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtlantidaMDAApi.Models
{
    public class Persona
    {
        public string numeroIdentificacion { get; set; }
        public string tipoIdentificacion { get; set; }
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public string telefono { get; set; }
        public string celular { get; set; }
        public string correoElectronico { get; set; }
        public string zona { get; set; }
        public string departamento { get; set; }
        public string municipio { get; set; }
        public string direccionDomicilio { get; set; }
        public string fechaInicioTrabajo { get; set; }
        public string tipoIngreso { get; set; }
        // si tipoIngreso=asalariado
        public string salariosAnuales { get; set; }
        public string salarioBase { get; set; }
        public bool otrosIngresos { get; set; }
        public string horasExtra { get; set; }
        public string comiciones { get; set; }
        public string honorariosAlquileres { get; set; }
        public string remesas { get; set; }
        // si tipoIngreso=asalariado

        // si tipoIngreso=comerciante individual
        public string utilidadNeta { get; set; }
        public string documento { get; set; }
        // si tipoIngreso=comerciante individual
    }
}