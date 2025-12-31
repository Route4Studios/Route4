# Route4-MoviePlug Architecture Documentation

## Project Overview

**Route4-MoviePlug** is a multi-tenant SaaS CMS platform designed to manage entertainment client projects. The platform enables studios to create and manage exclusive VIP membership splash pages for their films. The first client is **Making of MARY**, a feature film.

- **Company**: Route4 Studios
- **Product**: Route4-MoviePlug (SaaS Platform)
- **First Client**: Making of MARY
- **Status**: Feature 1 Complete (Public Splash Page with Cover Image)

---

## System Architecture

### High-Level Overview

```
┌─────────────────────────────────────────────────────────────┐
│                     User Browser                             │
│              http://localhost:4200                           │
└────────────────────┬────────────────────────────────────────┘
                     │ HTTP Requests
                     ↓
┌─────────────────────────────────────────────────────────────┐
│         Angular 19 Frontend (SSR Enabled)                    │
│    Port 4200 (Development) / Port 3000 (Production)         │
│  - SplashPageComponent (loads client data from API)         │
│  - AdminDashboard (lazy-loaded protected routes)            │
│  - Client/Splash Management Components                       │
│  - Cinematic gradient theme with responsive design          │
└────────────────────┬────────────────────────────────────────┘
                     │ /api/* requests (via proxy)
                     ↓
┌─────────────────────────────────────────────────────────────┐
│        .NET 9 Web API (ASP.NET Core)                        │
│            Port 5158 (Development)                           │
│  - HealthController (root endpoint)                         │
│  - ClientsController (client management)                    │
│  - VipMembershipController (splash pages)                   │
│  - TenantResolutionMiddleware (multi-tenancy)               │
└────────────────────┬────────────────────────────────────────┘
                     │
                     ↓
┌─────────────────────────────────────────────────────────────┐
│  Entity Framework Core 9 (In-Memory Database)               │
│  - Client table (name, slug, created_at)                    │
│  - SplashPage table (title, subtitle, description, etc.)    │
│  - Benefit table (icon, title, description, display_order)  │
│                                                              │
│  Seed Data: Making of MARY client + 1 splash page + 4 benefits
└─────────────────────────────────────────────────────────────┘
```

---

## Technology Stack

### Backend
- **Framework**: ASP.NET Core 9 Web API
- **ORM**: Entity Framework Core 9
- **Database**: In-Memory (development), SQL Server (migration planned)
- **Language**: C#
- **Features**: CORS, Multi-tenant Middleware, Seed Data

### Frontend
- **Framework**: Angular 19
- **Rendering**: Server-Side Rendering (SSR) enabled
- **Routing**: Standalone components with lazy-loaded modules
- **HTTP**: HttpClient with fetch API
- **Styling**: SCSS with responsive breakpoints
- **Language**: TypeScript 5.7

### DevTools
- **Build Tool**: Angular CLI / Vite (esbuild)
- **Package Manager**: npm
- **Development Server**: ng serve (port 4200)

---

## Backend Architecture

### Project Structure
```
Route4MoviePlug.Api/
├── Controllers/
│   ├── HealthController.cs          # API health check endpoint
│   ├── ClientsController.cs         # Client CRUD operations
│   └── VipMembershipController.cs   # Splash page management (named SplashPageController in routing)
├── Data/
│   └── Route4DbContext.cs           # EF Core DbContext with seed data
├── Models/
│   └── Models.cs                    # Domain models and DTOs
├── Middleware/
│   └── TenantResolutionMiddleware.cs # Multi-tenancy support
├── Program.cs                        # Startup configuration
└── Route4MoviePlug.Api.csproj       # Project file
```

### Key Components

#### 1. **HealthController** (`GET /`)
Returns API metadata and status.
```json
{
  "service": "Route4-MoviePlug API",
  "status": "Running",
  "version": "1.0.0",
  "endpoints": {
    "clients": "/api/clients",
    "clientById": "/api/clients/{slug}",
    "splashPage": "/api/clients/{clientSlug}/splashpage"
  }
}
```

#### 2. **ClientsController**
Manages studio clients with CRUD operations.

