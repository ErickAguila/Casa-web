using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CASA_WEB.Modelos
{
    public class Persona
    {
        public int rut { get; set; }
        public string dv { get; set; }
        public string nombre { get; set; }
        public string a_paterno { get; set; }
        public string a_materno { get; set; }
        public DateTime f_nacimiento { get; set; }
        public string correo { get; set; }
        public int fono { get; set; }
        public int usuario_codigo { get; set; }
        public int estadopersona_codigo { get; set; }
    }
}