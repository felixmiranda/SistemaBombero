

-- AGREGAR EL CORRELATIVO DEL SUB MENU EN LOS PERMISOS DEL PERFIL 21
-- EL CORRELATIVO SE OBTIENE DEL PASO 1

-- Jefe Ventas
-- Coordinador Ventas
-- 21 -> GERENTE DE MALL
declare @PerfilNuevo int  
declare @PerfilEjecutivoVentas int = 18
declare @PerfilJefeVentas int = 19
declare @PerfilCoordinadorVentas int = 20
declare @Menu_c_iid int 
declare @Menu_c_iid_01 int 
declare @Menu_c_iid_02 int 
declare @Menu_c_iid_03 int 
declare @Menu_c_iid_04 int 

select @PerfilNuevo = perf_c_yid from [dbo].[SGA_T_PERFIL]
where perf_c_vnomb = 'Gerente de Mall'

select @Menu_c_iid  = Menu_c_iid  from SGA_T_MENU where menu_c_vnomb = 'PUBLICIDAD'
select @Menu_c_iid_01  = Menu_c_iid  from SGA_T_MENU where menu_c_vnomb = 'Mis Reservas'
select @Menu_c_iid_02  = Menu_c_iid  from SGA_T_MENU where menu_c_vnomb = 'Reserva de espacio publicitario'
select @Menu_c_iid_03  = Menu_c_iid  from SGA_T_MENU where menu_c_vnomb = 'Aprobación de espacio vendido'
select @Menu_c_iid_04  = Menu_c_iid  from SGA_T_MENU where menu_c_vnomb = 'Reporte de espacio publicitario'

---Agregar Menu al Perfil Gerente de Mall
INSERT INTO SGA_T_PERFIL_MENU (
		perf_c_yid,
		menu_c_iid,
		perf_menu_c_calta,
		perf_menu_c_cmod,
		perf_menu_c_celim,
		perf_menu_c_cvisual,
		perf_menu_c_cimpre,
		perf_menu_c_cproc,
		bita_c_zfec_reg,
		bita_c_vusu_red_reg,
		bita_c_vnom_completo_reg
) VALUES (
		@PerfilNuevo, -- PERFILES AUTORIZADOS 21 
		@Menu_c_iid, --NUEVO CORRELATIVO DE SUB MENU QUE SE GENERÓ DEL PASO 1
		'A',
		'A',
		'A',
		'A',
		'A',
		'A',
		GETDATE(),
		'SISTEMA',
		'SISTEMA'
);

INSERT INTO SGA_T_PERFIL_MENU (
		perf_c_yid,
		menu_c_iid,
		perf_menu_c_calta,
		perf_menu_c_cmod,
		perf_menu_c_celim,
		perf_menu_c_cvisual,
		perf_menu_c_cimpre,
		perf_menu_c_cproc,
		bita_c_zfec_reg,
		bita_c_vusu_red_reg,
		bita_c_vnom_completo_reg
) VALUES (
		@PerfilNuevo, -- PERFILES AUTORIZADOS 21 
		@Menu_c_iid_01, --NUEVO CORRELATIVO DE SUB MENU QUE SE GENERÓ DEL PASO 1
		'A',
		'A',
		'A',
		'A',
		'A',
		'A',
		GETDATE(),
		'SISTEMA',
		'SISTEMA'
);

INSERT INTO SGA_T_PERFIL_MENU (
		perf_c_yid,
		menu_c_iid,
		perf_menu_c_calta,
		perf_menu_c_cmod,
		perf_menu_c_celim,
		perf_menu_c_cvisual,
		perf_menu_c_cimpre,
		perf_menu_c_cproc,
		bita_c_zfec_reg,
		bita_c_vusu_red_reg,
		bita_c_vnom_completo_reg
) VALUES (
		@PerfilNuevo, -- PERFILES AUTORIZADOS 21 
		@Menu_c_iid_02, --NUEVO CORRELATIVO DE SUB MENU QUE SE GENERÓ DEL PASO 1
		'A',
		'A',
		'A',
		'A',
		'A',
		'A',
		GETDATE(),
		'SISTEMA',
		'SISTEMA'
);

