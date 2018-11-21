using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CASA_WEB.Modelos;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Reflection;
using Newtonsoft.Json;

namespace CASA_WEB.Controladores
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }
        
        public String Login(string username, string password)
        {
            try
            {
                Conexion objConexion = new Conexion();
                OracleConnection cn = objConexion.getConexion();
                cn.Open();

                OracleCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USUARIO_PKG.PRC_LOGIN_USUARIO";

                cmd.Parameters.Add("p_usuario", OracleDbType.Varchar2, username, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add("p_password", OracleDbType.Varchar2, password, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
                cmd.ExecuteNonQuery();

                OracleRefCursor cursor = (OracleRefCursor)cmd.Parameters["p_cursor"].Value;
                OracleDataReader dr = cursor.GetDataReader();

                Usuario usu = new Usuario();
                while (dr.Read())
                {
                    usu.codigo_usuario = Convert.ToInt32(dr["CODIGO_USUARIO"]);
                    usu.username = dr["USERNAME"].ToString();
                    usu.password = dr["PASSWORD"].ToString();
                    usu.estadousuario_codigo = Convert.ToInt32(dr["ESTADOUSUARIO_CODIGO"]);
                    usu.perfil_codigo = Convert.ToInt32(dr["PERFIL_CODIGO"]);
                }
                if (usu.codigo_usuario > 0)
                {
                    cn.Close();
                    cmd.Dispose();
                    cn.Dispose();
                    objConexion = null;
                    Session["CODIGO_USUARIO"] = usu.codigo_usuario;
                    ObtenerDatosUsuario(usu.codigo_usuario);
                    return JsonConvert.SerializeObject(usu).ToString();
                }
                else
                {
                    cn.Close();
                    cmd.Dispose();
                    cn.Dispose();
                    objConexion = null;
                    return "error";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ObtenerDatosUsuario(int codigo)
        {
            try
            {
                Conexion objConexion = new Conexion();
                OracleConnection cn = objConexion.getConexion();
                cn.Open();

                OracleCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "WEB_PKG.PRC_OBTENER_USUARIO";

                cmd.Parameters.Add("p_codigo_usuario", OracleDbType.Int32, codigo, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
                cmd.ExecuteNonQuery();

                OracleRefCursor cursor = (OracleRefCursor)cmd.Parameters["p_cursor"].Value;
                OracleDataReader dr = cursor.GetDataReader();

                Persona per = new Persona();
                while (dr.Read())
                {
                    per.rut = Convert.ToInt32(dr["RUT"]);
                    per.nombre = dr["nombre"].ToString();
                    per.a_paterno = dr["A_PATERNO"].ToString();
                }
                cn.Close();
                cmd.Dispose();
                cn.Dispose();
                objConexion = null;
                Session["NOMBRE"] = per.nombre + " " + per.a_paterno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public String CerrarSesion()
        {
            try
            {
                Session.Clear();
                Session.Abandon();
                return "ok";
            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message);
            }
        }

    }
}