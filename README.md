# Route4-MoviePlug - Client Project Management CMS

A multi-tenant SaaS platform by Route4 Studios to manage and document client entertainment journeys. Built with .NET 9 and Angular 19.

## Overview

Route4-MoviePlug is Route4 Studios' production management platform for cinematic movie projects destined for streaming platforms. This application serves as the engine to manage and document each client's entertainment journey from development through production.

### First Client: Making of MARY
Making of MARY is the inaugural client project, featuring a VIP membership splash page that provides exclusive behind-the-scenes access to the film-making process.

## Architecture

### Backend (.NET 9 Web API)
- **Multi-tenant architecture** with tenant resolution middleware
- **Entity Framework Core** with In-Memory database (ready for SQL Server)
- **RESTful API** for managing clients and splash pages
- **Middleware pipeline** for tenant identification via subdomain, header, or path

### Frontend (Angular 19)
- **Standalone components** with modern Angular architecture
- **SCSS styling** with cinematic theming
- **Responsive design** for all devices
- **Server-Side Rendering (SSR)** enabled

## Project Structure

```
route4-movieplug/
├── Controllers/
│   ├── ClientsController.cs          # Manage studio clients
│   └── SplashPageController.cs        # Manage client splash pages
├── Data/
│   └── Route4DbContext.cs             # EF Core database context
├── Middleware/
│   └── TenantResolutionMiddleware.cs  # Multi-tenancy support
├── Models/
│   └── Models.cs                      # Domain models & DTOs
├── ClientApp/                         # Angular frontend
│   └── src/
│       └── app/
│           ├── app.html               # VIP splash page template
│           ├── app.scss               # Cinematic styling
│           └── app.ts                 # Component logic
└── Program.cs                         # API startup configuration
```

## Features

### Current Features
- ✅ Multi-tenant client management
- ✅ Splash page CMS with benefits/features
- ✅ Tenant resolution via subdomain/header/path
- ✅ In-memory database with seed data
- ✅ CORS-enabled API for Angular
- ✅ Responsive VIP membership page
- ✅ Modern gradient & cinematic design

### Roadmap
- [ ] SQL Server database migration
- [ ] Admin dashboard for managing clients
- [ ] Rich text editor for splash page content
- [ ] Media upload and management
- [ ] User authentication & authorization
- [ ] Email notification system
- [ ] Analytics and tracking

## Getting Started

### Prerequisites
- .NET 9 SDK
- Node.js 18+ and npm
- Angular CLI (`npm install -g @angular/cli`)

### Running the Application

#### 1. Start the Backend API
```powershell
dotnet run --project Route4MoviePlug.Api.csproj
```
API will run on: http://localhost:5158

#### 2. Start the Angular Frontend
```powershell
cd ClientApp
npm install
npm start
```
Frontend will run on: http://localhost:4200

### API Endpoints

**Clients**
- `GET /api/clients` - List all active clients
- `GET /api/clients/{slug}` - Get client by slug
- `POST /api/clients` - Create new client

**Splash Pages**
- `GET /api/clients/{clientSlug}/splashpage` - Get published splash page
- `POST /api/clients/{clientSlug}/splashpage` - Create splash page
- `PUT /api/clients/{clientSlug}/splashpage/{id}/publish` - Publish splash page

### Tenant Resolution

The system supports three methods of tenant identification:

1. **Header**: `X-Tenant-Id: making-of-mary`
2. **Subdomain**: `making-of-mary.route4studios.com`
3. **Path**: `/clients/making-of-mary/splashpage`

## Development Notes

### Database
Currently using **EF Core In-Memory Database** for rapid development. To migrate to SQL Server:

1. Install package: `dotnet add package Microsoft.EntityFrameworkCore.SqlServer`
2. Update `Program.cs` to use SQL Server provider
3. Run migrations: `dotnet ef migrations add InitialCreate`
4. Apply migrations: `dotnet ef database update`

### Seed Data
The application seeds one client (Making of MARY) with a complete splash page on startup.

## Technology Stack

**Backend**
- .NET 9
- ASP.NET Core Web API
- Entity Framework Core 9
- C# 13

**Frontend**
- Angular 19
- TypeScript 5.7
- SCSS
- RxJS

**DevOps**
- VS Code
- Git
- npm/NuGet package management

## Contributing

This is a Route4 Studios internal project. For questions or contributions, contact the development team.

## License

Proprietary - Route4 Studios © 2025

---

**Route4-MoviePlug** - Production management SaaS platform for entertainment projects.
**Making of MARY** - First client showcasing exclusive VIP access to the complete filmmaking journey.