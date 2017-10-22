using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOM.EntityLayer
{
    [Serializable()]
    public class BitacoraBE
    {
        int _iMenu_c_iid;
        string _sBita_c_vopcion;
        string _sBita_c_vrutapagina;
        string _sColab_c_cusu_red;
        string _sColab_c_vnomb_completo;

        public int intMenu_c_iid
        {
            get { return _iMenu_c_iid; }
            set { _iMenu_c_iid = value; }
        }

        public string strBita_c_vopcion
        {
            get { return _sBita_c_vopcion; }
            set { _sBita_c_vopcion = value; }
        }

        public string strBita_c_vrutapagina
        {
            get { return _sBita_c_vrutapagina; }
            set { _sBita_c_vrutapagina = value; }
        }

        public string strColab_c_cusu_red
        {
            get { return _sColab_c_cusu_red; }
            set { _sColab_c_cusu_red = value; }
        }

        public string strColab_c_vnomb_completo
        {
            get { return _sColab_c_vnomb_completo; }
            set { _sColab_c_vnomb_completo = value; }
        }
    }
}
