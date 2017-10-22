﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
#region Metadatos de relaciones en EDM

[assembly: EdmRelationshipAttribute("DB_9F356E_HipnosModel", "FK_AUD_T_BITACORA_AUD_T_BITA_TIPO_MOV", "AUD_T_BITA_TIPO_MOV", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(BOM.EntityLayer.AUD_T_BITA_TIPO_MOV), "AUD_T_BITACORA", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(BOM.EntityLayer.AUD_T_BITACORA), true)]

#endregion

namespace BOM.EntityLayer
{
    #region Contextos
    
    /// <summary>
    /// No hay documentación de metadatos disponible.
    /// </summary>
    public partial class AUDDBEntities : ObjectContext
    {
        #region Constructores
    
        /// <summary>
        /// Inicializa un nuevo objeto AUDDBEntities usando la cadena de conexión encontrada en la sección 'AUDDBEntities' del archivo de configuración de la aplicación.
        /// </summary>
        public AUDDBEntities() : base("name=AUDDBEntities", "AUDDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Inicializar un nuevo objeto AUDDBEntities.
        /// </summary>
        public AUDDBEntities(string connectionString) : base(connectionString, "AUDDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Inicializar un nuevo objeto AUDDBEntities.
        /// </summary>
        public AUDDBEntities(EntityConnection connection) : base(connection, "AUDDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Métodos parciales
    
        partial void OnContextCreated();
    
        #endregion
    
        #region Propiedades de ObjectSet
    
        /// <summary>
        /// No hay documentación de metadatos disponible.
        /// </summary>
        public ObjectSet<AUD_T_BITA_TIPO_MOV> AUD_T_BITA_TIPO_MOV
        {
            get
            {
                if ((_AUD_T_BITA_TIPO_MOV == null))
                {
                    _AUD_T_BITA_TIPO_MOV = base.CreateObjectSet<AUD_T_BITA_TIPO_MOV>("AUD_T_BITA_TIPO_MOV");
                }
                return _AUD_T_BITA_TIPO_MOV;
            }
        }
        private ObjectSet<AUD_T_BITA_TIPO_MOV> _AUD_T_BITA_TIPO_MOV;
    
        /// <summary>
        /// No hay documentación de metadatos disponible.
        /// </summary>
        public ObjectSet<AUD_T_BITACORA> AUD_T_BITACORA
        {
            get
            {
                if ((_AUD_T_BITACORA == null))
                {
                    _AUD_T_BITACORA = base.CreateObjectSet<AUD_T_BITACORA>("AUD_T_BITACORA");
                }
                return _AUD_T_BITACORA;
            }
        }
        private ObjectSet<AUD_T_BITACORA> _AUD_T_BITACORA;

        #endregion

        #region Métodos AddTo
    
        /// <summary>
        /// Método desusado para agregar un nuevo objeto al EntitySet AUD_T_BITA_TIPO_MOV. Considere la posibilidad de usar el método .Add de la propiedad ObjectSet&lt;T&gt; asociada.
        /// </summary>
        public void AddToAUD_T_BITA_TIPO_MOV(AUD_T_BITA_TIPO_MOV aUD_T_BITA_TIPO_MOV)
        {
            base.AddObject("AUD_T_BITA_TIPO_MOV", aUD_T_BITA_TIPO_MOV);
        }
    
        /// <summary>
        /// Método desusado para agregar un nuevo objeto al EntitySet AUD_T_BITACORA. Considere la posibilidad de usar el método .Add de la propiedad ObjectSet&lt;T&gt; asociada.
        /// </summary>
        public void AddToAUD_T_BITACORA(AUD_T_BITACORA aUD_T_BITACORA)
        {
            base.AddObject("AUD_T_BITACORA", aUD_T_BITACORA);
        }

        #endregion

    }

    #endregion

    #region Entidades
    
    /// <summary>
    /// No hay documentación de metadatos disponible.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="DB_9F356E_HipnosModel", Name="AUD_T_BITA_TIPO_MOV")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class AUD_T_BITA_TIPO_MOV : EntityObject
    {
        #region Método de generador
    
        /// <summary>
        /// Crear un nuevo objeto AUD_T_BITA_TIPO_MOV.
        /// </summary>
        /// <param name="bita_tip_mov_c_iid">Valor inicial de la propiedad bita_tip_mov_c_iid.</param>
        /// <param name="bita_tip_mov_c_vnomb">Valor inicial de la propiedad bita_tip_mov_c_vnomb.</param>
        public static AUD_T_BITA_TIPO_MOV CreateAUD_T_BITA_TIPO_MOV(global::System.Int32 bita_tip_mov_c_iid, global::System.String bita_tip_mov_c_vnomb)
        {
            AUD_T_BITA_TIPO_MOV aUD_T_BITA_TIPO_MOV = new AUD_T_BITA_TIPO_MOV();
            aUD_T_BITA_TIPO_MOV.bita_tip_mov_c_iid = bita_tip_mov_c_iid;
            aUD_T_BITA_TIPO_MOV.bita_tip_mov_c_vnomb = bita_tip_mov_c_vnomb;
            return aUD_T_BITA_TIPO_MOV;
        }

        #endregion

        #region Propiedades primitivas
    
        /// <summary>
        /// No hay documentación de metadatos disponible.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 bita_tip_mov_c_iid
        {
            get
            {
                return _bita_tip_mov_c_iid;
            }
            set
            {
                if (_bita_tip_mov_c_iid != value)
                {
                    Onbita_tip_mov_c_iidChanging(value);
                    ReportPropertyChanging("bita_tip_mov_c_iid");
                    _bita_tip_mov_c_iid = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("bita_tip_mov_c_iid");
                    Onbita_tip_mov_c_iidChanged();
                }
            }
        }
        private global::System.Int32 _bita_tip_mov_c_iid;
        partial void Onbita_tip_mov_c_iidChanging(global::System.Int32 value);
        partial void Onbita_tip_mov_c_iidChanged();
    
        /// <summary>
        /// No hay documentación de metadatos disponible.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String bita_tip_mov_c_vnomb
        {
            get
            {
                return _bita_tip_mov_c_vnomb;
            }
            set
            {
                Onbita_tip_mov_c_vnombChanging(value);
                ReportPropertyChanging("bita_tip_mov_c_vnomb");
                _bita_tip_mov_c_vnomb = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("bita_tip_mov_c_vnomb");
                Onbita_tip_mov_c_vnombChanged();
            }
        }
        private global::System.String _bita_tip_mov_c_vnomb;
        partial void Onbita_tip_mov_c_vnombChanging(global::System.String value);
        partial void Onbita_tip_mov_c_vnombChanged();
    
        /// <summary>
        /// No hay documentación de metadatos disponible.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> bita_c_zfec_reg
        {
            get
            {
                return _bita_c_zfec_reg;
            }
            set
            {
                Onbita_c_zfec_regChanging(value);
                ReportPropertyChanging("bita_c_zfec_reg");
                _bita_c_zfec_reg = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("bita_c_zfec_reg");
                Onbita_c_zfec_regChanged();
            }
        }
        private Nullable<global::System.DateTime> _bita_c_zfec_reg;
        partial void Onbita_c_zfec_regChanging(Nullable<global::System.DateTime> value);
        partial void Onbita_c_zfec_regChanged();
    
        /// <summary>
        /// No hay documentación de metadatos disponible.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String bita_c_vusu_red_reg
        {
            get
            {
                return _bita_c_vusu_red_reg;
            }
            set
            {
                Onbita_c_vusu_red_regChanging(value);
                ReportPropertyChanging("bita_c_vusu_red_reg");
                _bita_c_vusu_red_reg = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("bita_c_vusu_red_reg");
                Onbita_c_vusu_red_regChanged();
            }
        }
        private global::System.String _bita_c_vusu_red_reg;
        partial void Onbita_c_vusu_red_regChanging(global::System.String value);
        partial void Onbita_c_vusu_red_regChanged();
    
        /// <summary>
        /// No hay documentación de metadatos disponible.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String bita_c_vnom_completo_reg
        {
            get
            {
                return _bita_c_vnom_completo_reg;
            }
            set
            {
                Onbita_c_vnom_completo_regChanging(value);
                ReportPropertyChanging("bita_c_vnom_completo_reg");
                _bita_c_vnom_completo_reg = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("bita_c_vnom_completo_reg");
                Onbita_c_vnom_completo_regChanged();
            }
        }
        private global::System.String _bita_c_vnom_completo_reg;
        partial void Onbita_c_vnom_completo_regChanging(global::System.String value);
        partial void Onbita_c_vnom_completo_regChanged();

        #endregion

    
        #region Propiedades de navegación
    
        /// <summary>
        /// No hay documentación de metadatos disponible.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("DB_9F356E_HipnosModel", "FK_AUD_T_BITACORA_AUD_T_BITA_TIPO_MOV", "AUD_T_BITACORA")]
        public EntityCollection<AUD_T_BITACORA> AUD_T_BITACORA
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<AUD_T_BITACORA>("DB_9F356E_HipnosModel.FK_AUD_T_BITACORA_AUD_T_BITA_TIPO_MOV", "AUD_T_BITACORA");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<AUD_T_BITACORA>("DB_9F356E_HipnosModel.FK_AUD_T_BITACORA_AUD_T_BITA_TIPO_MOV", "AUD_T_BITACORA", value);
                }
            }
        }

