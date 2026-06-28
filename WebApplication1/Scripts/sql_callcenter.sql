USE CallCenterDB;


CREATE TABLE [incidencias] (
	[id] INT NOT NULL IDENTITY UNIQUE,
	[cliente_id] INT,
	[tipo_incidencia_id] INT,
	[prioridad_id] INT,
	[estado_id] INT,
	[usuario_creador_id] INT,
	[usuario_asignado_id] INT,
	[asunto] VARCHAR(225),
	[descripcion] VARCHAR(225),
	[comentario_cierre] TEXT,
	[comentario_resolucion] TEXT,
	[fecha_alta] DATETIME,
	[fecha_resolucion] DATETIME,
	[fecha_cierre] DATETIME,
	PRIMARY KEY([id])
);


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


CREATE TABLE [roles] (
	[id] INT NOT NULL IDENTITY UNIQUE,
	[nombre] VARCHAR(225),
	PRIMARY KEY([id])
);


CREATE TABLE [estados] (
	[id] INT NOT NULL IDENTITY UNIQUE,
	[nombre] VARCHAR(225),
	PRIMARY KEY([id])
);


CREATE TABLE [prioridades] (
	[id] INT NOT NULL IDENTITY UNIQUE,
	[nombre] VARCHAR(225),
	PRIMARY KEY([id])
);


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


CREATE TABLE [tipos_incidencias] (
	[id] INT NOT NULL IDENTITY,
	[nombre] VARCHAR(225),
	PRIMARY KEY([id])
);


-- =============================================
-- CREACIÓN DE CLAVES FORÁNEAS 
-- =============================================

-- Foráneas de la tabla [incidencias]
ALTER TABLE [incidencias]
ADD FOREIGN KEY([cliente_id]) REFERENCES [clientes]([id]);


ALTER TABLE [incidencias]
ADD FOREIGN KEY([usuario_creador_id]) REFERENCES [usuarios]([id]);


ALTER TABLE [incidencias]
ADD FOREIGN KEY([usuario_asignado_id]) REFERENCES [usuarios]([id]);


ALTER TABLE [incidencias]
ADD FOREIGN KEY([estado_id]) REFERENCES [estados]([id]);


ALTER TABLE [incidencias]
ADD FOREIGN KEY([tipo_incidencia_id]) REFERENCES [tipos_incidencias]([id]);


ALTER TABLE [incidencias]
ADD FOREIGN KEY([prioridad_id]) REFERENCES [prioridades]([id]);


-- Foráneas de la tabla [usuarios]
ALTER TABLE [usuarios]
ADD FOREIGN KEY([rol_id]) REFERENCES [roles]([id]);


-- Foráneas de la tabla [incidencias_historico]
ALTER TABLE [incidencias_historico]
ADD FOREIGN KEY([id_incidencia]) REFERENCES [incidencias]([id]);


ALTER TABLE [incidencias_historico]
ADD FOREIGN KEY([estado_incidencia_id]) REFERENCES [estados]([id]);


ALTER TABLE [incidencias_historico]
ADD FOREIGN KEY([tipo_incidencia_id]) REFERENCES [tipos_incidencias]([id]);


ALTER TABLE [incidencias_historico]
ADD FOREIGN KEY([cliente_id]) REFERENCES [clientes]([id]);


ALTER TABLE [incidencias_historico]
ADD FOREIGN KEY([usuario_id]) REFERENCES [usuarios]([id]);


ALTER TABLE [incidencias_historico]
ADD FOREIGN KEY([prioridad_incidencia_id]) REFERENCES [prioridades]([id]);

-- ===================================================================
-- 1. TABLAS DICCIONARIO / CATÁLOGOS (Roles, Estados, Prioridades, Tipos)
-- ===================================================================

-- Cumpliendo la consigna: "distintos perfiles: Administrador, Telefonista y Supervisor"
INSERT INTO [roles] ([nombre]) VALUES 
('Administrador'), -- ID 1
('Telefonista'),   -- ID 2
('Supervisor');    -- ID 3