**Endpoints:**
- `GET /api/clients` - List all clients
- `GET /api/clients/{slug}` - Get specific client by slug
- `POST /api/clients` - Create new client
- `PUT /api/clients/{slug}` - Update client (future)
- `DELETE /api/clients/{slug}` - Delete client (future)

#### 3. **VipMembershipController** (SplashPageController)
Manages splash pages for VIP membership campaigns.

**Endpoints:**
- `GET /api/clients/{clientSlug}/splashpage` - Get active splash page with benefits
- `POST /api/clients/{clientSlug}/splashpage` - Create new splash page
- `PUT /api/clients/{clientSlug}/splashpage` - Publish splash page (unpublishes others)

**Data Structure:**
```csharp
public class Client {
  public Guid Id;
  public string Name;           // "Making of MARY"
  public string Slug;           // "making-of-mary"
  public ICollection<SplashPage> SplashPages;
}

public class SplashPage {
  public Guid Id;
  public string Title;
  public string Subtitle;
  public string Description;
  public bool IsPublished;
  public DateTime CreatedAt;
  public DateTime UpdatedAt;
  public ICollection<Benefit> Benefits;
}

public class Benefit {
  public Guid Id;
  public string Icon;           // Emoji (🎬, 📝, 🎥, 💬)
  public string Title;
  public string Description;
  public int DisplayOrder;
}
```

#### 4. **TenantResolutionMiddleware**
Identifies multi-tenant context using three strategies (priority order):
1. **Header**: `X-Tenant-Id` header
2. **Subdomain**: `{tenant}.localhost:5158`
3. **Path**: `/clients/{slug}`

Stores tenant ID in `HttpContext.Items["TenantId"]` for downstream processing.

#### 5. **Route4DbContext**
Entity Framework Core DbContext with seed data:
- **Making of MARY Client**: ID `11111111-1111-1111-1111-111111111111`
- **Splash Page**: Title "MAKING OF MARY - VIP Experience", subtitle "Join the exclusive community"
- **4 Benefits**: 🎬 Behind-the-Scenes, 📝 Early Script Access, 🎥 Director's Commentary, 💬 Community Chat

---

## Frontend Architecture

### Project Structure
```
ClientApp/
├── src/
│   ├── app/
│   │   ├── app.routes.ts                    # Main routing configuration
│   │   ├── app.html                         # Root template (router-outlet only)
│   │   ├── app.config.ts                    # App configuration with providers
│   │   ├── pages/
│   │   │   ├── splash-page/
│   │   │   │   ├── splash-page.component.ts      # Loads client splash page from API
│   │   │   │   ├── splash-page.component.html    # Dynamic template with *ngIf
│   │   │   │   └── splash-page.component.scss    # Cinematic styling
│   │   │   └── admin/
│   │   │       ├── admin.routes.ts               # Admin sub-routes
│   │   │       ├── admin-dashboard/              # Dashboard component
│   │   │       ├── clients/                      # Client management
│   │   │       └── splash-editor/                # Splash page editor
│   │   └── styles.scss                      # Global styles
│   ├── main.ts                              # Application bootstrap
│   ├── main.server.ts                       # SSR server entry point
│   └── server.ts                            # SSR server configuration
├── public/
│   ├── assets/
│   │   └── clients/
│   │       └── making-of-mary/
│   │           └── cover.jpg                # Client cover image
│   └── favicon.ico
├── proxy.conf.json                          # Development proxy config
├── angular.json                             # Angular CLI configuration
├── tsconfig.app.json                        # TypeScript configuration
└── package.json                             # Dependencies
```

### Routing Structure

**Public Routes:**
- `/` → SplashPageComponent (public splash page)
- `/splash` → SplashPageComponent (alternative route)

**Admin Routes (Lazy-Loaded):**
- `/admin` → AdminDashboard
- `/admin/clients` → ClientsList
- `/admin/clients/:id/splash` → SplashEditor

**Fallback:**
- `**` → Redirect to `/` (wildcard catches all unknown routes)

### Key Components

