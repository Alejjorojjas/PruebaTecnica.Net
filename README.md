# Prueba Tecnica .NET - Grupo Digital Bank

Sistema web por capas para registrar, consultar, modificar y eliminar usuarios usando ASP.NET Web Forms, WCF y SQL Server.

## Arquitectura

La solucion esta separada en proyectos para mantener responsabilidades claras:

| Capa | Proyecto / recurso | Responsabilidad |
| --- | --- | --- |
| Presentacion | `GrupoDigitalBank.Web` | Paginas Web Forms `Usuario` y `Usuario consulta` |
| Contratos | `GrupoDigitalBank.Contratos` | Interfaz WCF `IUsuarioServicio` |
| Dominio | `GrupoDigitalBank.Dominio` | Modelos compartidos `Usuario` y `ResultadoOperacion` |
| Negocio / Servicio | `GrupoDigitalBank.ServicioWCF` | Servicio WCF con metodos `Agregar`, `Modificar`, `Consultar`, `Eliminar` |
| Datos | `database/01_crear_base_datos.sql` | Base de datos, tabla `Usuario` y procedimiento `sp_Usuario_CRUD` |

La conexion a SQL Server se realiza desde el proyecto WCF, tal como solicita el enunciado.

## Requisitos

- Visual Studio 2022 o superior con soporte para ASP.NET Web Forms, WCF y .NET Framework.
- .NET Framework 4.8 Developer Pack / Targeting Pack.
- SQL Server Express LocalDB o SQL Server Express.
- IIS Express, incluido normalmente con Visual Studio.

Guia detallada: [docs/01-configuracion-entorno.md](docs/01-configuracion-entorno.md)

## Configuracion de base de datos

1. Abrir SQL Server Object Explorer, SSMS o Azure Data Studio.
2. Conectar a LocalDB con:

```text
(localdb)\MSSQLLocalDB
```

3. Ejecutar el script:

```text
database/01_crear_base_datos.sql
```

Opcionalmente, ejecutar datos de prueba:

```text
database/02_datos_prueba.sql
```

Para SQL Server Express, usar normalmente:

```text
.\SQLEXPRESS
```

Y actualizar la cadena de conexion en:

```text
src/GrupoDigitalBank.ServicioWCF/Web.config
```

Ejemplo para SQL Server Express:

```xml
<add name="GrupoDigitalBankDb"
     connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=GrupoDigitalBankDb;Integrated Security=True;MultipleActiveResultSets=True"
     providerName="System.Data.SqlClient" />
```

## Ejecucion en Visual Studio

1. Abrir `PruebaTecnicaGrupoDigitalBank.sln`.
2. Compilar la solucion completa.
3. Configurar multiples proyectos de inicio:
   - `GrupoDigitalBank.ServicioWCF`: Start
   - `GrupoDigitalBank.Web`: Start
4. Ejecutar con IIS Express.
5. Verificar primero el servicio:

```text
http://localhost:50901/UsuarioServicio.svc
```

6. Abrir la aplicacion web:

```text
http://localhost:50900/Usuario.aspx
```

Si Visual Studio cambia los puertos, actualizar el endpoint WCF en `src/GrupoDigitalBank.Web/Web.config`.

## Funcionalidades

- Registro de usuarios con nombre, fecha de nacimiento y sexo.
- Consulta de usuarios registrados en una grilla.
- Modificacion de usuarios desde la grilla.
- Eliminacion de usuarios desde la grilla.
- Validaciones de formulario y de negocio.
- Acceso a datos parametrizado mediante procedimiento almacenado.

## Documentacion

- [Configuracion del entorno](docs/01-configuracion-entorno.md)
- [Arquitectura](docs/02-arquitectura.md)
- [Plan de pruebas](docs/03-plan-pruebas.md)
- [Guia de entrega](docs/04-guia-entrega.md)

## Notas de entrega

Este repositorio esta preparado para enviarse por GitHub. Antes de enviarlo, se recomienda ejecutar el plan de pruebas y agregar capturas de pantalla si el reclutador las permite.
