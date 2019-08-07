--use master;
--go

--drop database BDPuntoVenta;
--GO

--CREATE DATABASE BDPuntoVenta;
--GO

--USE BDPuntoVenta;
--GO

CREATE SCHEMA Productos;
GO

CREATE SCHEMA RecursosHumanos;
GO

CREATE SCHEMA Ventas;
GO

CREATE SCHEMA Reportes;
GO

CREATE TABLE Productos.Categoria(
Id_Categoria INT IDENTITY(1,1) PRIMARY KEY, --Valor inicial 1, incremento 1
Nombre_Categoria VARCHAR(30) NOT NULL,
Codigo AS ('CAT-'+CONVERT(VARCHAR, Id_Categoria)))
GO


GO
CREATE TABLE Productos.Catalogo(
Id_Producto INT IDENTITY(1,1) PRIMARY KEY,
Nombre_Producto VARCHAR(50) NOT NULL,
Id_Categoria INT NOT NULL,
Precio_Costo MONEY NOT NULL,
Ganancia  DECIMAL(6,2),
Precio_Venta MONEY NOT NULL,
Unidad VARCHAR(15) DEFAULT 'Pieza',
Codigo AS ('PRO-'+CONVERT(VARCHAR, Id_Producto)),
CONSTRAINT FKCategoria FOREIGN KEY (Id_Categoria) references [Productos].[Categoria]([Id_Categoria]))
GO

GO
--Registros Inventarios 
CREATE TABLE Productos.Inventario
(Id_Producto INT PRIMARY KEY,
Cantidad DECIMAL(8,3) DEFAULT 0,
CONSTRAINT FkInventario FOREIGN KEY (Id_Producto) references Productos.Catalogo(Id_Producto))
GO
--ALTER TABLE [Productos].[Inventario] ALTER COLUMN Cantidad DECIMAL(8,3)


CREATE TABLE RecursosHumanos.Empleados(
Id_Empleado INT primary key IDENTITY(1,1),
Usuario VARCHAR(30) NOT NULL,
Contraseña VARCHAR(80) NOT NULL,
Nombre_Completo VARCHAR(30) NOT NULL)
GO

CREATE TABLE Ventas.Clientes (
Id_Cliente INT IDENTITY(1,1) primary key NOT NULL,
Nombre_Cliente VARCHAR(50) NOT NULL,
Direccion VARCHAR (50) NULL,
Telefono VARCHAR (30) NULL,
Credito MONEY NOT NULL)
GO

INSERT INTO Ventas.Clientes (Nombre_Cliente, Credito) VALUES ('Publico en General', 0)

/*
CREATE TABLE Ventas.Clientes (
Id_Cliente INT IDENTITY(1,1) NOT NULL,
Nombre_Cliente VARCHAR(50) NOT NULL,
Direccion VARCHAR (50) NULL,
Ciudad VARCHAR (50) NULL,
Telefono VARCHAR (30) NULL,
Credito MONEY NOT NULL,
Codigo AS ('CLI-'+CONVERT(VARCHAR, Id_Cliente)))
GO


--registro cuentas a credito


--Venta


CREATE TABLE Ventas.Venta(
Id_Venta INT primary key identity(1,1),
Id_Cliente INT NULL, --Campo nulo publico en general 
Id_Empleado INT NULL,
Fecha_Venta DATETIME NOT NULL,
TOTAL DOUBLE NOT NULL
)

CREATE TABLE Ventas.DetallesVenta(
Id_Venta INT NOT NULL, --cambiar id folio...
Id_Producto INT NOT NULL, --crear llave foranea
Cantidad INT NOT NULL)
GO

--Tabla Virtual Ventas perfeccionar ...
CREATE VIEW Ventas.V_Venta
WITH SCHEMABINDING AS
SELECT Ventas.Pedidos.Id_Pedido AS ID, Ventas.Clientes.Nombre_Cliente,
RecursosHumanos.Empleados.Apellido_Empleado + ',' + RecursosHumanos.Empleados.Nombre_Empleado AS Cajero, 
Ventas.Venta.Fecha_Venta AS Fecha
FROM Ventas.Venta INNER JOIN Ventas.Clientes ON Ventas.Ventas.Id_Cliente=Ventas.Clientes.Id_Cliente
INNER JOIN RecursosHumanos.Empleados ON Ventas.Venta.Id_Empleado=RecursosHumanos.Empleados.Id_Empleado;
GO*/