        #endregion

    }
    
    /// <summary>
    /// No hay documentación de metadatos disponible.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="DB_9F356E_HipnosModel", Name="AUD_T_BITACORA")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class AUD_T_BITACORA : EntityObject
    {
        #region Método de generador
    
        /// <summary>
        /// Crear un nuevo objeto AUD_T_BITACORA.
        /// </summary>
        /// <param name="bita_c_iid">Valor inicial de la propiedad bita_c_iid.</param>
        /// <param name="sist_c_iid">Valor inicial de la propiedad sist_c_iid.</param>
        /// <param name="bita_tip_mov_c_iid">Valor inicial de la propiedad bita_tip_mov_c_iid.</param>
        /// <param name="bita_c_vopcion">Valor inicial de la propiedad bita_c_vopcion.</param>
        /// <param name="bita_c_vsqlproceso">Valor inicial de la propiedad bita_c_vsqlproceso.</param>
        /// <param name="bita_c_vquery_transaccion">Valor inicial de la propiedad bita_c_vquery_transaccion.</param>
        /// <param name="bita_c_vnum_ip">Valor inicial de la propiedad bita_c_vnum_ip.</param>
        /// <param name="colab_c_cusu_red">Valor inicial de la propiedad colab_c_cusu_red.</param>
        /// <param name="colab_c_vnomb_completo">Valor inicial de la propiedad colab_c_vnomb_completo.</param>
        /// <param name="bita_c_zfec_reg">Valor inicial de la propiedad bita_c_zfec_reg.</param>
        public static AUD_T_BITACORA CreateAUD_T_BITACORA(global::System.Int32 bita_c_iid, global::System.Int32 sist_c_iid, global::System.Int32 bita_tip_mov_c_iid, global::System.String bita_c_vopcion, global::System.String bita_c_vsqlproceso, global::System.String bita_c_vquery_transaccion, global::System.String bita_c_vnum_ip, global::System.String colab_c_cusu_red, global::System.String colab_c_vnomb_completo, global::System.DateTime bita_c_zfec_reg)
        {
            AUD_T_BITACORA aUD_T_BITACORA = new AUD_T_BITACORA();
            aUD_T_BITACORA.bita_c_iid = bita_c_iid;
            aUD_T_BITACORA.sist_c_iid = sist_c_iid;
            aUD_T_BITACORA.bita_tip_mov_c_iid = bita_tip_mov_c_iid;
            aUD_T_BITACORA.bita_c_vopcion = bita_c_vopcion;
            aUD_T_BITACORA.bita_c_vsqlproceso = bita_c_vsqlproceso;
            aUD_T_BITACORA.bita_c_vquery_transaccion = bita_c_vquery_transaccion;
            aUD_T_BITACORA.bita_c_vnum_ip = bita_c_vnum_ip;
            aUD_T_BITACORA.colab_c_cusu_red = colab_c_cusu_red;
            aUD_T_BITACORA.colab_c_vnomb_completo = colab_c_vnomb_completo;
            aUD_T_BITACORA.bita_c_zfec_reg = bita_c_zfec_reg;
            return aUD_T_BITACORA;
        }

        #endregion

        #region Propiedades primitivas
    
        /// <summary>
        /// No hay documentación de metadatos disponible.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 bita_c_iid
        {
            get
            {
                return _bita_c_iid;
            }
            set
            {
                if (_bita_c_iid != value)
                {
                    Onbita_c_iidChanging(value);
                    ReportPropertyChanging("bita_c_iid");
                    _bita_c_iid = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("bita_c_iid");
                    Onbita_c_iidChanged();
                }
            }
        }
        private global::System.Int32 _bita_c_iid;
        partial void Onbita_c_iidChanging(global::System.Int32 value);
        partial void Onbita_c_iidChanged();
    
        /// <summary>
        /// No hay documentación de metadatos disponible.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 sist_c_iid
        {
            get
            {
                return _sist_c_iid;
            }
            set
            {
                Onsist_c_iidChanging(value);
                ReportPropertyChanging("sist_c_iid");
                _sist_c_iid = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("sist_c_iid");
                Onsist_c_iidChanged();
            }
        }
        private global::System.Int32 _sist_c_iid;
        partial void Onsist_c_iidChanging(global::System.Int32 value);
        partial void Onsist_c_iidChanged();
    
        /// <summary>
        /// No hay documentación de metadatos disponible.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 bita_tip_mov_c_iid
        {
            get
            {
                return _bita_tip_mov_c_iid;
            }
            set
            {
                Onbita_tip_mov_c_iidChanging(value);
                ReportPropertyChanging("bita_tip_mov_c_iid");
                _bita_tip_mov_c_iid = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("bita_tip_mov_c_iid");
                Onbita_tip_mov_c_iidChanged();
            }
        }
        private global::System.Int32 _bita_tip_mov_c_iid;
        partial void Onbita_tip_mov_c_iidChanging(global::System.Int32 value);
        partial void Onbita_tip_mov_c_iidChanged();
    
        /// <summary>
        /// No hay documentación de metadatos disponible.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> menu_c_iid
        {
            get
            {
                return _menu_c_iid;
            }
            set
            {
                Onmenu_c_iidChanging(value);
                ReportPropertyChanging("menu_c_iid");
                _menu_c_iid = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("menu_c_iid");
                Onmenu_c_iidChanged();
            }
        }
        private Nullable<global::System.Int32> _menu_c_iid;
        partial void Onmenu_c_iidChanging(Nullable<global::System.Int32> value);
        partial void Onmenu_c_iidChanged();
    
        /// <summary>
        /// No hay documentación de metadatos disponible.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String bita_c_vopcion
        {
            get
            {
                return _bita_c_vopcion;
            }
            set
            {
                Onbita_c_vopcionChanging(value);
                ReportPropertyChanging("bita_c_vopcion");
                _bita_c_vopcion = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("bita_c_vopcion");
                Onbita_c_vopcionChanged();
            }
        }
        private global::System.String _bita_c_vopcion;
        partial void Onbita_c_vopcionChanging(global::System.String value);
        partial void Onbita_c_vopcionChanged();
    
        /// <summary>
        /// No hay documentación de metadatos disponible.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String bita_c_vrutapagina
        {
            get
            {
                return _bita_c_vrutapagina;
            }
            set
            {
                Onbita_c_vrutapaginaChanging(value);
                ReportPropertyChanging("bita_c_vrutapagina");
                _bita_c_vrutapagina = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("bita_c_vrutapagina");
                Onbita_c_vrutapaginaChanged();
            }
        }
        private global::System.String _bita_c_vrutapagina;
        partial void Onbita_c_vrutapaginaChanging(global::System.String value);
        partial void Onbita_c_vrutapaginaChanged();
    
        /// <summary>
        /// No hay documentación de metadatos disponible.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String bita_c_vsqlproceso
        {
            get
            {
                return _bita_c_vsqlproceso;
            }
            set
            {
                Onbita_c_vsqlprocesoChanging(value);
                ReportPropertyChanging("bita_c_vsqlproceso");
                _bita_c_vsqlproceso = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("bita_c_vsqlproceso");
                Onbita_c_vsqlprocesoChanged();
            }
        }
        private global::System.String _bita_c_vsqlproceso;
        partial void Onbita_c_vsqlprocesoChanging(global::System.String value);
        partial void Onbita_c_vsqlprocesoChanged();
    
        /// <summary>
        /// No hay documentación de metadatos disponible.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String bita_c_vquery_transaccion
        {
            get
            {
                return _bita_c_vquery_transaccion;
            }
            set
            {
                Onbita_c_vquery_transaccionChanging(value);
                ReportPropertyChanging("bita_c_vquery_transaccion");
                _bita_c_vquery_transaccion = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("bita_c_vquery_transaccion");
                Onbita_c_vquery_transaccionChanged();
            }
        }
        private global::System.String _bita_c_vquery_transaccion;
        partial void Onbita_c_vquery_transaccionChanging(global::System.String value);
        partial void Onbita_c_vquery_transaccionChanged();
    
        /// <summary>
        /// No hay documentación de metadatos disponible.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String bita_c_vnum_ip
        {
            get
            {
                return _bita_c_vnum_ip;
            }
            set
            {
                Onbita_c_vnum_ipChanging(value);
                ReportPropertyChanging("bita_c_vnum_ip");
                _bita_c_vnum_ip = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("bita_c_vnum_ip");
                Onbita_c_vnum_ipChanged();
            }
        }
        private global::System.String _bita_c_vnum_ip;
        partial void Onbita_c_vnum_ipChanging(global::System.String value);
        partial void Onbita_c_vnum_ipChanged();
    
        /// <summary>
        /// No hay documentación de metadatos disponible.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String colab_c_cusu_red
        {
            get
            {
                return _colab_c_cusu_red;
            }
            set
            {
                Oncolab_c_cusu_redChanging(value);
                ReportPropertyChanging("colab_c_cusu_red");
                _colab_c_cusu_red = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("colab_c_cusu_red");
                Oncolab_c_cusu_redChanged();
            }
        }
        private global::System.String _colab_c_cusu_red;
        partial void Oncolab_c_cusu_redChanging(global::System.String value);
        partial void Oncolab_c_cusu_redChanged();
    
        /// <summary>
        /// No hay documentación de metadatos disponible.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String colab_c_vnomb_completo
        {
            get
            {
                return _colab_c_vnomb_completo;
            }
            set
            {
                Oncolab_c_vnomb_completoChanging(value);
                ReportPropertyChanging("colab_c_vnomb_completo");
                _colab_c_vnomb_completo = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("colab_c_vnomb_completo");
                Oncolab_c_vnomb_completoChanged();
            }
        }
        private global::System.String _colab_c_vnomb_completo;
        partial void Oncolab_c_vnomb_completoChanging(global::System.String value);
        partial void Oncolab_c_vnomb_completoChanged();
    
        /// <summary>
        /// No hay documentación de metadatos disponible.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime bita_c_zfec_reg
        {
            get
            {
                return _bita_c_zfec_reg;
            }
            set
            {
                Onbita_c_zfec_regChanging(value);
                ReportPropertyChanging("bita_c_zfec_reg");
                _bita_c_zfec_reg = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("bita_c_zfec_reg");
                Onbita_c_zfec_regChanged();
            }
        }
        private global::System.DateTime _bita_c_zfec_reg;
        partial void Onbita_c_zfec_regChanging(global::System.DateTime value);
        partial void Onbita_c_zfec_regChanged();

        #endregion

    
        #region Propiedades de navegación
    
        /// <summary>
        /// No hay documentación de metadatos disponible.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("DB_9F356E_HipnosModel", "FK_AUD_T_BITACORA_AUD_T_BITA_TIPO_MOV", "AUD_T_BITA_TIPO_MOV")]
        public AUD_T_BITA_TIPO_MOV AUD_T_BITA_TIPO_MOV
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<AUD_T_BITA_TIPO_MOV>("DB_9F356E_HipnosModel.FK_AUD_T_BITACORA_AUD_T_BITA_TIPO_MOV", "AUD_T_BITA_TIPO_MOV").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<AUD_T_BITA_TIPO_MOV>("DB_9F356E_HipnosModel.FK_AUD_T_BITACORA_AUD_T_BITA_TIPO_MOV", "AUD_T_BITA_TIPO_MOV").Value = value;
            }
        }
        /// <summary>
        /// No hay documentación de metadatos disponible.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<AUD_T_BITA_TIPO_MOV> AUD_T_BITA_TIPO_MOVReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<AUD_T_BITA_TIPO_MOV>("DB_9F356E_HipnosModel.FK_AUD_T_BITACORA_AUD_T_BITA_TIPO_MOV", "AUD_T_BITA_TIPO_MOV");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<AUD_T_BITA_TIPO_MOV>("DB_9F356E_HipnosModel.FK_AUD_T_BITACORA_AUD_T_BITA_TIPO_MOV", "AUD_T_BITA_TIPO_MOV", value);
                }
            }
        }

        #endregion

    }

    #endregion

    
}
