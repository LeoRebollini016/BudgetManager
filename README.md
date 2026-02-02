# BudgetManager

**Proyecto:** BudgetManager
**Autor:** Leonel Rebollini

## Resumen
Aplicación para gestionar finanzas personales desarrollada con .NET 9 y Razor Pages. Este proyecto muestra diseño por capas, uso de Identity para autenticación, Entity Framework Core y Dapper (ORM) para persistencia, consultas optimizadas buenas prácticas en estructura y migraciones.

## Características principales
- Registro e inicio de sesión con `ASP.NET Core Identity` (usuarios con claves `Guid`).
- Gestión de cuentas y tipos de cuenta (crear, listar, editar, eliminar).
- Registro y listado de transacciones (ingresos/gastos) por cuenta.
- Validaciones en el frontend (tag helpers) y backend (DataAnnotations).
- Arquitectura organizada en capas: `Domain`, `Application`, `Infraestructure`, `Web`.

## Tecnologías
- .NET 9 (Razor Pages / ASP.NET Core)
- C# 13
- Entity Framework Core / Dapper (ORM)
- ASP.NET Core Identity (con `ApplicationUser : IdentityUser<Guid>`)
- SQL Server / PostgreSQL (configurable) / SQLite (configurable)

## Estructura del repositorio (resumen)
- `BudgetManager` — proyecto web (UI/runtime)
- `BudgetManager.Application` — lógica de aplicación, handlers (MediatR)
- `BudgetManager.Domain` — entidades y DTOs
- `BudgetManager.Infraestructure` — DbContexts, Identity, implementaciones de persistencia
- `BudgetManager.Bootstrap` — configuración de arranque (dependencias)

## Preparar el entorno (local)
1. Clona el repositorio y sitúate en la carpeta raíz:

```bash
git clone https://github.com/LeoRebollini016/BudgetManager.git
cd BudgetManager
```

2. Configura la cadena de conexión en `BudgetManager/appsettings.json` (o variables de entorno). Ejemplo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BudgetManagerDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

3. Asegúrate de tener `dotnet-ef` y los paquetes necesarios:

```bash
dotnet tool install --global dotnet-ef
dotnet add BudgetManager.Infraestructure package Microsoft.EntityFrameworkCore.Design
dotnet add BudgetManager.Infraestructure package Microsoft.EntityFrameworkCore.SqlServer
```

> Si usas otro proveedor (Postgres, SQLite) cambia el paquete y la llamada `Use...` en `Program.cs`.

## Crear y aplicar migraciones (UserIdentityDbContext)
Desde la raíz del repositorio ejecuta:

```bash
# Crear la migración
dotnet ef migrations add InitialIdentity --project BudgetManager.Infraestructure --startup-project BudgetManager --context UserIdentityDbContext

# Aplicar la migración a la base de datos
dotnet ef database update --project BudgetManager.Infraestructure --startup-project BudgetManager --context UserIdentityDbContext
```

Si por error aplicaste una migración y quieres deshacerla:

```bash
# Revertir a migración anterior (sustituir NombreMigracionAnterior)
dotnet ef database update <NombreMigracionAnterior> --project BudgetManager.Infraestructure --startup-project BudgetManager --context UserIdentityDbContext

# O dejar la base vacía
dotnet ef database update 0 --project BudgetManager.Infraestructure --startup-project BudgetManager --context UserIdentityDbContext

# Eliminar la última migración creada (archivos y snapshot)
dotnet ef migrations remove --project BudgetManager.Infraestructure --startup-project BudgetManager --context UserIdentityDbContext
```

> Antes de aplicar o revertir migraciones en bases de datos con datos reales, realiza un backup.

## Ejecutar la aplicación

```bash
dotnet run --project BudgetManager
```

Abre el navegador en `https://localhost:5001` o la URL que muestre la consola.

## Uso (demostración rápida)
- Registro: Navega a `/User/Register` y crea un usuario.
- Login: `/User/Login`.
- Después del login podrás crear cuentas, tipos de cuenta y agregar transacciones.

Si prefieres probar con datos de ejemplo, puedes registrarte desde la UI o agregar un seed en `Program.cs` (opcional).

### Arquitectura SOLID
- **Single Responsibility** - Cada clase tiene una responsabilidad única
- **Open/Closed** - Extensible sin modificación
- **Liskov Substitution** - Interfaces implementadas correctamente
- **Interface Segregation** - Interfaces específicas y cohesivas
- **Dependency Inversion** - Dependencias hacia abstracciones

### ✅ Calidad de Código y Testing
- **Consultas Optimizadas:** Uso de **Dapper** para reportes complejos, con consultas SQL centralizadas en clases estáticas para mejorar el rendimiento y la legibilidad.
- **Unit Testing:** Cobertura de la capa de aplicación utilizando `xUnit`, `Moq` y `FluentAssertions`.
- **Pruebas Parametrizadas:** Uso intensivo de `[Theory]` e `[InlineData]` para validar múltiples escenarios de negocio (límites de saldo, rangos de fechas, cálculos de reportes) con código eficiente.
- **Arquitectura de Pruebas:** Implementación de `TestBuilders` (Object Mother pattern) para la creación de datos de prueba consistentes y mantenibles.
- **Centralización de consultas SQL** en clases `*Queries`
- **Manejo de conexiones** con `using` statements
- **Validaciones robustas**

<img width="524" height="564" alt="TestUnitarios" src="https://github.com/user-attachments/assets/68568ffe-9236-44e6-8d4e-0d6194ee1563" />

### Experiencia de Usuario
- Interfaz responsive con Bootstrap
- Validaciones cliente y servidor
- Navegación intuitiva 
- Mensajes de error descriptivos

## 📊 Funcionalidades Principales

- **Gestión de Cuentas** - Creación y administración de cuentas financieras
- **Categorización** - Organización de ingresos y gastos
- **Registro de Transacciones** - Control detallado de movimientos
- **Reportes** - Análisis financiero y visualización de datos

## Futuras mejoras
- Testing y CI: agregar pruebas unitarias/integración con xUnit/Moq.

## Contacto
- Leonel Rebollini — `leorebollini@gmail.com` — https://github.com/LeoRebollini016

---
