using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BOM.EntityLayer;
using BOM.DataLayer.Interfaces.Reserve;

namespace BOM.BusinessLayer.Interfaces.Reserve
{
    public interface IReserveSpaceAdvertisingBL
    {
        List<ADV_T_PUB_ELEMENTO_ACTIVACION> f_listar_elementoActivacionXProductoBL(int producto);
        List<ADV_T_PUB_PRODUCTO> f_listar_pub_productosBL();
        List<SP_PUB_RESERVA_DETALLE_XIDESPACIO_LISTAR_Result> f_listar_detalle_ocupacionXelementoEspacioBL(int idespacio, int idelemento, DateTime fechaDesde, DateTime fechaHasta);
        List<DIO_PUB_T_TIPO_ASIGNACION> f_listar_pub_tipo_asignacionBL();
        string f_diaUtilVencimientoBL(int pi_diaUtil);
        IList<DIO_SP_EJECUTIVO_BTL_LISTAR_Result> f_listar_ejecutivosBTLBL();
        int f_guardar_reserva_cabeceraBL(DIO_PUB_T_RESERVA obj);
        string f_guardar_reserva_detalleBL(int idCabecera, int id_espacio, int tipo_periodo, string fechaDesde
            , string fechaHasta, string grupoFechasIntermitente, string nombcompletoreg, string usuario_cusured, string ejec_cdoc);
        int f_guardar_reserva_masterBL(DIO_PUB_T_RESERVA_MASTER obj);
        List<SP_PUB_RESERVA_XIDMASTER_LISTAR_Result> f_listar_reservas_xidmasterBL(int reser_mast_c_iid);
        void f_eliminar_espacios_guardadosBL(string codigo_espacios, int id_reserva_master);//codigo_espacios:1,2,3,4,....
        List<DIO_SP_PUB_RESERVA_ENMEMORIA_XIDMASTER_LISTAR_Result> f_listar_reservas_enmemoria_xidmasterBL(int reser_mast_c_iid);
        DIO_PUB_T_RESERVA f_reserva_seleccionar_xidBL(int idreserva);
        List<DIO_SP_PUB_RESERVA_DET_LISTAR_Result> f_listar_detallexReservaBL(int idreserva);
        void f_actualizar_reserva_cabeceraBL(DIO_PUB_T_RESERVA obj);
        List<ADV_T_CLIENTE> f_ListarAgenciaBL();
        List<DIO_PUB_T_MARCA> f_ListarMarcaBL();

        ADV_T_CLIENTE f_seleccionar_clienteBL(string vraz_social);
        ADV_T_CLIENTE f_seleccionar_agenciaBL(string vraz_social);
        DIO_PUB_T_MARCA f_seleccionar_o_registrar_marcaBL(string vnomb_marca, string vusu_red, string nombrecompleto);
        void f_asociar_clienteBL(DIO_PUB_T_RESERVA objreserva);
        IList<DIO_SP_REQUISITO_PUBLICIDAD_LISTAR_Result> f_ListarConfiguracionRequisitosBL();
        int f_cerrar_ReservaBL(int idMaster);
        IList<SP_PUB_ESPACIOS_NO_DISPONIBLE_LISTAR_Result> f_obtenerEspaciosNoDisponiblesBL(string cadena_espacios_sw,DateTime fechaDesde,DateTime fechaHasta);
        void f_modificar_Prioridad_ReservaBL(int idReservaAfecto);

        List<DIO_SP_PUB_RESERVA_PENDIENTE_XID_MASTER_Result> f_listar_reservas_pendientes_xidmasterBL(int reser_mast_c_iid);
        bool f_eliminar_Reserva_CopadaTotalmenteBL(int idReserva);
        bool f_eliminar_Reserva_Master_CopadaTotalmenteBL(int idMaster);
    }

    public class ReserveSpaceAdvertisingBL : IReserveSpaceAdvertisingBL
    {
        public void Dispose()
        {
            GC.Collect();
        }

        public List<DIO_SP_PUB_RESERVA_PENDIENTE_XID_MASTER_Result> f_listar_reservas_pendientes_xidmasterBL(int reser_mast_c_iid)
        {
            return new ReserveSpaceAdvertisingDA().f_listar_reservas_pendientes_xidmasterDA(reser_mast_c_iid);
        }
        public ADV_T_CLIENTE f_seleccionar_clienteBL(string vraz_social)
        {
            return new ReserveSpaceAdvertisingDA().f_seleccionar_clienteDA(vraz_social);
        }
        public ADV_T_CLIENTE f_seleccionar_agenciaBL(string vraz_social)
        {
            return new ReserveSpaceAdvertisingDA().f_seleccionar_agenciaDA(vraz_social);
        }
        public DIO_PUB_T_MARCA f_seleccionar_o_registrar_marcaBL(string vnomb_marca, string vusu_red, string nombrecompleto)
        {
            return new ReserveSpaceAdvertisingDA().f_seleccionar_o_registrar_marcaDA(vnomb_marca,vusu_red,nombrecompleto);
        }


        public List<ADV_T_PUB_ELEMENTO_ACTIVACION> f_listar_elementoActivacionXProductoBL(int producto)
        {
            return new ReserveSpaceAdvertisingDA().f_listar_elementoActivacionXProductoDA(producto);
        }

