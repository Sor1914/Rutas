Drop Database Rutas;
Create Database Rutas;
Use Rutas;
Use Prueba;

--aun falta crear la tabla del precio de la gas
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

insert into horarios (horaEntrada, horaAlmuerzo, HoraSalida, estado, UsuarioCreo, FechaCreo)
values('8:00', '12:00', '17:00', 1, 'system', CURRENT_TIMESTAMP);

insert into horarios (horaEntrada, horaAlmuerzo, HoraSalida, estado, UsuarioCreo, FechaCreo)
values('8:00', '13:00', '18:00', 1, 'system', CURRENT_TIMESTAMP);

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

alter table  TipoVehiculos
alter column CapacidadPeso
Decimal (8,2);

alter table  TipoVehiculos
alter column KMXGalon
Decimal (8,2);

alter table  TipoVehiculos
alter column Galones
Decimal (8,2);


Create Table Vehiculos
(
	Placa varchar(7) primary key,
	KmRecorridos decimal(8,2),	
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
	PesoProducto decimal(8,2),	
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

alter table proveedorproducto
alter column PrecioProducto Decimal(8,2)


Create Table Pedidos
(
	IdPedido int IDENTITY(1,1) PRIMARY KEY,	
	Latitud Varchar(50),
	Longitud Varchar(50),
	Estado int,
	UsuarioCreo varchar(10),
	FechaCreo DateTime Default Current_Timestamp,
	UsuarioModifico Varchar(10),
	FechaModifico Datetime
)

Create Table DetallePedido
(
	IdDetalle int Identity(1,1) Primary Key,
	Cantidad decimal (8,2),		
	IdProducto int foreign key references Productos(IdProducto),
	IdPedido int foreign key references Pedidos(IdPedido),
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


INSERT INTO Usuarios(Usuario, Contrasenia, Email, Nombres, Apellidos, IdRol, UsuarioCreo, Estado)
VALUES ('JSOR',ENCRYPTBYPASSPHRASE('JS0R', 'Sor1906197912' ),'jonathansor2000sm@gmail.com','Jonathan Elias','Sor Monroy',1, 'Sys',1);

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
	WHERE CONCAT(USUARIO,EMAIL,NOMBRES,APELLIDOS) LIKE '%{0}%'
Order By Usuarios.usuario

--Consultas conductores
Select ROW_NUMBER() OVER(Order by Conductores.FechaCreo ASC) as Numero,
Conductores.*
from Conductores
where CONCAT(Licencia, Usuario) Like '%{0}%'
and estado = 1
order by usuario
OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY;

Select count(1) conductores from Conductores where CONCAT(Licencia, Usuario) Like '%{0}%';

insert into Conductores(Licencia, TipoLicencia, idHorario, LimiteParadas, Usuario, Estado, UsuarioCreo, FechaCreo)
values('{0}', '{1}', {2}, '{3}', '{4}', 1, '{5}', CURRENT_TIMESTAMP);

update conductores set TipoLicencia = '{0}', idHorario = '{1}', 
LimiteParadas = '{2}', UsuarioModifico = '{4}', FechaModifico = CURRENT_TIMESTAMP where
Licencia = '{5}'

update conductores set estado = {0} where licencia '{1}';

select idHorario from horarios where estado = 1;

select * from Conductores

select Concat(Nombres, Apellidos) from usuarios where usuario = '{0}';

select usuario from usuarios 
where idRol = '3' and usuario not in (select usuario from conductores)

insert into Conductores(Licencia, TipoLicencia, idHorario, LimiteParadas, Usuario, Estado, UsuarioCreo, FechaCreo)
values('234234', 'm', 2, '4', 'Dbalan', 1, 'JSOR', CURRENT_TIMESTAMP);

select * from conductores
update conductores set estado = 0, usuarioModifico = 'JSOR', fechaModifico = current_timestamp where licencia = '234234';

--Vehiculos
Select ROW_NUMBER() OVER(Order by TipoVehiculos.FechaCreo ASC) as Numero,
TipoVehiculos.*
from TipoVehiculos
where CONCAT(Descripcion, TipoGas) Like '%{0}%'
and estado = 1
order by IdTipoVehiculo
OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY;

select count(1) from tipoVehiculos where
 CONCAT(Descripcion, TipoGas) Like '%{0}%'
and estado = 1;

insert into TipoVehiculos(CapacidadPeso, KMXGalon, Galones, TipoGas, Descripcion, Estado, UsuarioCreo, FechaCreo)
Values ('{0}','{1}','{2}','{3}','{4}',1, '{5}', CURRENT_TIMESTAMP)

update TipoVehiculos set CapacidadPeso = '{0}', KMXGalon = '{1}', Galones = '{2}',Descripcion = '{3}', UsuarioModifico = '{4}', FechaModifico = CURRENT_TIMESTAMP
where IdTipoVehiculo = '{5}'

update TipoVehiculos set Estado = '{0}', usuarioModifico = '{1}', FechaModifico = CURRENT_TIMESTAMP 
where IdTipoVehiculo = '{2}'

--Vehiculos
Select ROW_NUMBER() OVER(Order by FechaCreo ASC) as Numero,
vehiculos.*
from vehiculos
where CONCAT(Placa, LicenciaConductor) Like '%{0}%'
and estado = 1
order by Numero
OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY;

select count(1) from vehiculos where CONCAT(Placa, LicenciaConductor) Like '%{0}%'
and estado = 1

select * from Conductores where Licencia not in (select Licencia from vehiculos where licencia = Conductores.Licencia and estado = 1) and estado = 1; 

SELECT * FROM TipoVehiculos where estado = 1; 

Insert into Vehiculos(Placa, KmRecorridos, LicenciaConductor, idTipoVehiculo, Estado, UsuarioCreo, FechaCreo)
values ('{0}', {1}, '{2}', {3}, 1, '{4}', '{5}', CURRENT_TIMESTAMP);

update Vehiculos set Placa = '{0}', LicenciaConductor = '{1}', idTipoVehiculo = '{2}', UsuarioModifico = '{3}', FechaModifico = CURRENT_TIMESTAMP,   3
where Placa = '{4}';

update Vehiculos set Estado = {0}, UsuarioModifico = '{1}', FechaModifico = CURRENT_TIMESTAMP
where Placa = '{2}';



insert into TipoVehiculos(CapacidadPeso, KMXGalon, Galones, TipoGas, Descripcion, Estado, UsuarioCreo, FechaCreo)
Values ('10','10','10','Pre','PickUp',1, '0', CURRENT_TIMESTAMP)

use rutas;
select * from Vehiculos

Insert into Vehiculos(Placa, KmRecorridos, LicenciaConductor, idTipoVehiculo, Estado, UsuarioCreo, FechaCreo)
values ('100', 100.00, 234234, 3, 1, '0', CURRENT_TIMESTAMP);

--Proveedores
Select ROW_NUMBER() OVER(Order by FechaCreo ASC) as Numero,
Proveedor.*
from Proveedor
where CONCAT(NombreProveedor, DireccionProveedor) Like '%{0}%'
and estado = 1
order by Numero
OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY;

select count(1) from Proveedor
where CONCAT(NombreProveedor, DireccionProveedor) Like '%{0}%'
and estado = 1

Insert into proveedor(NombreProveedor, DireccionProveedor, Longitud, Latitud, Estado, UsuarioCreo, FechaCreo)
values('{0}','{1}','{2}','{3}',1,'{4}', CURRENT_TIMESTAMP);

Update proveedor set DireccionProveedor = '{0}', Longitud = '{1}', Latitud = '{2}', UsuarioModifico = '{3}', FechaModifico = CURRENT_TIMESTAMP
where IdProveedor = '{4}';

Update proveedor set Estado = {0}, UsuarioModifico = '{1}', FechaModifico = CURRENT_TIMESTAMP
where IdProveedor = '{2}'

--Productos
Select * from Productos

Select ROW_NUMBER() OVER(Order by FechaCreo ASC) as Numero,
Productos.*
from Productos
where CONCAT(NombreProducto, TipoProducto) Like '%{0}%'
and estado = 1
order by Numero
OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY;

select count(1) from Productos
where CONCAT(NombreProducto, TipoProducto) Like '%{0}%'
and estado = 1

insert into Productos(NombreProducto, TipoProducto, PesoProducto, Estado, UsuarioCreo, FechaCreo)
values('{0}', '{1}',{2}, 1, '{3}', CURRENT_TIMESTAMP);

update productos set PesoProducto = '{0}', UsuarioModifico = '{1}'
where IdProducto = '{2}'

update productos set Estado = {0}, UsuarioModifico = '{1}'
where IdProducto = '{2}'

use rutas;
select * from Roles

Select Count(1) from Usuarios inner join Roles on Usuarios.IdRol = Roles.IdRol 	WHERE CONCAT(usuarios.USUARIO,usuarios.EMAIL,usuarios.NOMBRES,usuarios.APELLIDOS) LIKE '%%';
	
insert into ProveedorProducto (IdProveedor, IdProducto, PrecioProducto, Existencia, Estado, UsuarioCreo, FechaCreo)
values ({0}, {1}, {2}, {3}, 1, '{4}', CURRENT_TIMESTAMP) 

insert into ProveedorProducto (IdProveedor, IdProducto, PrecioProducto, Existencia, Estado, UsuarioCreo, FechaCreo)
values (3, 3, 10, 10, 1, 'JSOR', CURRENT_TIMESTAMP)

select 
ROW_NUMBER() OVER(Order by proveedorProducto.FechaCreo ASC) as Numero,
proveedor.NombreProveedor,
productos.NombreProducto,
proveedorProducto.PrecioProducto,
proveedorProducto.Existencia,
proveedorProducto.UsuarioCreo,
proveedorProducto.FechaCreo,
proveedorProducto.Estado,
proveedorProducto.IdProducto,
proveedorProducto.IdProveedor
from proveedorProducto
inner join proveedor 
on proveedorProducto.IdProveedor = proveedor.IdProveedor
inner join productos
on productos.idProducto = proveedorProducto.idProducto
where proveedorProducto.Estado = 1

select * from ProveedorProducto
delete proveedorProducto where IdProveedor = {0} and IdProducto = {1}

Insert Pedidos (Latitud, Longitud, Estado, UsuarioCreo, FechaCreo)
values ('{0}','{1}',1,'{2}',CURRENT_TIMESTAMP);

Insert DetallePedido(Cantidad, IdProducto, IdPedido, Estado, UsuarioCreo, FechaCreo)
Values (10,0,(Select max(IdPedido) from Pedidos), 1, 'JSOR', Current_timestamp)

Insert DetallePedido(Cantidad, IdProducto, IdPedido, Estado, UsuarioCreo, FechaCreo)
Values (1,0,(Select max(IdPedido) from Pedidos), 1, 'JSOR', Current_timestamp)

Insert DetallePedido(Cantidad, IdProducto, IdPedido, Estado, UsuarioCreo, FechaCreo)
Values (1,0,(Select max(IdPedido) from Pedidos), 1, 'JSOR', Current_timestamp)

Insert DetallePedido(Cantidad, IdProducto, IdPedido, Estado, UsuarioCreo, FechaCreo)
Values ({0},{1},(Select max(IdPedido) from Pedidos), 1, '{2}', Current_timestamp)

select * from detallepedido

Select 
pedidos.IdPedido as IdPedido,
pedidos.Latitud as LatitudInicial, 
pedidos.Longitud as LongitudInicial,
proveedor.Latitud as LatitudFinal,
proveedor.Longitud as Longitudfinal,
pedidos.UsuarioCreo,
productos.NombreProducto
from pedidos
inner join DetallePedido
on pedidos.IdPedido = DetallePedido.IdPedido
inner join Productos
on DetallePedido.IdProducto = Productos.IdProducto
inner join ProveedorProducto
on Productos.IdProducto = ProveedorProducto.IdProducto
inner join Proveedor
on Proveedor.IdProveedor = ProveedorProducto.IdProveedor
where pedidos.Estado = 1


select * from Conductores where Licencia not in (select LicenciaConductor from vehiculos where licencia = Conductores.Licencia and estado = 1) and estado = 1;

select * from vehiculos