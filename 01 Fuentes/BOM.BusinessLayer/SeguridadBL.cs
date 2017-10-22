using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BOM.EntityLayer;
using BOM.DataLayer;

namespace BOM.BusinessLayer
{
    public interface ISeguridadBL
    {
        SGA_T_MENU[] ObtenerMenuPadres(string prmstr_codUsuario, int prmint_codSistema);
        SGA_T_MENU[] ObtenerMenuHijos(string prmstr_codUsuario, int prmint_codMenuPadre, int prmint_codSistema);
        string VerificarAccesso(string prmstrUsername, int prmint_codSistema);
        int PermisoUsuarioConsultar(string user, string aspPath);
        SGA_SP_OBTENER_MENU_X_USUARIO_Result f_ObtenerMenuxUsuarioBL(string sUserRed, string sRutaPagina);
        List<SGA_SP_VALIDAR_USUARIO_SELECCIONAR_Result> f_ObtenerUsuarioBL(SGA_T_USUARIO objUsuario, int pi_Sistema);
        int f_ObtenerPerfilBL(string ps_cCod_Id, int pi_SistemaID);
        List<SGA_SP_VALIDAR_USUARIO_DA_SELECCIONAR_Result> f_ObtenerUsuarioDA_BL(SGA_T_USUARIO objUsuario, int pi_Sistema);
        string f_ObtenerCodigoUsuarioBL(string s_usua_c_cusu_red);
    }

    public class SeguridadBL : ISeguridadBL
    {
        public void Dispose()
        {
            GC.Collect();
        }
        public SGA_T_MENU[] ObtenerMenuPadres(string prmstr_codUsuario, int prmint_codSistema)
        {
            ISeguridadDA objDSeguridad = null;

            try
            {
                objDSeguridad = new SeguridadDA();
                return objDSeguridad.ObtenerMenuPadres(prmstr_codUsuario, prmint_codSistema);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDSeguridad.Dispose();
            }
        }
        public SGA_T_MENU[] ObtenerMenuHijos(string prmstr_codUsuario, int prmint_codMenuPadre, int prmint_codSistema)
        {
            ISeguridadDA objDSeguridad = null;

            try
            {
                objDSeguridad = new SeguridadDA();
                return objDSeguridad.ObtenerMenuHijos(prmstr_codUsuario, prmint_codMenuPadre, prmint_codSistema);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDSeguridad.Dispose();
            }
        }
        public string VerificarAccesso(string prmstrUsername, int prmint_codSistema)
        {
            ISeguridadDA objDSeguridad = null;

            try
            {
                objDSeguridad = new SeguridadDA();
                return objDSeguridad.VerificarAccesso(prmstrUsername, prmint_codSistema);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDSeguridad.Dispose();
            }
        }
        public int PermisoUsuarioConsultar(string user, string aspPath)
        {
            ISeguridadDA objDSeguridad = null;

            try
            {
                objDSeguridad = new SeguridadDA();
                return objDSeguridad.PermisoUsuarioConsultar(user, aspPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDSeguridad.Dispose();
            }
        }
        public SGA_SP_OBTENER_MENU_X_USUARIO_Result f_ObtenerMenuxUsuarioBL(string sUserRed, string sRutaPagina)
        {
            ISeguridadDA objDSeguridad = null;

            try
            {
                objDSeguridad = new SeguridadDA();
                return objDSeguridad.f_ObtenerMenuxUsuarioDA(sUserRed, sRutaPagina);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDSeguridad.Dispose();
            }
        }
        public List<SGA_SP_VALIDAR_USUARIO_SELECCIONAR_Result> f_ObtenerUsuarioBL(SGA_T_USUARIO objUsuario, int pi_Sistema)
        {
            return new SeguridadDA().f_ObtenerUsuarioDA(objUsuario, pi_Sistema).ToList();
        }
        public int f_ObtenerPerfilBL(string ps_cCod_Id, int pi_SistemaID)
        {
            return new SeguridadDA().f_ObtenerPerfilDA(ps_cCod_Id, pi_SistemaID);
        }
        public List<SGA_SP_VALIDAR_USUARIO_DA_SELECCIONAR_Result> f_ObtenerUsuarioDA_BL(SGA_T_USUARIO objUsuario, int pi_Sistema)
        {
            return new SeguridadDA().f_ObtenerUsuarioDA_DA(objUsuario, pi_Sistema);
        }
        public string f_ObtenerCodigoUsuarioBL(string s_usua_c_cusu_red)
        {
            return new SeguridadDA().f_ObtenerCodigoUsuarioDA(s_usua_c_cusu_red);
        }
    }
}