        public List<ADV_T_PUB_PRODUCTO> f_listar_pub_productosBL()
        {
            return new ReserveSpaceAdvertisingDA().f_listar_pub_productosDA();
        }
        public List<SP_PUB_RESERVA_DETALLE_XIDESPACIO_LISTAR_Result> f_listar_detalle_ocupacionXelementoEspacioBL(int idespacio, int idelemento, DateTime fechaDesde, DateTime fechaHasta)
        {
            return new ReserveSpaceAdvertisingDA().f_listar_detalle_ocupacionXelementoEspacioDA(idespacio, idelemento, fechaDesde, fechaHasta);
        }
        public IList<SP_PUB_ESPACIOS_NO_DISPONIBLE_LISTAR_Result> f_obtenerEspaciosNoDisponiblesBL(string cadena_espacios_sw, DateTime fechaDesde, DateTime fechaHasta)
        {
            return new ReserveSpaceAdvertisingDA().f_obtenerEspaciosNoDisponiblesDA(cadena_espacios_sw, fechaDesde, fechaHasta);
        }

        
        public List<DIO_PUB_T_TIPO_ASIGNACION> f_listar_pub_tipo_asignacionBL()
        {
            return new ReserveSpaceAdvertisingDA().f_listar_pub_tipo_asignacionDA();
        }

        public string f_diaUtilVencimientoBL(int pi_diaUtil)
        {
            return new ReserveSpaceAdvertisingDA().f_diaUtilVencimientoDA(pi_diaUtil);
        }

        public IList<DIO_SP_EJECUTIVO_BTL_LISTAR_Result> f_listar_ejecutivosBTLBL()
        {
            return new ReserveSpaceAdvertisingDA().f_listar_ejecutivosBTLDA();
        }
        public int f_guardar_reserva_cabeceraBL(DIO_PUB_T_RESERVA obj)
        {
            return new ReserveSpaceAdvertisingDA().f_guardar_reserva_cabeceraDA(obj);
        }
        public void f_actualizar_reserva_cabeceraBL(DIO_PUB_T_RESERVA obj)
        {
            new ReserveSpaceAdvertisingDA().f_actualizar_reserva_cabeceraDA(obj);
        }
        public void f_asociar_clienteBL(DIO_PUB_T_RESERVA objreserva)
        {
            new ReserveSpaceAdvertisingDA().f_asociar_clienteDA(objreserva);
        }
        public void f_modificar_Prioridad_ReservaBL(int idReservaAfecto)
        {
            new ReserveSpaceAdvertisingDA().f_modificar_Prioridad_ReservaDA(idReservaAfecto);
        }

        public string f_guardar_reserva_detalleBL(int idCabecera, int id_espacio, int tipo_periodo, string fechaDesde,
            string fechaHasta, string grupoFechasIntermitente, string nombcompletoreg, string usuario_cusured, string ejec_cdoc)
        {
            return new ReserveSpaceAdvertisingDA().f_guardar_reserva_detalleDA(idCabecera, id_espacio, tipo_periodo,
                fechaDesde, fechaHasta, grupoFechasIntermitente, nombcompletoreg, usuario_cusured, ejec_cdoc);
        }
        public int f_guardar_reserva_masterBL(DIO_PUB_T_RESERVA_MASTER obj)
        {
            return new ReserveSpaceAdvertisingDA().f_guardar_reserva_masterDA(obj);
        }
        public List<SP_PUB_RESERVA_XIDMASTER_LISTAR_Result> f_listar_reservas_xidmasterBL(int reser_mast_c_iid)
        {
            return new ReserveSpaceAdvertisingDA().f_listar_reservas_xidmasterDA(reser_mast_c_iid);
        }

        public void f_eliminar_espacios_guardadosBL(string codigo_espacios,int id_reserva_master)//codigo_espacios:1,2,3,4,....
        {
            new ReserveSpaceAdvertisingDA().f_eliminar_espacios_guardadosDA(codigo_espacios, id_reserva_master);
        }

        public List<DIO_SP_PUB_RESERVA_ENMEMORIA_XIDMASTER_LISTAR_Result> f_listar_reservas_enmemoria_xidmasterBL(int reser_mast_c_iid)
        {
            return new ReserveSpaceAdvertisingDA().f_listar_reservas_enmemoria_xidmasterDA(reser_mast_c_iid);
        }
        public DIO_PUB_T_RESERVA f_reserva_seleccionar_xidBL(int idreserva)
        {
            return new ReserveSpaceAdvertisingDA().f_reserva_seleccionar_xidDA(idreserva);
        }
        public List<DIO_SP_PUB_RESERVA_DET_LISTAR_Result> f_listar_detallexReservaBL(int idreserva)
        {
            return new ReserveSpaceAdvertisingDA().f_listar_detallexReservaDA(idreserva);
        }
        public List<ADV_T_CLIENTE> f_ListarAgenciaBL()
        {
            return new ReserveSpaceAdvertisingDA().f_ListarAgenciaDA();
        }
        public List<DIO_PUB_T_MARCA> f_ListarMarcaBL()
        {
            return new ReserveSpaceAdvertisingDA().f_ListarMarcaDA();
        }

        public IList<DIO_SP_REQUISITO_PUBLICIDAD_LISTAR_Result> f_ListarConfiguracionRequisitosBL()
        {
            return new ReserveSpaceAdvertisingDA().f_ListarConfiguracionRequisitosDA();
        }

        public int f_cerrar_ReservaBL(int idMaster)
        {
            return new ReserveSpaceAdvertisingDA().f_cerrar_ReservaDA(idMaster);
        }
        public bool f_eliminar_Reserva_CopadaTotalmenteBL(int idReserva)
        {
            return new ReserveSpaceAdvertisingDA().f_eliminar_Reserva_CopadaTotalmenteDA(idReserva);
        }

        public bool f_eliminar_Reserva_Master_CopadaTotalmenteBL(int idMaster)
        {
            return new ReserveSpaceAdvertisingDA().f_eliminar_Reserva_Master_CopadaTotalmenteDA(idMaster);
        }
        
    }
}