--Tabla virual Productos
go
CREATE VIEW Productos.V_Productos
WITH SCHEMABINDING 
AS
SELECT Productos.Catalogo.Id_Producto AS ID, Productos.Catalogo.Nombre_Producto As Nombre, Productos.Catalogo.Unidad AS Unidad,
Productos.Categoria.Nombre_Categoria AS Categoria,
Productos.Catalogo.Precio_Costo AS [Precio de Costo], Productos.Catalogo.Ganancia AS Ganancia, Productos.Catalogo.Precio_Venta AS [Precio de Venta],
Productos.Inventario.Cantidad AS  Cantidad
FROM Productos.Categoria JOIN Productos.Catalogo
ON Productos.Catalogo.Id_Categoria=Productos.Categoria.Id_Categoria
JOIN Productos.Inventario ON Productos.Catalogo.Id_Producto=Productos.Inventario.Id_Producto 
GO


CREATE VIEW  Productos.V_Inventario 
WITH SCHEMABINDING 
AS
SELECT Productos.Catalogo.Nombre_Producto AS Producto, Productos.Inventario.Cantidad AS Cantidad 
FROM Productos.Catalogo INNER JOIN Productos.Inventario ON Productos.Catalogo.Id_Producto=Productos.Inventario.Id_Producto
GO

/*--Crear  tabla virutal para el registro de ventas
CREATE TABLE Reportes.Fechas(
Fecha INT NOT NULL, 
Año CHAR(4) NULL,
NombreMes VARCHAR(12) NULL,
NumeroMes TINYINT NULL);
GO

--Validar campos para cadenas vacias
ALTER TABLE Productos.Categorias ADD CONSTRAINT Cont_Categoria_NombreVacio
CHECK(Nombre_Categoria<>'');
GO

ALTER TABLE Productos.Producto ADD CONSTRAINT Cont_Producto_NombreVacio
CHECK(Nombre_Producto<>'');
GO

ALTER TABLE RecusosHumanos.Empleados ADD CONSTRAINT Cont_Empleado_NombreVacio
CHECK(Nombre_Emplado<>'');
GO

--Checar costos positivos
ALTER TABLE Productos.Producto ADD CONSTRAINT Cont_ProductoPrecioCostoPositivo
CHECK(Precio_Costo>=0);
GO

ALTER TABLE Productos.Producto ADD CONSTRAINT Cont_ProductoPrecioVentaPositivo
CHECK(Precio_Venta>=0);
GO

*/


--Constraint campo Nombre_Categoria Unico
ALTER TABLE Productos.Categoria ADD CONSTRAINT ConsNombreCategoriaUnico
UNIQUE (Nombre_Categoria);
GO

--Constraint campo Nombre_Producto Unico
ALTER TABLE Productos.Catalogo ADD CONSTRAINT ConsNombreProductoUnico
UNIQUE (Nombre_PRoducto);
GO

--Constraint campo RecursosHumanos.Empleados, Usuario Unico
ALTER TABLE RecursosHumanos.Empleados ADD CONSTRAINT ConsUsuarioNombreUnico
UNIQUE([Usuario], [Nombre_Completo]);
GO


--PROCEDIMIENTOS ALMACENADOS 
 GO