#### 1. **SplashPageComponent**
Displays the public VIP membership splash page with dynamic content from API.

**Features:**
- Fetches splash page data from `/api/clients/{clientSlug}/splashpage`
- Displays title, subtitle, description
- Shows benefits with icons and descriptions
- Responsive grid layout with hover effects
- Loading and error states

**Template:**
- Header with navigation links
- Hero section with cover image and content
- Benefits grid section
- CTA button for "Join VIP Access"
- Footer

#### 2. **AdminDashboard**
Entry point for admin UI with quick-action cards.

**Features:**
- "Manage Clients" card → links to `/admin/clients`
- "Edit Splash Page" card → links to splash editor for Making of MARY
- Dark header (#1a1a2e) with responsive layout

#### 3. **ClientsList** (Placeholder)
Future implementation for managing all clients.

#### 4. **SplashEditor** (Placeholder)
Future implementation for editing splash page content and publishing.

### Styling System

**Color Scheme:**
- Primary: `#1a1a2e` (Dark blue/black)
- Accent: `#e94560` (Red/pink)
- Gradients: Orange → Red → Pink → Purple (cinematic effect)

**Responsive Design:**
- Desktop: Full grid layout
- Tablet: Adjusted spacing and font sizes
- Mobile (< 768px): Single column layout, stacked components

**Special Effects:**
- Gradient text on titles
- Glass-morphism effects on cards
- Hover animations on images and buttons
- Box shadows for depth
- Transition animations on interactive elements

---

## Development Workflow

### Running the Application

**Terminal 1 - Backend:**
```powershell
dotnet run --project Route4MoviePlug.Api.csproj
# Listens on http://localhost:5158
```

**Terminal 2 - Frontend:**
```powershell
cd ClientApp
ng serve --port 4200
# Listens on http://localhost:4200
```

**Access the Application:**
- Frontend: http://localhost:4200
- Backend API: http://localhost:5158
- Health Check: http://localhost:5158/

### Development Features

- **Hot Reload**: Frontend auto-recompiles on file changes
- **Proxy Configuration**: `/api` requests routed to backend automatically
- **SSR Enabled**: Server-side rendering for better performance and SEO
- **In-Memory Database**: Seeded with Making of MARY data on startup
- **CORS Enabled**: Frontend can communicate with backend

---

## Multi-Tenancy Implementation

### Tenant Resolution

The system identifies the current tenant through the **TenantResolutionMiddleware**:

1. **Request arrives** → Middleware extracts tenant identifier
2. **Three strategies** (checked in order):
   - Header: `X-Tenant-Id: {tenant-id}`
   - Subdomain: `making-of-mary.localhost:5158`
   - Path: `/clients/making-of-mary/splashpage`
3. **Tenant stored** in `HttpContext.Items["TenantId"]`
4. **Controllers use** tenant context to scope data queries

### Current Implementation

- **Single Tenant Setup**: Making of MARY client hardcoded in seed data
- **Multi-Tenant Ready**: Middleware supports multiple clients
- **Future Enhancements**: 
  - Tenant isolation in database queries
  - Per-tenant authentication
  - Tenant-specific configurations

---

## API Endpoints Reference

### Health Check
```
GET http://localhost:5158/
Response: API metadata and available endpoints
```

### Clients API
```
GET http://localhost:5158/api/clients
Response: List of all clients

GET http://localhost:5158/api/clients/making-of-mary
Response: Making of MARY client details

POST http://localhost:5158/api/clients
Body: { "name": "New Client", "slug": "new-client" }
Response: Created client
```

### Splash Pages API
```
GET http://localhost:5158/api/clients/making-of-mary/splashpage
Response: {
  "title": "MAKING OF MARY - VIP Experience",
  "subtitle": "Join the exclusive community",
  "description": "...",
  "isPublished": true,
  "benefits": [
    { "icon": "🎬", "title": "Behind-the-Scenes", "description": "...", "displayOrder": 1 },
    { "icon": "📝", "title": "Early Script Access", "description": "...", "displayOrder": 2 },
    { "icon": "🎥", "title": "Director's Commentary", "description": "...", "displayOrder": 3 },
    { "icon": "💬", "title": "Community Chat", "description": "...", "displayOrder": 4 }
  ]
}

POST http://localhost:5158/api/clients/making-of-mary/splashpage
Body: Splash page data
Response: Created splash page

PUT http://localhost:5158/api/clients/making-of-mary/splashpage
Body: Splash page data
Response: Publishes splash page (unpublishes others)
```

---

## Asset Structure

### Client Assets
```
ClientApp/public/assets/clients/
└── making-of-mary/
    ├── cover.jpg              # Splash page cover image
    └── cover.svg              # Fallback SVG placeholder
```

**Asset Serving:**
- Public folder is served statically by Angular
- Accessible at: `http://localhost:4200/assets/clients/{clientSlug}/{filename}`
- Used in splash page component: `<img src="assets/clients/making-of-mary/cover.jpg">`

---

## Feature Status

### ✅ Completed (Feature 1)
- [x] Multi-tenant architecture with middleware
- [x] Client management (CRUD structure)
- [x] Splash page data model with benefits
- [x] Public splash page display component
- [x] API endpoints for clients and splash pages
- [x] Cover image display on splash page
- [x] Cinematic styling and responsive design
- [x] Angular routing (public + lazy-loaded admin routes)
- [x] Frontend-backend integration via proxy
- [x] In-memory database with seed data
- [x] SSR configuration
- [x] HttpClient with fetch API

### 🔄 In Progress
- [ ] Admin dashboard completion
- [ ] Client management UI
- [ ] Splash page editor with form controls
- [ ] Image upload functionality

### 📋 Planned
- [ ] Authentication/authorization guards
- [ ] Database migration to SQL Server
- [ ] User membership registration flow
- [ ] Email notifications
- [ ] Analytics and reporting
- [ ] Production deployment

---

## Deployment Considerations

### Development
- Frontend: `ng serve --port 4200`
- Backend: `dotnet run`
- Database: In-memory (EF Core)

### Production (Planned)
- Frontend: Build Angular app and deploy to static hosting (Azure Blob Storage, S3, etc.)
- Backend: Deploy .NET API to Azure App Service or similar
- Database: Migrate to SQL Server
- Assets: Use CDN for cover images and client assets
- SSL/TLS: Enable HTTPS

---

## Project Files

### Key Backend Files
- **Route4MoviePlug.Api.csproj**: Main project file
- **Controllers/HealthController.cs**: Root endpoint
- **Controllers/ClientsController.cs**: Client management
- **Controllers/VipMembershipController.cs**: Splash page management
- **Data/Route4DbContext.cs**: EF Core context and seed data
- **Models/Models.cs**: Domain models and DTOs
- **Middleware/TenantResolutionMiddleware.cs**: Multi-tenancy logic
- **Program.cs**: Startup and configuration
- **Route4MoviePlug.Api.http**: REST client test file

### Key Frontend Files
- **src/app/app.routes.ts**: Main routing
- **src/app/app.config.ts**: App configuration with providers
- **src/app/pages/splash-page/splash-page.component.ts**: Splash page logic
- **src/app/pages/splash-page/splash-page.component.html**: Splash page template
- **src/app/pages/splash-page/splash-page.component.scss**: Splash page styling
- **src/app/pages/admin/admin.routes.ts**: Admin sub-routes
- **src/app/pages/admin/admin-dashboard/admin-dashboard.component.ts**: Admin dashboard
- **proxy.conf.json**: API proxy configuration
- **angular.json**: Angular CLI configuration
- **ClientApp/public/assets/clients/making-of-mary/cover.jpg**: Client cover image

---

## Conclusion

Route4-MoviePlug is a modern, scalable SaaS platform built with:
- **Enterprise-grade backend** (.NET 9 with EF Core and multi-tenancy)
- **Responsive frontend** (Angular 19 with SSR and cinematic design)
- **Clean architecture** with separated concerns and lazy-loaded components
- **Production-ready** codebase with seed data and comprehensive API

The platform is ready for Feature 1 client demonstration and positioned for rapid feature development and multi-tenant scaling.
