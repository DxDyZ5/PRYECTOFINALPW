create database PROYECTOFINALPW

use PROYECTOFINALPW


create table Caso(
Id int identity(1,1) primary key , 
 Fecha DateTime not null ,
  ClienteId int references Cliente(Id) not null, 
  TipoId int references TipoCaso(Id)  not null,
  Latitud decimal(10,6) not null,
  Longitud decimal(10,6) not null,
  Descripcion varchar(300) not null,
  AbogadoId int references Abogado(Id) not null,
  EstadoId int references EstadoCaso(Id)  not null,
)


create table Cliente(
 Id  int identity(1,1) primary key,
 Nombre varchar(50) not  null,
 Apellido varchar(50) not null,
 Cedula varchar(14) not null,
Correo varchar(256) not null,
Telefono varchar(16) not null,
Celular varchar(16) not null,
Direccion  varchar(32) not null,
Estado_Civil varchar(20) not null,
)



create table Abogado(
 Id  int primary key identity(1,1),
Nombre varchar(50) not  null  
)

create table TipoCaso(
 Id  int primary key identity(1,1),
tipo varchar(50) not  null  
)

create table EstadoCaso(
 Id  int primary key identity(1,1),
Estado varchar(50) not  null  
)

select * from  Abogado
select * from  Cliente
select * from  Caso
select * from  EstadoCaso
select * from  TipoCaso


insert into Abogado values
('Alejandro Sánchez'),
('Marta Rodríguez'),
('Ricardo Morales'),
('Gabriela Pérez'),
('Ignacio Vargas'),
('Laura Gómez'),
('Federico Mendoza'),
('Valeria Castro'),
('Ernesto Medina'),
('Carla Herrera')


insert into TipoCaso values
('Civil'),
('Penal'),
('Familia'),
('Laboral'),
('Inmigración'),
('Propiedad Intelectual'),
('Comercial'),
('Ambiental'),
('Contratos'),
('Derechos Humanos')


insert into EstadoCaso values
('En Proceso'),
('Pendiente'),
('Resuelto'),
('Cerrado'),
('En Revisión'),
('Archivado'),
('En Curso'),
('Urgente'),
('Suspendido'),
('Investigación en Curso')