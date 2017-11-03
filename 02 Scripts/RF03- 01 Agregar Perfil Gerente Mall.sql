IF not EXISTS (
				select * from [dbo].[SGA_T_PERFIL] 
				where [perf_c_vnomb] = 'Gerente de Mall')

BEGIN

	INSERT INTO [dbo].[SGA_T_PERFIL]
			   ([perf_c_vnomb]
			   ,[perf_c_vdesc]
			   ,[perf_c_cestado]
			   ,[sist_c_iid]
			   ,[bita_c_zfec_reg]
			   ,[bita_c_vusu_red_reg]
			   ,[bita_c_vnom_completo_reg])
		 VALUES
			   ('Gerente de Mall'
			   ,'Usuario con permisos especiales'
			   ,'A'
			   ,5
			   ,getdate()
			   ,'RP0945'
			   ,'MIGUEL MARTINEZ QUICAÑO')
END