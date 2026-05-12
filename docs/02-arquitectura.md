# Arquitectura

La solucion sigue una arquitectura por capas simple, explicita y facil de evaluar.

## Capas

| Capa | Implementacion | Detalle |
| --- | --- | --- |
| Presentacion | `GrupoDigitalBank.Web` | ASP.NET Web Forms con dos paginas: `Usuario.aspx` y `UsuarioConsulta.aspx` |
| Contratos | `GrupoDigitalBank.Contratos` | Define `IUsuarioServicio`, el contrato WCF compartido |
| Dominio | `GrupoDigitalBank.Dominio` | Contiene entidades transportables por WCF |
| Negocio | `GrupoDigitalBank.ServicioWCF` | Implementa reglas basicas, validaciones y conexion a base de datos |
| Datos | `database/01_crear_base_datos.sql` | Tabla `Usuario` y procedimiento almacenado CRUD |

## Flujo principal

```mermaid
flowchart LR
    A["Usuario en navegador"] --> B["ASP.NET Web Forms"]
    B --> C["Contrato WCF IUsuarioServicio"]
    C --> D["Servicio WCF UsuarioServicio"]
    D --> E["ADO.NET / SqlCommand"]
    E --> F["sp_Usuario_CRUD"]
    F --> G["Tabla dbo.Usuario"]
```

## Decisiones tecnicas

- Se usa .NET Framework 4.8 porque WCF clasico y Web Forms tienen soporte nativo en este stack.
- Se separa `GrupoDigitalBank.Contratos` para que la capa web consuma el servicio por contrato y no por implementacion.
- Se usa ADO.NET parametrizado para evitar concatenacion de SQL y mantener bajo el acoplamiento.
- Se usa un unico procedimiento almacenado `sp_Usuario_CRUD`, alineado con el enunciado.
- La cadena de conexion vive en `Web.config` del servicio WCF porque la conexion a datos debe realizarse desde esa capa.

## Entidad principal

Tabla `dbo.Usuario`:

| Campo | Tipo | Regla |
| --- | --- | --- |
| `Id` | `INT IDENTITY` | Llave primaria |
| `Nombre` | `VARCHAR(100)` | Obligatorio |
| `FechaNacimiento` | `DATE` | Obligatorio |
| `Sexo` | `CHAR(1)` | Valores `M` o `F` |
| `FechaCreacion` | `DATETIME2(0)` | Auditoria basica |

## Metodos WCF

| Metodo | Responsabilidad |
| --- | --- |
| `Agregar` | Inserta un usuario |
| `Modificar` | Actualiza un usuario existente |
| `Consultar` | Retorna todos los usuarios |
| `Eliminar` | Elimina un usuario por `Id` |

## Consideraciones empresariales incluidas

- Validaciones en presentacion y servicio.
- Manejo de mensajes de exito y error.
- Configuracion externa de conexion a base de datos.
- Script SQL idempotente para crear base, tabla y procedimiento.
- Documentacion de arquitectura, pruebas y entrega.
