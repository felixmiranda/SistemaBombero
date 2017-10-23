CREATE PROCEDURE [PUBLICIDAD].[DIO_SP_PUB_REPORTE_ESPACIOS_PUBLICITARIOS]

/*****************************************
Descripción		: Permite listar los espaciospublicitarios
Retorno			: Lista
Notas			: --
Autor y CodRed	: Felix Miranda
Fecha y hora	: 22-10-2017
Modificaciones	: --
Ejecución		: [PUBLICIDAD].[DIO_SP_PUB_REPORTE_ESPACIOS_PUBLICITARIOS] 'REAL PLAZA AREQUIPA','HEWLETT - PACKARD PERU S.R.L.','40974057',4
*****************************************/
/*
declare @pv_ejecutivo as char(8) ='40974057'
declare @pv_inmueble as varchar(500) = 'REAL PLAZA AREQUIPA'
declare @pv_cliente as varchar(500) = 'HEWLETT - PACKARD PERU S.R.L.'
*/

@pv_inmueble varchar(500),
@pv_cliente varchar(500),
@pv_ejecutivo varchar(20),
@pv_estado int

as
declare @tmp_master table(
reser_mast_c_iid int
)

INSERT @tmp_master
select ma.reser_mast_c_iid
from [PUBLICIDAD].[DIO_PUB_T_RESERVA_MASTER] MA
INNER JOIN [PUBLICIDAD].[DIO_PUB_T_RESERVA] RE ON MA.reser_mast_c_iid = RE.reser_mast_c_iid
INNER JOIN [ADVANCE].[ADV_T_INMUEBLE] I ON I.inm_c_icod = RE.inm_c_icod
LEFT JOIN [ADVANCE].[ADV_T_CLIENTE] CLI ON CLI.cli_c_vdoc_id = RE.cli_c_vdoc_id
WHERE isnull(I.inm_c_vnomb,'') LIKE '%'+ @pv_inmueble +'%'
and isnull(cli.cli_c_vraz_soc,'') like '%'+ @pv_cliente +'%'



SELECT 
I.inm_c_vnomb AS 'INMUEBLE',
ISNULL(MAR.marc_c_vnomb,'') AS 'MARCA',
COL.colab_c_vnomb + ' ' + COL.colab_c_vape_pat AS 'EJECUTIVO',
PROD.pub_prod_c_vnomb AS 'PRODUCTO',
ACT.pub_elem_act_c_vnomb AS 'ELEMENTO_ACTIVACION', 
RE.pub_esp_c_vcod AS 'COD_ESPACIO',
'' as 'DESC_ESPACIO',
'' AS 'TIPO_ASIGNACION',
ISNULL(CLI.cli_c_vraz_soc,'') AS 'CLIENTE',
case when ROW_NUMBER()over ( partition by ma.reser_mast_c_iid order by re.reser_c_iid) = 1 then 1 else 0 end as 'MARCAR',
ISNULL(AG.cli_c_vraz_soc,'') AS 'AGENCIA',
'' AS 'FECHA_INICIO',
'' AS 'FECHA_FIN',
--(Convert(varchar(10),RE.reser_c_dfech_inicio,103)+' - '+Convert(varchar(10),RE.reser_c_dfech_fin,103))  AS 'PERIODO',
RE.reser_c_dfech_inicio,
RE.reser_c_dfech_fin,
'' AS 'RESERVA',
CASE 
	WHEN re.reser_c_flat_bpendiente = 1  
	THEN 'PENDIENTE'
	WHEN re.reser_c_flat_bpendiente =0
	THEN 'CERRADA'
END AS  'ESTADORESERVA'
FROM [PUBLICIDAD].[DIO_PUB_T_RESERVA_MASTER] MA
INNER JOIN [PUBLICIDAD].[DIO_PUB_T_RESERVA] RE ON MA.reser_mast_c_iid = RE.reser_mast_c_iid
INNER JOIN [ADVANCE].[ADV_T_INMUEBLE] I ON I.inm_c_icod = RE.inm_c_icod
INNER JOIN [ADVANCE].[ADV_T_PUB_PRODUCTO] PROD ON PROD.pub_prod_c_iid = RE.pub_prod_c_iid
INNER JOIN [ADVANCE].[ADV_T_PUB_ELEMENTO_ACTIVACION] ACT ON ACT.pub_elem_act_c_iid = RE.pub_elem_act_c_iid
LEFT JOIN [ADVANCE].[ADV_T_CLIENTE] CLI ON CLI.cli_c_vdoc_id = RE.cli_c_vdoc_id
LEFT JOIN [PUBLICIDAD].[DIO_PUB_T_MARCA] MAR ON MAR.marc_c_icod = RE.marc_c_icod
LEFT JOIN [ADVANCE].[ADV_T_CLIENTE] AG ON AG.cli_c_vdoc_id = RE.agen_c_vdoc_id
LEFT JOIN [ADVANCE].[ADV_T_COLABORADOR] COL ON COL.colab_c_cdoc_id = MA.ejec_c_cdoc_id
where ma.reser_mast_c_iid in (select t.reser_mast_c_iid from @tmp_master t)
and (ma.ejec_c_cdoc_id = @pv_ejecutivo or @pv_ejecutivo='')
and re.esp_ocu_est_c_iid=@pv_estado 
--and re.reser_c_bactivo=1 and re.reser_c_flat_bpendiente in (0,1)
order by ma.reser_mast_c_iid