--CREATE DATABASE BDPuntoVenta;
--GO

--USE BDPuntoVenta;
--GO

--Procedimiento almacenado listar Empleados

CREATE PROCEDURE RecursosHumanos.ListarUsuarios
AS
	SELECT  [Nombre_Completo] AS 'Nombre Cajero' FROM [RecursosHumanos].[Empleados]
RETURN 
GO

--Procedimiento almacenado agregar empleado

CREATE PROCEDURE RecursosHumanos.InsertarUsuarios
@Usuario VARCHAR(30),
@Contraseña VARCHAR(30),
@Nombre_Completo VARCHAR(30)
AS
INSERT INTO [RecursosHumanos].[Empleados] ([Usuario], [Nombre_Completo])
VALUES(NULLIF(RTRIM(LTRIM(@Usuario)),''),
NULLIF(RTRIM(LTRIM(@Contraseña)),''),
NULLIF(RTRIM(LTRIM(@Nombre_Completo)),''));
GO

--Procedimiento almacenado buscar empleado
CREATE PROCEDURE RecursosHumanos.BuscarUsuarios
@NombreBuscado AS VARCHAR(50)
AS
BEGIN
SET NOCOUNT ON;
SELECT  [Nombre_Completo]
FROM [RecursosHumanos].[Empleados]
WHERE [Nombre_Completo] LIKE '%' + @NombreBuscado + '%'
ORDER BY [Nombre_Completo]
END;
GO

--Procedimiento al almacenado editar empleado
CREATE PROCEDURE RecursosHumanos.EditarEmpleado
@NombreEmpleadoAntes VARCHAR(35),
@NombreEmpleado VARCHAR(35),
@Usuario VARCHAR(35),
@Contraseña VARCHAR(35)
AS 
BEGIN 
SET NOCOUNT ON
DECLARE @Id_Empleado INT
SET @Id_Empleado= (SELECT [Id_Empleado] FROM [RecursosHumanos].[Empleados]
                     WHERE [Nombre_Completo]= NULLIF(RTRIM(LTRIM(@NombreEmpleadoAntes)),''));
END
BEGIN
UPDATE [RecursosHumanos].[Empleados]
SET [Nombre_Completo] = NULLIF(RTRIM(LTRIM(@NombreEmpleado)),''),
[Usuario] = NULLIF(RTRIM(LTRIM(@Usuario)),''),
[Contraseña] = NULLIF(RTRIM(LTRIM(@Contraseña)),'')
WHERE[Id_Empleado] =@Id_Empleado
END;
GO

--Procedimiento almacenado borrar cajero
CREATE PROCEDURE RecursosHumanos.BorrarCajero
@NombreEmpleado VARCHAR(55)
AS 
BEGIN 
SET NOCOUNT ON
DECLARE @Id_Empleado INT
SET @Id_Empleado = (SELECT  [Id_Empleado] FROM [RecursosHumanos].[Empleados]
                     WHERE [Nombre_Completo]= NULLIF(RTRIM(LTRIM(@NombreEmpleado)),''));
END
BEGIN 
SET NOCOUNT ON
DELETE FROM [RecursosHumanos].[Empleados] WHERE Id_Empleado=@Id_Empleado
END;

GO