INSERT INTO SGA_T_PERFIL_MENU (
		perf_c_yid,
		menu_c_iid,
		perf_menu_c_calta,
		perf_menu_c_cmod,
		perf_menu_c_celim,
		perf_menu_c_cvisual,
		perf_menu_c_cimpre,
		perf_menu_c_cproc,
		bita_c_zfec_reg,
		bita_c_vusu_red_reg,
		bita_c_vnom_completo_reg
) VALUES (
		@PerfilNuevo, -- PERFILES AUTORIZADOS 21 
		@Menu_c_iid_03, --NUEVO CORRELATIVO DE SUB MENU QUE SE GENERÓ DEL PASO 1
		'A',
		'A',
		'A',
		'A',
		'A',
		'A',
		GETDATE(),
		'SISTEMA',
		'SISTEMA'
);

INSERT INTO SGA_T_PERFIL_MENU (
		perf_c_yid,
		menu_c_iid,
		perf_menu_c_calta,
		perf_menu_c_cmod,
		perf_menu_c_celim,
		perf_menu_c_cvisual,
		perf_menu_c_cimpre,
		perf_menu_c_cproc,
		bita_c_zfec_reg,
		bita_c_vusu_red_reg,
		bita_c_vnom_completo_reg
) VALUES (
		@PerfilNuevo, -- PERFILES AUTORIZADOS 21 
		@Menu_c_iid_04, --NUEVO CORRELATIVO DE SUB MENU QUE SE GENERÓ DEL PASO 1
		'A',
		'A',
		'A',
		'A',
		'A',
		'A',
		GETDATE(),
		'SISTEMA',
		'SISTEMA'
);

---Agregar Menu al Perfil Ejecutivo de ventas

INSERT INTO SGA_T_PERFIL_MENU (
		perf_c_yid,
		menu_c_iid,
		perf_menu_c_calta,
		perf_menu_c_cmod,
		perf_menu_c_celim,
		perf_menu_c_cvisual,
		perf_menu_c_cimpre,
		perf_menu_c_cproc,
		bita_c_zfec_reg,
		bita_c_vusu_red_reg,
		bita_c_vnom_completo_reg
) VALUES (
		@PerfilEjecutivoVentas, -- PERFILES AUTORIZADOS 21 
		@Menu_c_iid_04, --NUEVO CORRELATIVO DE SUB MENU QUE SE GENERÓ DEL PASO 1
		'A',
		'A',
		'A',
		'A',
		'A',
		'A',
		GETDATE(),
		'SISTEMA',
		'SISTEMA'
);

---Agregar Menu al Perfil Jefe de Ventas

INSERT INTO SGA_T_PERFIL_MENU (
		perf_c_yid,
		menu_c_iid,
		perf_menu_c_calta,
		perf_menu_c_cmod,
		perf_menu_c_celim,
		perf_menu_c_cvisual,
		perf_menu_c_cimpre,
		perf_menu_c_cproc,
		bita_c_zfec_reg,
		bita_c_vusu_red_reg,
		bita_c_vnom_completo_reg
) VALUES (
		@PerfilJefeVentas, -- PERFILES AUTORIZADOS 21 
		@Menu_c_iid_04, --NUEVO CORRELATIVO DE SUB MENU QUE SE GENERÓ DEL PASO 1
		'A',
		'A',
		'A',
		'A',
		'A',
		'A',
		GETDATE(),
		'SISTEMA',
		'SISTEMA'
);

---Agregar Menu al Perfil Coordinador de Ventas
INSERT INTO SGA_T_PERFIL_MENU (
		perf_c_yid,
		menu_c_iid,
		perf_menu_c_calta,
		perf_menu_c_cmod,
		perf_menu_c_celim,
		perf_menu_c_cvisual,
		perf_menu_c_cimpre,
		perf_menu_c_cproc,
		bita_c_zfec_reg,
		bita_c_vusu_red_reg,
		bita_c_vnom_completo_reg
) VALUES (
		@PerfilCoordinadorVentas, -- PERFILES AUTORIZADOS 21 
		@Menu_c_iid_04, --NUEVO CORRELATIVO DE SUB MENU QUE SE GENERÓ DEL PASO 1
		'A',
		'A',
		'A',
		'A',
		'A',
		'A',
		GETDATE(),
		'SISTEMA',
		'SISTEMA'
);