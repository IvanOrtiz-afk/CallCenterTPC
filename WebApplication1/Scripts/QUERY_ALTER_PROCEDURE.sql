ALTER PROCEDURE spFiltrarClientesParaIncidencias
    @filtro VARCHAR(100) = '' 
AS
BEGIN
    -- Solo traemos clientes dados de alta (activo = 1)
    SELECT id, nombre, apellido, documento, email, telefono, activo
    FROM clientes
    WHERE activo = 1 
      AND (
          nombre LIKE '%' + @filtro + '%' OR
          apellido LIKE '%' + @filtro + '%' OR
          (nombre + ' ' + apellido) LIKE '%' + @filtro + '%' OR
          (apellido + ' ' + nombre) LIKE '%' + @filtro + '%' OR
          CAST(documento AS VARCHAR) LIKE '%' + @filtro + '%'
      )
END
GO