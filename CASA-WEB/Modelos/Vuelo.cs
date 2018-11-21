using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CASA_WEB.Modelos
{
    public class Vuelo
    {
        public int codigo_vuelo { get; set; }
        public int rut_piloto { get; set; }
        public int rut_copiloto { get; set; }
        public string aeronave_matricula { get; set; }
        public DateTime fecha_vuelo { get; set; }
        public string condicion_vuelo { get; set; }
        public int cant_horas_total { get; set; }
        public int cant_horas_piloto { get; set; }
        public int cant_horas_copiloto { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public string mision { get; set; }
    }
}