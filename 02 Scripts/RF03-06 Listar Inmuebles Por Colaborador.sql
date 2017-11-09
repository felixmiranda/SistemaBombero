CREATE PROCEDURE [ADVANCE].[SP_ADV_OBTENER_LISTA_INMUEBLES_COLABORADOR]
/*****************************************
Descripción		: Obtiene lista de inmuebles por colaborador
Retorno			: 
Notas			: --
Autor y CodRed	: Jonathan Garcia Rojas
Fecha y hora	: 08-11-2017
Modificaciones	: --
Ejecución		: [ADVANCE].[SP_ADV_OBTENER_LISTA_INMUEBLES_COLABORADOR]  'RP0008' 
*****************************************/

@CodigoColaborador varchar(10)

AS
BEGIN 

	SELECT INMC.colab_c_cusu_red,INMC.inm_c_icod,INM.inm_c_vnomb
	FROM ADVANCE.ADV_T_COLAB_INM INMC
	INNER JOIN ADVANCE.ADV_T_INMUEBLE INM ON INMC.inm_c_icod = INM.inm_c_icod
	WHERE INMC.colab_c_cusu_red = @CodigoColaborador

END