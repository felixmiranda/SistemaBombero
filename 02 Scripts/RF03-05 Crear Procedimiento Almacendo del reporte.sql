IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[PUBLICIDAD].[DIO_SP_PUB_REPORTE_ESPACIOS_PUBLICITARIOS]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [PUBLICIDAD].[DIO_SP_PUB_REPORTE_ESPACIOS_PUBLICITARIOS]
END

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [PUBLICIDAD].[DIO_SP_PUB_REPORTE_ESPACIOS_PUBLICITARIOS]

/*****************************************
Descripción		: Permite listar los espaciospublicitarios
Retorno			: Lista
Notas			: --
Autor y CodRed	: Felix Miranda
Fecha y hora	: 22-10-2017
Modificaciones	: --
Ejecución		: exec [PUBLICIDAD].[DIO_SP_PUB_REPORTE_ESPACIOS_PUBLICITARIOS] '14','0',0,0
*****************************************/

--declare @pv_ejecutivo as char(8) ='0'
--declare @pv_inmueble as varchar(500) = '14'
--declare @pv_estado as int = 0
--declare @pv_producto as int = 0


@pv_inmueble varchar(500),
@pv_ejecutivo varchar(20),
@pv_estado int,
@pv_producto int

as
declare @tmp_master table(
reser_mast_c_iid int
)

INSERT @tmp_master
SELECT ma.reser_mast_c_iid
FROM [PUBLICIDAD].[DIO_PUB_T_RESERVA_MASTER] MA
	INNER JOIN [PUBLICIDAD].[DIO_PUB_T_RESERVA] RE ON MA.reser_mast_c_iid = RE.reser_mast_c_iid
	INNER JOIN [ADVANCE].[ADV_T_INMUEBLE] I ON I.inm_c_icod = RE.inm_c_icod
	LEFT JOIN [ADVANCE].[ADV_T_CLIENTE] CLI ON CLI.cli_c_vdoc_id = RE.cli_c_vdoc_id
WHERE (I.inm_c_icod = @pv_inmueble or @pv_inmueble='0')

SELECT 

	I.inm_c_vnomb AS 'INMUEBLE',
	ISNULL(MAR.marc_c_vnomb,'--') AS 'MARCA',
	COL.colab_c_vnomb + ' ' + COL.colab_c_vape_pat AS 'EJECUTIVO',
	PROD.pub_prod_c_vnomb AS 'PRODUCTO',
	ACT.pub_elem_act_c_vnomb AS 'ELEMENTO_ACTIVACION', 
	RE.pub_esp_c_vcod AS 'COD_ESPACIO',
	RE.reser_c_vdesc_activacion as 'DESC_ESPACIO',
	CASE 
		WHEN RE.tip_asig_c_iid = 1
		THEN 'PAGADO'
		WHEN RE.tip_asig_c_iid = 2
		THEN 'BONIFICACION'
		WHEN RE.tip_asig_c_iid = 3
		THEN 'CESION'
		WHEN RE.tip_asig_c_iid = 4
		THEN 'CANJE'
	END AS 'TIPO_ASIGNACION',
	ISNULL(CLI.cli_c_vraz_soc,'--') AS 'CLIENTE',
	case when ROW_NUMBER()over ( partition by ma.reser_mast_c_iid order by re.reser_c_iid) = 1 then 1 else 0 end as 'MARCAR',
	ISNULL(AG.cli_c_vraz_soc,'--') AS 'AGENCIA',
	Convert(varchar(10),RE.reser_c_dfech_inicio,103) AS 'FECHA_INICIO',
	Convert(varchar(10),RE.reser_c_dfech_fin,103) AS 'FECHA_FIN',
	--(Convert(varchar(10),RE.reser_c_dfech_inicio,103)+' - '+Convert(varchar(10),RE.reser_c_dfech_fin,103))  AS 'PERIODO',

	--CASE 
	--	WHEN RE.esp_ocu_est_c_iid = 1
	--	THEN ''
	--	WHEN RE.esp_ocu_est_c_iid = 1
	--	THEN ''
	--	WHEN RE.esp_ocu_est_c_iid = 1
	--	THEN ''
	--	WHEN RE.esp_ocu_est_c_iid = 1
	--	THEN ''
	--	END  AS 'RESERVA',
	est.esp_ocu_est_c_vnomb as 'RESERVA',
	CASE 
		WHEN re.reser_c_flat_bpendiente = 1  
		THEN 'PENDIENTE'
		WHEN re.reser_c_flat_bpendiente =0
		THEN 'CERRADA'
	END AS  'ESTADORESERVA',
	isnull(RE.reser_c_eprecio_alquiler,0.00) AS 'PRECIO_ALQUILER'
FROM [PUBLICIDAD].[DIO_PUB_T_RESERVA_MASTER] MA
INNER JOIN [PUBLICIDAD].[DIO_PUB_T_RESERVA] RE ON MA.reser_mast_c_iid = RE.reser_mast_c_iid
INNER JOIN [ADVANCE].[ADV_T_INMUEBLE] I ON I.inm_c_icod = RE.inm_c_icod
INNER JOIN [ADVANCE].[ADV_T_PUB_PRODUCTO] PROD ON PROD.pub_prod_c_iid = RE.pub_prod_c_iid
INNER JOIN [ADVANCE].[ADV_T_PUB_ELEMENTO_ACTIVACION] ACT ON ACT.pub_elem_act_c_iid = RE.pub_elem_act_c_iid
INNER JOIN PUBLICIDAD.DIO_PUB_T_ESPACIO_OCUP_ESTADO est on est.esp_ocu_est_c_iid=re.esp_ocu_est_c_iid
LEFT JOIN [ADVANCE].[ADV_T_CLIENTE] CLI ON CLI.cli_c_vdoc_id = RE.cli_c_vdoc_id
LEFT JOIN [PUBLICIDAD].[DIO_PUB_T_MARCA] MAR ON MAR.marc_c_icod = RE.marc_c_icod
LEFT JOIN [ADVANCE].[ADV_T_CLIENTE] AG ON AG.cli_c_vdoc_id = RE.agen_c_vdoc_id
LEFT JOIN [ADVANCE].[ADV_T_COLABORADOR] COL ON COL.colab_c_cdoc_id = MA.ejec_c_cdoc_id
WHERE ma.reser_mast_c_iid in (select t.reser_mast_c_iid from @tmp_master t)
	AND (ma.ejec_c_cdoc_id = @pv_ejecutivo or @pv_ejecutivo='0')
	AND (re.esp_ocu_est_c_iid=@pv_estado OR @pv_estado = '')
	AND (re.pub_prod_c_iid = @pv_producto or @pv_producto = '') 
	AND re.reser_c_bactivo=1 
AND (I.inm_c_icod = @pv_inmueble or @pv_inmueble='0')
order by ma.reser_mast_c_iid