--Procedimiento almacenado que muestra los productos registrados.
CREATE PROCEDURE Productos.MostrarProductos
@RegistrosPorPagina INT, @NumeroPagina INT
AS
BEGIN
SET NOCOUNT ON
SELECT [ID], [Nombre], [Unidad], [Categoria], [Precio de Costo], [Ganancia], [Precio de Venta], [Cantidad]
FROM [Productos].[V_Productos] ORDER BY [Nombre], [Categoria]
OFFSET(@NumeroPagina - 1)*(@RegistrosPorPagina) ROWS
FETCH NEXT @RegistrosPorPagina ROWS ONLY END;
GO
GO
--Procedimiento almacenadao para obtener el numero de paginas del total de registros
CREATE PROCEDURE Productos.ProductosPaginas
@RegistrosPorPagina INT,
@TotalPaginas INT OUTPUT
AS 
DECLARE @CantidadFilas INT
BEGIN
SET NOCOUNT ON
SET @CantidadFilas = (SELECT COUNT([ID]) FROM Productos.V_Productos)
SET @TotalPaginas = @CantidadFilas/@RegistrosPorPagina
IF((@CantidadFilas % @RegistrosPorPagina) > 0)
BEGIN 
	SET @TotalPaginas = @TotalPaginas + 1
	RETURN;
END 
IF @TotalPaginas = 0
BEGIN 
	SET @TotalPaginas = 1
END
END;
GO

go
--Declarar procedimiento para buscar productos
CREATE PROCEDURE Productos.CatalogoBuscar
@NombreBuscado AS VARCHAR(50)
AS
BEGIN
SET NOCOUNT ON;
SELECT  [ID],[Nombre],[Categoria],[Unidad],[Precio de Costo],[Ganancia],[Precio de Venta],[Cantidad]
FROM [Productos].[V_Productos]
WHERE [Nombre] LIKE '%' + @NombreBuscado + '%'
ORDER BY [ID]
END;
GO

go
--Declarar procedimiento para agregar productos
CREATE PROCEDURE Productos.CatalogoInsertar
@NombreProducto VARCHAR(55),
@NombreCategoria VARCHAR(55),
@Unidad VARCHAR(25),
@PrecioCosto MONEY,
@Ganancia DECIMAL(6,2),
@PrecioVenta MONEY,
@Cantidad DECIMAL(8,3)
AS
BEGIN 
SET NOCOUNT ON
DECLARE @Id_Categoria INT
SET @Id_Categoria = (SELECT Id_Categoria FROM Productos.Categoria
                     WHERE Nombre_Categoria = NULLIF(RTRIM(LTRIM(@NombreCategoria)),''));
IF(@Id_Categoria IS NOT NULL)
BEGIN 
INSERT INTO Productos.Catalogo(Nombre_Producto, Id_Categoria, Unidad, Precio_Costo, Ganancia, Precio_Venta)
VALUES(
NULLIF(RTRIM(LTRIM(@NombreProducto)),''),
@Id_Categoria,
NULLIF(RTRIM(LTRIM(@Unidad)),''),
NULLIF(RTRIM(LTRIM(@PrecioCosto)),''),
NULLIF(RTRIM(LTRIM(@Ganancia)),''), 
NULLIF(RTRIM(LTRIM(@PrecioVenta)),''));
END
ELSE 
BEGIN
RAISERROR('Seleccione Categoria', 16, 1);
END
BEGIN
SET NOCOUNT ON
DECLARE @IdProducto INT
SET @IdProducto=(SELECT Id_Producto FROM Productos.Catalogo WHERE Productos.Catalogo.Nombre_Producto=@NombreProducto)
IF(@IdProducto IS NOT NULL)
INSERT INTO Productos.Inventario(Id_Producto, Cantidad) 
VALUES (@IdProducto, NULLIF(RTRIM(LTRIM(@Cantidad)),''));
END
END;


--EXECUTE Productos.CatalogoInsertar 'Cheetos','Papitas','Pieza',8.00,25.00,10.00,10

go
--Procedimiento almacenado para editar productos
CREATE PROCEDURE Productos.CatalogoEditar
@Id_Producto INT,
@NombreProducto VARCHAR(55),
@NombreCategoria VARCHAR(55),
@Unidad VARCHAR(25),
@PrecioCosto MONEY,
@Ganancia DECIMAL(4,2),
@PrecioVenta MONEY,
@Cantidad DECIMAL(8,3)
AS
BEGIN 
SET NOCOUNT ON
DECLARE @Id_Categoria INT
SET @Id_Categoria = (SELECT Id_Categoria FROM Productos.Categoria
                     WHERE Nombre_Categoria = NULLIF(RTRIM(LTRIM(@NombreCategoria)),''));
