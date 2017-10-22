using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOM.UIGeneral
{
    public class IEnum
    {
        public enum Sistema
        {
            Bombero = 5,
        }

        public enum TransaccionBitacora
        {
            Insertar = 1,
            Actualizar = 2,
            Eliminar = 3
        }

        public enum Transaccion
        {
            Nuevo = 0,
            Modificar = 1,
            Eliminar = 2,
            Guardar = 3
        }
        public enum MensajeAccion
        {
            Correcto = 1,
            Alerta = 2,
            Error = 3,
        }
        public enum Requisitos_Publicidad
        {
            Requisito_Entrega_Padre = 2,
            Requisito_Apertura_Padre = 3
        }
        public enum Perfiles
        {
            Administrador = 17,
            EjecutivoVta_BTL = 18,
            JefeVta_BTL = 19,
            Coordinador_BTL = 20
        }


    }

    public class NameCapas
    {

        public const string K_APPNAME = "BOM.UserLayer";
    }
}
