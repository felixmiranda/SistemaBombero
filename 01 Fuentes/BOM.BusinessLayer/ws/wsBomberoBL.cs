using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BOM.EntityLayer;
using BOM.DataLayer.ws;
namespace BOM.BusinessLayer.ws
{
    public interface IwsBomberoBL
    {
        int f_EspacioReservadoBL(int pub_esp_c_iid);
    }
    public class wsBomberoBL : IwsBomberoBL
    {
        public void Dispose()
        {
            GC.Collect();
        }
        public int f_EspacioReservadoBL(int pub_esp_c_iid)
        {
            return new wsBomberoDA().f_EspacioReservadoDA(pub_esp_c_iid);
        }
    }
}