IF(@Id_Categoria IS NOT NULL)
BEGIN 
UPDATE Productos.Catalogo 
SET Nombre_Producto = NULLIF(RTRIM(LTRIM(@NombreProducto)),''),
Id_Categoria = @Id_Categoria,
Unidad = NULLIF(RTRIM(LTRIM(@Unidad)),''),
Precio_Costo = NULLIF(RTRIM(LTRIM(@PrecioCosto)),''),
Ganancia= NULLIF(RTRIM(LTRIM(@Ganancia)),''), 
Precio_Venta=NULLIF(RTRIM(LTRIM(@PrecioVenta)),'')
WHERE Id_Producto=@Id_Producto
END
ELSE 
BEGIN
RAISERROR('Actualizacion no exitosa', 16, 1);
END
BEGIN
UPDATE Productos.Inventario
SET Id_Producto=@Id_Producto,
Cantidad= NULLIF(RTRIM(LTRIM(@Cantidad)),'')
WHERE Id_Producto=@Id_Producto
END
END;
GO

go
--Crear procedimiento Eliminar Productos
CREATE PROCEDURE Productos.CatalogoEliminar
@Id_Producto INT
AS 
BEGIN 
SET NOCOUNT ON
DELETE FROM Productos.Inventario WHERE Id_Producto=@Id_Producto
END
BEGIN 
SET NOCOUNT ON
DELETE FROM Productos.Catalogo WHERE Id_Producto=@Id_Producto
END;

go
--Procedimiento almacenado para listar categoria
CREATE PROCEDURE Productos.CategoriaListar
AS
	SELECT [Nombre_Categoria] AS Categorias FROM [Productos].[Categoria]
RETURN 
GO
go
--Procedimiento almacenado listar categorias en combo box
CREATE PROCEDURE Productos.CategoriaListarCB
AS
	SELECT [Id_Categoria] AS ID, [Nombre_Categoria] AS Categorias FROM [Productos].[Categoria]
RETURN 
GO
GO
--Procedimiento almacenado buscsar catalogo
CREATE PROCEDURE Productos.CategoriaBuscar
@NombreBuscado AS VARCHAR(50)
AS
BEGIN
SET NOCOUNT ON;
SELECT  [Nombre_Categoria] AS Categorias
FROM [Productos].[Categoria]
WHERE [Nombre_Categoria] LIKE '%' + @NombreBuscado + '%'
ORDER BY [Id_Categoria]
END;

GO
--Procedimiento almacenado para Insertar Categoria
CREATE PROCEDURE Productos.CategoriaInsertar
@NombreCategoria VARCHAR(55)
AS
INSERT INTO Productos.Categoria(Nombre_Categoria)
VALUES(NULLIF(RTRIM(LTRIM(@NombreCategoria)),''))
GO

--Procedimiento almacenado para Elimiar Categoria
CREATE PROCEDURE Productos.CategoriaEliminar
@NombreCategoria VARCHAR(55)
AS 
BEGIN 
SET NOCOUNT ON
DECLARE @Id_Categoria INT
SET @Id_Categoria = (SELECT Id_Categoria FROM Productos.Categoria
                     WHERE Nombre_Categoria = NULLIF(RTRIM(LTRIM(@NombreCategoria)),''));
END
BEGIN 
SET NOCOUNT ON
DELETE FROM Productos.Categoria WHERE Id_Categoria=@Id_Categoria
END;
GO
GO
--Procedimiento actualizar o editar categoria
CREATE PROCEDURE Productos.CategoriaEditar
@NombreCategoria VARCHAR(55),
@NuevoNombreCategoria VARCHAR(55)
AS 
BEGIN 
SET NOCOUNT ON
DECLARE @Id_Categoria INT
SET @Id_Categoria = (SELECT Id_Categoria FROM Productos.Categoria
                     WHERE Nombre_Categoria = NULLIF(RTRIM(LTRIM(@NombreCategoria)),''));
