# ChatBlockchain.Api

API de ejemplo: Chat + Blockchain (Proyecto .NET)

Descripción
-----------
Este repositorio contiene una API construida en .NET (probablemente .NET 8/10 según configuración) que implementa un servicio de chat con componentes de autenticación y WebSockets, junto con capas de aplicación, dominio y persistencia (EF Core).

Estructura del repositorio
--------------------------
- `src/ChatBlockchain.Api/` - Proyecto Web API principal (controladores, middleware, configuración).
- `src/ChatBlockchain.Application/` - Lógica de negocio, comandos/queries y DTOs.
- `src/ChatBlockchain.Core/` - Modelos, interfaces y opciones.
- `src/ChatBlockchain.Infraestructure/` - Persistencia (EF Core), repositorios y servicios infra.

Requisitos
----------
- .NET SDK 8.0 o superior (ver `global.json` si aplica).
- SQL Server o proveedor configurado en `appsettings.json`.
- (Opcional) Visual Studio 2022/2023 o VS Code.

Cómo compilar
-------------
En macOS con zsh, desde la raíz del repositorio:

```bash
dotnet build ChatBlockchain.Api.sln
```

Cómo ejecutar
-------------
Desde la carpeta del proyecto API:

```bash
cd src/ChatBlockchain.Api
dotnet run
```

Desde la razi del proyecto:
```bash
dotnet run --project src/ChatBlockchain.Api/ChatBlockchain.Api.csproj --environment Development  
```

La API debería quedar accesible en `https://localhost:5001` o la URL mostrada en la salida.

Migraciones y base de datos (EF Core)
------------------------------------
Este proyecto incluye migraciones en `src/ChatBlockchain.Infraestructure/Migrations`.

Para aplicar migraciones a la base de datos:

```bash
cd src/ChatBlockchain.Infraestructure
dotnet ef database update --project ../ChatBlockchain.Infraestructure --startup-project ../ChatBlockchain.Api
```

Si necesitas crear una nueva migración desde la carpeta raíz:

```bash
dotnet ef migrations add NombreMigracion --project src/ChatBlockchain.Infraestructure --startup-project src/ChatBlockchain.Api
```

Endpoints principales
--------------------
- `POST /auth/login` - Autenticación (controlador `AuthController`).
- `POST /auth/register` - Registrar usuario.
- `GET /users` / `GET /users/{id}` - Endpoints de usuarios (controlador `UserController`).
- WebSockets: middleware `WebSocketMiddleware` y `WebSocketManager` en `src/ChatBlockchain.Api/WebSocketApi`.

Notas de desarrollo
-------------------
- Revisar `appsettings.json` y `appsettings.Development.json` para cadenas de conexión y opciones.
- La dependencia entre proyectos se maneja mediante la solución `ChatBlockchain.Api.sln`.
- Ejecuta las pruebas (si existen) con `dotnet test`.

Contacto
--------
Repositorio: ChatBlockchain.Api
Autor: AugustoSan

---

Archivo generado automáticamente por una herramienta de asistencia. Ajusta y amplía según las necesidades del proyecto.
