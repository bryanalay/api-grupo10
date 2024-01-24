USE almidb001;
GO
ALTER PROCEDURE GetOrden
	@Transaccion as VARCHAR(50),
	@XML as XML = null
AS
BEGIN
	
	DECLARE @nombre as varchar(50);
	DECLARE @descripcion as varchar(50);
	DECLARE @cantidad as int;
	DECLARE @respuesta as VARCHAR(10);
	DECLARE @leyenda AS VARCHAR(100);
	DECLARE @id AS INT;
	DECLARE @tarea AS varchar(50);
	DECLARE @fecha AS varchar(50);
	DECLARE @estado AS varchar(50);
	DECLARE @cliente AS varchar(50);
	DECLARE @empleado_asignado AS varchar(50);

	BEGIN TRY

		IF(@Transaccion = 'GET_ORDENES')
		BEGIN
			SELECT * FROM orden;
			SET @respuesta = 'Ok';
			SET @leyenda = 'Consulta Exitosa';
		END

		IF(@Transaccion = 'POST_ORDEN')
		BEGIN
			SET @tarea = (select @XML.value('(/Orden/Tarea)[1]','varchar(50)'))
			SET @fecha = (select @XML.value('(/Orden/Fecha)[1]','varchar(50)'))
			SET @estado = (select @XML.value('(/Orden/Estado)[1]','varchar(50)'))
			SET @cliente = (select @XML.value('(/Orden/Cliente)[1]','varchar(50)'))
			SET @empleado_asignado = (select @XML.value('(/Orden/EmpleadoAsignado)[1]','varchar(50)'))

			INSERT INTO orden(tarea,fecha,estado,cliente,empleado_asignado)
			values(@tarea,@fecha,@estado,@cliente,@empleado_asignado);

			SET @respuesta = 'Ok';
			SET @leyenda = 'Se guardo la orden '+@tarea+' correctamente';
		END

		IF(@Transaccion = 'DELETE_ORDEN_BY_ID')
		BEGIN
			
			SET @id = (select @XML.value('(/Orden/Id)[1]','INT'))

			DELETE FROM orden where id = @id;

			SET @respuesta = 'Ok';
			SET @leyenda = 'Se elimino la orden correctamente';
		END

		IF(@Transaccion = 'UPDATE_ORDEN_BY_ID')
		BEGIN
			SET @tarea = (select @XML.value('(/Orden/Tarea)[1]','varchar(50)'))
			SET @fecha = (select @XML.value('(/Orden/Fecha)[1]','varchar(50)'))
			SET @estado = (select @XML.value('(/Orden/Estado)[1]','varchar(50)'))
			SET @cliente = (select @XML.value('(/Orden/Cliente)[1]','varchar(50)'))
			SET @empleado_asignado = (select @XML.value('(/Orden/EmpleadoAsignado)[1]','varchar(50)'))
			SET @id = (select @XML.value('(/Orden/Id)[1]','int'))
			UPDATE orden
			SET 
				tarea = @tarea,
				fecha = @fecha,
				estado = @estado,
				cliente = @cliente,
				empleado_asignado = @empleado_asignado

			WHERE id = @id;

			SET @respuesta = 'Ok';
			SET @leyenda = 'Se actualizo orden con id '+CAST(@id AS VARCHAR)+' correctamente';
		END

	END TRY
	BEGIN CATCH
	END CATCH
	SELECT @respuesta as respuesta, @leyenda as leyenda
END
GO