use CallCenterDB;

CREATE TABLE [incidencias] (
	[id] INT NOT NULL IDENTITY UNIQUE,
	[cliente_id] INT,
	[tipo_incidencia_id] INT,
	[prioridad_id] INT,
	[estado_id] INT,
	[usuario_creador_id] INT,
	[usuario_asignado_id] INT,
	[descripcion] VARCHAR(225),
	[comentario_cierre] TEXT,
	[comentario_resolucion] TEXT,
	[fecha_alta] DATETIME,
	[fecha_resolucion] DATETIME,
	[fecha_cierre] DATETIME,
	PRIMARY KEY([id])
);
GO

CREATE TABLE [usuarios] (
	[id] INT NOT NULL IDENTITY UNIQUE,
	[nombre] VARCHAR(225),
	[apellido] VARCHAR(225),
	[email] VARCHAR(225),
	[password] VARCHAR(225),
	[rol_id] INT,
	[activo] BIT,
	[fecha_creacion] DATETIME,
	PRIMARY KEY([id])
);
GO

CREATE TABLE [roles] (
	[id] INT NOT NULL IDENTITY UNIQUE,
	[nombre] VARCHAR(225),
	PRIMARY KEY([id])
);
GO

CREATE TABLE [estados] (
	[id] INT NOT NULL IDENTITY UNIQUE,
	[nombre] VARCHAR(225),
	PRIMARY KEY([id])
);
GO

CREATE TABLE [prioridades] (
	[id] INT NOT NULL IDENTITY UNIQUE,
	[nombre] VARCHAR(225),
	PRIMARY KEY([id])
);
GO

CREATE TABLE [incidencias_historico] (
	[id] INT NOT NULL IDENTITY,
	[id_incidencia] INT,
	[estado_incidencia_id] INT,
	[tipo_incidencia_id] INT,
	[cliente_id] INT,
	[usuario_id] INT,
	[prioridad_incidencia_id] INT,
	[fecha] DATETIME,
	PRIMARY KEY([id])
);
GO

CREATE TABLE [clientes] (
	[id] INT NOT NULL IDENTITY UNIQUE,
	[nombre] VARCHAR(225),
	[apellido] VARCHAR(225),
	[documento] INT,
	[email] VARCHAR(225),
	[telefono] INT,
	[activo] BIT,
	[fecha_creacion] DATETIME,
	PRIMARY KEY([id])
);
GO

CREATE TABLE [tipos_incidencias] (
	[id] INT NOT NULL IDENTITY,
	[nombre] VARCHAR(225),
	PRIMARY KEY([id])
);
GO

-- =============================================
-- CREACIÓN DE CLAVES FORÁNEAS CORREGIDAS
-- =============================================

-- Foráneas de la tabla [incidencias]
ALTER TABLE [incidencias]
ADD FOREIGN KEY([cliente_id]) REFERENCES [clientes]([id]);
GO

ALTER TABLE [incidencias]
ADD FOREIGN KEY([usuario_creador_id]) REFERENCES [usuarios]([id]);
GO

ALTER TABLE [incidencias]
ADD FOREIGN KEY([usuario_asignado_id]) REFERENCES [usuarios]([id]);
GO

ALTER TABLE [incidencias]
ADD FOREIGN KEY([estado_id]) REFERENCES [estados]([id]);
GO

ALTER TABLE [incidencias]
ADD FOREIGN KEY([tipo_incidencia_id]) REFERENCES [tipos_incidencias]([id]);
GO

ALTER TABLE [incidencias]
ADD FOREIGN KEY([prioridad_id]) REFERENCES [prioridades]([id]);
GO

-- Foráneas de la tabla [usuarios]
ALTER TABLE [usuarios]
ADD FOREIGN KEY([rol_id]) REFERENCES [roles]([id]);
GO

-- Foráneas de la tabla [incidencias_historico]
ALTER TABLE [incidencias_historico]
ADD FOREIGN KEY([id_incidencia]) REFERENCES [incidencias]([id]);
GO

ALTER TABLE [incidencias_historico]
ADD FOREIGN KEY([estado_incidencia_id]) REFERENCES [estados]([id]);
GO

ALTER TABLE [incidencias_historico]
ADD FOREIGN KEY([tipo_incidencia_id]) REFERENCES [tipos_incidencias]([id]);
GO

ALTER TABLE [incidencias_historico]
ADD FOREIGN KEY([cliente_id]) REFERENCES [clientes]([id]);
GO

ALTER TABLE [incidencias_historico]
ADD FOREIGN KEY([usuario_id]) REFERENCES [usuarios]([id]);
GO

ALTER TABLE [incidencias_historico]
ADD FOREIGN KEY([prioridad_incidencia_id]) REFERENCES [prioridades]([id]);
GO