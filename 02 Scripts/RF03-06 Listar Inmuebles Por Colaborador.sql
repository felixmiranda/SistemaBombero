CREATE PROCEDURE [ADVANCE].[SP_ADV_OBTENER_LISTA_INMUEBLES_COLABORADOR]
/*****************************************
Descripción		: Obtiene lista de inmuebles por colaborador
Retorno			: 
Notas			: --
Autor y CodRed	: Jonathan Garcia Rojas
Fecha y hora	: 08-11-2017
Modificaciones	: --
Ejecución		: [ADVANCE].[SP_ADV_OBTENER_LISTA_INMUEBLES_COLABORADOR]  'RP0008',20
*****************************************/

@CodigoColaborador varchar(10),
@CodigoPerfil INT

AS
BEGIN 

	DECLARE @NombrePerfil varchar(150) 
	
	SELECT @NombrePerfil  = RTRIM(perf_c_vnomb) FROM ANUBIS_BOM.dbo.SGA_T_PERFIL WHERE perf_c_yid = @CodigoPerfil

	IF(UPPER(@NombrePerfil) = 'GERENTE DE MALL')
	BEGIN
		SELECT DISTINCT INMC.inm_c_icod,INM.inm_c_vnomb
		FROM ADVANCE.ADV_T_COLAB_INM INMC
		INNER JOIN ADVANCE.ADV_T_INMUEBLE INM ON INMC.inm_c_icod = INM.inm_c_icod
		WHERE INMC.colab_c_cusu_red = @CodigoColaborador
		AND INM.inm_c_bactivo = 1
	END
	ELSE
	BEGIN
		SELECT inm_c_icod,inm_c_vnomb
		FROM ADVANCE.ADV_T_INMUEBLE
		WHERE inm_c_bactivo = 1 AND inm_c_vnomb LIKE 'REAL PLAZA%'
	END


END

