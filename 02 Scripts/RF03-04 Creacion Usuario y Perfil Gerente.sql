
-- EL CORRELATIVO SE OBTIENE DEL PASO 1

-- 21 -> GERENTE DE MALL
declare @PerfilNuevo int

select @PerfilNuevo = perf_c_yid from [dbo].[SGA_T_PERFIL]
where perf_c_vnomb = 'Gerente de Mall'

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
           ,'Garcia'
           ,'Rojas'
           ,'Jonathan '
           ,'43116257'
           ,'jonathan_g33@hotmail.com'
           ,NULL
           ,'123456'
           ,'A'
           ,'1'
           ,getdate()
           ,'SISTEMA'
           ,'USUARIO GERENTE DE MALL')



INSERT INTO [dbo].[SGA_T_USUARIO_PERFIL]
           ([perf_c_yid]
           ,[usua_c_cdoc_id]
           ,[usua_perfil_c_cestado]
           ,[bita_c_zfec_reg]
           ,[bita_c_vusu_red_reg]
           ,[bita_c_vnom_completo_reg])
     VALUES
           (@PerfilNuevo
           ,'43116257'
           ,'A'
           ,GETDATE()
           ,'SISTEMA'
           ,'USUARIO GERENTE DE MALL')
GO


