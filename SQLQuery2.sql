CREATE DATABASE refaccionaria2
USE refaccionaria2
CREATE SCHEMA ADMINREFACCIONARIA
CREATE TABLE ADMINREFACCIONARIA.Categoria(
	idCategoria BIGINT IDENTITY(1,1) NOT NULL,
	nombre VARCHAR(50) NOT NULL,

	CONSTRAINT PK_Categoria PRIMARY KEY(idCategoria)
)
CREATE TABLE ADMINREFACCIONARIA.Vehiculo(
	idVehiculo BIGINT IDENTITY(1,1) NOT NULL,
	numSerie VARCHAR(50) NOT NULL,
	nombreMarca VARCHAR(50) NOT NULL,
	nombreModelo VARCHAR(50) NOT NULL,
	ano int NOT NULL,
	descTecnica VARCHAR(100) NOT NULL,
	
	CONSTRAINT PK_VEHICULO PRIMARY KEY(idVehiculo)
)
CREATE TABLE ADMINREFACCIONARIA.refaccion(
	idPieza BIGINT IDENTITY(1,1) NOT NULL,
	idVehiculo BIGINT NOT NULL,
	idCategoria BIGINT NOT NULL,
	nombre VARCHAR(50) NOT NULL,
	descripcion VARCHAR(150) NOT NULL,
	precioCompra FLOAT NOT NULL,
	precioVenta FLOAT NOT NULL,
	stock INT NOT NULL,

	CONSTRAINT PK_REFACCION PRIMARY KEY(idPieza),
	CONSTRAINT FK_VEHICULO FOREIGN KEY(idVehiculo) REFERENCES ADMINREFACCIONARIA.Vehiculo(idVehiculo),
	CONSTRAINT FK_CATEGORIA FOREIGN KEY(idCategoria) REFERENCES ADMINREFACCIONARIA.Categoria(idCategoria)
)
CREATE TABLE ADMINREFACCIONARIA.persona(
	idPersona BIGINT NOT NULL IDENTITY(1,1),
	rfc VARCHAR(25) NOT NULL,
	nombre VARCHAR(50) NOT NULL,
	correo VARCHAR(50) NOT NULL, 
	telefono VARCHAR(20) NOT NULL,
	direccion VARCHAR(50) NOT NULL,
	cp VARCHAR(10) NOT NULL, 
	estado VARCHAR(50) NOT NULL,

	CONSTRAINT PK_Persona PRIMARY KEY(idPersona)
)
CREATE TABLE ADMINREFACCIONARIA.proveedor(
	idProveedor BIGINT NOT NULL IDENTITY(1,1),
	idPersona BIGINT NOT NULL,
	empresa VARCHAR(50) NOT NULL,
	giro VARCHAR(50) NOT NULL,
	gerente VARCHAR(50) NOT NULL,

	CONSTRAINT PK_Proveedor PRIMARY KEY(idProveedor),
	CONSTRAINT FK_Persona FOREIGN KEY(idPersona) REFERENCES ADMINREFACCIONARIA.persona(idPersona)
)
CREATE TABLE ADMINREFACCIONARIA.empleado(
	idEmpleado BIGINT NOT NULL IDENTITY(1,1),
	idPersona BIGINT NOT NULL,
	fechaInicio DATE NOT NULL,
	Antiguedad INT NULL,
	rol VARCHAR(50) NOT NULL,

	CONSTRAINT PK_EMPLEADO PRIMARY KEY(idEmpleado),
	CONSTRAINT FK_PERSONA2 FOREIGN KEY(idPersona) REFERENCES ADMINREFACCIONARIA.persona(idPersona)
)
CREATE TABLE ADMINREFACCIONARIA.Compra(
	idCompra BIGINT NOT NULL IDENTITY(1,1),
	idProveedor BIGINT NOT NULL,
	fechaCompra DATE NOT NULL,
	total FLOAT NULL,

	CONSTRAINT PK_COMPRA PRIMARY KEY(idCompra),
	CONSTRAINT FK_PROVEEDOR FOREIGN KEY(idProveedor) REFERENCES ADMINREFACCIONARIA.proveedor(idProveedor)
)
CREATE TABLE ADMINREFACCIONARIA.Venta(
	idVenta BIGINT NOT NULL IDENTITY(1,1),
	idEmpleado BIGINT NOT NULL,
	estadoDeVenta VARCHAR(50) NOT NULL,
	total FLOAT NULL,

	CONSTRAINT PK_VENTA PRIMARY KEY(idVenta),
	CONSTRAINT FK_EMPLEADO FOREIGN KEY(idEmpleado) REFERENCES ADMINREFACCIONARIA.empleado(idEmpleado)
)
CREATE TABLE ADMINREFACCIONARIA.detalleCompra(
	idDetalleCompra BIGINT NOT NULL IDENTITY(1,1),
	idPieza BIGINT NOT NULL,
	idCompra BIGINT NOT NULL,
	cantidad INT NOT NULL,
	subtotal FLOAT NULL,

	CONSTRAINT PK_DCOMPRA PRIMARY KEY(idDetalleCompra),
	CONSTRAINT FK_PIEZA FOREIGN KEY(idPieza) REFERENCES ADMINREFACCIONARIA.refaccion(idPieza),
	CONSTRAINT FK_COMPRA FOREIGN KEY (idCompra) REFERENCES ADMINREFACCIONARIA.Compra(idCompra)
)
CREATE TABLE ADMINREFACCIONARIA.detalleVenta(
	idDetalleVenta BIGINT NOT NULL IDENTITY(1,1),
	idPieza BIGINT NOT NULL,
	idVenta BIGINT NOT NULL,
	cantidad INT NOT NULL,
	descuento FLOAT NULL,
	subtotal FLOAT NULL,

	CONSTRAINT PK_DVENTA PRIMARY KEY(idDetalleVenta),
	CONSTRAINT FK_PIEZA2 FOREIGN KEY(idPieza) REFERENCES ADMINREFACCIONARIA.refaccion(idPieza),
	CONSTRAINT FK_VENTA FOREIGN KEY (idVenta) REFERENCES ADMINREFACCIONARIA.Venta(idVenta)
	
)

