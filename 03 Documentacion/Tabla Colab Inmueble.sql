


CREATE TABLE [ADVANCE].[ADV_T_COLAB_INM](
	[colab_c_cdoc_id] [char](8) NOT NULL,
	colab_c_cusu_red varchar(10) NOT NULL,
	[inm_c_icod] [int] NOT NULL,
	perf_c_yid int not null,
	[bita_c_zfec_reg] [datetime] NULL,
	[bita_c_vusu_red_reg] [varchar](10) NULL,
	[bita_c_vnomb_completo_reg] [varchar](100) NULL
)

GO


--INSERT [ADVANCE].[ADV_T_COLAB_INM]
--select COLAB.colab_c_cdoc_id,C.colab_c_cusu_red,COLAB.inm_c_icod,P.perf_c_yid,getdate(),'SISTEMA','SISTEMA'
--from ADVDB_BOMBERO.dbo.ADV_T_COLAB_INM COLAB
--INNER JOIN ADVDB_BOMBERO.dbo.ADV_T_COLABORADOR C ON COLAB.colab_c_cdoc_id = C.colab_c_cdoc_id
--INNER JOIN SGADB..SGA_T_USUARIO_PERFIL P ON P.usua_c_cdoc_id = C.colab_c_cusu_red


