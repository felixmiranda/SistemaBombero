using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BOM.EntityLayer;
using System.Data.Entity;
using System.Data.Objects;
namespace BOM.DataLayer.Interfaces.Reserve
{
    public interface IReserveSpaceAdvertisingDA
    {
        List<ADV_T_PUB_ELEMENTO_ACTIVACION> f_listar_elementoActivacionXProductoDA(int producto);
        List<ADV_T_PUB_PRODUCTO> f_listar_pub_productosDA();
        List<SP_PUB_RESERVA_DETALLE_XIDESPACIO_LISTAR_Result> f_listar_detalle_ocupacionXelementoEspacioDA(int idespacio,
            int idelemento, DateTime fechaDesde, DateTime fechaHasta);
        List<DIO_PUB_T_TIPO_ASIGNACION> f_listar_pub_tipo_asignacionDA();
        string f_diaUtilVencimientoDA(int pi_diaUtil);
        IList<DIO_SP_EJECUTIVO_BTL_LISTAR_Result> f_listar_ejecutivosBTLDA();
        int f_guardar_reserva_cabeceraDA(DIO_PUB_T_RESERVA obj);
        string f_guardar_reserva_detalleDA(int idCabecera, int id_espacio, int tipo_periodo, string fechaDesde
            , string fechaHasta, string grupoFechasIntermitente, string nombcompletoreg, string usuario_cusured, string ejec_cdoc);
        int f_guardar_reserva_masterDA(DIO_PUB_T_RESERVA_MASTER obj);
        List<SP_PUB_RESERVA_XIDMASTER_LISTAR_Result> f_listar_reservas_xidmasterDA(int reser_mast_c_iid);
        void f_eliminar_espacios_guardadosDA(string codigo_espacios, int id_reserva_master);
        List<DIO_SP_PUB_RESERVA_ENMEMORIA_XIDMASTER_LISTAR_Result> f_listar_reservas_enmemoria_xidmasterDA(int reser_mast_c_iid);
        DIO_PUB_T_RESERVA f_reserva_seleccionar_xidDA(int idreserva);
        List<DIO_SP_PUB_RESERVA_DET_LISTAR_Result> f_listar_detallexReservaDA(int idreserva);
        void f_actualizar_reserva_cabeceraDA(DIO_PUB_T_RESERVA obj);
        List<ADV_T_CLIENTE> f_ListarAgenciaDA();
        List<DIO_PUB_T_MARCA> f_ListarMarcaDA();
        ADV_T_CLIENTE f_seleccionar_clienteDA(string vraz_social);
        ADV_T_CLIENTE f_seleccionar_agenciaDA(string vraz_social);
        DIO_PUB_T_MARCA f_seleccionar_o_registrar_marcaDA(string vnomb_marca,string vusu_red,string nombrecompleto);
        void f_asociar_clienteDA(DIO_PUB_T_RESERVA objreserva);
        IList<DIO_SP_REQUISITO_PUBLICIDAD_LISTAR_Result> f_ListarConfiguracionRequisitosDA();
        int f_cerrar_ReservaDA(int idMaster);
        IList<SP_PUB_ESPACIOS_NO_DISPONIBLE_LISTAR_Result> f_obtenerEspaciosNoDisponiblesDA(string cadena_espacios_sw, DateTime fechaDesde, DateTime fechaHasta);
        void f_modificar_Prioridad_ReservaDA(int idReservaAfecto);

        List<DIO_SP_PUB_RESERVA_PENDIENTE_XID_MASTER_Result> f_listar_reservas_pendientes_xidmasterDA(int idmaster);
        bool f_eliminar_Reserva_CopadaTotalmenteDA(int idReserva);
        bool f_eliminar_Reserva_Master_CopadaTotalmenteDA(int idMaster);

    }

    public class ReserveSpaceAdvertisingDA : IReserveSpaceAdvertisingDA
    {
        public void Dispose()
        {
            GC.Collect();
        }

