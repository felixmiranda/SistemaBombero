using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Configuration;
using System.Data.Entity;
using System.Data.Objects;
using BOM.EntityLayer;
using System.Data.Objects.DataClasses;
using System.Data;

namespace BOM.DataLayer
{
    public interface ISeguridadDA : IDisposable
    {
        SGA_T_MENU[] ObtenerMenuPadres(string prmstr_codUsuario, int prmint_codSistema);
        SGA_T_MENU[] ObtenerMenuHijos(string prmstr_codUsuario, int prmint_codMenuPadre, int prmint_codSistema);
        string VerificarAccesso(string prmstrUsername, int prmint_codSistema);
        int PermisoUsuarioConsultar(string user, string aspPath);
        SGA_SP_OBTENER_MENU_X_USUARIO_Result f_ObtenerMenuxUsuarioDA(string sUserRed, string sRutaPagina);
        List<SGA_SP_VALIDAR_USUARIO_SELECCIONAR_Result> f_ObtenerUsuarioDA(SGA_T_USUARIO objUsuario, int pi_Sistema);
        int f_ObtenerPerfilDA(string ps_cCod_Id, int pi_SistemaID);
        List<SGA_SP_VALIDAR_USUARIO_DA_SELECCIONAR_Result> f_ObtenerUsuarioDA_DA(SGA_T_USUARIO objUsuario, int pi_Sistema);
        string f_ObtenerCodigoUsuarioDA(string s_usua_c_cusu_red);
    }

