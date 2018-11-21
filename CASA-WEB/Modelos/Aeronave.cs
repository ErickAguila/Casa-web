using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CASA_WEB.Modelos
{
    public class Aeronave
    {
        public string matricula { get; set; }
        public int cant_horas_vuelo { get; set; }
        public DateTime f_inspeccion { get; set; }
        public DateTime f_aeronavegabilidad { get; set; }
        public int anio_fabricacion { get; set; }
        public string descrip_tipoaeronave { get; set; }
        public string DESCRIP_ESTADOAERONAVE { get; set; }
        
    }
}