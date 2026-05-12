IF DB_ID(N'GrupoDigitalBankDb') IS NULL
BEGIN
    CREATE DATABASE GrupoDigitalBankDb;
END
GO

USE GrupoDigitalBankDb;
GO

IF OBJECT_ID(N'dbo.Usuario', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Usuario
    (
        Id INT IDENTITY(1,1) NOT NULL,
        Nombre VARCHAR(100) NOT NULL,
        FechaNacimiento DATE NOT NULL,
        Sexo CHAR(1) NOT NULL,
        FechaCreacion DATETIME2(0) NOT NULL
            CONSTRAINT DF_Usuario_FechaCreacion DEFAULT SYSUTCDATETIME(),
        CONSTRAINT PK_Usuario PRIMARY KEY CLUSTERED (Id),
        CONSTRAINT CK_Usuario_Sexo CHECK (Sexo IN ('M', 'F')),
        CONSTRAINT CK_Usuario_Nombre_NoVacio CHECK (LEN(LTRIM(RTRIM(Nombre))) > 0)
    );
END
GO

IF OBJECT_ID(N'dbo.sp_Usuario_CRUD', N'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.sp_Usuario_CRUD;
END
GO

CREATE PROCEDURE dbo.sp_Usuario_CRUD
    @Operacion VARCHAR(10),
    @Id INT = NULL,
    @Nombre VARCHAR(100) = NULL,
    @FechaNacimiento DATE = NULL,
    @Sexo CHAR(1) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    SET @Operacion = UPPER(LTRIM(RTRIM(@Operacion)));

    IF @Operacion = 'CONSULTAR'
    BEGIN
        SELECT
            Id,
            Nombre,
            FechaNacimiento,
            Sexo
        FROM dbo.Usuario
        ORDER BY Id DESC;

        RETURN;
    END

    IF @Operacion NOT IN ('AGREGAR', 'MODIFICAR', 'ELIMINAR')
    BEGIN
        RAISERROR('Operacion no soportada por dbo.sp_Usuario_CRUD.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @Operacion = 'AGREGAR'
        BEGIN
            IF NULLIF(LTRIM(RTRIM(@Nombre)), '') IS NULL
                RAISERROR('El nombre es obligatorio.', 16, 1);

            IF @FechaNacimiento IS NULL
                RAISERROR('La fecha de nacimiento es obligatoria.', 16, 1);

            IF @Sexo NOT IN ('M', 'F')
                RAISERROR('El sexo debe ser M o F.', 16, 1);

            INSERT INTO dbo.Usuario (Nombre, FechaNacimiento, Sexo)
            VALUES (LTRIM(RTRIM(@Nombre)), @FechaNacimiento, @Sexo);

            SELECT
                CAST(SCOPE_IDENTITY() AS INT) AS Id,
                'Usuario agregado correctamente.' AS Mensaje;
        END

        IF @Operacion = 'MODIFICAR'
        BEGIN
            IF @Id IS NULL OR NOT EXISTS (SELECT 1 FROM dbo.Usuario WHERE Id = @Id)
                RAISERROR('El usuario indicado no existe.', 16, 1);

            IF NULLIF(LTRIM(RTRIM(@Nombre)), '') IS NULL
                RAISERROR('El nombre es obligatorio.', 16, 1);

            IF @FechaNacimiento IS NULL
                RAISERROR('La fecha de nacimiento es obligatoria.', 16, 1);

            IF @Sexo NOT IN ('M', 'F')
                RAISERROR('El sexo debe ser M o F.', 16, 1);

            UPDATE dbo.Usuario
            SET
                Nombre = LTRIM(RTRIM(@Nombre)),
                FechaNacimiento = @FechaNacimiento,
                Sexo = @Sexo
            WHERE Id = @Id;

            SELECT
                @Id AS Id,
                'Usuario modificado correctamente.' AS Mensaje;
        END

        IF @Operacion = 'ELIMINAR'
        BEGIN
            IF @Id IS NULL OR NOT EXISTS (SELECT 1 FROM dbo.Usuario WHERE Id = @Id)
                RAISERROR('El usuario indicado no existe.', 16, 1);

            DELETE FROM dbo.Usuario
            WHERE Id = @Id;

            SELECT
                @Id AS Id,
                'Usuario eliminado correctamente.' AS Mensaje;
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @MensajeError NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@MensajeError, 16, 1);
    END CATCH
END
GO
