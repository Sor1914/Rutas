Drop Database Rutas;
Create Database Rutas;
Use Rutas;
Use Prueba;

Create Table Roles(	
	IdRol int not null IDENTITY(1,1) PRIMARY KEY,	
	Descripcion Varchar(50),
	Estado int,
	UsuarioCreo varchar(10),
	FechaCreo DateTime Default Current_Timestamp,
	UsuarioModifico Varchar(10),
	FechaModifico Datetime
);

Create Table Usuarios(
	Usuario Varchar(10),
	Contrasenia VARBINARY(MAX),
	Email varchar(40),
	Nombres varchar(50),
	Apellidos varchar(50),
	IdRol int,
	Estado int,
	UsuarioCreo varchar(10),
	FechaCreo DateTime Default Current_Timestamp,
	UsuarioModifico Varchar(10),
	FechaModifico Datetime,
	Primary Key(Usuario),
	foreign key(idRol) references Roles(IdRol)
);

Create Table Horarios
(
	IdHorario int IDENTITY(1,1) PRIMARY KEY,
	HoraEntrada Time,
	HoraAlmuerzo Time,
	HoraSalida Time,	
	Estado int,
	UsuarioCreo varchar(10),
	FechaCreo DateTime Default Current_Timestamp,
	UsuarioModifico Varchar(10),
	FechaModifico Datetime
)

Create Table Conductores(	
	Licencia varchar(16) Primary Key,	
	TipoLicencia varchar(1),
	idHorario int,
	LimiteParadas int,	
	Usuario Varchar(10),
	Estado int,
	UsuarioCreo varchar(10),
	FechaCreo DateTime Default Current_Timestamp,
	UsuarioModifico Varchar(10),
	FechaModifico Datetime
	foreign key(Usuario) references Usuarios(Usuario),
	foreign key (idHorario) references Horarios(IdHorario)
);

Create Table TipoVehiculos(
	IdTipoVehiculo int IDENTITY(1,1) PRIMARY KEY,
	Cantidad int, 
	CapacidadPeso decimal(4,2),
	KMXGalon decimal(4,2),
	Galones decimal(4,2),
	TipoGas varchar(3),
	Descripcion varchar(20),
	Estado int,
	UsuarioCreo varchar(10),
	FechaCreo DateTime Default Current_Timestamp,
	UsuarioModifico Varchar(10),
	FechaModifico Datetime
);

Create Table Vehiculos
(
	IdVehiculo int IDENTITY(1,1) PRIMARY KEY,
	Placa varchar(7),
	KmRecorridos decimal(4,2),	
	LicenciaConductor Varchar(16),
	idTipoVehiculo int,
	Estado int,
	UsuarioCreo varchar(10),
	FechaCreo DateTime Default Current_Timestamp,
	UsuarioModifico Varchar(10),
	FechaModifico Datetime
	foreign key(LicenciaConductor) references Conductores(LIcencia),
	foreign key(idTipoVehiculo) references TipoVehiculos(IdTipoVehiculo)
)

Create Table Proveedor
(
	IdProveedor int Identity(1,1) Primary Key,
	NombreProveedor Varchar(50),
	DireccionProveedor Varchar(50),
	Longitud Varchar(50),
	Latitud Varchar(50),
	Estado int,
	UsuarioCreo varchar(10),
	FechaCreo DateTime Default Current_Timestamp,
	UsuarioModifico Varchar(10),
	FechaModifico Datetime
)

Create Table Productos
(
	IdProducto int IDENTITY(1,1) PRIMARY KEY,	
	NombreProducto varchar (50),
	TipoProducto varchar(10),
	PesoProducto decimal(2,2),	
	Estado int,
	UsuarioCreo varchar(10),
	FechaCreo DateTime Default Current_Timestamp,
	UsuarioModifico Varchar(10),
	FechaModifico Datetime
)

Create Table ProveedorProducto
(
	IdProveedor int Foreign Key References Proveedor(IdProveedor),
	IdProducto int Foreign Key References Productos(IdProducto),
	Primary Key (IdProveedor, IdProducto),
	PrecioProducto decimal(2,2),
	Existencia int,
	Estado int,
	UsuarioCreo varchar(10),
	FechaCreo DateTime Default Current_Timestamp,
	UsuarioModifico Varchar(10),
	FechaModifico Datetime
)