-- Cumpliendo la consigna: "modelo de estados (Abierto, En Análisis, Cerrado, Reabierto, Asignado, Resuelto)"
INSERT INTO [estados] ([nombre]) VALUES 
('Abierto'),     -- ID 1
('En Análisis'), -- ID 2
('Cerrado'),     -- ID 3
('Reabierto'),   -- ID 4
('Asignado'),    -- ID 5
('Resuelto');    -- ID 6

-- Prioridades (Administrables según consigna)
INSERT INTO [prioridades] ([nombre]) VALUES 
('Baja'),   -- ID 1
('Media'),  -- ID 2
('Alta'),   -- ID 3
('Crítica');-- ID 4

-- Tipos de incidencias (Administrables según consigna)
INSERT INTO [tipos_incidencias] ([nombre]) VALUES 
('Problema Técnico'),    -- ID 1
('Facturación'),         -- ID 2
('Reclamo de Atención'), -- ID 3
('Baja de Servicio');    -- ID 4

-- ===================================================================
-- 2. USUARIOS Y CLIENTES
-- ===================================================================

-- Creamos un usuario de cada rol para que puedas probar la visibilidad
-- Las contraseñas en un entorno real irían encriptadas (hash)
INSERT INTO [usuarios] ([nombre], [apellido], [email], [password], [rol_id], [activo], [fecha_creacion]) VALUES
('Carlos', 'Admin', 'admin@callcenter.com', '1234', 1, 1, GETDATE()),       -- ID 1 (Admin)
('Laura', 'Perez', 'lperez@callcenter.com', '1234', 2, 1, GETDATE()),       -- ID 2 (Telefonista 1)
('Miguel', 'Gomez', 'mgomez@callcenter.com', '1234', 2, 1, GETDATE()),      -- ID 3 (Telefonista 2)
('Ana', 'Super', 'asuper@callcenter.com', '1234', 3, 1, GETDATE());         -- ID 4 (Supervisor)

-- Clientes de prueba para asignarles los reclamos
INSERT INTO [clientes] ([nombre], [apellido], [documento], [email], [telefono], [activo], [fecha_creacion]) VALUES
('Juan', 'Lopez', 32111222, 'jlopez@cliente.com', 1144556677, 1, GETDATE()), -- ID 1
('Maria', 'Garcia', 28333444, 'mgarcia@cliente.com', 1155667788, 1, GETDATE()), -- ID 2
('Pedro', 'Martinez', 40555666, 'pmartinez@cliente.com', 1166778899, 1, GETDATE()); -- ID 3

-- ===================================================================
-- 3. INCIDENCIAS (Simulando distintos escenarios)
-- ===================================================================

INSERT INTO [incidencias] 
([cliente_id], [tipo_incidencia_id], [prioridad_id], [estado_id], [usuario_creador_id], [usuario_asignado_id], [asunto], [descripcion], [comentario_cierre], [comentario_resolucion], [fecha_alta], [fecha_resolucion], [fecha_cierre]) 
VALUES
-- Caso 1: Recién creada por Tele 1 (Queda Asignada al creador, estado Abierto)
(1, 1, 3, 1, 2, 2, 'Corte de Internet', 'El cliente no tiene servicio desde ayer a la noche.', NULL, NULL, GETDATE(), NULL, NULL),

-- Caso 2: Creada por Tele 2, pero reasignada por el Supervisor a Tele 1 (Estado Asignado)
(2, 2, 2, 5, 3, 2, 'Error en Factura', 'Le cobraron dos veces el abono mensual.', NULL, NULL, GETDATE(), NULL, NULL),

-- Caso 3: En progreso (Estado En Análisis)
(3, 1, 4, 2, 2, 2, 'Módem quemado', 'El módem hace ruido y huele a quemado. Requiere visita.', NULL, NULL, GETDATE(), NULL, NULL),

