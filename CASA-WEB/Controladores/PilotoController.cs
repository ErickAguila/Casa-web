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
    public class PilotoController : Controller
    {
        // GET: Piloto
        public ActionResult Index()
        {
            ViewBag.nombre = Session["NOMBRE"].ToString();
            return View();
        }

        public ActionResult Vuelos()
        {
            ViewBag.codigo = Convert.ToInt32(Session["CODIGO_USUARIO"]);
            ViewBag.nombre = Session["NOMBRE"].ToString();
            return View();
        }

        public ActionResult AeronavesDisponibles()
        {
            ViewBag.nombre = Session["NOMBRE"].ToString();
            ViewBag.codigo = Convert.ToInt32(Session["CODIGO_USUARIO"]);
            return View();
        }

        public String ListarVuelos(int rutPiloto)
        {
            try
            {
                Conexion objConexion = new Conexion();
                OracleConnection cn = objConexion.getConexion();
                cn.Open();

                OracleCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "WEB_PKG.PRC_HORA_VUELO";

                cmd.Parameters.Add("p_rut_piloto", OracleDbType.Int32, rutPiloto, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
                cmd.ExecuteNonQuery();

                OracleRefCursor cursor = (OracleRefCursor)cmd.Parameters["p_cursor"].Value;
                OracleDataReader dr = cursor.GetDataReader();

                List<Vuelo> ListVuelos = new List<Vuelo>();
                while (dr.Read())
                {
                    Vuelo v = new Vuelo();
                    v.rut_piloto = Convert.ToInt32(dr["RUT_PILOTO"]);
                    v.rut_copiloto = Convert.ToInt32(dr["RUT_COPILOTO"]);
                    v.aeronave_matricula = dr["AERONAVE_MATRICULA"].ToString();
                    v.fecha_vuelo = Convert.ToDateTime(dr["FECHA_VUELO"]);
                    v.condicion_vuelo = dr["CONDICION_VUELO"].ToString();
                    v.cant_horas_total = Convert.ToInt32(dr["CANT_HORAS_TOTAL"]);
                    v.cant_horas_piloto = Convert.ToInt32(dr["CANT_HORAS_PILOTO"]);
                    v.cant_horas_copiloto = Convert.ToInt32(dr["CANT_HORAS_COPILOTO"]);
                    v.origen = dr["ORIGEN"].ToString();
                    v.destino = dr["DESTINO"].ToString();
                    v.mision = dr["MISION"].ToString();
                    ListVuelos.Add(v);
                }
                cn.Close();
                cmd.Dispose();
                cn.Dispose();
                objConexion = null;
                return JsonConvert.SerializeObject(ListVuelos).ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public String ListarAeronavesDisponibles()
        {
            try
            {
                Conexion objConexion = new Conexion();
                OracleConnection cn = objConexion.getConexion();
                cn.Open();

                OracleCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "WEB_PKG.PRC_AERONAVE_DISPONIBLE";
                cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
                cmd.ExecuteNonQuery();

                OracleRefCursor cursor = (OracleRefCursor)cmd.Parameters["p_cursor"].Value;
                OracleDataReader dr = cursor.GetDataReader();
                
                List<Aeronave> listAeronave = new List<Aeronave>();
                while (dr.Read())
                {
                    Aeronave aero = new Aeronave();
                    aero.matricula = dr["MATRICULA"].ToString();
                    aero.cant_horas_vuelo = Convert.ToInt32(dr["CANT_HORAS_VUELO"]);
                    aero.f_inspeccion = Convert.ToDateTime(dr["F_INSPECCION"]);
                    aero.f_aeronavegabilidad = Convert.ToDateTime(dr["F_AERONAVEGABILIDAD"]);
                    aero.anio_fabricacion = Convert.ToInt32(dr["ANIO_FABRICACION"]);
                    aero.descrip_tipoaeronave = dr["descrip_tipoaeronave"].ToString();
                    aero.DESCRIP_ESTADOAERONAVE = dr["DESCRIP_ESTADOAERONAVE"].ToString();
                    listAeronave.Add(aero);
                }                
                cn.Close();
                cmd.Dispose();
                cn.Dispose();
                objConexion = null;
                return JsonConvert.SerializeObject(listAeronave).ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int ObtenerUsuario(int codigo)
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
                return (int)per.rut;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}