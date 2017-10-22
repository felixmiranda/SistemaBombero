using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOM.UserLayer
{
    public static class ClsUtilCore
    {
        public static class UIMensajes
        {

            public static String fn_GenerarScriptMensajeGenerico(string ps_Tipo, string ps_Mensaje)
            {
                string script = "";

                if (ps_Tipo.Equals("error"))
                {
                    script = "<script>$.growl.error({ title: 'Error!', message: '" + ps_Mensaje + "' });</script>";
                }
                else if (ps_Tipo.Equals("info"))
                {
                    script = "<script>$.growl({ title: 'Info',  message: '" + ps_Mensaje + "' });</script>";
                }
                else if (ps_Tipo.Equals("success"))
                {
                    script = "<script>$.growl.notice({ title: 'Éxito!',  message: '" + ps_Mensaje + "' });</script>";
                }
                else if (ps_Tipo.Equals("warning"))
                {
                    script = "<script>$.growl.warning({ title: 'Cuidado!',  message: '" + ps_Mensaje + "' });</script>";
                }

                return script;

            }


            /// <summary>
            /// Descripción: Genera una cadena que contiene el script para mostrar una lista de errores en la pagina
            /// Autor: Hector Vives (ETECH)
            /// Fecha y Hora Creación: 08-09-2016
            /// </summary>
            /// <param name="plist_Mensajes"></param>
            /// <returns></returns>
            public static String f_ObtenerScriptError(List<String> plist_Mensajes)
            {
                var cadena = "<script> $('#modal-error-sistema').find('ul').eq(0).empty(); ";

                foreach (var s in plist_Mensajes)
                {
                    cadena += "$('#modal-error-sistema').find('ul').eq(0).append('<li>" + s + "</li>'); ";
                }
                cadena += "$('#modal-error-sistema').modal('show');";

                cadena += "</script>";
                return cadena;
            }

            /// <summary>
            /// Descripción: Genera una cadena que contiene el script para mostrar un error en la pagina
            /// Autor: Hector Vives (ETECH)
            /// Fecha y Hora Creación: 08-09-2016
            /// </summary>
            /// <param name="ps_Mensaje"></param>
            /// <returns></returns>
            public static String f_ObtenerScriptError(String ps_Mensaje)
            {
                var cadena = "<script> $('#modal-error-sistema').find('ul').eq(0).empty(); ";

                cadena += "$('#modal-error-sistema').find('ul').eq(0).append('<li>" + ps_Mensaje + "</li>'); ";

                cadena += "$('#modal-error-sistema').modal('show');";

                cadena += "</script>";
                return cadena;
            }

            /// <summary>
            /// Descripción: Genera una cadena que contiene el script para mostrar un error en la pagina
            /// </summary>
            /// <param name="ps_Mensaje"></param>
            /// <returns></returns>
            public static String f_ObtenerScriptErrorSimple(String ps_Mensaje)
            {
                var cadena = "<script> $('#alert_mensaje').html('<strong>Error! : </strong>" + ps_Mensaje + "'); ";

                cadena += "$('#alert_content').show('slow');";

                cadena += "</script>";
                return cadena;
            }
        }
    }
}