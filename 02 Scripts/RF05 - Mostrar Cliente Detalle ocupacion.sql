
ALTER procedure [PUBLICIDAD].[SP_PUB_RESERVA_DETALLE_XIDESPACIO_LISTAR]
--------Descripción		: SP que devuelve el detalle de la ocupacion por espacio y elemento entre un rago de fechas.
--------Retorno			: Lista de los espacios x FILTRO multiple 
--------Notas				: N/A
--------Autor y CodRed	: Miguel Martinez / RP0945
--------Fecha y hora		: 15-02-2017
--------Usuario Modifica  : Jonathan Garcia
--------Modificaciones	: Agregar Cliente Marca
--------Fecha y hora		: 22-10-2017
 @pi_esp_c_iid int ,
 @pi_pub_elem_act_c_iid int ,
 @pd_fec_inicio DateTime ,
 @pi_fec_fin DateTime 
as
declare @fec_inicio datetime,@fec_fin datetime,@elementovnomb varchar(200)
set @fec_inicio=convert(varchar(8),@pd_fec_inicio,112)
set @fec_fin=convert(varchar(8),@pi_fec_fin,112);

 WITH FECHAS(fecha) AS (
			SELECT @fec_inicio fecha
			UNION ALL
			SELECT DATEADD(day, 1, fecha) fecha
			FROM FECHAS WHERE fecha < @fec_fin 
			)

select
	fec.fecha,
	isnull(est.esp_ocu_est_c_vnomb,'DISPONIBLE') as estado, 
      CASE  
	     WHEN (est.esp_ocu_est_c_vnomb is null and fec.fecha<convert(varchar(8),GETDATE(),112)) 
			THEN '---'		--detalle de la ocupacion
		 WHEN (est.esp_ocu_est_c_vnomb is null) 
			THEN ''			--detalle de la ocupacion
		WHEN (est.esp_ocu_est_c_vnomb='VENDIDO') 
			THEN
			 (select 
			     asig.tip_asig_c_vnomb +' - '+ isnull(colb.colab_c_vnomb,'') + ' ' + isnull(colb.colab_c_vape_pat,'')
				 from PUBLICIDAD.DIO_PUB_T_RESERVA_DET as d1
					inner join PUBLICIDAD.DIO_PUB_T_RESERVA as r1 on r1.reser_c_iid = d1.reser_c_iid and r1.pub_esp_c_iid=@pi_esp_c_iid and d1.reser_det_c_bactivo = 1
					inner join PUBLICIDAD.DIO_PUB_T_RESERVA_MASTER mas on mas.reser_mast_c_iid=r1.reser_mast_c_iid
					inner join PUBLICIDAD.DIO_PUB_T_TIPO_ASIGNACION asig on asig.tip_asig_c_iid=r1.tip_asig_c_iid
					inner join ADVANCE.ADV_T_COLABORADOR colb on colb.colab_c_cdoc_id = mas.ejec_c_cdoc_id collate Modern_Spanish_CI_AS
					where r.pub_esp_c_iid = r1.pub_esp_c_iid and fec.fecha = d1.reser_det_c_dfech and esp_ocu_est_c_iid = 3 and r1.reser_c_bactivo=1
			 )
		 WHEN (est.esp_ocu_est_c_vnomb='RESERVADO') 
			THEN
			(isnull(STUFF((    	
			 select ' / ' + convert(varchar(10),reser_c_inum_orden)
				 +' - '+ isnull(colb.colab_c_vnomb,'') + ' ' + isnull(colb.colab_c_vape_pat,'') +' ('+CONVERT(VARCHAR(10),reser_c_dfech_vencimiento,103)+')'
				 from PUBLICIDAD.DIO_PUB_T_RESERVA_DET as d1
					inner join PUBLICIDAD.DIO_PUB_T_RESERVA as r1
					on r1.reser_c_iid = d1.reser_c_iid and r1.pub_esp_c_iid=@pi_esp_c_iid and d1.reser_det_c_bactivo = 1 
					inner join PUBLICIDAD.DIO_PUB_T_RESERVA_MASTER mas on mas.reser_mast_c_iid=r1.reser_mast_c_iid
					inner join ADVANCE.ADV_T_COLABORADOR colb
					on colb.colab_c_cdoc_id = mas.ejec_c_cdoc_id collate Modern_Spanish_CI_AS
					where  r.pub_esp_c_iid = r1.pub_esp_c_iid and fec.fecha = d1.reser_det_c_dfech and r1.esp_ocu_est_c_iid=2 and r1.reser_c_bactivo=1
					order by reser_c_inum_orden
                        FOR XML PATH('') 
                        ), 1, 2, '' ),''))
         END  as  detalle,
		 CASE 
			WHEN (est.esp_ocu_est_c_vnomb='VENDIDO') OR (est.esp_ocu_est_c_vnomb='RESERVADO') 
			THEN 
				ISNULL(cli.cli_c_vraz_soc,'') + ' - ' + ISNULL(marc.marc_c_vnomb,'') 
				ELSE '--'
			END as 'ClienteMarca'
from PUBLICIDAD.DIO_PUB_T_RESERVA_DET d 
inner join PUBLICIDAD.DIO_PUB_T_RESERVA r on r.reser_c_iid = d.reser_c_iid and r.pub_esp_c_iid=@pi_esp_c_iid  and  d.reser_det_c_bactivo = 1
--inner join ADVANCE.ADV_T_PUB_ELEMENTO_ACTIVACION ele on ele.pub_elem_act_c_iid=r.pub_elem_act_c_iid and r.pub_elem_act_c_iid = @pi_pub_elem_act_c_iid
inner join PUBLICIDAD.DIO_PUB_T_ESPACIO_OCUP_ESTADO est on est.esp_ocu_est_c_iid=r.esp_ocu_est_c_iid
right join FECHAS fec on convert(varchar(8),d.reser_det_c_dfech,112)=fec.fecha
left join PUBLICIDAD.DIO_PUB_T_MARCA marc on marc.marc_c_icod = r.marc_c_icod
left join ADVANCE.ADV_T_CLIENTE cli on cli.cli_c_vdoc_id = r.cli_c_vdoc_id
where isnull(r.esp_ocu_est_c_iid,0) !=4 
group by fec.fecha,est.esp_ocu_est_c_vnomb, r.pub_esp_c_iid,cli.cli_c_vraz_soc,marc.marc_c_vnomb
order by fec.fecha asc
OPTION (MaxRecursion 0)

