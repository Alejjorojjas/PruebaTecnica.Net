# Plan de pruebas

Este plan permite validar la prueba antes de enviarla.

## Pruebas de base de datos

Ejecutar en `GrupoDigitalBankDb`:

```sql
EXEC dbo.sp_Usuario_CRUD
    @Operacion = 'AGREGAR',
    @Nombre = 'Ana Perez',
    @FechaNacimiento = '1995-04-12',
    @Sexo = 'F';

EXEC dbo.sp_Usuario_CRUD
    @Operacion = 'CONSULTAR';

EXEC dbo.sp_Usuario_CRUD
    @Operacion = 'MODIFICAR',
    @Id = 1,
    @Nombre = 'Ana Maria Perez',
    @FechaNacimiento = '1995-04-12',
    @Sexo = 'F';

EXEC dbo.sp_Usuario_CRUD
    @Operacion = 'ELIMINAR',
    @Id = 1;
```

## Pruebas funcionales web

| Caso | Pasos | Resultado esperado |
| --- | --- | --- |
| Registrar usuario | Abrir `Usuario`, diligenciar campos y guardar | Se muestra mensaje de exito y se inserta en BD |
| Validar obligatorios | Guardar sin nombre, fecha o sexo | La pagina muestra validaciones |
| Consultar usuarios | Abrir `Usuario consulta` | La grilla lista registros existentes |
| Modificar usuario | Clic en `Modificar`, cambiar nombre y guardar | Se actualiza el registro |
| Eliminar usuario | Clic en `Eliminar` y confirmar | Se elimina el registro |

## Pruebas de integracion

- Abrir `http://localhost:50901/UsuarioServicio.svc` y verificar que WCF responda.
- Abrir `http://localhost:50900/Usuario.aspx` y registrar un usuario.
- Confirmar en SQL Server que el registro existe.
- Modificar desde la web y confirmar el cambio en SQL Server.
- Eliminar desde la web y confirmar que ya no aparece en la consulta.

## Errores comunes a validar

| Sintoma | Posible causa | Solucion |
| --- | --- | --- |
| No carga el servicio WCF | Falta WCF tooling o IIS Express | Revisar componentes de Visual Studio |
| Error de conexion a BD | LocalDB no instalado o cadena incorrecta | Validar `sqllocaldb info` y `Web.config` del servicio |
| La web no encuentra el servicio | Puerto del WCF cambio | Actualizar endpoint en `GrupoDigitalBank.Web/Web.config` |
| No aparece .NET Framework 4.8 | Falta Targeting Pack | Instalar componente desde Visual Studio Installer |
