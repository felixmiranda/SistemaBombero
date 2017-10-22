
INSERT INTO [dbo].[SGA_T_USUARIO]
           ([usua_c_cusu_red]
           ,[usua_c_cape_pat]
           ,[usua_c_cape_mat]
           ,[usua_c_cape_nombres]
           ,[usua_c_cdoc_id]
           ,[usua_c_vcorreo1]
           ,[usua_c_vcorreo2]
           ,[usua_c_vcontrasena]
           ,[usua_c_vestado]
           ,[usua_c_vnuevacont]
           ,[bita_c_zfec_reg]
           ,[bita_c_vusu_red_reg]
           ,[bita_c_vnom_completo_reg])
     VALUES
           ('RP2504'
           ,'Miranda'
           ,'Robles'
           ,'Felix '
           ,'18216010'
           ,'felixmiranda.net@gmail.com'
           ,'felix@mirros.pe'
           ,'123456'
           ,'A'
           ,'1'
           ,getdate()
           ,'SISTEMA'
           ,'USUARIO GERENTE DE MALL')
GO


INSERT INTO [dbo].[SGA_T_USUARIO_PERFIL]
           ([perf_c_yid]
           ,[usua_c_cdoc_id]
           ,[usua_perfil_c_cestado]
           ,[bita_c_zfec_reg]
           ,[bita_c_vusu_red_reg]
           ,[bita_c_vnom_completo_reg])
     VALUES
           (21
           ,'18216010'
           ,'A'
           ,GETDATE()
           ,'SISTEMA'
           ,'USUARIO GERENTE DE MALL')
GO


