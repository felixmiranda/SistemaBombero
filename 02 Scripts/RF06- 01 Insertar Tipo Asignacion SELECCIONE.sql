use Dionisio
GO

SET IDENTITY_INSERT [PUBLICIDAD].DIO_PUB_T_TIPO_ASIGNACION  ON

insert into [PUBLICIDAD].DIO_PUB_T_TIPO_ASIGNACION (tip_asig_c_iid, tip_asig_c_vnomb, tip_asig_c_bactivo, bita_c_zfec_reg, bita_c_vusu_red_reg, bita_c_vnomb_completo_reg)
values (0,'SELECCIONE', 1, GETDATE(),'SISTEMA', 'SISTEMA')

SET IDENTITY_INSERT [PUBLICIDAD].DIO_PUB_T_TIPO_ASIGNACION OFF