END
BEGIN
UPDATE Productos.Categoria
SET Nombre_Categoria = NULLIF(RTRIM(LTRIM(@NuevoNombreCategoria)),'')
WHERE Id_Categoria=@Id_Categoria
END;
GO
GO
--Procedimiento Inventario
CREATE PROCEDURE Productos.InventarioListar
AS
	SELECT [Producto], [Cantidad] FROM [Productos].[V_Inventario]
RETURN 
GO

GO
--Procedimiento agregar productos
CREATE PROCEDURE Productos.InventarioModificar
@NombreProducto VARCHAR(55),
@Cantidad DECIMAL(8,3)
AS 
BEGIN 
SET NOCOUNT ON
DECLARE @Id_Producto INT
SET @Id_Producto= (SELECT Id_Producto FROM Productos.Catalogo
                     WHERE Nombre_Producto = NULLIF(RTRIM(LTRIM(@NombreProducto)),''));
END
BEGIN
UPDATE Productos.Inventario
SET Cantidad = NULLIF(RTRIM(LTRIM(@Cantidad)),'')
WHERE Id_Producto=@Id_Producto
END;
GO

GO
--Procedimiento almacenado buscsar catalogo
CREATE PROCEDURE Productos.InventarioBuscar
@NombreBuscado AS VARCHAR(50)
AS
BEGIN
SET NOCOUNT ON;
SELECT  [Producto], [Cantidad]
FROM [Productos].[V_Inventario]
WHERE [Producto] LIKE '%' + @NombreBuscado + '%'
ORDER BY [Producto]
END;
GO

GO
--Procedimiento almacenado listar usuario 
CREATE PROCEDURE RecursosHumanos.ListarUsuario
AS
	SELECT [Id_Empleado] AS ID, [Usuario], [Contraseña] FROM [RecursosHumanos].[Empleados]
RETURN 
GO


GO
CREATE TABLE Ventas.Venta(
Id INT primary key identity(1,1),
Total DECIMAL(8,2) NOT NULL,
Fecha_Venta DATETIME NOT NULL,
Id_Cliente INT NULL FOREIGN KEY REFERENCES Ventas.Clientes(Id_Cliente) , --Campo nulo publico en general 
Id_Empleado INT NULL FOREIGN KEY REFERENCES RecursosHumanos.Empleados(Id_Empleado),
Iva DECIMAL(8,2) NOT NULL,
)
GO

CREATE TABLE Ventas.Venta_Producto(
Id INT primary key identity(1,1),
VentaId INT NOT NULL FOREIGN KEY REFERENCES Ventas.Venta(Id),
ProductoId INT NOT NULL FOREIGN KEY REFERENCES Productos.Catalogo(Id_Producto),
Cantidad INT NOT NULL,
TotalVendido DECIMAL(8,2) NOT NULL
)
GO


GO
CREATE PROCEDURE Ventas.Insertar_Venta_Producto
@ProductoId as INTEGER,
@VentaId as INTEGER,
@TotalVendido as DECIMAL(8,2),
@Cantidad as INTEGER,
@Id int OUTPUT
AS
BEGIN
INSERT INTO Ventas.Venta_Producto (VentaId, ProductoId, Cantidad, TotalVendido) values (@VentaId, @ProductoId,@Cantidad, @TotalVendido)
SELECT @Id = SCOPE_IDENTITY()
END
GO
--Procedimiento almacenado listar Clientes
GO
CREATE PROCEDURE Ventas.ListarClientes
AS
	SELECT [Nombre_Cliente] AS Cliente, [Direccion], [Telefono], [Credito] AS 'Limite Crédito' FROM [Ventas].[Clientes]
RETURN 
GO

CREATE TABLE Ventas.Venta_Producto(
Id INT primary key identity(1,1),
VentaId INT NOT NULL FOREIGN KEY REFERENCES Ventas.Venta(Id),
ProductoId INT NOT NULL FOREIGN KEY REFERENCES Productos.Catalogo(Id_Producto),
Cantidad INT NOT NULL,
TotalVendido DECIMAL(8,2) NOT NULL
)
GO