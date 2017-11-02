declare @Menu_c_iid int 

select @Menu_c_iid = Menu_c_iid from SGA_T_MENU where menu_c_vnomb = 'PUBLICIDAD'

--REGISTRAR NUEVO SUB MENU
INSERT INTO SGA_T_MENU (
		menu_c_iid_padre, 
		menu_c_vnomb, 
		menu_c_ynivel, 
		menu_c_vpag_asp, 
		menu_c_vpag_asp2,
		bita_c_zfec_reg, 
		bita_c_vusu_red_reg, 
		bita_c_vnom_completo_reg
) VALUES (
		@Menu_c_iid, -- MENU PADRE
		'Reporte de espacio publicitario', 
		2, -- NIVEL - SUB MENU
		'/Interfaces/Reserve/frmSpaceAdvertinsingReport.aspx', 
		'menu-icon fa fa-calendar',
		GETDATE(), 
		'SISTEMA', 
		'SISTEMA'
);