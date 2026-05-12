# Guia de entrega

## Checklist antes de enviar

- La solucion abre correctamente en Visual Studio.
- La base de datos se crea ejecutando `database/01_crear_base_datos.sql`.
- El servicio WCF responde en el navegador.
- La pagina `Usuario` permite agregar y modificar.
- La pagina `Usuario consulta` permite consultar y eliminar.
- El README explica como ejecutar el proyecto.
- La documentacion incluye arquitectura y pruebas.

## Preparar repositorio Git

Si el directorio todavia no es repositorio Git:

```powershell
git init
git add .
git commit -m "Implementa prueba tecnica .NET por capas"
```

Crear un repositorio en GitHub y asociarlo:

```powershell
git branch -M main
git remote add origin https://github.com/TU_USUARIO/prueba-tecnica-grupo-digital-bank.git
git push -u origin main
```

## Mensaje sugerido para el correo

```text
Cordial saludo,

Comparto la prueba tecnica para el cargo de Desarrollador .NET.

Repositorio:
https://github.com/TU_USUARIO/prueba-tecnica-grupo-digital-bank

La solucion fue desarrollada con ASP.NET Web Forms, WCF, .NET Framework 4.8 y SQL Server. Incluye script de base de datos, procedimiento almacenado CRUD, documentacion de arquitectura, guia de configuracion y plan de pruebas.

Quedo atento a sus comentarios.

Muchas gracias.
```
