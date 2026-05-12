USE GrupoDigitalBankDb;
GO

EXEC dbo.sp_Usuario_CRUD
    @Operacion = 'AGREGAR',
    @Nombre = 'Ana Perez',
    @FechaNacimiento = '1995-04-12',
    @Sexo = 'F';

EXEC dbo.sp_Usuario_CRUD
    @Operacion = 'AGREGAR',
    @Nombre = 'Carlos Ramirez',
    @FechaNacimiento = '1990-09-23',
    @Sexo = 'M';

EXEC dbo.sp_Usuario_CRUD
    @Operacion = 'CONSULTAR';
GO