       public void f_asociar_clienteDA(DIO_PUB_T_RESERVA objreserva)
        {
            try
            {
                using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
                {
                    var query = (from c in contexto.DIO_PUB_T_RESERVA
                                 where c.reser_c_iid == objreserva.reser_c_iid
                                 select c).FirstOrDefault();

                    query.cli_c_vdoc_id = objreserva.cli_c_vdoc_id;
                    query.marc_c_icod = objreserva.marc_c_icod;
                    query.agen_c_vdoc_id = objreserva.agen_c_vdoc_id;
                    query.reser_mast_c_fac_bagencia = objreserva.reser_mast_c_fac_bagencia;
                    contexto.SaveChanges();
                }
            }
            catch (Exception)
            { 
                throw;
            }
        }
        public List<ADV_T_CLIENTE> f_ListarAgenciaDA()
        {
            using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
            {
                return (from x in contexto.DIO_PUB_T_AGENCIA
                        join y in contexto.ADV_T_CLIENTE on x.agen_c_vdoc_id equals y.cli_c_vdoc_id
                        where x.agen_c_bactivo == true && y.cli_c_bactivo == true
                        select  y).ToList();
            }
        }
        public List<DIO_PUB_T_MARCA> f_ListarMarcaDA()
        {
            using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
            {
                return (from x in contexto.DIO_PUB_T_MARCA
                        where x.marc_c_bactivo == true 
                        select x).ToList();
            }
        }

        public ADV_T_CLIENTE f_seleccionar_clienteDA(string vraz_social)
        {
            using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
            {
                return (from x in contexto.ADV_T_CLIENTE
                        where x.cli_c_bactivo == true && x.cli_c_vraz_soc.ToUpper() == vraz_social.ToUpper()
                        select x).FirstOrDefault();
            }
        }
        public ADV_T_CLIENTE f_seleccionar_agenciaDA(string vraz_social)
        {
            using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
            {
                return (from x in contexto.DIO_PUB_T_AGENCIA
                        join y in contexto.ADV_T_CLIENTE on x.agen_c_vdoc_id equals y.cli_c_vdoc_id
                        where x.agen_c_bactivo == true && y.cli_c_bactivo == true && y.cli_c_vraz_soc.ToUpper() == vraz_social.ToUpper()
                        select y).FirstOrDefault();
            }
        }
        public DIO_PUB_T_MARCA f_seleccionar_o_registrar_marcaDA(string vnomb_marca,string vusu_red,string nombrecompleto)
        {
            try 
	        {	        
	           using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
               {
                   var query= (from x in contexto.DIO_PUB_T_MARCA
                               where x.marc_c_bactivo == true && x.marc_c_vnomb.ToUpper() == vnomb_marca.ToUpper()
                               select x).FirstOrDefault();

                   if(query != null){
                       return query;
                   }
                   else{
                       DIO_PUB_T_MARCA obj = new DIO_PUB_T_MARCA();
                       obj.marc_c_vnomb = vnomb_marca.ToUpper();
                       obj.marc_c_bactivo = true;
                       obj.bita_c_zfec_reg = DateTime.Today;
                       obj.bita_c_vusu_red_reg = vusu_red;
                       obj.bita_c_vnomb_completo_reg = nombrecompleto;
                       contexto.AddToDIO_PUB_T_MARCA(obj);
                       contexto.SaveChanges();
                       return f_seleccionar_o_registrar_marcaDA(vnomb_marca, vusu_red, nombrecompleto);
                   }
               }
	        }
	        catch (Exception)
	        {
	        	throw;
	        }
        }
        
        public DIO_PUB_T_RESERVA f_reserva_seleccionar_xidDA(int idreserva)
        {
            using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
            {
                return (from x in contexto.DIO_PUB_T_RESERVA
                        where x.reser_c_iid == idreserva
                        select x).FirstOrDefault();
            }
        }
        public List<ADV_T_PUB_ELEMENTO_ACTIVACION> f_listar_elementoActivacionXProductoDA(int producto)
        {
            try
            {
                using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
                {
                    return (from x in contexto.ADV_T_PUB_ELEMENTO_ACTIVACION
                            where x.pub_elem_act_c_bactivo == true && x.pub_prod_c_iid == producto
                            orderby x.pub_elem_act_c_iid ascending
                            select x).ToList();
                }
            }
            catch { throw; }
        }