    public class SeguridadDA : ISeguridadDA
    {
        public void Dispose()
        {
            GC.Collect();
        }
        /// <summary>
        /// Descripción: Funcion para obtener los menu padres segun el usuario enviado
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 04/09/2015
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="prmstr_codUsuario"></param>
        /// <param name="prmint_codSistema"></param>
        /// <returns></returns>
        public SGA_T_MENU[] ObtenerMenuPadres(string prmstr_codUsuario, int prmint_codSistema)
        {
            SGADB_BOMEntities sgaBEL = new SGADB_BOMEntities();
            List<SGA_T_MENU> lsMenus = null;
            SGA_T_MENU MenuBE = null;

            try
            {
                ObjectResult<SGA_T_MENU> retMenu = sgaBEL.ObtenerMenuPadres(prmstr_codUsuario, prmint_codSistema);

                var _menuPadre = from res in retMenu
                                 select new
                                 {
                                     menu_c_iid = res.menu_c_iid,
                                     menu_c_vnomb = res.menu_c_vnomb,
                                     menu_c_vpag_asp = res.menu_c_vpag_asp,
                                     menu_c_vpag_asp2 = res.menu_c_vpag_asp2,
                                     menu_c_iid_padre = res.menu_c_iid_padre,
                                     menu_c_ynivel = res.menu_c_ynivel,
                                     bita_c_zfec_reg = res.bita_c_zfec_reg,
                                     bita_c_vusu_red_reg = res.bita_c_vusu_red_reg,
                                     bita_c_vnom_completo_reg = res.bita_c_vnom_completo_reg,
                                 };

                lsMenus = new List<SGA_T_MENU>();

                foreach (var item in _menuPadre)
                {
                    MenuBE = new SGA_T_MENU();

                    MenuBE.menu_c_iid = item.menu_c_iid;
                    MenuBE.menu_c_vnomb = item.menu_c_vnomb;
                    MenuBE.menu_c_vpag_asp = item.menu_c_vpag_asp;
                    MenuBE.menu_c_vpag_asp2 = item.menu_c_vpag_asp2;
                    MenuBE.menu_c_iid_padre = item.menu_c_iid_padre;
                    MenuBE.menu_c_ynivel = item.menu_c_ynivel;
                    MenuBE.bita_c_zfec_reg = item.bita_c_zfec_reg;
                    MenuBE.bita_c_vusu_red_reg = item.bita_c_vusu_red_reg;
                    MenuBE.bita_c_vnom_completo_reg = item.bita_c_vnom_completo_reg;

                    lsMenus.Add(MenuBE);
                }

                return lsMenus.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sgaBEL.Dispose();
            }
        }
        /// <summary>
        /// Descripción: Funcion para obtener los menus hijos segun el usuario logueado
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 04/09/0215
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="prmstr_codUsuario"></param>
        /// <param name="prmint_codMenuPadre"></param>
        /// <param name="prmint_codSistema"></param>
        /// <returns></returns>
        public SGA_T_MENU[] ObtenerMenuHijos(string prmstr_codUsuario, int prmint_codMenuPadre, int prmint_codSistema)
        {
            SGADB_BOMEntities sgaBEL = new SGADB_BOMEntities();
            List<SGA_T_MENU> lsMenus = null;
            SGA_T_MENU MenuBE = null;

            try
            {
                ObjectResult<SGA_T_MENU> retMenu = sgaBEL.ObtenerMenuHijos(prmstr_codUsuario, prmint_codMenuPadre, prmint_codSistema);

                var _menuPadre = from res in retMenu
                                 select new
                                 {
                                     menu_c_iid = res.menu_c_iid,
                                     menu_c_vnomb = res.menu_c_vnomb,
                                     menu_c_vpag_asp = res.menu_c_vpag_asp,
                                     menu_c_vpag_asp2 = res.menu_c_vpag_asp2,
                                     menu_c_iid_padre = res.menu_c_iid_padre,
                                     menu_c_ynivel = res.menu_c_ynivel
                                 };

                lsMenus = new List<SGA_T_MENU>();

                foreach (var item in _menuPadre)
                {
                    MenuBE = new SGA_T_MENU();

                    MenuBE.menu_c_iid = item.menu_c_iid;
                    MenuBE.menu_c_vnomb = item.menu_c_vnomb;
                    MenuBE.menu_c_vpag_asp = item.menu_c_vpag_asp;
                    MenuBE.menu_c_vpag_asp2 = item.menu_c_vpag_asp2;
                    MenuBE.menu_c_iid_padre = item.menu_c_iid_padre;
                    MenuBE.menu_c_ynivel = item.menu_c_ynivel;

                    lsMenus.Add(MenuBE);
                }

                return lsMenus.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sgaBEL.Dispose();
            }
        }
        /// <summary>
        /// Descripción: Funcion para verificar el acceso al sistema segun el usuario enviado como parametro
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 04/09/2015
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="prmstrUsername"></param>
        /// <param name="prmint_codSistema"></param>
        /// <returns></returns>
        public string VerificarAccesso(string prmstrUsername, int prmint_codSistema)
        {
            SGADB_BOMEntities sgaBEL = new SGADB_BOMEntities();
            string strUsuario = string.Empty;

            try
            {
                ObjectResult<string> resAcceso = sgaBEL.VerificarAccesoSistema(prmstrUsername, prmint_codSistema);

                var acceso = from res in resAcceso
                             select res;

                foreach (var item in acceso)
                    strUsuario = item.ToString();

                return strUsuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sgaBEL.Dispose();
            }
        }
        /// <summary>
        /// Descripción: Funcion para consultar los permisos que tiene el usuario
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 04/09/2015
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="user"></param>
        /// <param name="aspPath"></param>
        /// <returns></returns>
        public int PermisoUsuarioConsultar(string user, string aspPath)
        {
            try
            {
                using (SGADB_BOMEntities contexto = new SGADB_BOMEntities())
                {
                    return Convert.ToInt16(contexto.SGA_SP_USUARIO_PERMISO_ASP_CONSULTAR(user, aspPath).FirstOrDefault());
                }
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// Descripción: Obtiene el menu por usuario
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 04/09/2015
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="sUserRed"></param>
        /// <param name="sRutaPagina"></param>
        /// <returns></returns>
        public SGA_SP_OBTENER_MENU_X_USUARIO_Result f_ObtenerMenuxUsuarioDA(string sUserRed, string sRutaPagina)
        {
            try
            {
                using (SGADB_BOMEntities contexto = new SGADB_BOMEntities())
                {
                    return contexto.SGA_SP_OBTENER_MENU_X_USUARIO(sUserRed, sRutaPagina).FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Descripción: Funcion para obtener las crdenciales del usuario
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 04/09/2015
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="objUsuario"></param>
        /// <returns></returns>
        public List<SGA_SP_VALIDAR_USUARIO_SELECCIONAR_Result> f_ObtenerUsuarioDA(SGA_T_USUARIO objUsuario, int pi_Sistema)
        {
            using (SGADB_BOMEntities contexto = new SGADB_BOMEntities())
            {
                return contexto.SGA_SP_VALIDAR_USUARIO_SELECCIONAR(objUsuario.usua_c_cusu_red, objUsuario.usua_c_vcontrasena, pi_Sistema).ToList(); ;
            }
        }

        /// <summary>
        /// Descripción: Funcion para obtener el codigo de perfil
        /// Autor: Henry Medina Murayari / RPEXT094
        /// Fecha y Hora Creación: 27/05/2016
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="user"></param>
        /// <param name="aspPath"></param>
        /// <returns></returns>
        public int f_ObtenerPerfilDA(string ps_cCod_Id, int pi_SistemaID)
        {
            int idPerfil;
            using (SGADB_BOMEntities contexto = new SGADB_BOMEntities())
            {
                List<SGA_T_USUARIO_PERFIL> objPerfilUsuario = new List<SGA_T_USUARIO_PERFIL>();
                idPerfil = (from x in contexto.SGA_T_USUARIO_PERFIL
                            join y in contexto.SGA_T_PERFIL
                            on x.perf_c_yid equals y.perf_c_yid
                            where x.usua_c_cdoc_id == ps_cCod_Id
                            && y.sist_c_iid == pi_SistemaID
                            select x).ToList()[0].perf_c_yid;
            }

            return idPerfil;
        }

        /// <summary>
        /// Descripción: Funcion para obtener las crdenciales del usuario DA
        /// Autor: Henry Medina Murayari / RPEXT094
        /// Fecha y Hora Creación: 28/06/2016 - 05:25 p.m
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="objUsuario"></param>
        /// <returns></returns>
        public List<SGA_SP_VALIDAR_USUARIO_DA_SELECCIONAR_Result> f_ObtenerUsuarioDA_DA(SGA_T_USUARIO objUsuario, int pi_Sistema)
        {
            using (SGADB_BOMEntities contexto = new SGADB_BOMEntities())
            {
                return contexto.SGA_SP_VALIDAR_USUARIO_DA_SELECCIONAR(objUsuario.usua_c_cusu_red, pi_Sistema).ToList(); ;
            }
        }

        /// <summary>
        /// Descripción: Funcion para obtener el codigo de usuario cdoc
        /// Autor: Henry Medina Murayari / RPEXT094
        /// Fecha y Hora Creación: 28/06/2016 08:04 p.m.
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="user"></param>
        /// <param name="aspPath"></param>
        /// <returns></returns>
        public string f_ObtenerCodigoUsuarioDA(string s_usua_c_cusu_red)
        {
            string usua_c_cusu_red;

            using (SGADB_BOMEntities contexto = new SGADB_BOMEntities())
            {
                List<SGA_T_USUARIO> objUsuario = new List<SGA_T_USUARIO>();
                usua_c_cusu_red = (from x in contexto.SGA_T_USUARIO
                                   where x.usua_c_cusu_red == s_usua_c_cusu_red
                                   select x).ToList()[0].usua_c_cdoc_id;
            }

            return usua_c_cusu_red;
        }
    }
}
