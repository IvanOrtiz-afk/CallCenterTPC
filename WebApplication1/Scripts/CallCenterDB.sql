CREATE DATABASE CallCenterDB;
GO

USE CallCenterDB;
GO

CREATE TABLE Clientes
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50),
    Apellido VARCHAR(50),
    DNI VARCHAR(20),
    Email VARCHAR(100),
    Telefono VARCHAR(30),
    Activo BIT
);
GO

INSERT INTO Clientes
VALUES
('Juan','Perez','30111222','juan@gmail.com','11111111',1),
('Ana','Gomez','32444555','ana@gmail.com','22222222',1),
('Carlos','Lopez','28777888','carlos@gmail.com','33333333',1);
GO