CREATE TRIGGER ADMINREFACCIONARIA.sumaStock
ON ADMINREFACCIONARIA.detalleCompra
FOR INSERT AS
	DECLARE @idCompra as BIGINT
	DECLARE @cantidad as INT
	DECLARE @idPieza as BIGINT
BEGIN
	IF EXISTS(SELECT * FROM inserted)
	BEGIN
		SELECT @cantidad = cantidad FROM inserted	
		SELECT @idPieza = idPieza FROM inserted
		UPDATE ADMINREFACCIONARIA.refaccion SET stock =
		(SELECT stock FROM ADMINREFACCIONARIA.refaccion WHERE idPieza = @idPieza) + @cantidad
		WHERE idPieza = @idPieza
	END
END;
CREATE TRIGGER ADMINREFACCIONARIA.calculaPrecioVenta
ON ADMINREFACCIONARIA.refaccion
FOR INSERT AS 
	DECLARE @idPiezaT as BIGINT 
	DECLARE @pCompra as FLOAT
BEGIN
	IF EXISTS(SELECT * FROM inserted)
	BEGIN
		SELECT @idPiezaT = idPieza FROM inserted
		SELECT @pCompra = precioCompra FROM inserted
		UPDATE ADMINREFACCIONARIA.refaccion SET precioVenta =
		@pCompra + ((@pCompra) * 0.20) WHERE idPieza = @idPiezaT
	END
END;
CREATE TRIGGER ADMINREFACCIONARIA.restaStock
ON ADMINREFACCIONARIA.detalleVenta
INSTEAD OF  INSERT AS
	DECLARE @idVenta as BIGINT
	DECLARE @cantidad as INT
	DECLARE @idPieza as BIGINT
	DECLARE @stock as BIGINT
	DECLARE @descuento as FLOAT
BEGIN
	IF EXISTS(SELECT * FROM inserted)
	BEGIN
		SELECT @cantidad = cantidad FROM inserted	
		SELECT @idPieza = idPieza FROM inserted
		SELECT @idVenta = idVenta FROM inserted
		SELECT @descuento = descuento FROM inserted
		SELECT @stock = stock FROM ADMINREFACCIONARIA.refaccion WHERE idPieza = @idPieza
		IF ( @stock  <= 0 OR @cantidad > @stock)
			BEGIN
				
				RAISERROR('NO HAY SUFICIENTE STOCK',16,1)
			END
		ELSE
			BEGIN
				
				INSERT INTO ADMINREFACCIONARIA.detalleVenta(idVenta,idPieza,cantidad,descuento) VALUES(@idVenta,@idPieza,@cantidad,@descuento)
				UPDATE ADMINREFACCIONARIA.refaccion SET stock =
				(SELECT stock FROM ADMINREFACCIONARIA.refaccion WHERE idPieza = @idPieza) - @cantidad
				WHERE idPieza = @idPieza
			END
	END
END;
CREATE TRIGGER ADMINREFACCIONARIA.calculaSubtotalVenta
ON ADMINREFACCIONARIA.detalleVenta
FOR INSERT,UPDATE AS
	DECLARE @idDetVent as BIGINT
	DECLARE @idPieza AS BIGINT
	DECLARE @descuento AS FLOAT
	DECLARE @precioVenta AS FLOAT
	DECLARE @cantidad AS INT
	DECLARE @subtotal AS FLOAT

