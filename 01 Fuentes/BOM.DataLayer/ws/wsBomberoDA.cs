using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BOM.EntityLayer;
using System.Data.Entity;
using System.Data.Objects;

namespace BOM.DataLayer.ws
{
    public interface IwsBomberoDA
    {
        int f_EspacioReservadoDA(int pub_esp_c_iid);
    }
    public class wsBomberoDA : IwsBomberoDA
    {
        public void Dispose()
        {
            GC.Collect();
        }

        public int f_EspacioReservadoDA(int pub_esp_c_iid)
        {
            try
            {
                ObjectParameter id_result = new ObjectParameter("id_result", typeof(string));
                using (BD_DIONISIOEntities c = new BD_DIONISIOEntities())
                {
                    c.DIO_SP_CONSULTA_ESPACIO_RESERVADO(
                         pub_esp_c_iid, id_result
                        );
                }
                return Convert.ToInt16(id_result.Value);
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }




    
}
