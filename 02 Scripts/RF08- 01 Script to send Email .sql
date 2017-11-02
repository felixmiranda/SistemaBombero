
USE DIONISIO
go
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
DATEDIFF(DAY,Convert(varchar(10),dateadd(day,4,GETDATE()),103),Convert(varchar(10),RE.reser_c_dfech_vencimiento,103) ) = 3

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
 N'<td>' + @Cod_espacio  + N'</td>'+
 N'<td>' + @Inmueble  + N'</td>'+
 N'<td>' + @Marca  + N'</td>'+
 N'</tr>'


 --select * from #ReservasPorVencer where RowNumber = @RowIni
 set @RowIni += 1 
end 


set @TableHtml = 
N'<h1>Reservas proximas a vencer </h1>' +
N'<p>Hola ' + @NombreCompleto+ ' </p>' +
N'<p>El alquiler por el espacio publicitario finalizara en 72 horas. Por favor programar la desinstalacion correspondiente</p>'+
N'<table border="1">' +
N'<thead><tr><th>Codigo Espacio</th><th>Marca</th><th>Inmueble</th></tr></thead>' +
N'<tbody>' + @TableDinamica +
N'</tbody>' +
N'</table><br><p>Saludos, </p><br>' +
N'<strong>Nota: </strong><i>El presente correo ha sido generado y enviado en forma automatica. Por favor no responder el correo</i>'
drop table #ReservasPorVencer
--print @TableHtml
EXEC msdb.dbo.sp_send_dbmail @recipients='felixmiranda.net@gmail.com', --@Correo
@profile_name = 'Felix Miranda',
@subject = 'Alerta vencimiento Reserva',
@body = @TableHtml,
@body_format = 'HTML';