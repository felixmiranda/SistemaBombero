USE DIONISIO
GO

CREATE PROCEDURE [PUBLICIDAD].[SP_PUB_RESERVA_LISTA_NO_DISPONIBLE]
/*****************************************
Descripción		: Obtiene el detalle de los espacios no disponibles que vienen del servicio Advance WS
Retorno			: 
Notas			: --
Autor y CodRed	: Jonathan Garcia Rojas
Fecha y hora	: 08-10-2017
Modificaciones	: --
Ejecución		: [PUBLICIDAD].[SP_PUB_RESERVA_LISTA_NO_DISPONIBLE]  '2017-10-01','2017-12-31','19,20,21,22,23,24,25,26,27,28,29,30,31,33,34,35,37,38,39,40,42,43,48,49,50,51,52,53,54,55,56,295,296,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,75,76,77,78,79,80,81,82,83,84,299,44,45,46,47,85,86,87,88,91,92,93,94,95,96,297,314,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,120,122,123,125,127,128,129,130,131,132,133,134,135,298,300,301,342,302,136,137,138,139,140,141,142,143,144,145,146,147,148,149,150,312,313'
*****************************************/
(
 @fechaInicio date ,
 @fechafin date ,
 @espaciosId nvarchar(max) = '1,2,3,4,5,6,7,8,9,10,11,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,33,34,35,37,38,39,40,42,43,48,49,50,51,52,53,54,55,56,295,296,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,75,76,77,78,79,80,81,82,83,84,299,44,45,46,47,85,86,87,88,91,92,93,94,95,96,297,314,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,120,122,123,125,127,128,129,130,131,132,133,134,135,298,300,301,342,302,136,137,138,139,140,141,142,143,144,145,146,147,148,149,150,312,313'
)
AS
BEGIN

DECLARE @fec_inicio DATETIME,@fec_fin DATETIME,@diasTotales INT
SET @fec_inicio=CONVERT(varchar(8),@fechaInicio,112)
SET @fec_fin=CONVERT(varchar(8),@fechafin,112);

SELECT @diasTotales =  DATEDIFF(DAY,@fec_inicio,@fec_fin)


CREATE TABLE #RESERVAS_DETALLE
(
	pub_esp_c_iid int,
	pub_esp_c_vcod varchar(100),
	fecha_reserva date,
	estado_reserva varchar(150)
)

 ;WITH FECHAS(fecha) AS (
			SELECT @fec_inicio fecha
			UNION ALL
			SELECT DATEADD(day, 1, fecha) fecha
			FROM FECHAS WHERE fecha < @fec_fin 
			)

INSERT INTO #RESERVAS_DETALLE
SELECT r.pub_esp_c_iid,r.pub_esp_c_vcod,fec.fecha,est.esp_ocu_est_c_vnomb
from PUBLICIDAD.DIO_PUB_T_RESERVA_DET d 
inner join PUBLICIDAD.DIO_PUB_T_RESERVA r on r.reser_c_iid = d.reser_c_iid and d.reser_det_c_bactivo = 1
inner join PUBLICIDAD.DIO_PUB_T_ESPACIO_OCUP_ESTADO est on est.esp_ocu_est_c_iid=r.esp_ocu_est_c_iid
--inner join PUBLICIDAD.Split(@espaciosId,',') esp on esp.Value = r.pub_esp_c_iid
right join FECHAS fec on convert(varchar(8),d.reser_det_c_dfech,112)=fec.fecha
where isnull(r.esp_ocu_est_c_iid,0) !=4 
order by r.pub_esp_c_vcod,fec.fecha asc
OPTION (MaxRecursion 0)

;WITH TEMP_ESPACIOS_DISPONIBLES 
(	pub_esp_c_idd,
	pub_esp_c_vcod,
	TotalNoDisponible,
	TotalDiasConsulta,
	EsDisponible
)
AS(
	select
	rd.pub_esp_c_iid,
	rd.pub_esp_c_vcod,
	 sum(
		case when estado_reserva = 'VENDIDO' OR estado_reserva  ='RESERVADO'
		THEN 1 ELSE 0 END
	) TotalNoDisponible,
	@diasTotales TotalDias,
	CASE WHEN 
	(
		sum(
		case when estado_reserva = 'VENDIDO' OR estado_reserva  ='RESERVADO'
		THEN 1 ELSE 0 END
		) <= (@diasTotales *0.2)
	)THEN 'S' ELSE 'N' END Disponible

	from #RESERVAS_DETALLE rd
	group  by pub_esp_c_iid,pub_esp_c_vcod 
	--order by pub_esp_c_vcod 
)

SELECT  pub_esp_c_idd,
pub_esp_c_vcod
FROM TEMP_ESPACIOS_DISPONIBLES
WHERE EsDisponible = 'N'
order by 1 asc

DROP TABLE #RESERVAS_DETALLE
--select * from #RESERVAS_DETALLE


END