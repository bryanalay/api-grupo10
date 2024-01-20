USE almidb001;
GO
ALTER PROCEDURE GetInventario
	@Transaccion as VARCHAR(50),
	@XML as XML = null
AS
BEGIN
	
	DECLARE @nombre as varchar(50);
	DECLARE @descripcion as varchar(50);
	DECLARE @cantidad as int;
	DECLARE @respuesta as VARCHAR(10);
	DECLARE @leyenda AS VARCHAR(100);

	BEGIN TRY

		IF(@Transaccion = 'GET_INVENTARIO')
		BEGIN
			SELECT * FROM inventario;
			SET @respuesta = 'Ok';
			SET @leyenda = 'Consulta Exitosa';
		END

		IF(@Transaccion = 'POST_INVENTARIO')
		BEGIN
			SET @nombre = (select @XML.value('(/InventarioPiezas/Nombre)[1]','varchar(50)'))
			SET @descripcion = (select @XML.value('(/InventarioPiezas/Descripcion)[1]','varchar(50)'))
			SET @cantidad = (select @XML.value('(/InventarioPiezas/Cantidad)[1]','INT'))

			INSERT INTO inventario(nombre,descripcion,cantidad)
			values(@nombre,@descripcion,@cantidad);

			SET @respuesta = 'Ok';
			SET @leyenda = 'Se guardo '+@nombre+' correctamente';
		END

	END TRY
	BEGIN CATCH
	END CATCH
	SELECT @respuesta as respuesta, @leyenda as leyenda
END
GO