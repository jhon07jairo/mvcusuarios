create database DB_USUARIOS;
USE DB_USUARIOS;

CREATE TABLE USUARIO(
id_usuario int identity(1,1) primary key,
nombre varchar(100),
fecha_nacimiento date,
sexo varchar(1)
);

CREATE PROCEDURE sp_Listar
as 
begin	
	select * from USUARIO
end

CREATE PROCEDURE sp_Obtener(
@id_usuario int
)
as
begin
	select * from USUARIO where id_usuario = @id_usuario
end

CREATE PROCEDURE sp_Guardar(
@nombre varchar(100),
@fecha_nacimiento date,
@sexo varchar(1) 
)
as
begin
	insert into USUARIO(nombre, fecha_nacimiento, sexo) values (@nombre, @fecha_nacimiento, @sexo)
end

CREATE PROCEDURE sp_Editar(
@id_usuario int,
@nombre varchar(100),
@fecha_nacimiento date,
@sexo varchar(1) 
)
as
begin
	update USUARIO set nombre = @nombre, fecha_nacimiento = @fecha_nacimiento, sexo = @sexo where id_usuario = @id_usuario;
end

CREATE PROCEDURE sp_Eliminar(
@id_usuario int
)
as
begin
	delete from USUARIO where id_usuario = @id_usuario;
end

select * from USUARIO

CREATE TABLE Logs
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATETIME default getdate(),
    Accion NVARCHAR(MAX),
    Mensaje NVARCHAR(MAX)
);