        public List<ADV_T_PUB_PRODUCTO> f_listar_pub_productosDA()
        {
            try
            {
                using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
                {
                    return (from x in contexto.ADV_T_PUB_PRODUCTO
                            where x.pub_prod_c_bactivo == true
                            orderby x.pub_prod_c_iid ascending
                            select x).ToList();
                }
            }
            catch { throw; }
        }

        public List<DIO_PUB_T_TIPO_ASIGNACION> f_listar_pub_tipo_asignacionDA()
        {
            try
            {
                using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
                {
                    return (from x in contexto.DIO_PUB_T_TIPO_ASIGNACION
                            where x.tip_asig_c_bactivo == true
                            orderby x.tip_asig_c_iid ascending
                            select x).ToList();
                }
            }
            catch { throw; }
        }


        public List<SP_PUB_RESERVA_DETALLE_XIDESPACIO_LISTAR_Result> f_listar_detalle_ocupacionXelementoEspacioDA(int idespacio,
            int idelemento, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
                {
                    return contexto.SP_PUB_RESERVA_DETALLE_XIDESPACIO_LISTAR(
                        idespacio,
                        idelemento,
                        fechaDesde,
                        fechaHasta).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<DIO_SP_PUB_RESERVA_PENDIENTE_XID_MASTER_Result> f_listar_reservas_pendientes_xidmasterDA(int idmaster)
        {
            try
            {
                using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
                {
                    return contexto.DIO_SP_PUB_RESERVA_PENDIENTE_XID_MASTER(
                        idmaster).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        public IList<SP_PUB_ESPACIOS_NO_DISPONIBLE_LISTAR_Result> f_obtenerEspaciosNoDisponiblesDA(string cadena_espacios_sw, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
                {
                    return contexto.SP_PUB_ESPACIOS_NO_DISPONIBLE_LISTAR(
                        fechaDesde,
                        fechaHasta,
                        cadena_espacios_sw).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<DIO_SP_PUB_RESERVA_ENMEMORIA_XIDMASTER_LISTAR_Result> f_listar_reservas_enmemoria_xidmasterDA(int reser_mast_c_iid)
        {
            try
            {
                using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
                {
                    return contexto.DIO_SP_PUB_RESERVA_ENMEMORIA_XIDMASTER_LISTAR(
                       reser_mast_c_iid).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public List<DIO_SP_PUB_RESERVA_DET_LISTAR_Result> f_listar_detallexReservaDA(int idreserva)
        {
            try
            {
                using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
                {
                    return contexto.DIO_SP_PUB_RESERVA_DET_LISTAR(
                       idreserva).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string f_diaUtilVencimientoDA(int pi_diaUtil)
        {
            try
            {
                ObjectParameter pd_dia_util = new ObjectParameter("dia_util", typeof(string));
                using (BD_DIONISIOEntities c = new BD_DIONISIOEntities())
                {
                    c.SP_ADV_DIA_UTIL_SELECCIONAR(pi_diaUtil, pd_dia_util);
                }
                return pd_dia_util.Value.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        ////////////////////////////

        public IList<DIO_SP_EJECUTIVO_BTL_LISTAR_Result> f_listar_ejecutivosBTLDA()
        {
            try
            {
                using (SGADB_BOMEntities contexto = new SGADB_BOMEntities())
                {
                    return contexto.DIO_SP_EJECUTIVO_BTL_LISTAR().ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SP_PUB_RESERVA_XIDMASTER_LISTAR_Result> f_listar_reservas_xidmasterDA(int reser_mast_c_iid)
        {
            try
            {
                using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
                {
                    return contexto.SP_PUB_RESERVA_XIDMASTER_LISTAR(reser_mast_c_iid).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int f_guardar_reserva_cabeceraDA(DIO_PUB_T_RESERVA obj)
        {
            try
            {
                ObjectParameter pi_cabecera = new ObjectParameter("id_cabecera", typeof(string));
                using (BD_DIONISIOEntities c = new BD_DIONISIOEntities())
                {
                    c.SP_PUB_RESERVA_INSERTAR(
                        obj.reser_mast_c_iid,
                        obj.tip_asig_c_iid,
                        obj.tipo_periodo_c_iid,
                        obj.reser_c_vdim_final,
                        obj.reser_c_eprecio_alquiler,
                        obj.reser_c_vdesc_activacion,
                        obj.reser_c_vcomen_activacion,
                        obj.reser_c_dfech_vencimiento,
                        true,
                        obj.inm_c_icod,
                        obj.pub_esp_c_iid,
                        obj.pub_esp_c_vcod,
                        obj.pub_prod_c_iid,
                        obj.pub_prod_c_vnomb,
                        obj.pub_elem_act_c_iid,
                        obj.pub_esp_c_vmedida,  //obj.pub_esp_c_vmedida, se captura de la grilla
                        obj.pub_esp_c_earea,   //obj.pub_esp_c_earea, se captura de la grilla
                        obj.pub_esp_c_emonto_tarifa_base,   //obj.pub_esp_c_emonto_tarifa_base,    se captura de la grilla
                        obj.pub_esp_c_emonto_tarifa_top,   //obj.pub_esp_c_emonto_tarifa_top, se captura de la grilla
                        obj.bita_c_vusu_red_reg,
                        obj.bita_c_vnomb_completo_reg,
                        pi_cabecera
                        );
                }
                return Convert.ToInt16(pi_cabecera.Value);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void f_actualizar_reserva_cabeceraDA(DIO_PUB_T_RESERVA obj)
        {
            try
            {
                using (BD_DIONISIOEntities c = new BD_DIONISIOEntities())
                {

                    c.SP_PUB_RESERVA_ACTUALIZAR(
                        obj.reser_c_iid,
                        obj.tipo_periodo_c_iid,
                        obj.reser_c_vdim_final,
                        obj.reser_c_eprecio_alquiler,
                        obj.reser_c_vdesc_activacion,
                        obj.reser_c_vcomen_activacion,
                        obj.pub_elem_act_c_iid
                        );
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void f_modificar_Prioridad_ReservaDA(int idReservaAfecto)
        {
            try
            {
                using (BD_DIONISIOEntities c = new BD_DIONISIOEntities())
                {
                    c.SP_PUB_RESERVA_PRIORIDAD_MODIFICAR(
                        idReservaAfecto
                        );
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void f_eliminar_espacios_guardadosDA(string codigo_espacios, int id_reserva_master)
        {
            try
            {
                using (BD_DIONISIOEntities c = new BD_DIONISIOEntities())
                {
                    c.DIO_SP_PUB_RESERVA_XSELECCION_DESACTIVAR(
                        codigo_espacios,
                        id_reserva_master
                        );
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
     
        public int f_guardar_reserva_masterDA(DIO_PUB_T_RESERVA_MASTER obj)
        {
            try
            {
                ObjectParameter pi_id_master = new ObjectParameter("id_master", typeof(string));
                using (BD_DIONISIOEntities c = new BD_DIONISIOEntities())
                {
                    c.SP_PUB_RESERVA_MASTER_INSERTAR(
                         obj.ejec_c_cdoc_id
                         , obj.bita_c_vusu_red_reg
                         , obj.bita_c_vnomb_completo_reg
                         , pi_id_master
                        );
                }
                return Convert.ToInt16(pi_id_master.Value);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int f_cerrar_ReservaDA(int idMaster)
        {
            try
            {
                ObjectParameter id_result = new ObjectParameter("id_result", typeof(string));
                using (BD_DIONISIOEntities c = new BD_DIONISIOEntities())
                {
                    c.SP_PUB_RESERVA_CERRAR(
                         idMaster
                         , id_result
                        );
                }
                return Convert.ToInt16(id_result.Value);
            }
            catch (Exception)
            {
                throw;
            }
        }
        

        public string f_guardar_reserva_detalleDA(int idCabecera, int id_espacio, int tipo_periodo, string fechaDesde
            , string fechaHasta, string grupoFechasIntermitente, string nombcompletoreg, string usuario_cusured, string ejec_cdoc)
        {
            try
            {
                ObjectParameter ps_fechasNoRegistradas = new ObjectParameter("ps_fechasNoRegistradas", typeof(string));
                using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
                {
                    contexto.SP_PUB_RESERVA_DET_INSERTAR(
                        idCabecera,
                        id_espacio,
                        tipo_periodo,
                        fechaDesde,
                        fechaHasta,
                        grupoFechasIntermitente,
                        ejec_cdoc,
                        usuario_cusured,
                        nombcompletoreg,
                        ps_fechasNoRegistradas
                        );
                }
                return ps_fechasNoRegistradas.Value.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Descripción: Obtiene HTML configuracion de requisitos
        /// Autor: Jair Tasayco Bautista
        /// Fecha y Hora Creación: 2017-03-10
        /// Modificado: --
        /// Fecha y hora Modificación: --
        /// </summary>
        /// <param name="reser_mast_c_iid"></param>
        /// <returns></returns>
        public IList<DIO_SP_REQUISITO_PUBLICIDAD_LISTAR_Result> f_ListarConfiguracionRequisitosDA()
        {
            try
            {
                using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
                {
                    return contexto.DIO_SP_REQUISITO_PUBLICIDAD_LISTAR().ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Descripción: Elimina la reserva el cual no se registró ningun detalle (fechas)
        /// Autor: Miguel Ange
        /// Fecha y Hora Creación: 2017-03-10
        /// Modificado: --
        /// Fecha y hora Modificación: --
        /// </summary>
        /// <param name="reser_mast_c_iid"></param>
        /// <returns></returns>
        public bool f_eliminar_Reserva_CopadaTotalmenteDA(int idReserva)
        {
            try
            {
                bool ver=false;
                using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
                {
                    var query = (from x in contexto.DIO_PUB_T_RESERVA
                                 join y in contexto.DIO_PUB_T_RESERVA_DET on x.reser_c_iid equals y.reser_c_iid
                                 where y.reser_det_c_bactivo == true && x.reser_c_iid == idReserva
                                 select x).FirstOrDefault();

                    if (query == null)
                    {
                        var queryReserva = (from x in contexto.DIO_PUB_T_RESERVA
                                            where x.reser_c_iid == idReserva
                                            select x).FirstOrDefault();

                        ver= true;
                        queryReserva.reser_c_bactivo = false;
                        queryReserva.esp_ocu_est_c_iid = 4;
                        contexto.SaveChanges();
                    }
                }
                return ver;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool f_eliminar_Reserva_Master_CopadaTotalmenteDA(int idMaster)
        {
            try
            {
                bool ver=false;
                using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
                {
                    var query = (from x in contexto.DIO_PUB_T_RESERVA_MASTER
                                 join y in contexto.DIO_PUB_T_RESERVA on x.reser_mast_c_iid equals y.reser_mast_c_iid
                                 where y.esp_ocu_est_c_iid == 2 && y.reser_c_bactivo == true && x.reser_mast_c_iid == idMaster
                                 select x).FirstOrDefault();
                    if (query == null)
                    {
                        var queryMaster = (from x in contexto.DIO_PUB_T_RESERVA_MASTER
                                           where x.reser_mast_c_iid == idMaster
                                           select x).FirstOrDefault();
                        ver= true;
                        queryMaster.reser_mast_c_bactivo = false;
                        contexto.SaveChanges();
                    }
                }
                return ver;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

}
