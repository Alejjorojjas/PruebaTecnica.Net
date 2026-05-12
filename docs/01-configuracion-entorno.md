# Configuracion del entorno local

Esta guia deja el equipo listo para compilar y ejecutar la prueba tecnica con .NET Framework 4.8, ASP.NET Web Forms, WCF y SQL Server.

## 1. Instalar o modificar Visual Studio

Abre **Visual Studio Installer** y selecciona **Modify** sobre tu instalacion de Visual Studio.

En **Workloads**, marca:

- **ASP.NET and web development**
- **.NET desktop development**
- **Data storage and processing**

En **Individual components**, valida que esten instalados:

- **.NET Framework 4.8 SDK**
- **.NET Framework 4.8 targeting pack**
- **Windows Communication Foundation / WCF tooling**
- **IIS Express**
- **SQL Server Express LocalDB**
- **SQL Server Data Tools**
- **NuGet package manager**

Referencias oficiales:

- [Install Visual Studio and choose preferred features](https://learn.microsoft.com/visualstudio/install/install-visual-studio?view=vs-2022)
- [Visual Studio workload and component IDs](https://learn.microsoft.com/visualstudio/install/workload-component-id-vs-enterprise?view=vs-2022)

## 2. Validar herramientas desde terminal

Despues de instalar los componentes, abre **Developer PowerShell for Visual Studio** y ejecuta:

```powershell
where msbuild
sqllocaldb info
dotnet --info
```

Para esta prueba, lo mas importante es que `msbuild` y `sqllocaldb` respondan correctamente.

## 3. Crear la base de datos en LocalDB

Conecta a:

```text
(localdb)\MSSQLLocalDB
```

Luego ejecuta:

```text
database/01_crear_base_datos.sql
```

Puedes ejecutarlo desde SQL Server Object Explorer en Visual Studio, SSMS o Azure Data Studio.

Referencia oficial:

- [SQL Server Express LocalDB](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver17)

## 4. Usar SQL Server Express

Si prefieres SQL Server Express, conecta normalmente a:

```text
.\SQLEXPRESS
```

Ejecuta el mismo script SQL y cambia la cadena de conexion del servicio WCF en:

```text
src/GrupoDigitalBank.ServicioWCF/Web.config
```

Cadena recomendada:

```xml
<add name="GrupoDigitalBankDb"
     connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=GrupoDigitalBankDb;Integrated Security=True;MultipleActiveResultSets=True"
     providerName="System.Data.SqlClient" />
```

## 5. Abrir y ejecutar la solucion

1. Abre `PruebaTecnicaGrupoDigitalBank.sln`.
2. Compila la solucion completa.
3. Clic derecho sobre la solucion, selecciona **Configure Startup Projects**.
4. Selecciona **Multiple startup projects**.
5. Marca como **Start**:
   - `GrupoDigitalBank.ServicioWCF`
   - `GrupoDigitalBank.Web`
6. Ejecuta con IIS Express.

URLs esperadas:

```text
Servicio WCF: http://localhost:50901/UsuarioServicio.svc
Web:          http://localhost:50900/Usuario.aspx
```

Si Visual Studio asigna puertos diferentes, actualiza el endpoint en:

```text
src/GrupoDigitalBank.Web/Web.config
```

## 6. Validacion rapida

1. Abre la pagina `Usuario`.
2. Registra un usuario.
3. Abre `Usuario consulta`.
4. Modifica el usuario.
5. Elimina el usuario.

Si los pasos funcionan, el flujo Web -> WCF -> SQL Server esta correctamente configurado.
