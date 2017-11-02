
USE DIONISIO
go

CREATE PROCEDURE SP_EnvioCorreo_01_dias_Antes_Reserva
----Descripción		: Store procedure . Envio de corroe 01 dias antes del vencimiento de la reserva.
----Retorno			: ------
----Notas			: N/A
----Autor y CodRed	: Felix Miranda / 
----Fecha y hora	: 02/11/2017
----Usuario Modifica: -
----Modificaciones	: --
----Fecha y hora	: -- 
as

declare @TableHtml nvarchar(max);



select  
ROW_NUMBER() OVER(ORDER BY pub_esp_c_vcod ) AS [RowNumber],ISNULL(MAR.marc_c_vnomb, '--') as MARCA, I.inm_c_vnomb AS 'INMUEBLE', RE.pub_esp_c_vcod AS 'COD_ESPACIO',--Convert(varchar(10),RE.reser_c_dfech_vencimiento,103) FechaVencimiento  ,Convert(varchar(10),dateadd(day,4,GETDATE()),103)  as FechaActual, DATEDIFF(DAY,Convert(varchar(10),dateadd(day,4,GETDATE()),103),Convert(varchar(10),RE.reser_c_dfech_vencimiento,103) ) as DiferenciaDias, 
u.usua_c_vcorreo1 as CorreoCordinador, ma.bita_c_vnomb_completo_reg as NombreCompleto
INTO #ReservasPorVencer
from PUBLICIDAD.DIO_PUB_T_RESERVA_MASTER MA 
INNER JOIN PUBLICIDAD.DIO_PUB_T_RESERVA  RE on ma.reser_mast_c_iid = re.reser_mast_c_iid
inner join [PUBLICIDAD].[DIO_PUB_T_MARCA] MAR on MAR.marc_c_icod = RE.marc_c_icod
INNER JOIN [ADVANCE].[ADV_T_INMUEBLE] I ON I.inm_c_icod = RE.inm_c_icod
INNER JOIN [ANUBIS_BOM].dbo.[SGA_T_USUARIO] U on u.usua_c_cdoc_id = ma.ejec_c_cdoc_id
where 
DATEDIFF(DAY,Convert(varchar(10),GETDATE(),103),Convert(varchar(10),RE.reser_c_dfech_vencimiento,103) ) = 1
and re.esp_ocu_est_c_iid = 2 and re.reser_c_flat_bpendiente = 0


declare @TableDinamica nvarchar(max);
declare @RowsMax int 
declare  @RowIni int
declare @Cod_espacio varchar(50)
declare @Inmueble varchar(100)
declare @Marca varchar(100)
declare @Correo varchar(50)
declare @NombreCompleto varchar(100)

select distinct  @Correo = CorreoCordinador from #ReservasPorVencer
select distinct  @NombreCompleto = NombreCompleto from #ReservasPorVencer

 select  @RowsMax = max(RowNumber) from #ReservasPorVencer
 select  @RowIni = min(RowNumber) from #ReservasPorVencer
--select @RowsMax, @RowIni
set @TableDinamica = ''
while (@RowIni <= @RowsMax )
begin
select  @Cod_espacio = cod_espacio, @Inmueble = INMUEBLE, @Marca = MARCA from #ReservasPorVencer where RowNumber = @RowIni 

 set @TableDinamica = @TableDinamica + 
 N'<tr>'+ 
 N'<td style="border:solid 1px #237b96;width:200px;text-align:center;">' + @Cod_espacio  + N'</td>'+
 N'<td style="border:solid 1px #237b96;width:200px;text-align:center;">' + @Inmueble  + N'</td>'+
 N'<td style="border:solid 1px #237b96;width:200px;text-align:center;">' + @Marca  + N'</td>'+
 N'</tr>'


 --select * from #ReservasPorVencer where RowNumber = @RowIni
 set @RowIni += 1 
end 


set @TableHtml = 
N'<div id = "contenedor" style="width: 830px; margin: 0 auto;font-family:Calibri,Georgia,Serif;font-size:14px;">' +
N'		<div id = "cabecera" style="background-color:#237b96; solid #004B8A;height:90px;">' +
N'			<img src="http://www.yoquieroserrealplaza.com.pe/WebBombero/images/logo_real_blanco_email.png" width="300px" alt="LogoAdvance.png" />' +
N'		</div>' +
N'		<div id = "cuerpo" style="padding:15px;">' +
N'			<p>Estimado(a) '+@NombreCompleto+' <b></b>,</p>' +
N'			<p>El alquiler por el espacio publicitario finalizara en 72 horas. Por favor programar la desinstalacion correspondiente' +
N'		<br/><br/>'+
N'		</div>' +
N'<table border="0" cellspacing="0" cellpadding="2" style="font-family:Calibri,Georgia,Serif;font-size:14px;">' +
N'<thead><tr style="border:solid 1px;background:#237b96; color:#fff;font-weigth:bold"><th>Codigo Espacio</th><th>Marca</th><th>Inmueble</th></tr></thead>' +
N'<tbody>' + @TableDinamica +
N'</tbody>' +
N'</table>' +
N'		<div id = "pie">' +
N'			<p>Saludos,</p>' +
N'			<br/><br/>' +
N'			<p><b>Nota:</b> <i>El presente correo ha sido generado y enviado en forma automática. Por favor no responder el correo.</i></p>' +
N'		</div>' +
N'<div>'
drop table #ReservasPorVencer
print @TableHtml
EXEC msdb.dbo.sp_send_dbmail @recipients='felixmiranda.net@gmail.com', --@Correo
@profile_name = 'Felix Miranda',
@subject = 'Alerta vencimiento Reserva',
@body = @TableHtml,
@body_format = 'HTML',
@importance = 'HIGH';