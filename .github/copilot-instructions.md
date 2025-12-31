## Route4-MoviePlug - Client Project Management CMS

**Project Overview:**
Multi-tenant SaaS CMS platform by Route4 Studios to manage client entertainment projects. Making of MARY is the first client.

**Architecture:**
- Backend: .NET 9 Web API with EF Core (In-Memory DB)
- Frontend: Angular 19 with SSR
- Multi-tenant: Middleware for tenant resolution (subdomain/header/path)

**Key Components:**
- ClientsController: Manage studio clients
- SplashPageController: CMS for client splash pages
- TenantResolutionMiddleware: Multi-tenancy support
- Route4DbContext: EF Core data layer

**Current Status:**
- ✅ Multi-tenant architecture implemented
- ✅ API endpoints for clients and splash pages
- ✅ Angular VIP membership page for Making of MARY
- ✅ Middleware pipeline configured
- ✅ In-memory database with seed data

**Next Steps:**
- Migrate to SQL Server database
- Build admin dashboard
- Add authentication/authorization
- Implement media management
