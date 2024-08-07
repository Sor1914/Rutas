Create Database Rutas;
Use Rutas;

Create Table Roles(	
	IdRol int not null IDENTITY(1,1) PRIMARY KEY,	
	Descripcion Varchar(50)
);

Create Table Usuarios(
	IdUsuario int IDENTITY(1,1) PRIMARY KEY,
	Usuario Varchar(10),
	Contrasenia VARBINARY(MAX),
	Email varchar(40),
	Nombres varchar(50),
	Apellidos varchar(50),
	IdRol int,
	foreign key(idRol) references Roles(IdRol)
);

Create Table Horarios
(
	IdHorario int IDENTITY(1,1) PRIMARY KEY,
	HoraEntrada Time,
	HoraAlmuerzo Time,
	HoraSalida Time,	
)

Create Table Conductores(
	IdConductor int IDENTITY(1,1) PRIMARY KEY,
	Licencia varchar(16),	
	TipoLicencia varchar(1),
	idHorario int,
	LimiteParadas int,	
	IdUsuario int,
	foreign key(IdUsuario) references Usuarios(IdUsuario),
	foreign key (idHorario) references Horarios(IdHorario)
);

Create Table TipoVehiculos(
	IdTipoVehiculo int IDENTITY(1,1) PRIMARY KEY,
	Cantidad int, 
	CapacidadPeso decimal(4,2),
	KMXGalon decimal(4,2),
	Galones decimal(4,2),
	TipoGas varchar(3),
	Descripcion varchar(20)
);

Create Table Vehiculos
(
	IdVehiculo int IDENTITY(1,1) PRIMARY KEY,
	Placa varchar(7),
	KmRecorridos decimal(4,2),	
	idConductor int,
	idTipoVehiculo int,
	foreign key(idConductor) references Conductores(IdConductor),
	foreign key(idTipoVehiculo) references TipoVehiculos(IdTipoVehiculo)
)

Create Table Proveedor
(
	IdProveedor int Identity(1,1) Primary Key,
	NombreProveedor Varchar(50),
	DireccionProveedor Varchar(50),
	Longitud Varchar(50),
	Latitud Varchar(50)
)

Create Table Productos
(
	IdProducto int IDENTITY(1,1) PRIMARY KEY,	
	NombreProducto varchar (50),
	TipoProducto varchar(10),
	PesoProducto decimal(2,2),				
)

Create Table ProveedorProducto
(
	IdProveedor int Foreign Key References Proveedor(IdProveedor),
	IdProducto int Foreign Key References Productos(IdProducto),
	Primary Key (IdProveedor, IdProducto),
	PrecioProducto decimal(2,2),
	Existencia int
)

Create Table Rutas
(
	IdRuta int IDENTITY(1,1) PRIMARY KEY,	
	FechaProgramada DateTime,
	LatitudInicial Varchar(50),
	LatitudFinal Varchar(50),
	LongitudInicial Varchar(50),
	LongitudFinal Varchar(50)	
)


Create Table DetalleRuta
(
	IdDetalle int Identity(1,1) Primary Key,
	FechaCreoEnvio DateTime,
	Latitud Varchar(50),
	Longitud Varchar(50),
	Cantidad decimal (2,2),	
	PrioridadVisita int,
	IdVehiculo int foreign key references Vehiculos(IdVehiculo),
	IdProducto int foreign key references Productos(IdProducto),
	IdRuta int foreign key references Rutas(IdRuta),
	IdUsuario int foreign key references Usuarios(IdUsuario)
)

INSERT INTO Roles(Descripcion)
VALUES
('Administrador'),
('Conductor'),
('Usuario');


INSERT INTO Usuarios(Usuario, Contrasenia, Email, Nombres, Apellidos, IdRol)
VALUES ('JSOR',ENCRYPTBYPASSPHRASE('JS0R', 'Sor1906197912' ),'jonathansor2000sm@gmail.com','Jonathan Elias','Sor Monroy',1);