-- Caso 4: Resuelta satisfactoriamente (Estado Resuelto, con comentario final)
(1, 3, 1, 6, 3, 3, 'Consulta de saldo', 'Cliente quiere saber cuánto debe.', NULL, 'Se le informó por teléfono el saldo actual de $5000. Cliente conforme.', '2023-10-01 10:00:00', GETDATE(), NULL),

-- Caso 5: Cerrada (Estado Cerrado, sí o sí exige comentario de cierre según consigna)
(2, 4, 2, 3, 2, 2, 'Baja por viaje', 'Solicita baja de servicio por viaje al exterior.', 'No se pudo retener. Se procesó la baja y se cierra el reclamo.', NULL, '2023-09-15 08:30:00', NULL, GETDATE());

-- ===================================================================
-- 4. HISTÓRICO DE INCIDENCIAS (Auditoría)
-- ===================================================================

INSERT INTO [incidencias_historico] 
([id_incidencia], [estado_incidencia_id], [tipo_incidencia_id], [cliente_id], [usuario_id], [prioridad_incidencia_id], [fecha]) 
VALUES
-- Historial Caso 1 (Apenas nace, estado 1: Abierto)
(1, 1, 1, 1, 2, 3, GETDATE()),

-- Historial Caso 2 (Nace Abierta por Tele2, luego cambia a Asignada por el Supervisor)
(2, 1, 2, 2, 3, 2, GETDATE()), -- Creada por Miguel (Tele2 = ID 3)
(2, 5, 2, 2, 4, 2, GETDATE()), -- Supervisor Ana (ID 4) interviene y la pasa a estado Asignado (ID 5)

-- Historial Caso 3 (Nace Abierta, luego pasa a En Análisis)
(3, 1, 1, 3, 2, 4, GETDATE()), 
(3, 2, 1, 3, 2, 4, GETDATE()), -- Modificada, cambia a En Análisis (ID 2)

-- Historial Caso 4 (De Abierta a Resuelta directamente)
(4, 1, 3, 1, 3, 1, '2023-10-01 10:00:00'),
(4, 6, 3, 1, 3, 1, GETDATE()),

-- Historial Caso 5 (De Abierta a Cerrada)
(5, 1, 4, 2, 2, 2, '2023-09-15 08:30:00'),
(5, 3, 4, 2, 2, 2, GETDATE());

--- Trigger historico 

CREATE TRIGGER [dbo].[TR_Incidencias_Auditoria]
ON [dbo].[incidencias]
AFTER INSERT, UPDATE
AS
BEGIN
    -- Evita que el motor devuelva mensajes de recuento de filas afectadas
    SET NOCOUNT ON;

    -- Insertamos de forma automática en el histórico leyendo la tabla interna 'inserted'
    INSERT INTO [dbo].[incidencias_historico] 
    (
        [id_incidencia],
        [estado_incidencia_id],
        [tipo_incidencia_id],
        [cliente_id],
        [usuario_id],
        [prioridad_incidencia_id],
        [fecha]
    )
    SELECT 
        i.[id],
        i.[estado_id],
        i.[tipo_incidencia_id],
        i.[cliente_id],
        -- Si aún no tiene usuario asignado, registramos el creador para no romper la FK
        ISNULL(i.[usuario_asignado_id], i.[usuario_creador_id]),
        i.[prioridad_id],
        GETDATE()
    FROM inserted i;
END;



DROP TABLE IF EXISTS [incidencias_historico];
DROP TABLE IF EXISTS [incidencias];

-- 2. Luego las tablas que dependen de catálogos pero a su vez son referenciadas
DROP TABLE IF EXISTS [usuarios];
DROP TABLE IF EXISTS [clientes];

-- 3. Finalmente, los catálogos/diccionarios independientes
DROP TABLE IF EXISTS [roles];
DROP TABLE IF EXISTS [estados];
DROP TABLE IF EXISTS [prioridades];
DROP TABLE IF EXISTS [tipos_incidencias];



