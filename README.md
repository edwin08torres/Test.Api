# AutosChallenge API

Prueba técnica para Backend Developer C# — API REST con .NET, Entity Framework Core, PostgreSQL, XUnit y Docker Compose.

## Tecnologías utilizadas

- .NET 9
- Entity Framework Core (Npgsql para PostgreSQL)
- PostgreSQL 15
- XUnit + EF Core InMemory (para pruebas unitarias)
- Docker / Docker Compose

## Estructura del proyecto

- **AutosChallenge.Api**: Capa de presentación (Controllers, Program.cs, configuración).
- **AutosChallenge.Core**: Entidades del dominio (ej. `MarcaAuto`).
- **AutosChallenge.Infrastructure**: DbContext, Migrations y configuración de EF Core.
- **AutosChallenge.Tests**: Pruebas unitarias con XUnit y base de datos en memoria.

## Cómo levantar el proyecto

### Opción 1: Docker Compose (recomendado)

```bash
docker-compose up -d --build
```

Esto levanta dos servicios:
- `db`: PostgreSQL 15 en el puerto 5432
- `api`: la API REST en el puerto 8080

La API se conecta automáticamente al contenedor de base de datos usando el `docker-compose.yml` incluido.

> **Nota:** El archivo `docker-compose.yml` fue diseñado y validado en su estructura. Durante el desarrollo local se presentaron limitaciones puntuales de red al hacer `pull` de las imágenes desde el registry de Docker (error de conexión intermitente, no relacionado con la configuración del proyecto). El archivo está listo para levantar ambos servicios en un entorno con conectividad estable.

### Opción 2: Entorno local (sin Docker)

1. Instalar PostgreSQL 15 en la máquina local.
2. Crear la base de datos:
```sql
   CREATE DATABASE "AutosDb";
```
3. Ajustar el `appsettings.json` en `AutosChallenge.Api` con las credenciales correspondientes:
```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=AutosDb;Username=postgres;Password=TU_PASSWORD"
   }
```
4. Aplicar las migraciones:
```bash
   dotnet ef database update --project AutosChallenge.Infrastructure --startup-project AutosChallenge.Api
```
5. Correr la API:
```bash
   dotnet run --project AutosChallenge.Api
```

## Endpoints

### GET /api/MarcasAutos

Devuelve todas las marcas de autos registradas en la base de datos.

**Respuesta de ejemplo:**
```json
[
  { "id": 1, "nombre": "Toyota" },
  { "id": 2, "nombre": "Ford" },
  { "id": 3, "nombre": "Volkswagen" }
]
```

Documentación interactiva disponible en Swagger: `http://localhost:5219/swagger` (ambiente Development).

## Migraciones y Data Seed

El proyecto incluye una migración inicial (`InitialCreate`) que genera la tabla `MarcasAutos`, junto con un mecanismo de seed que carga automáticamente 3 registros iniciales: Toyota, Ford y Volkswagen.

## Pruebas unitarias

El proyecto `AutosChallenge.Tests` contiene pruebas unitarias con XUnit que validan el comportamiento del endpoint de `MarcasAutosController`, utilizando una base de datos en memoria (EF Core InMemory) para aislar las pruebas de la base de datos real.

Para ejecutar las pruebas:

```bash
dotnet test
```

Para generar un reporte de cobertura de código:

```bash
dotnet test --collect:"XPlat Code Coverage"
```

## Notas adicionales

- Las credenciales incluidas en `appsettings.json` son únicamente para efectos de esta prueba técnica. En un entorno de producción real, se recomienda el uso de variables de entorno o Azure Key Vault / User Secrets para el manejo de credenciales sensibles.
- El código incluye comentarios explicativos en las secciones clave de la lógica de negocio.
