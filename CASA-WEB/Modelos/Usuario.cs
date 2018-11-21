using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CASA_WEB.Modelos
{
    public class Usuario
    {
        public int codigo_usuario { get; set; }
        public String username { get; set; }
        public String password { get; set; }
        public int perfil_codigo { get; set; }
        public int estadousuario_codigo { get; set; }
    }
}