BEGIN
	IF EXISTS(SELECT * FROM inserted)
	BEGIN
		SELECT @idDetVent = idVenta FROM inserted
		SELECT @idPieza = idPieza FROM inserted
		SELECT @cantidad = cantidad  FROM inserted
		SELECT @descuento = descuento FROM inserted
		SELECT @precioVenta = precioVenta FROM ADMINREFACCIONARIA.refaccion WHERE idPieza = @idPieza

		UPDATE ADMINREFACCIONARIA.detalleVenta SET subtotal = ((SELECT precioVenta FROM ADMINREFACCIONARIA.refaccion WHERE idPieza = @idPieza) * @cantidad) - @descuento
		WHERE idVenta = @idDetVent
		SELECT @subtotal = subtotal FROM inserted
		IF(@subtotal > 0)
			BEGIN
				UPDATE ADMINREFACCIONARIA.Venta SET total = (SELECT subtotal FROM ADMINREFACCIONARIA.detalleVenta)
				WHERE idVenta = @idDetVent
			END
		ELSE
			BEGIN
				UPDATE ADMINREFACCIONARIA.Venta SET total = (SELECT total FROM ADMINREFACCIONARIA.Venta WHERE idVenta = @idDetVent ) + (SELECT subtotal FROM ADMINREFACCIONARIA.detalleVenta WHERE idVenta = @idDetVent)
				WHERE idVenta = @idDetVent
			END
	END
END;
CREATE TRIGGER ADMINREFACCIONARIA.restaSubtotalVenta
ON ADMINREFACCIONARIA.detalleVenta
INSTEAD OF DELETE AS
	DECLARE @idDetVent as BIGINT
	DECLARE @idPieza AS BIGINT
	DECLARE @descuento AS FLOAT
	DECLARE @precioVenta AS FLOAT
	DECLARE @cantidad AS INT

BEGIN
	IF EXISTS(SELECT * FROM deleted)
	BEGIN
		SELECT @idDetVent = idVenta FROM deleted
		SELECT @idPieza = idPieza FROM deleted
		SELECT @precioVenta = precioVenta FROM ADMINREFACCIONARIA.refaccion WHERE idPieza = @idPieza
		UPDATE ADMINREFACCIONARIA.Venta SET total = (SELECT total FROM ADMINREFACCIONARIA.Venta WHERE idVenta = @idDetVent ) - (SELECT subtotal FROM ADMINREFACCIONARIA.detalleVenta WHERE idVenta = @idDetVent)
		WHERE idVenta = @idDetVent
		DELETE ADMINREFACCIONARIA.detalleVenta WHERE idVenta = @idDetVent AND idPieza = @idPieza
	END
END;

CREATE TRIGGER ADMINREFACCIONARIA.calculaSubtotalCompra
ON ADMINREFACCIONARIA.detalleCompra
FOR INSERT,UPDATE AS
	DECLARE @idDetComp as BIGINT
	DECLARE @idPieza AS BIGINT
	DECLARE @precioCompra AS FLOAT
	DECLARE @cantidad AS INT
	DECLARE @subtotal AS FLOAT

BEGIN
	IF EXISTS(SELECT * FROM inserted)
	BEGIN
		SELECT @idDetComp = idCompra FROM inserted
		SELECT @idPieza = idPieza FROM inserted
		SELECT @cantidad = cantidad  FROM inserted
		
		SELECT @precioCompra = precioCompra FROM ADMINREFACCIONARIA.refaccion WHERE idPieza = @idPieza
		UPDATE ADMINREFACCIONARIA.detalleCompra SET subtotal = (SELECT precioCompra FROM ADMINREFACCIONARIA.refaccion WHERE idPieza = @idPieza) * @cantidad
		WHERE idCompra = @idDetComp
		SELECT @subtotal = subtotal FROM inserted
		IF(@subtotal > 0)
			BEGIN
				UPDATE ADMINREFACCIONARIA.Compra SET total = (SELECT subtotal FROM ADMINREFACCIONARIA.detalleCompra)
				WHERE idCompra = @idDetComp
			END
		ELSE
				UPDATE ADMINREFACCIONARIA.Compra SET total = (SELECT total FROM ADMINREFACCIONARIA.Compra WHERE idCompra = @idDetComp ) + (SELECT subtotal FROM ADMINREFACCIONARIA.detalleCompra WHERE idCompra = @idDetComp)
				WHERE idCompra = @idDetComp			
	END
END;