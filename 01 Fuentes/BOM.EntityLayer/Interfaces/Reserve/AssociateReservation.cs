using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOM.EntityLayer.Interfaces.Reserve
{
    public class AssociateReservation
    {
        public string s_class_num_tab { get; set; }
        public string s_nombre_tab { get; set; }

        public int i_id_inmueble { get; set; }
        //public string s_nomb_inmueble { get; set; }
        public int i_id_espacio { get; set; }
        public string s_cod_espacio { get; set; }
        public int i_id_producto { get; set; }
        public int i_id_elemento_activacion { get; set; }

        public int i_reser_mast_c_iid { get; set; }
        public int i_reser_c_iid { get; set; }

        public string s_nombre_producto { get; set; }
        public string s_tarifa_fria { get; set; }
        public string s_tarifa_base { get; set; }
        public string s_requisito_producto_entrega { get; set; }
        public string s_requisito_producto_apertura { get; set; }
        public string s_texto_tipo_elemento { get; set; }
        public string s_esp_vmedida { get; set; }
        public decimal s_esp_earea { get; set; }

        

    }
}
