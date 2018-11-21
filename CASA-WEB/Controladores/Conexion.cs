using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Oracle.ManagedDataAccess.Client;
using System.Configuration;
namespace CASA_WEB.Controladores
{
    public class Conexion
    {
        private OracleConnection cn { get; set; }
        public OracleConnection getConexion()
        {
            if (cn == null)
            {
                string conexion = System.Configuration.ConfigurationManager.AppSettings["CONEXION"].ToString();
                cn = new OracleConnection(conexion);
            }
            return cn;
        }
    }
}