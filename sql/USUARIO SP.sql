USE almidb001;
GO
ALTER PROCEDURE GetUsuario
	@Transaccion as VARCHAR(50),
	@XML as XML = null
AS
BEGIN
	
	DECLARE @nombre as varchar(50);
	DECLARE @password as VARCHAR(50);
	DECLARE @descripcion as varchar(50);
	DECLARE @cantidad as int;
	DECLARE @respuesta as VARCHAR(10);
	DECLARE @leyenda AS VARCHAR(100);

	BEGIN TRY

		IF(@Transaccion = 'GET_USUARIOS')
		BEGIN
			SELECT * FROM usuario;
			SET @respuesta = 'Ok';
			SET @leyenda = 'Consulta Exitosa';
		END

		/*IF(@Transaccion = 'POST_USUARIO')
		BEGIN
			SET @nombre = (select @XML.value('(/InventarioPiezas/Nombre)[1]','varchar(50)'))
			SET @descripcion = (select @XML.value('(/InventarioPiezas/Descripcion)[1]','varchar(50)'))
			SET @cantidad = (select @XML.value('(/InventarioPiezas/Cantidad)[1]','INT'))

			INSERT INTO usuario(nombre,descripcion,cantidad)
			values(@nombre,@descripcion,@cantidad);

			SET @respuesta = 'Ok';
			SET @leyenda = 'Se guardo '+@nombre+' correctamente';
		END*/
		IF (@Transaccion = 'USUARIO_LOGIN')
        BEGIN
			SET @nombre = (select @XML.value('(/Usuario/Nombre)[1]','varchar(50)'))
			SET @password = (select @XML.value('(/Usuario/Password)[1]','varchar(50)'))

            IF EXISTS (SELECT 1 FROM usuario WHERE nombre = @nombre AND password = @password)
            BEGIN
                SET @respuesta = 'Ok';
                SET @leyenda = 'Inicio de sesión exitoso';
            END
            ELSE
            BEGIN
                SET @respuesta = 'Error';
                SET @leyenda = 'Credenciales inválidas';
            END
        END

	END TRY
	BEGIN CATCH
	END CATCH
	SELECT @respuesta as respuesta, @leyenda as leyenda
END
GO