Create Table Rutas
(
	IdRuta int IDENTITY(1,1) PRIMARY KEY,	
	FechaProgramada DateTime,
	LatitudInicial Varchar(50),
	LatitudFinal Varchar(50),
	LongitudInicial Varchar(50),
	LongitudFinal Varchar(50),
	Estado int,
	UsuarioCreo varchar(10),
	FechaCreo DateTime Default Current_Timestamp,
	UsuarioModifico Varchar(10),
	FechaModifico Datetime
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
	Usuario Varchar(10) foreign key references Usuarios(Usuario),
	Estado int,
	UsuarioCreo varchar(10),
	FechaCreo DateTime Default Current_Timestamp,
	UsuarioModifico Varchar(10),
	FechaModifico Datetime
);

INSERT INTO Roles(Descripcion, UsuarioCreo)
VALUES
('Administrador', 'Sys'),
('Conductor', 'Sys'),
('Usuario', 'Sys');

INSERT INTO Usuarios(Usuario, Contrasenia, Email, Nombres, Apellidos, IdRol, UsuarioCreo)
VALUES ('JSOR78',ENCRYPTBYPASSPHRASE('JS0R', 'Sor1906197912' ),'jonathansor2000sm@gmail.com','Jonathan Elias','Sor Monroy',1, 'Sys');

--Querys para Desarrollo

INSERT INTO Usuarios(Usuario, Contrasenia, Email, Nombres, Apellidos, IdRol, UsuarioCreo)
VALUES ('{0}',ENCRYPTBYPASSPHRASE('JS0R', '{1}' ),'{2}','{3}','{4}',3, 'Sys');



Select * from usuarios

Select Usuario from Usuarios where Upper(Usuario)= Upper('jsor');

SELECT Usuario, Email, Nombres, Apellidos, IdRol FROM Usuarios WHERE UPPER(Usuario)= UPPER('jsor1') AND CAST(DECRYPTBYPASSPHRASE('JS0R',Contrasenia) AS VARCHAR(MAX)) = 'Sor1906197912';

UPDATE Usuarios SET IdRol = {0}, UsuarioModifico = '{1}', FechaModifico = CURRENT_TIMESTAMP() where Usuario = '{0}';

UPDATE Usuarios SET Estado = 0, UsuarioModifico = '{1}', FechaModifico = CURRENT_TIMESTAMP() where Usuario = '{0}';

Select * from Roles Where estado =1 




Select Usuarios.*, Roles.Descripcion as Rol From Usuarios 
inner join Roles
	on Usuarios.IdRol = Roles.IdRol
Where Usuarios.Estado = 1
Order By Usuarios.usuario
OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY

Select Usuarios.Usuario, Usuarios.Email, Usuarios.Nombres, Usuarios.Apellidos, Usuarios.Estado, Roles.Descripcion as NombreRol From Usuarios 
inner join Roles
	on Usuarios.IdRol = Roles.IdRol
Order By Usuarios.usuario
OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY;

UPDATE Usuarios SET IdRol = 2, UsuarioModifico = 'JSOR', FechaModifico = CURRENT_TIMESTAMP where Usuario = '';

Select ROW_NUMBER() OVER(Order By Usuarios.usuario ASC) AS No, Usuarios.Usuario, Usuarios.Email, Usuarios.Nombres, Usuarios.Apellidos, Usuarios.Estado, Roles.Descripcion as NombreRol, Roles.IdRol From Usuarios
inner join Roles
	on Usuarios.IdRol = Roles.IdRol
Order By Usuarios.usuario
OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY;

UPDATE Usuarios SET Estado = 0, UsuarioModifico = 'JSOR', FechaModi	fico = CURRENT_TIMESTAMP() where Usuario = 'JSOR3';


Select ROW_NUMBER() OVER(Order By Usuarios.usuario ASC) AS Numero, Usuarios.Usuario, Usuarios.Email, Usuarios.Nombres, Usuarios.Apellidos, Usuarios.Estado, Roles.Descripcion as NombreRol, Roles.IdRol From Usuarios
inner join Roles
	on Usuarios.IdRol = Roles.IdRol
	WHERE USUARIO|EMAIL||NOMBRES||APELLIDOS LIKE '%J%'
Order By Usuarios.usuario
