
# Route4 Non-Dependent Release Engine
## Design Document - v1
--

## 1. Executive Summary

**Route4** is a multi-tenant, RdaS, Ritual discipline as service, software that enables creators to release work with restraint, ritual, and trust. It builds audience through documented care, protects authorship, and treats capital as an outcome rather than a driver.

### 1.1 Core Principles
- **Expose decisions, not outcomes**
- **Reveal authorship last**
- **Configuration over customization**
- **State drives behavior, not events**

### 1.2 What Route4 Is NOT
- Not a social network
- Not a marketing tool  
- Not a recruitment platform
- Not a collaboration platform

### 1.3 What Route4 IS
A release engine that orchestrates ritual-based content release cycles while protecting story integrity and building authentic audience relationships.

### 1.4 Competitive Positioning & Defensible Moat

Route4 has minimal direct competitors because its "ritual discipline + story protection" architecture is structurally unique. It solves a problem no other platform addresses.

#### 1.4.1 What Competitors Are Missing

| Platform | Ritual Discipline | Story Protection | Witness Tracking | Care-Based Gates | Why Not a Threat |
|----------|-------------------|------------------|------------------|------------------|-----------------|
| **FilmFreeway** | ❌ | ❌ | ✅ (festivals) | ❌ | No ritual enforcement; transactional |
| **Eventive** | ❌ | ❌ | ❌ | ❌ | Pure aggregation; no community layer |
| **Vimeo OTT** | ❌ | ❌ | ❌ | ❌ | Self-serve only; no guidance |
| **Discord Communities** | ❌ (optional) | ❌ | ✅ (manual) | ❌ | Unstructured; creator has to architect everything |
| **YouTube** | ❌ | ❌ | ✅ (views/subs) | ❌ | Algorithm-driven; story leakage inevitable |
| **Route4** | ✅ | ✅ | ✅ | ✅ | **Complete ritual infrastructure** |

#### 1.4.2 Route4's Defensible Moat

**The problem Route4 solves:**
> "How do I release work with restraint, protect my story, build real audience presence, and charge fairly—without relying on algorithms or giving away my narrative?"

**No other platform answers this question.**

#### 1.4.3 Why Competitors Can't Compete

1. **FilmFreeway can't add ritual discipline**
   - Designed for submission abundance (1000s of festivals)
   - Ritual discipline requires enforced *restraint* (opposite of their model)
   - Their revenue model is submission fees (conflict with care-based gates)

2. **Eventive can't add story protection**
   - Built for aggregation (collect submissions, distribute to festivals)
   - No concept of visibility levels or authorship protection
   - Would require complete architectural redesign

3. **YouTube/Discord can't enforce ritual**
   - Unstructured platforms designed for continuous content
   - No state machine; no enforced release cycle
   - Creators responsible for all discipline (defeats purpose)

4. **DIY creators can build pieces, but not the spine**
   - Use Discord for community (unstructured)
   - Use YouTube for audience (algorithm leak)
   - Use Stripe for gates (manual)
   - Piece together story protection themselves (fragile)
   - Result: 100 hours of integration, zero ritual coherence

#### 1.4.4 Route4's Competitive Advantage

Route4 provides:
- Enforced release cycle (no creator can skip ritual stages)
- Story protection via visibility levels (L0–L3 automatically enforced)
- Witness presence tracking (documented audience without metrics)
- Care-based pricing (charges only at irreversible thresholds)
- Immutable archive (canon sealed, no edits post-Drop)

**Competitors offer tools. Route4 offers structure.**

#### 1.4.5 Lock-In Mechanism

Once a creator completes Route4's rituals and experiences:
- **Restraint discipline** (forced to slow down, think clearly)
- **Story integrity** (no accidental leakage; visibility levels enforced)
- **Witness confidence** (real presence, not metrics)
- **Care-based trust** (charges feel aligned, not extractive)

**They will never want unstructured release again.**

This is Route4's sustainable lock-in: **not platform lock-in, but discipline lock-in.**

---

## 2. System Architecture

### 2.1.a Technology Stack
- **Backend**: .NET 9 Web API with EF Core
- **Frontend**: Angular 19 with SSR
- **Database**: SQL Server (persistent for all environments except tests)
- **Multi-tenancy**: Middleware-based tenant resolution
- **External Integrations**: Discord.NET, Stripe, Frame.io, FFmpeg

## 2.1.b Overall Aesthetic

Route4's visual design embodies restraint, ritual discipline, and decision visibility. Every element serves the release cycle—not engagement, not virality, not metrics.

### Color Theme
```
Primary Navy:    #1a1a2e  (backgrounds, primary UI)
Secondary Navy:  #16213e  (elevated surfaces, cards)
Accent Red:      #e94560  (ritual state transitions only)
Gray Neutral:    #6b7280  (secondary text, disabled states)
White:           #ffffff  (primary text on dark backgrounds)
Accent Gold:     #ffd700 (secondary text on dark backgrounds)
```

### Visual Principles

**Typography:**
- Serif fonts for ritual names and release titles (Merriweather, Georgia)
- Sans-serif for system states and UI elements (Inter, system fonts)
- Line height 1.6+ for body text (breathing room)
- No all-caps except for state labels (DRAFT, PRACTICE, DROP)

**Layout:**
- Minimal density: generous whitespace between sections
- No sidebars, no feeds, no infinite scroll
- Vertical rhythm: consistent spacing units (8px grid)
- Content-first: ritual state drives page structure

**What NOT to Include:**
- ❌ No engagement metrics (likes, shares, views)
- ❌ No CTAs with urgency language ("Sign up now!", "Don't miss out!")
- ❌ No social features (follow buttons, trending sections)
- ❌ No badges or gamification (streaks, achievements)
- ❌ No auto-playing media or animated backgrounds
- ❌ No carousels (discovery violates restraint)

**Interaction Patterns:**
- Buttons use subtle hover states (no dramatic color shifts)
- Transitions are slow and deliberate (300ms+)
- Empty states feel intentional, not lonely
- Loading states are quiet (spinner only, no skeleton screens)

**Ritual State Visibility:**
- Current state always visible (header or persistent nav)
- Next ritual displayed as milestone, not countdown
- Witness participation count (not follower count)
- Red accent appears only for state transitions

**Example UI Elements:**
```
✅ "142 witness participations" (factual presence)
❌ "142 followers!" (social metric)

✅ "Next: DROP (ritual 8 of 12)" (state-driven)
❌ "Going viral! Keep it up!" (engagement-driven)

✅ Subdued button: [Enter PRACTICE] (decision point)
❌ Bright CTA: "START NOW →" (urgency)
```

This aesthetic ensures Route4 feels like a tool for creators, not a platform optimizing for attention.

### 2.1.c Route4 Studios Branding

**Logo & Visual Identity:**

Route4 Studios represents the platform's geographic origin (Ohio) and its role as a curation layer for distributed creator communities.

![Route4 Studios Logo](https://github.com/route4studios/route4-movieplug/raw/main/branding/route4_studios_logo.png)

**Logo Specifications:**
- Shape: Ohio state outline with silver chrome border
- Primary Text: "ROUTE 4" (bold, industrial sans-serif)
- Secondary Text: "STUDIOS" (bold, matching ROUTE 4)
- Accent Color: Red (#e94560) for the numeral "4"
- Background: Dark navy (#1a1a2e)
- File Format: PNG (transparent background, 512x512px minimum)
- File Location: `ClientApp/public/assets/route4_logo.jpeg`

**Usage Guidelines:**
- Minimum size: 48px (header placement, as implemented on homepage)
- Padding: 16px minimum clearspace on all sides
- Opacity: 0.9 (subtle, not aggressive)
- Never scale below 48px (legibility loss)
- Use on dark backgrounds only (navy, dark gray, black)
- No color inversion or manipulation (logo is locked to silver + red)

**Placement:**
- Homepage header (left side, 48px height)
- Email footers (24px height, light background acceptable with opacity adjustment)
- Documentation headers (32px height)
- Never use as site favicon (too detailed, use simplified icon instead)

**Philosophy:**
The Route4 Studios logo reinforces the platform's identity as a curation layer rooted in specific geography (Ohio), not a nebulous "platform" optimizing for global scale. The chrome border conveys craft and precision; the red numeral "4" is the only vibrant element, maintaining restraint while preventing invisibility.

---

### 2.2 Core Components

#### 2.2.1 Three-Layer Architecture
```
Platform Layer     → Release cycles, ritual stages, visibility levels
Client Configuration → Story, timing, ritual names, language, tone  
Execution Layer    → Discord hosts rituals; Angular surfaces content
```

#### 2.2.2 Creator Tiers & Onboarding

Route4 offers two tiers, each designed for different creator needs and long-term value alignment:

**Tier A — Submission Only**
- Creator has completed Route4's rituals
- Achieved witness threshold + revenue signals
- Canon sealed and archive immutable
- Route4 submits to Filmhub (one-time service)
- Creator manages all outreach + platform relationships independently

**Tier B — Managed Partnership (Includes AI Business Manager)**
- Everything in Tier A
- Route4 AI Business Manager: AI-curated outreach campaigns (starting at Gate 0)
- Route4 manages all community targeting + witness recruitment
- Route4 maintains ongoing platform relationships post-distribution
- Quarterly earnings reports + payment processing
- Creator receives revenue share (85% to creator, 15% to Route4)
- Best for creators seeking hands-off distribution + audience building

Both tiers require: completion of full release cycle (DRAFT → ARCHIVE)

### 2.3 System Boundaries
```
Outreach Tools           → Deliver invitations (outside Route4 or via Gate 0 - Tier B)
Route4                   → Receive & structure presence (Gates 1-5)
Discord                  → Host rituals (inside Route4)
Filmhub                  → Multi-platform distribution (automatic, based on config)
```

---

## 3. Visibility Levels & Content Protection

### 3.1 Visibility Level Hierarchy

| Level | Access | Purpose | Examples |
|-------|--------|---------|----------|
| **L0** | Private (Core team only) | Team secrets | Scripts, edits, canon debates |
| **L1** | Witnessed Process | Craft observation | Writing sessions, shot councils |
| **L2** | Fragments (Public) | Residue, no context | BTS clips, anonymous signals |
| **L3** | Revelation (Public) | Full story access | Episodes, credits, reflection |

### 3.2 Content Flow Enforcement
- Platform never owns story
- Client configures everything
- Visibility levels prevent story leakage
- Automated enforcement through state machine

---

## 4. Release Cycle & Ritual System

### 4.1 Master Release State Machine
```
DRAFT → SIGNAL_I → PROCESS → HOLD → SIGNAL_II → DROP → ECHO → ARCHIVE
```

### 4.2 Ritual Definitions

#### 4.2.1 Pre-Release Rituals
1. **Signal I - Casting Call** (L2)
  - Public invitation without story context
  - Establishes tone and legitimacy
  - No character outcomes revealed

2. **Table Read (N)** (L1)
  - Witnessed process ritual
  - Audio recorded for archive
  - Silent witness mode

3. **Writing Table (N)** (L1)
  - Lock story intent
  - Decisions over exposition
  - Craft revealed through discussion

4. **Shot Council (N)** (L1)
  - Visual intention translation
  - Safe stills and masked previews
  - Why shots exist, not what they show

#### 4.2.2 Release Rituals
5. **The Hold**
  - Intentional system-enforced silence
  - Compression before release
  - No explanation or engagement

6. **Signal II - Anonymous Song Drop** (L2)
  - Emotional alignment without authorship
  - Discovery-first listening
  - No credits revealed

7. **The Drop** (L3)
  - Primary release
  - 24-hour read-only window
  - **Canon sealed permanently** (no edits allowed after publication)

**What "Canon Sealed" Means:**
- The creative work is locked in its final, authoritative form
- No revisions, re-edits, or changes permitted after The Drop
- Archive becomes immutable (read-only, versioned)
- Protects witness trust (what they experienced is permanent)
- Required condition for distribution eligibility

This enforces Route4's restraint principle: creators cannot second-guess or "fix" work after audience witnesses it.

#### 4.2.3 Post-Release Rituals
8. **The Echo** (L3)
  - Reflection without spoilers
  - Mixed witness and public discussion
  - Delayed opening

9. **Fragments** (L2)
  - BTS artifacts without context
  - Extended atmosphere
  - No explanations

10. **The Interval** (L3)
   - Meta conversation
   - Post-event podcast/discussion
   - Creative process exploration

11. **Private Viewing** (L3)
   - Witness-only attendance
   - QR-based access
   - No recording, no replay

12. **Archive** (L3)
   - Permanent canon preservation
   - Immutable record
   - Ongoing witness access

---

## 5. Presence Model & Audience Formation

### 5.1 Presence Framework
Presence answers four questions:
- **Who may be present?** (Role-based access)
- **How may they be present?** (Interaction mode)
- **When is presence allowed?** (Ritual windows)
- **What does presence change?** (Status effects)

### 5.2 Witness System
- **Witness** = role, verb, and status
- No leveling or gamification
- Unlocks: Reflection channels, invitations, private viewings, archive access
- Ritual participation recorded without fanfare

### 5.3 Audience Formation Strategy

#### Step 1: Signal I (Casting Call)
- First public-facing action
- Route4 hosts canonical casting call
- External outreach points to Route4
- Short, identical messages across channels

#### Step 2: Intake Without Noise
- Route4 records who arrived, when
- First passive witness registration
- No metrics or promotion

#### Step 3: Documented Table Read
- First ritual with documentation
- Audio minimum, stills if appropriate
- Archives as non-canonical process artifact

---

## 6. Technical Implementation

### 6.1 Database Architecture

#### 6.1.1 Core Entities
```sql
Clients (multi-tenant scope)
├── CastingCalls (Signal I invitations)
├── ReleaseInstances (Episodes + state machine)
├── WitnessEvents (Witness participation tracking)
├── ShortUrls (Trackable invitation links)
└── StateTransitions (Audit trail)
```

#### 6.1.2 Database Requirements
- SQL Server for all environments (except isolated tests)
- Connection strings via appsettings.json
- Environment-specific overrides
- No in-memory databases in production/development

### 6.2 Multi-Tenancy Implementation

#### 6.2.1 Tenant Resolution
```
Request: making-of-mary.localhost:5158
   ↓
TenantResolutionMiddleware extracts "making-of-mary"
   ↓
EF Core filters all queries by Client
   ↓
Returns client-specific data only
```

#### 6.2.2 Tenant Isolation
- Subdomain/header/path-based resolution
- Complete data separation per client
- Shared platform logic, isolated client data

### 6.3 API Design

#### 6.3.1 Controllers & Endpoints
```
ClientsController
├── GET /api/clients/{clientSlug}
├── GET /api/clients/{clientSlug}/castingcall
└── POST /api/clients/{clientSlug}/castingcall

CastingCallController
├── GET /api/clients/{clientSlug}/castingcall/active
└── POST /api/clients/{clientSlug}/castingcall

ShortUrlController
├── POST /api/shorturl/create
└── GET /shorturl/{code}

ReleaseManagementController
VipMembershipController
PaymentsController (Stripe)
Route4DiscordAdminController
HealthController
```

#### 6.3.2 Business Services
```
ReleaseManagementService
├── Release state machine orchestration
└── Ritual transition logic

DiscordBotService (Real/Mock)
├── Channel provisioning
├── Role assignment
└── Lock/unlock automation

StripePaymentService
├── Care-based threshold pricing
└── Gate transitions

MediaProvider (Frame.io abstraction)
├── Asset versioning
└── Visibility enforcement
```

---

## 7. Discord Integration

### 7.1 Discord as Execution Layer
- **Never the front door**
- Route4 decides state, Discord reflects state
- Discord never advances release or unlocks meaning
- Ritual execution surface, not community hub

### 7.2 Permission & Channel Management

#### 7.2.1 Permission Presets
| Mode | Permissions |
|------|-------------|
| **Read-Only** | View messages, no posting |
| **Silent Witness** | View + listen, no chat/reactions |
| **Reflection** | Limited posting, slow mode |
| **Locked** | No access |
| **Admin Only** | Core team only |

#### 7.2.2 Channel Templates
```
Orientation:     #signal, #how-to-witness, #start-here
Releases:        #releases, #soundtrack
Reflection:      #after-the-drop, #interval
Residue:         #fragments
Process:         #writing-table, #shot-council, #cut-room (time-locked)
Private:         #core-team, #canon-drafts (Voltron only)
```

### 7.3 Discord Invite Flow
```
Route4 Intake (No Discord visible)
   ↓
Eligibility Determination (Route4)
   ↓
Ritual Window Opens (Route4 → Discord)
   ↓
Time-limited invite generated
   ↓
Join & Permissions (Discord.NET)
   ↓
Ritual Close (Auto-lock)
```

---

## 8. Frontend Architecture (Angular)

### 8.1 Application Purpose
- Official releases display
- Witness identity & verification
- Archive access
- Invitations & ritual notifications
- Deep links to Discord

### 8.2 Navigation Structure
```
Signal     → Pre-release artifacts
Episodes   → Primary releases
Archive    → Complete history
Voltron    → Core team admin
Profile    → Witness status & invitations
```

### 8.3 Key Pages
- **Splash Page**: Client info, casting call landing
- **VIP Membership**: Client-specific membership
- **Release View**: Episodes, L3 content
- **Archive**: Permanent records
- **Admin Dashboard**: Client configuration
- **Casting Call Editor**: Signal I creation

---

## 9. Media Pipeline & Asset Management

### 9.1 Route4.FFmpeg — Separate Service for Media Processing

#### 9.1.1 Architectural Rationale: Why Separate Project?

**Problem:** Multiple tenants (clients) can trigger FFmpeg format conversion simultaneously. CPU-intensive media processing cannot block the main API request cycle. Route4.Api must remain responsive while format jobs queue and process asynchronously.

**Solution:** Route4.FFmpeg is a **standalone Worker Service** (separate .NET project) that:
- Monitors a shared job queue (Redis or SQL Server)
- Processes format conversion requests independently
- Logs completion status + processing time for billing/audit
- Scales horizontally (multiple FFmpeg workers can run in parallel)
- Isolates CPU load from main API tier

**Architecture Diagram:**
```
Route4.Api                          Route4.FFmpeg (Worker Service)
├── Receives ritual trigger         ├── Polls job queue
├── Creates FFmpeg job record       ├── Dequeues format requests
├── Enqueues to Redis/MSSQL         ├── Runs ffmpeg CLI commands
└── Returns 202 (Accepted)          ├── Logs processing metrics
                                    ├── Updates job status
                                    └── Stores output files to Frame.io
```

**Multi-Tenant Isolation:**
```
Tenant A: Signal → Enqueue Fragment Proxy Job (15 min duration)
Tenant B: Process → Enqueue Process Preview Job (concurrently)
Tenant C: Drop → Enqueue Release Renditions Job (concurrently)
↓
Redis Queue: [Job_A, Job_B, Job_C, ...]
↓
Worker 1: Processing Job_A
Worker 2: Processing Job_B
Worker 3: Processing Job_C
```

#### 9.1.2 Implementation Architecture

**Project Structure:**
```
Route4.FFmpeg/
├── Route4.FFmpeg.Worker/          (Console app, hosted service)
├── Route4.FFmpeg.Models/          (Job models, shared with API)
├── Route4.FFmpeg.Services/
│   ├── IJobQueueService.cs       (Redis/MSSQL abstraction)
│   ├── IFFmpegService.cs         (FFmpeg CLI wrapper)
│   ├── IFrameioUploadService.cs  (Asset storage)
│   └── JobProcessingService.cs   (Main worker logic)
└── appsettings.json              (FFmpeg bin path, queue config)
```

**Shared Models (Route4.FFmpeg.Models):**
```csharp
public class FFmpegJob
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public required string ReleaseKey { get; set; }
    public required string JobType { get; set; } // CreateFragmentProxy, CreateProcessPreview, CreateReleaseRenditions
    public required string InputAssetPath { get; set; } // S3/Frame.io URL
    public string Status { get; set; } = "Queued"; // Queued, Processing, Completed, Failed
    
    // Job metadata
    public Dictionary<string, string>? Parameters { get; set; } // duration, aspect-ratio, etc.
    public string? OutputPath { get; set; } // Where output was stored
    
    // Timing & audit
    public DateTime CreatedAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public long? ProcessingDurationMs { get; set; } // For billing/logging
    public string? ErrorMessage { get; set; }
}
```

#### 9.1.3 Primary Job Types

**1. CreateFragmentProxy (L2 — Public Residue)**
```
Input:  Raw ritual asset (ceremony recording, 60 min)
Process:
  - Auto-trim to 15-90 seconds (detect silence, extract key moment)
  - Optional mute/room-tone audio replacement
  - Safe crop/blur/mask (remove identifiable backgrounds)
  - Multi-aspect outputs (16:9, 9:16, 1:1 for platform distribution)
Output: S1E1__fragment__public__v1.mp4 (various aspects)
Time:   ~3-5 min per job (depending on input duration)
```

**2. CreateProcessPreview (L1 — Witnessed Craft)**
```
Input:  Process ritual recording (writing session, 45 min)
Process:
  - Mute audio (privacy)
  - Low-res encode (480p, 2 Mbps)
  - Burn-in watermark ("PROCESS WITNESS ONLY")
  - Extract stills every 30 seconds
Output: S1E1__process-writing__process_preview__v1.mp4 + stills
Time:   ~2-3 min per job
```

**3. CreateReleaseRenditions (L3 — Public Release)**
```
Input:  Master release file (2160p, ProRes, 10 GB)
Process:
  - Public release encode (1080p, H.264, 8 Mbps)
  - Private master archive (lossless, original quality)
  - Thumbnail sets (poster, hero, grid)
  - Credits roll variants (different languages/aspect ratios)
Output:
  - S1E1__drop__release_public__v1.mp4 (main)
  - S1E1__archive__master__v1.mp4 (private)
  - S1E1__drop__thumbnail__v1.jpg (poster)
Time:   ~15-45 min per job (Master file can be very large)
```

#### 9.1.4 Queue Implementation (Redis or MSSQL)

**Option A: Redis (Recommended for throughput)**
```csharp
// Route4.Api: Enqueue job
IJobQueueService queue = new RedisJobQueueService(connectionString);
var job = new FFmpegJob {
    Id = Guid.NewGuid(),
    ClientId = client.Id,
    ReleaseKey = "S1E1",
    JobType = "CreateFragmentProxy",
    InputAssetPath = "s3://bucket/route4-makingofmary/S1E1_raw.mp4",
    Parameters = new() { { "duration", "30" }, { "aspect-ratio", "16:9" } },
    CreatedAt = DateTime.UtcNow,
};
await queue.EnqueueAsync(job);
// Returns immediately to user (202 Accepted)

// Route4.FFmpeg Worker: Dequeue and process
while (true)
{
    var job = await queue.DequeueAsync(timeoutMs: 5000);
    if (job == null) continue;
    
    job.Status = "Processing";
    job.StartedAt = DateTime.UtcNow;
    await queue.UpdateAsync(job);
    
    try
    {
        var output = await ffmpegService.ProcessAsync(job);
        await frameioUploadService.UploadAsync(output);
        
        job.Status = "Completed";
        job.OutputPath = output.S3Path;
        job.ProcessingDurationMs = (long)(DateTime.UtcNow - job.StartedAt).TotalMilliseconds;
    }
    catch (Exception ex)
    {
        job.Status = "Failed";
        job.ErrorMessage = ex.Message;
    }
    finally
    {
        job.CompletedAt = DateTime.UtcNow;
        await queue.UpdateAsync(job);
    }
}
```

**Option B: MSSQL (If Redis unavailable)**
- Use Service Bus or simple polling with `WHERE Status = 'Queued'` LIMIT 1
- Slower throughput but no external dependency

#### 9.1.5 Status Tracking & Logging

**API Endpoint for Job Status:**
```
GET /api/clients/{clientSlug}/releases/{releaseKey}/ffmpeg-jobs
Response: List<FFmpegJob> with Status + ProcessingDurationMs

GET /api/clients/{clientSlug}/releases/{releaseKey}/ffmpeg-jobs/{jobId}
Response: Single job with detailed metrics
```

**Logging For Billing & Audit:**
```
[2026-01-05 14:32:15.123] FFmpegJob: CREATE_FRAGMENT_PROXY
  ClientId: {making-of-mary}
  ReleaseKey: S1E1
  InputSize: 2.4 GB
  Status: Queued → Processing (14:32:20) → Completed (14:35:45)
  ProcessingDurationMs: 205000 (3 min 25 sec)
  OutputSize: 45 MB (16:9) + 52 MB (9:16) + 38 MB (1:1)
  Cost Impact: Allocated to Gate 2 ($10–$50 range)
```

**Metrics to Track:**
- Time from enqueue → dequeue (queue wait time)
- Processing duration (actual FFmpeg execution)
- Input size vs output size (compression efficiency)
- Success/failure rate per job type
- Resource utilization (CPU, disk I/O)

#### 9.1.6 Scaling & Reliability

**Horizontal Scaling:**
```
Production Setup:
├── Route4.Api (Main tier)
│   └── 3 instances (web farm)
├── Route4.FFmpeg.Worker (Processing tier)
│   └── 2-5 instances (auto-scale based on queue depth)
└── Shared Infrastructure
    ├── Redis (job queue)
    ├── Frame.io (asset storage)
    └── MSSQL (job history + metrics)
```

**Resilience Patterns:**
- Idempotent job IDs (can retry same job safely)
- Dead-letter queue (jobs failing 3x moved to manual review)
- Heartbeat monitoring (workers send "alive" signal every 30 sec)
- Job timeout (if processing > 1 hour, kill and retry)

#### 9.1.7 FFmpeg Configuration (appsettings.json)

```json
{
  "FFmpeg": {
    "BinaryPath": "C:\\ffmpeg\\bin\\ffmpeg.exe",
    "TimeoutSeconds": 3600,
    "TempWorkingDirectory": "C:\\route4-ffmpeg-temp"
  },
  "JobQueue": {
    "Provider": "Redis",
    "ConnectionString": "localhost:6379"
  },
  "Frameio": {
    "ApiKey": "{{ secrets }}",
    "ProjectId": "route4-releases"
  }
}
```

---

### 9.2 Media Provider Integration (Frame.io)

#### 9.2.1 Folder Structure
```
route4-{clientSlug}/
├── {releaseKey}/
│   ├── 00-raw/          (L0)
│   ├── 10-process/      (L1)
│   ├── 20-fragments/    (L2)
│   ├── 30-release/      (L3)
│   └── 40-archive/      (L3)
```

#### 9.2.2 Asset Naming Convention
`{releaseKey}__{stage}__{kind}__v{n}.{ext}`

Examples:
- `S1E1__process-writing__process_preview__v1.mp4`
- `S1E1__drop__release_public__v1.mp4`

---

## 10. Business Model & Pricing

### 10.1 Care-Based Threshold Pricing Philosophy

Route4 uses a **care-based threshold model**. Creators are never charged for intent, planning, or uncertainty. Charges occur **only when a creator crosses an irreversible threshold** — when Route4 must protect meaning, allocate resources, or create permanence.

**Core Principle:** If Route4 hasn't done irreversible work or protected something of value, the creator hasn't paid.

### 10.2 Complete Ritual Pricing Breakdown

| Ritual | Stage | What Happens | Chargeable? | Why / Why Not | Fee Range |
|--------|-------|--------------|-------------|---------------|-----------|
| **Splash Page** | DRAFT | Client branding + VIP landing page | ❌ No | Passive presence, no protection | Free |
| **Signal I — Casting Call** | SIGNAL_I | Public invitation without story context | ❌ No | Declaration of intent, creator hasn't committed yet | Free |
| **Intake Without Noise** | SIGNAL_I | Route4 records who arrived (witness registration) | ❌ No | Passive tracking, no resources allocated | Free |
| **The Hold** | HOLD | System-enforced silence (no engagement) | ❌ No | Silence by design, no Route4 work involved | Free |
| **Table Read** | PROCESS | First ritual with documentation (audio + presence) | ✅ Yes (Gate 1) | Protected access + witness tracking begins | $50–$100 |
| **Writing Table** | PROCESS | Documented craft discussion + decisions locked | ✅ Yes (Gate 1) | Witness-only access, permanence guaranteed | Included in Gate 1 |
| **Shot Council** | PROCESS | Safe stills/previews + visual craft discussion | ✅ Yes (Gate 1) | Witness-only access, artifact generation begins | Included in Gate 1 |
| **Process Previews** | PROCESS | FFmpeg generates low-res proxies + watermarks | ✅ Yes (Gate 2) | Permanent artifacts created (media processing cost) | $10–$50 |
| **Signal II — Anonymous Song Drop** | SIGNAL_II | Public emotional artifact (no authorship revealed) | ✅ Yes (Gate 2) | Permanent artifact storage + version control | Included in Gate 2 |
| **Fragments** | FRAGMENTS | Public BTS residue (auto-trimmed, masked) | ✅ Yes (Gate 2) | FFmpeg processing + multi-format outputs | Included in Gate 2 |
| **The Drop** | DROP | Primary release + canon sealed + archive created | ✅ Yes (Gate 3) | Irreversible permanence, immutable record, highest protection | $100–$200 |
| **The Echo** | ECHO | Reflection channels opened (mixed witness + public) | ✅ Yes (Gate 3) | Witness access continues, archive grows | Included in Gate 3 |
| **Private Viewing** | ECHO | Witness-only scarcity event (QR access, no recording) | ✅ Yes (Gate 4) | Scarcity creation + access control infrastructure | $50–$100 |
| **Archive** | ARCHIVE | Permanent immutable record (versioned, forever access) | ✅ Yes (Gate 3) | Eternal storage + protection guarantee | Included in Gate 3 |
| **Filmhub Distribution** | ARCHIVE→DISTRIBUTED | Auto-submit to multi-platform (Tubi, Pluto, Roku, etc.) | ✅ Yes (Gate 5) | External escalation + platform relationship + ongoing royalty tracking | $100–$250 (one-time) |

### 10.3 Gate-Based Pricing Model

#### Gate 1 — Witness Activation ($50–$100)
**Triggered When:** Table Read scheduled (first ritual with documentation)  
**What Route4 Does:**
- Enables protected Discord channels (permission management)
- Begins tracking WitnessEvents (presence database)
- Activates witness-only content access
- Starts immutability guarantees

**Why This is Chargeable:** Route4 begins protecting access and allocating resources (infrastructure, database, Discord management).

#### Gate 2 — Artifact Generation ($10–$50)
**Triggered When:** Process previews or fragments are generated  
**What Route4 Does:**
- Runs FFmpeg jobs (CPU-intensive processing)
- Generates multi-format outputs (9:16, 1:1, 16:9)
- Stores versioned assets (Frame.io folder structure)
- Implements visibility level enforcement (L1 vs L2)

**Why This is Chargeable:** Real compute resources + permanent storage + version control.

#### Gate 3 — The Drop ($100–$200)
**Triggered When:** Release is published (state transitions to DROP)  
**What Route4 Does:**
- Seals canon (makes archive immutable)
- Creates permanent versioned record
- Locks all edit capabilities
- Begins long-term archive preservation

**Why This is Chargeable:** This is the irreversible threshold. Once dropped, canon is permanent.

#### Gate 4 — Private Viewing ($50–$100)
**Triggered When:** Private viewing ritual is scheduled  
**What Route4 Does:**
- Generates time-limited QR access codes
- Manages witness-only venue access
- Prevents recording/streaming
- Logs attendance audit trail

**Why This is Chargeable:** Scarcity creation + access control infrastructure.

#### Gate 5 — Filmhub Distribution ($100–$250, one-time)
**Triggered When:** Conditions met (witness threshold + revenue signals + archive sealed)  
**What Route4 Does:**
- API integration with Filmhub
- Metadata validation + submission
- Multi-platform distribution (Tubi, Pluto, Roku, Amazon Freevee)
- Ongoing royalty tracking + reporting

**Why This is Chargeable:** External escalation + platform relationship + operational overhead.

---

#### Gate 0 (Tier B Only) — Route4 AI Business Manager: Outreach Services

**Availability:** Exclusive to Tier B (Managed Partnership) creators  
**Activation:** When casting call (Signal I) is created, if Tier B is configured

**The Problem:**
Creators have casting calls ready, short URLs generated, but reaching witnesses requires expertise in community targeting and continuous management.

**Route4's Solution (Tier B Only):**
Route4 AI Business Manager provides fully-managed, AI-curated outreach campaigns to distribute casting calls and recruit witnesses from relevant film communities.

---

##### Tier B Outreach Level 1 — AI-Curated Outreach Script (Included in Tier B)

**What Route4 Does:**
- AI agent scrapes active film communities (Reddit r/Filmmakers, FilmFreeway forums, Discord servers, Facebook groups, Stage 32, Backstage)
- Analyzes community rules (identifies which allow casting call posts)
- Generates curated list of 20–50 communities matching casting call tone/genre
- Provides tailored outreach script for each community (tone-matched to community culture)
- Delivers posting schedule (optimal times based on community activity patterns)
- Includes compliance notes (community-specific rules to follow)

**Creator Responsibilities:**
- Monitor short URL clicks
- Track witness conversion

**When Delivered:** Automatically when Signal I (casting call) created  
**Cost:** Included in Tier B subscription (no separate charge)

---

##### Tier B Outreach Level 2 — Managed Outreach Campaign (Included in Tier B)

**What Route4 Does:**
- Everything in Level 1
- Route4 Business Account Manager posts casting call to curated communities
- Places ads on relevant film groups (FilmFreeway, Stage 32, Backstage)
- Monitors trackable short URL clicks
- Provides weekly outreach report (reach, engagement, click-through rate)

**Creator Responsibilities:**
- Respond to inquiries from potential witnesses

**When Executed:** Automatically after Signal I approved  
**Cost:** Included in Tier B subscription (Route4 covers ad spend budget)

---

##### Tier B Outreach Level 3 — Full Campaign Management (Included in Tier B)

**What Route4 Does:**
- Everything in Levels 1–2
- Route4 manages entire outreach lifecycle until witness threshold hit
- A/B testing of messaging across communities
- Quarterly campaign optimization (refines targeting based on who actually becomes witnesses)
- Continuous monitoring + adjustment until Gate 1 triggered

**Creator Responsibilities:**
- Notify Route4 when witness threshold met

**When Executed:** Continuously throughout Signal I → Table Read (Gate 1)  
**Cost:** Included in Tier B subscription  
**Incentive Model:** Route4 earns when witnesses convert (15% of final distribution revenue)

**Why This Aligns Incentives:**
Route4 only earns ongoing revenue when outreach successfully converts witnesses into sustained participation. If campaign fails to reach audience, Route4 earns nothing. This forces Route4 to optimize for quality witness conversion, not vanity metrics.

---

##### How AI Agent Works

**Input Data:**
- Casting call content (tone, genre, location, constraints)
- Client slug (e.g., "making-of-mary")
- Short URL (e.g., `mary/c/invite`)

**AI Agent Actions:**
1. **Scrape active film communities** (Reddit, FilmFreeway, Discord, Facebook, Stage 32, Backstage)
2. **Analyze community rules** (identifies posting policies, filters out anti-promotion communities)
3. **Extract engagement patterns** (what types of posts get traction? when are users most active?)
4. **Generate tailored scripts** (tone-matched to each community's culture, avoids spam language)
5. **Build posting schedule** (optimal timing based on historical community activity)
6. **Flag compliance risks** (communities with strict self-promotion bans)

**Output Deliverable:**
- Curated list of 20–50 communities (prioritized by fit + engagement potential)
- Custom outreach script per community (ready to copy-paste)
- Posting schedule with optimal times
- Compliance notes (what to avoid, what rules to follow)

**Human Review (Required):**
Before delivery to creator, Route4 staff reviews AI outputs to ensure:
- No hallucinated community info
- Scripts are authentic (not overly promotional)
- Compliance notes are accurate
- Targeting aligns with casting call intent

---

##### Risks & Mitigations

**Risk 1: Spam Reputation**
- Route4 could get banned from communities for over-posting
- **Mitigation:** Strict rate limits (max 3 communities per week), genuine engagement before posting, human review of all scripts

**Risk 2: Creator Expectations**
- Creators might expect guaranteed witnesses
- **Mitigation:** Terms clearly state "best effort" outreach, no guaranteed conversion. Refund policy only for Tier 1 if plan is not delivered.

**Risk 3: AI Hallucination**
- AI might generate incorrect community info or inappropriate scripts
- **Mitigation:** Mandatory human review of all AI outputs before delivery (at least for Tier 1 and Tier 2)

---

##### Integration with Route4 Flow

**Current Flow:**
```
Voltron creates casting call
   ↓
Short URL generated (`mary/c/invite`)
   ↓
??? (creator distributes manually)
```

**New Flow with Business Account Manager:**
```
Voltron creates casting call
   ↓
Short URL generated (`mary/c/invite`)
   ↓
Creator purchases Tier 1/2/3 Outreach Service
   ↓
Route4 AI generates curated community list + scripts
   ↓
Human review validates AI output
   ↓
Route4 (Tier 2/3) or Creator (Tier 1) executes outreach
   ↓
Short URL clicks tracked via ShortUrlController
   ↓
Witnesses convert → Gate 1 triggered
   ↓
(Optional Tier 3B) Route4 earns 10% of Gate 1 revenue
```

---

### 10.4 Revenue Recognition Timing

| Gate | Trigger Condition | When Charged | Refundable? |
|------|-------------------|--------------|------------|
| Gate 1 | Table Read scheduled | On scheduling confirmation | 30-day grace (cancel ritual) |
| Gate 2 | FFmpeg job submitted | On job completion | Non-refundable (compute spent) |
| Gate 3 | Release published (DROP) | On canon seal | Non-refundable (immutable) |
| Gate 4 | Private viewing scheduled | On QR code generation | 7-day grace (cancel event) |
| Distribution (Tier A) | ARCHIVE + conditions met (automatic) | On Filmhub submission | Non-refundable (placement live) |
| Distribution (Tier B) | ARCHIVE + conditions met (automatic) | 15% of platform earnings (quarterly) | N/A (ongoing revenue share) |

**Note:** Distribution is not a "gate" requiring approval—it's automatically triggered when state + thresholds are met, based on configuration set during onboarding.

### 10.5 Why This Pricing Model Protects Route4's Integrity

✅ **Never charges for intent** — Splash Page, Signal I, The Hold are free  
✅ **Only charges after commitment** — Chargeable gates are irreversible thresholds  
✅ **Aligns incentives** — Route4 profits when creators succeed (complete cycles)  
✅ **Prevents exploitation** — Bad actors can't spam free rituals (cost at Gate 1)  
✅ **Scales with creators** — Bigger rituals (The Drop, Distribution) cost more  
✅ **Encourages restraint** — Creators think carefully before crossing gates  
✅ **Transparency-first** — All fees disclosed upfront, tied to specific work  

### 10.6 Non-Dependent Distribution: Automatic Audience Escalation

Route4 follows Non-D principles: **state drives behavior, not events.** Distribution is not a gate requiring approval—it's configured once during onboarding and executes automatically when conditions are met.

**Core Principle:** Creators configure distribution preferences during setup. Route4 escalates audience reach automatically based on state + thresholds. No decisions required after onboarding.

---

#### 10.6.1 Configuration During Onboarding (Not Customization)

When a client sets up their Route4 project, they configure distribution preferences:

> "When your release completes all rituals and reaches ARCHIVE state with witness threshold + revenue signals, should Route4 automatically distribute to streaming platforms?"

**Configuration Options:**
- **Option 1:** Auto-distribute (Tier A: Submission Only) — $250 one-time fee when conditions met
  - Creator handles outreach independently
- **Option 2:** Auto-distribute + Outreach (Tier B: Managed Partnership) — 15% revenue share ongoing
  - Route4 AI Business Manager handles all outreach starting at Signal I (Gate 0)
  - Route4 handles distribution + platform management
- **Option 3:** No distribution (creator handles independently)
  - No Route4 involvement post-ARCHIVE

**Conditions for Automatic Distribution Execution:**
When a client selects Option 1 or Option 2, Route4 automatically submits to Filmhub when **ALL** of the following conditions are met:

✅ **State = ARCHIVE** — Release has completed full ritual cycle (DRAFT → DROP → ARCHIVE)  
✅ **Ritual Attestations** — Minimum 100 documented witness participations across all rituals  
✅ **Revenue Signals Present** — Minimum $1,000 in Gate 1-4 payments received  
✅ **Canon Sealed** — Archive is immutable, no edits allowed post-Drop  
✅ **No Distribution Blocks** — No copyright claims, no content violations flagged  

**When conditions met → Route4 auto-executes submission (no approval needed).**

**This is configuration, not customization.** Set once during onboarding, never touched again.

---

#### 10.6.2 Automatic Distribution Ladder

Route4 automatically escalates creators to wider audiences when they've proven success. No approval gates, no manual triggers—state + configuration = inevitable outcome.

| Stage | Audience Size | Non-D Trigger | What Happens Automatically | Revenue Model |
|-------|---------------|---------------|---------------------------|---------------|
| **Witness Circle** | 100–500 | PROCESS state reached | Private rituals (Discord channels unlocked) | Gate 1 ($50–$100) |
| **Fragments Distribution** | 500–5K | SIGNAL_II state reached | Anonymous BTS clips auto-posted to YouTube, TikTok (L2 visibility enforced) | Gate 2 ($10–$50) |
| **Streaming Distribution** | 5K–50K | ARCHIVE + 100+ ritual attestations + revenue ($1K+) | Auto-submit to Filmhub → Tubi, Pluto, Roku, Amazon Freevee | Tier A: $250 (one-time) OR Tier B: 15% revenue share |
| **Premium Distribution** | 50K+ | Revenue threshold ($10K+) reached | Auto-submit to Amazon Prime, Apple TV (requires quality bar) | Tier B: 15% revenue share (ongoing) |

**Client never makes distribution decisions.** System acts autonomously based on configuration + state.

---

#### 10.6.3 Non-D Execution Flow

**Example: Making of MARY**

**During Onboarding:**
- Mary's team configures: `distributionEnabled: true`, `distributionTier: "TierB"`
- This configuration is immutable once release cycle begins

**Ritual Execution (All Automatic):**

```
1. Signal I → 500 people see casting call
   ↓
2. 150 witness participations recorded (Gate 1 triggered when Table Read scheduled)
   ↓
3. PROCESS rituals complete → State machine advances to SIGNAL_II
   ↓
4. SIGNAL_II reached → Fragments auto-posted to YouTube/TikTok (L2 visibility enforced)
   ↓ (No approval needed—state drove behavior)
5. The Drop → Canon sealed, archive created (State = ARCHIVE)
   ↓
6. Background job detects:
     ✓ State = ARCHIVE
     ✓ Ritual attestations >= 100 (actual: 142 documented participations)
     ✓ Revenue signals >= $1,000
     ✓ distributionEnabled: true
     ✓ distributionTier: "TierB"
   ↓
7. ReleaseManagementService.TriggerFinalDistribution() executes (AUTOMATIC)
   ↓
8. Filmhub API submission (no Mary approval needed)
   ↓
9. 6 weeks later: Mary's film live on Tubi, Pluto, Roku, Amazon Freevee
   ↓
10. 12 months later: 50K views across platforms, $8K revenue
   ↓
11. Background job detects revenue threshold ($10K projected Year 2)
   ↓
12. Auto-submit to Amazon Prime (if configured and quality bar met)
```

**Mary never makes a decision after onboarding. Route4 escalates automatically.**

---

#### 10.6.3b Revenue Example: Tier B (Making of MARY - Episode 1)

**Context:** Mary's production team chose **Tier B (Managed Partnership)** during onboarding. Route4's AI Business Manager handles all witness recruitment, outreach, and platform management. Episode 1 reaches completion and generates the following revenue.

**Financial Timeline:**

| Gate/Phase | Ritual/Event | Creator Charge | Timing | Notes |
|-----------|--------------|----------------|--------|-------|
| Gate 0 | AI Business Manager Activation | $0 (Tier B included) | Day 1 | No charge; outreach included in partnership |
| Gate 1 | First 50 witness participations | $0 (under threshold) | Day 15 | Witness recruitment begins; no charge yet |
| Gate 1 | Threshold crossed (50+ witnesses) | **$50** | Day 18 | Creator pays once threshold met |
| Gate 2 | Process previews + fragments created | **$25** | Day 45 | FFmpeg jobs: low-res proxies + BTS clips |
| Gate 3 | Witness expansion (100+ witnesses) | **$100** | Day 60 | Creator community validates production value |
| Gate 4 | Premium witness (top 10 contributors) | **$75** | Day 80 | Elevated access for key participants |
| Gate 5 | Filmhub Distribution (Tier B revenue share) | **See below** | Day 100 | Automatic submission; triggers revenue split |

**Gates 1-4 Subtotal (Creator Charges):** $250

**Year 1 Distribution Revenue (Tier B Split):**

```
Platform Performance:
├── Tubi:          18K views  → $2,700 revenue
├── Pluto TV:      15K views  → $2,250 revenue
├── Roku:          12K views  → $1,800 revenue
└── Amazon Freevee: 5K views  → $1,250 revenue
   ════════════════════════════════════════════
   Total Platforms: 50K views → $8,000 gross revenue
```

**Revenue Split (Tier B: 85% Creator / 15% Route4):**

| Entity | Revenue Share | Amount | Formula |
|--------|---------------|--------|---------|
| **Creator (Mary's Production)** | 85% | **$6,800** | $8,000 × 0.85 |
| **Route4 Studios** | 15% | **$1,200** | $8,000 × 0.15 |

**Creator's Total Year 1 Earnings:**

```
Gate Charges Paid Out:        -$250
Filmmaker Earnings (Gate 1-4): -$250 (already paid)
Distribution Revenue (Tier B):  +$6,800
═════════════════════════════════════
Net Creator Income (Year 1):    $6,550
```

**Route4's Year 1 Revenue (Tier B):**

```
AI Business Manager Service:  $0 (included in revenue share model)
Filmhub Distribution Fee:      $0 (revenue share instead of flat fee)
Platform Relationship Mgmt:    $0 (included)
Distribution Revenue Share:    +$1,200 (15% of gross)
═════════════════════════════════════
Net Route4 Income (Year 1):    $1,200
```

**Projected Year 2 (If $10K Revenue Threshold Met):**

Assuming continued platform performance + Year 2 momentum:
- Projected views: 75K–100K (20-30% growth year-over-year typical for streaming)
- Projected revenue: $12,000–$15,000
- Creator share: $10,200–$12,750 (85%)
- Route4 share: $1,800–$2,250 (15%)

**Why Tier B Works for Mary:**

1. **No Upfront Capital:** Mary pays only $250 in Gate charges (Gates 1-4). Distribution happens at cost.
2. **Hands-Off Distribution:** Route4's AI Business Manager handles all outreach, community building, platform management. Mary focuses on next episode.
3. **Ongoing Revenue:** Instead of one-time $250 payment (Tier A), Mary gets 85% of platform earnings indefinitely.
4. **Platform Relationships:** Route4 maintains ongoing relationships with Tubi, Pluto, Roku, Amazon Freevee. Negotiates better placement, featured slots, seasonal bundles.
5. **Quarterly Reports:** Route4 provides detailed breakdown: views/platform, revenue/platform, trending data.

**Route4's Revenue Model (Tier B):**

- Low upfront cost ($0 out-of-pocket for distribution)
- Sustainable 15% revenue share (aligns incentives: Route4 profits when creator succeeds)
- Scales to 100+ creators (each 15% adds up; $1,200/creator × 50 creators = $60K ARR)
- Defensible moat: platform relationships + audience data

**Comparison: Tier A vs Tier B for Mary's Episode 1**

| Aspect | Tier A | Tier B |
|--------|--------|--------|
| **Gate Charges (1-4)** | $250 | $250 |
| **Distribution Fee** | $250 (Filmhub submit) | $0 (included) |
| **Outreach/Marketing** | Mary's responsibility | Route4 AI Business Manager |
| **Platform Relationships** | Mary handles | Route4 manages |
| **Year 1 Distribution Revenue** | $8,000 (100% to Mary) | $6,800 (85% to Mary) |
| **Route4 Revenue** | $250 (one-time) | $1,200 (Year 1) |
| **Best For** | Established creators with audience | Growing creators needing distribution support |

**Decision:** Mary chose Tier B because she wanted Route4 to handle the messy part (platform outreach) while she focused on Season 1 Episode 2. The $1,200 revenue loss Year 1 (vs Tier A) is offset by Route4's marketing support driving an estimated 20% higher platform adoption.

---

#### 10.6.3c Route4's Revenue Strategy: Why Sacrifice Threshold Fees for Tier B?

**The Tier B Trade-Off:**

Route4 makes a strategic bet with Tier B creators: **Forgo upfront threshold fees to capture long-term revenue share.**

```
Tier A Scenario (Mary chooses "Submission Only"):
├── Gates 1-4: $250 (threshold fees, paid by creator)
├── Gate 5: $250 (Filmhub submission)
└── Total Route4 Revenue (Tier A): $500 (one-time, then done)

Tier B Scenario (Mary chooses "Managed Partnership"):
├── Gates 1-4: $0 (included in partnership model)
├── Gate 5: $0 (included in partnership model)
├── Year 1 Revenue Share: $1,200 (15% of $8K platform revenue)
├── Year 2 Revenue Share: $1,800–$2,250 (15% of $12K–$15K)
├── Year 3 Revenue Share: $2,250–$3,750 (15% of $15K–$25K, assuming 2+ episodes)
└── Total Route4 Revenue (Tier B, 3 years): $5,250–$7,200
```

**Break-Even Analysis:**

| Timeline | Cumulative Revenue (Tier A) | Cumulative Revenue (Tier B) | Difference |
|----------|---------------------------|---------------------------|-----------|
| Year 1 | $500 | $1,200 | +$700 (Tier B ahead) |
| Year 2 | $500 | $3,000–$3,450 | +$2,500–$2,950 (Tier B ahead) |
| Year 3 | $500 | $5,250–$7,200 | +$4,750–$6,700 (Tier B ahead) |

**Route4 breaks even on the Tier B sacrifice by month 8–10 of Year 1.**

**Why This Works:**

1. **Alignment of Incentives:** Route4 only profits when creators succeed. If Mary's film generates $0 views, Route4 gets $0. This creates shared risk.

2. **AI Business Manager Pays For Itself:** The $0 upfront cost for Gates 1-4 is offset by aggressive AI Business Manager recruitment driving 20%+ higher platform adoption. That 20% = $1,600 additional Year 1 revenue → $240 for Route4.

3. **Compounding Effect:** Mary releases Episode 2, 3, 4... Each episode's revenue share stacks on previous episodes. By Year 2, Mary is generating 2–3 concurrent revenue streams.

4. **Customer Loyalty:** Tier B creators stay with Route4 because they're making real money ($6,800+ Year 1). They don't shop around for competing platforms.

5. **Network Effects:** As Route4 accumulates Tier B creators (10, 20, 50), the platform relationship data becomes valuable. Route4 knows which creators drive engagement on Tubi vs Pluto vs Roku. This becomes competitive moat—Route4 can negotiate better deals with platforms.

**Scaling Math (Route4's Portfolio):**

```
Scenario: Route4 onboards 50 creators in Year 1, 30 choose Tier B

Year 1 Revenue:
├── Tier A creators (20): 20 × $500 = $10,000
├── Tier B creators (30): 30 × $1,200 = $36,000
└── Total: $46,000

Year 2 Revenue (assuming Year 1 Tier B creators continue + 20 new Tier B):
├── Tier A creators (additional): 20 × $500 = $10,000
├── Tier B (Year 1 cohort, avg growth to $1,800): 30 × $1,800 = $54,000
├── Tier B (Year 2 new cohort): 20 × $1,200 = $24,000
└── Total: $88,000

Year 3 Revenue (cumulative cohorts):
├── Year 1 Tier B cohort (avg $2,500): 30 × $2,500 = $75,000
├── Year 2 Tier B cohort (avg $1,800): 20 × $1,800 = $36,000
├── Year 3 Tier B cohort (new): 20 × $1,200 = $24,000
└── Total: $135,000
```

**The Strategic Insight:**

Tier B is Route4's **long-term revenue engine**. Tier A is a one-time transaction. By Year 3, if Route4 accumulates 70 Tier B creators, the platform generates $135K annually from revenue share alone—far exceeding the $500-per-creator flat fees from Tier A.

**Risk Mitigation:**

If a Tier B creator's film underperforms (generates $0 views), Route4 still invested AI Business Manager resources upfront. To prevent this:
- AI Business Manager applies strict targeting criteria (only recruit witnesses with high intent)
- Route4 monitors engagement metrics; pivot strategy if views stagnate after 30 days
- Dead-letter creators: If Year 2 projects < $1K revenue, Route4 may deprioritize (focus AI resources on high-growth creators)

**Conclusion:**

Tier B's revenue model is **deliberately patient capital.** Route4 sacrifices $250–$500 upfront per creator to earn $1,200–$3,000+ over 2–3 years. This works only if:
1. Route4's AI Business Manager actually drives 20% higher platform adoption (proven through analytics)
2. Platform partnerships + audience data become defensible moat (competitors can't replicate)
3. Creator churn stays < 20% (creators keep releasing, Route4 keeps earning)

If all three hold, Tier B becomes Route4's path to sustainable, creator-aligned revenue.

---

#### 10.6.4 Distribution Communication Strategy: Transparent Log (Option A)

**Decision:** Route4 communicates distribution submission to creators through a transparent state transition, not through celebration language or platform hype.

**Why This Approach:**
- Maintains Route4's restraint philosophy (decisions, not outcomes)
- Documents what happened factually for the creator's record
- Avoids engagement-driven language ("Your film is now live!")
- Aligns with Route4 as curation layer (platform-agnostic)
- Creator discovers broader audience impact through their own platform monitoring

**Implementation:**

**1. State Transition — ARCHIVE → ARCHIVE_DISTRIBUTED**

When Filmhub distribution is triggered automatically, the release state machine advances:

```
Current State: ARCHIVE
↓
Distribution conditions verified
↓
New State: ARCHIVE_DISTRIBUTED
Timestamp: Auto-recorded
Metadata: { platforms: ["Tubi", "Pluto", "Roku", "Amazon Freevee"], submittedAt: "2026-01-05T14:32:00Z" }
```

**2. Creator Visibility — Timeline Entry**

In the creator's release timeline (audit log), a single entry appears:

```
[2026-01-05 14:32 UTC] STATE TRANSITION: ARCHIVE → ARCHIVE_DISTRIBUTED
Submitted to Filmhub (3 platforms: Tubi, Pluto, Roku, Amazon Freevee)
Pending platform approval and platform-specific ingestion timelines
```

**Characteristics:**
- Factual, not celebratory
- Timestamp and platform list documented
- Passive voice (submitted, not "launched")
- No engagement language
- No call-to-action or social sharing prompts
- Creator must actively monitor Filmhub/platforms for live status

**3. No Email Notification or Alert**

Route4 does NOT send an email saying "Your film has been distributed!" Instead:
- Creator sees the state transition in their release timeline
- Creator discovers live status when checking their Filmhub account or platform presence
- Discovery is self-directed, maintaining Route4's restraint

**4. Backend Implementation**

**Models/Route4Models.cs Updates:**
- Add `ARCHIVE_DISTRIBUTED` to ReleaseInstance.Status enum
- Add `DistributionMetadata` field to ReleaseInstance:
  ```csharp
  public class DistributionMetadata {
      public string[] Platforms { get; set; } = Array.Empty<string>();
      public DateTime? SubmittedAt { get; set; }
      public string? SubmissionStatus { get; set; } // Pending, Accepted, Rejected
  }
  ```

**Controllers/ReleaseManagementController.cs Updates:**
- Add endpoint: `GET /api/clients/{clientSlug}/releases/{releaseKey}/distribution-status`
  - Returns: DistributionMetadata + timestamps
  - Shows: Submitted platforms, submission date, platform-specific statuses

**Services/ReleaseManagementService.cs Updates:**
- Method: `TriggerFinalDistribution()` (already exists, needs update)
  - After Filmhub submission succeeds:
    1. Advance state to `ARCHIVE_DISTRIBUTED`
    2. Record platforms in DistributionMetadata
    3. Record submission timestamp
    4. Add audit log entry (no email sent)

**5. Frontend Display**

**Timeline/Audit Log Component:**
- Shows state transitions chronologically
- Entry: "Submitted to Filmhub (3 platforms: Tubi, Pluto, Roku, Amazon Freevee)"
- Links to help doc explaining platform ingestion timelines

**Release Details Card:**
- Current State: Displayed as `ARCHIVE_DISTRIBUTED` (if applicable)
- Distribution Status: Expandable section showing:
  - Submission date/time
  - Target platforms (Tubi, Pluto, Roku, Amazon Freevee)
  - Note: "Platform ingestion times vary. Check platforms directly for live status."

**6. Creator Experience Walkthrough**

```
Timeline of Events:

Day 100: Release reaches ARCHIVE state (Drop ceremony completed)
Day 101: Background job runs, detects conditions met
Day 101 14:32 UTC: ReleaseManagementService.TriggerFinalDistribution() executes
  → Filmhub API submission succeeds
  → ReleaseInstance.Status = ARCHIVE_DISTRIBUTED
  → DistributionMetadata.SubmittedAt = 2026-01-05T14:32Z
  → Timeline entry created: "Submitted to Filmhub..."

Mary logs in to Route4:
  → Sees ARCHIVE_DISTRIBUTED state on her release
  → Reads timeline: "Submitted to Filmhub (3 platforms...)"
  → Knows distribution has started

3 weeks later:
  → Mary logs into her Tubi account, sees her film live
  → Realizes: "Oh! This is what that state transition meant"
  → Earned discovery reinforces Route4's transparency

```

**7. Rationale: Why "Silent" is Wrong Here**

If Route4 submitted to Filmhub with NO record:
- ❌ Creator has no proof what happened
- ❌ Route4 becomes invisible (contradicts transparency)
- ❌ Creator might re-submit independently (duplicate submissions)
- ❌ Creates support burden ("Why isn't my film on Tubi?")

With transparent log:
- ✅ Creator has documented record of submission
- ✅ Creator knows exactly what platforms were targeted
- ✅ Route4 remains accountable (state transition is auditable)
- ✅ Creator discovery is self-directed (restraint maintained)

---

#### 10.6.5 Filmhub API Integration Checklist

**Filmhub Configuration:**
- [ ] Filmhub API credentials stored in appsettings.json (production: Azure Key Vault)
- [ ] IFilmhubService implemented with SubmitForDistributionAsync()
- [ ] Retry logic for transient API failures (exponential backoff, 3 attempts)
- [ ] Error handling: Log failures, set DistributionMetadata.SubmissionStatus = "Rejected"

**State Machine Updates:**
- [ ] ARCHIVE_DISTRIBUTED state added to ReleaseInstance.Status
- [ ] Middleware validation: ARCHIVE_DISTRIBUTED is immutable (no rollback)
- [ ] DistributionMetadata persisted in database

**Background Job (Scheduled Service):**
- [ ] Daily check: Find all ARCHIVE releases meeting conditions
- [ ] Filter: distributionEnabled = true AND conditions verified
- [ ] Execute: Filmhub submission, state transition, audit log entry
- [ ] Monitoring: Log all submission attempts (success/failure/retry)

**Testing:**
- [ ] Unit test: Condition verification (state, attestations, revenue)
- [ ] Integration test: Filmhub API submission (mocked)
- [ ] E2E test: Full flow from ARCHIVE → ARCHIVE_DISTRIBUTED

---

#### 10.6.4 Why This Is Non-Dependent

✅ **State drives behavior** — ARCHIVE state + conditions met = auto-distribution  
✅ **Configuration over customization** — Set distribution preference once, system executes  
✅ **No client dependency** — System acts autonomously based on configuration  
✅ **Inevitable outcome** — If configured "Yes," distribution happens when earned  
✅ **No approval gates** — No "Would you like to distribute?" prompt at ritual completion  

**Traditional (Dependent) Flow:**
```
Release complete → Email creator "Ready to distribute?" → Wait for approval → Execute
```

**Route4 (Non-Dependent) Flow:**
```
Release complete + conditions met → Auto-execute based on configuration
```

---

### 10.7 Distribution Tiers: Pricing & Execution

When automatic distribution triggers (ARCHIVE + thresholds met), Route4 executes based on configured tier.

#### Tier A — Submission Only ($250 one-time)

**What Route4 Does (Automatic):**
- API submission to Filmhub (no creator approval needed)
- Auto-distribution to Tubi, Pluto TV, Roku Channel, Amazon Freevee
- Metadata validation + poster/trailer management
- Submission confirmation notification

**Creator Responsibilities:**
- Manage Filmhub account (Route4 provides credentials post-submission)
- Handle platform earnings/payments
- Track annual earnings independently

**Revenue Model:**
- Route4: $250 (one-time, charged on Filmhub submission)
- Creator: 100% of platform earnings (minus Filmhub's 10–30% aggregation fee)

**Best For:**
- Creators who want independence
- Established creators with platform experience
- One-off releases

**Estimated Creator Earnings (Year 1):**
- 100K+ views on Tubi: $300–$600/year
- All platforms combined: $1,200–$1,800/year
- Route4 earns: $250 total

---

#### Tier B — Managed Partnership (Includes AI Business Manager + Distribution)

**What Route4 Does (Automatic + Ongoing):**
- **Outreach (Gate 0):** AI-curated community targeting + fully-managed campaigns (all three levels included)
- **Rituals (Gates 1-4):** Witness activation, artifact generation, Drop protection, private viewing infrastructure
- **Distribution:** Everything in Tier A + ongoing platform management
  - Maintain Filmhub account on creator's behalf
  - Monitor all platform earnings quarterly
  - Aggregate payments across platforms
  - Quarterly earnings reports + transparent tracking
  - Platform relationship management (respond to platform requests)
- Route4 earns 15% of creator's net earnings (ongoing)

**Creator Responsibilities:**
- Approve quarterly reports (compliance, not decision-making)
- Provide creative assets if platforms request updates
- Notify Route4 of any future work to be added to catalog

**Revenue Model:**
- Route4: 15% of creator's platform earnings (ongoing)
- Creator: 85% of platform earnings
- Example: Creator earns $1,500/year → Route4: $225/year, Creator: $1,275/year

**Best For:**
- Creators seeking complete hands-off experience (outreach + distribution)
- Filmmakers who want Route4 managing audience growth from casting call onward
- Series/multi-release creators (stacks over time)
- Creators prioritizing community building + revenue over independence

**5-Year Projection (with Tier B Outreach):**
- Year 1 witnesses: 150+ (driven by AI outreach management)
- Year 2 witnesses: 200–250 (improved targeting from Year 1 data)
- Year 3 revenue share: $2,250–$3,750 (multiple releases compounding)
- Year 5 total creator earnings: $15,000–$25,000 (sustained platform growth)

---

## 11. Outreach AI — Casting Call Distribution Engine

### 11.1 Overview & Philosophy

**Problem Statement:**
Creators have casting calls ready and trackable short URLs generated, but lack expertise in community targeting, timing, and continuous witness recruitment. Manual outreach is time-intensive, error-prone, and difficult to track.

**Route4's Solution:**
An AI-assisted outreach engine that curates film communities, generates compliant messaging, tracks all interactions, and provides creators with a "just do it" blueprint—either fully automated (where ToS-safe) or human-assisted with pre-filled payloads.

**Core Design Principles:**
- **Human-in-front for compliance:** No ToS violations; humans execute where APIs unavailable
- **Maximum trackability:** Every action logged with UTM'd short URLs for attribution
- **Minimal creator friction:** "Auto" channels run autonomously; "Script" channels deliver copy-paste payloads
- **Progressive disclosure:** Phase I builds directory; Phase II adds targeting; Phase III automates posting

---

### 11.2 System Architecture

#### 11.2.1 Three-Tier Architecture

```
Route4.Outreach.Directory (Phase I)
├── Community profiles (film commissions, forums, groups)
├── Contact tracking (who was reached, when, via what channel)
└── UTM short URL generation (per-channel attribution)

Route4.Outreach.Targeting (Phase II)
├── AI-curated community matching (genre, tone, location fit)
├── Script generation (tone-matched to community culture)
└── Posting schedule optimization (best times per platform)

Route4.Outreach.Execution (Phase III)
├── Auto: API-based posting (Discord, email, SMS, Reddit if allowed)
├── Script: Form-filler payloads (Backstage, film commissions, Facebook)
└── Feedback loop (clicks → conversions → witness participation)
```

#### 11.2.2 Channel Classification Matrix

| Channel | Type | ToS-Safe? | Route4 Capability | Human Requirement |
|---------|------|-----------|-------------------|-------------------|
| **Discord (Route4 servers)** | Auto | ✅ Yes | Bot posts via Discord.NET API | None (fully automated) |
| **Email (opt-in lists)** | Auto | ✅ Yes | Transactional email API (SendGrid/Mailgun) | None (fully automated) |
| **SMS (opt-in talent)** | Auto | ✅ Yes | Twilio API with compliance | None (fully automated) |
| **Reddit (approved subs)** | Auto | ⚠️ Conditional | Reddit API if moderator approval obtained | Initial mod contact required |
| **Meetup/Eventbrite** | Auto | ✅ Yes | API event creation with short URL | None (API-driven) |
| **Backstage** | Script | ❌ No API | Provide copy + checklist | Human logs in and posts |
| **Facebook/Instagram groups** | Script | ❌ ToS risk | Provide copy + image + timing | Human posts to avoid bans |
| **Film Commissions (web forms)** | Script | ✅ Yes | Form-filler payload (Chrome ext) | Human reviews and submits |
| **Stage 32, FilmFreeway forums** | Script | ⚠️ Conditional | Provide tailored copy | Human posts respecting rules |

**Legend:**
- **Auto:** Route4 executes via API; logs results automatically
- **Script:** Route4 generates payload; human executes; logs completion manually or via extension

---

### 11.3 Phase I — Directory Curation & Contact Tracking (Detailed Design)

**Goal:** Build a structured, queryable database of film communities with contact tracking to prevent duplicate outreach and measure campaign effectiveness.

#### 11.3.1 Data Models

**OutreachDirectory (Community Profiles)**

```csharp
public class OutreachCommunity
{
    public Guid Id { get; set; }
    public required string Name { get; set; } // "Greater Cleveland Film Commission"
    public required string Type { get; set; } // FilmCommission, Forum, FacebookGroup, RedditSub, DiscordServer, Email, SMS
    public required string Channel { get; set; } // Auto or Script
    
    // Contact Info
    public string? Website { get; set; }
    public string? SubmissionFormUrl { get; set; } // For film commissions
    public string? ApiEndpoint { get; set; } // For auto channels
    public string? ContactEmail { get; set; }
    public string? SocialHandle { get; set; } // @backstage, r/Filmmakers, etc.
    
    // Targeting Metadata
    public string[] Genres { get; set; } = Array.Empty<string>(); // Drama, Horror, Documentary
    public string[] Locations { get; set; } = Array.Empty<string>(); // Ohio, Northeast, USA
    public string[] Tags { get; set; } = Array.Empty<string>(); // IndieFilm, StudentFilm, LocalTalent
    public int? EstimatedReach { get; set; } // Community size (followers, members)
    
    // Compliance & Rules
    public string? PostingRules { get; set; } // "No self-promotion Mondays; max 1 post/week"
    public bool RequiresApproval { get; set; } // Moderator approval needed?
    public string? ComplianceNotes { get; set; } // "Must engage 3x before posting"
    public bool HasCaptcha { get; set; }
    
    // Performance Tracking
    public int TotalOutreachAttempts { get; set; } // How many times we've contacted
    public DateTime? LastContactedAt { get; set; }
    public int SuccessfulConversions { get; set; } // Witnesses recruited from this community
    public decimal ConversionRate { get; set; } // SuccessfulConversions / TotalOutreachAttempts
    
    // Form-Filler Data (for Script channels)
    public Dictionary<string, string>? FormFieldMap { get; set; } 
    // Example: { "project_name": "#projectTitle", "contact_email": "#email", "description": "textarea[name='desc']" }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true; // Can be disabled if banned/deprecated
}
```

**OutreachContact (Interaction Log)**

```csharp
public class OutreachContact
{
    public Guid Id { get; set; }
    public Guid CommunityId { get; set; } // FK → OutreachCommunity
    public Guid CastingCallId { get; set; } // FK → CastingCall
    public Guid ClientId { get; set; } // Multi-tenant scope
    
    // Execution Details
    public required string Channel { get; set; } // Auto or Script
    public required string Method { get; set; } // Discord, Email, SMS, Backstage, FacebookGroup, FilmCommission
    public required string Status { get; set; } // Queued, Sent, Delivered, Failed, Clicked, Converted
    
    // Tracking
    public required string ShortUrlVariant { get; set; } // mary/c/invite?utm_source=backstage&utm_campaign=S1E1
    public string? MessageId { get; set; } // Email messageId, Discord messageId, SMS sid, etc.
    public string? PostUrl { get; set; } // For Script channels: URL to the post human created
    
    // Timing
    public DateTime ScheduledAt { get; set; } // When it should be sent
    public DateTime? SentAt { get; set; } // When it was actually sent/posted
    public DateTime? ClickedAt { get; set; } // First click on short URL (from this contact)
    public DateTime? ConvertedAt { get; set; } // When witness registered (Gate 1 triggered)
    
    // Metrics
    public int TotalClicks { get; set; } // Clicks on this specific short URL variant
    public bool DidConvert { get; set; } // Did this contact result in a witness?
    
    // Notes
    public string? Notes { get; set; } // Human can log context: "Posted in weekly casting thread"
    public string? ErrorMessage { get; set; } // If Status=Failed
    
    public DateTime CreatedAt { get; set; }
}
```

**OutreachCampaign (Campaign Orchestration)**

```csharp
public class OutreachCampaign
{
    public Guid Id { get; set; }
    public Guid CastingCallId { get; set; } // FK → CastingCall
    public Guid ClientId { get; set; }
    public required string Name { get; set; } // "Making of MARY - S1E1 Casting Call"
    
    // Configuration
    public required string TierLevel { get; set; } // TierB_Level1, TierB_Level2, TierB_Level3
    public int TargetWitnessCount { get; set; } // Goal: 100 witnesses
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; } // When Gate 1 triggered or campaign stopped
    
    // AI-Generated Artifacts
    public string[]? CuratedCommunities { get; set; } // Array of OutreachCommunity IDs (AI-selected)
    public Dictionary<string, string>? Scripts { get; set; } // CommunityId → tailored message
    public Dictionary<string, DateTime>? PostingSchedule { get; set; } // CommunityId → optimal post time
    
    // Performance
    public int TotalContacts { get; set; } // How many OutreachContact records
    public int TotalClicks { get; set; }
    public int TotalConversions { get; set; } // Witnesses recruited
    public decimal ConversionRate { get; set; }
    public decimal CostPerWitness { get; set; } // If Tier B charges apply
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Status { get; set; } // Active, Paused, Completed, Cancelled
}
```

#### 11.3.2 Database Schema

```sql
CREATE TABLE OutreachCommunities (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(200) NOT NULL,
    Type NVARCHAR(50) NOT NULL, -- FilmCommission, Forum, etc.
    Channel NVARCHAR(10) NOT NULL, -- Auto or Script
    
    Website NVARCHAR(500),
    SubmissionFormUrl NVARCHAR(500),
    ApiEndpoint NVARCHAR(500),
    ContactEmail NVARCHAR(200),
    SocialHandle NVARCHAR(100),
    
    Genres NVARCHAR(MAX), -- JSON array
    Locations NVARCHAR(MAX), -- JSON array
    Tags NVARCHAR(MAX), -- JSON array
    EstimatedReach INT,
    
    PostingRules NVARCHAR(MAX),
    RequiresApproval BIT DEFAULT 0,
    ComplianceNotes NVARCHAR(MAX),
    HasCaptcha BIT DEFAULT 0,
    
    TotalOutreachAttempts INT DEFAULT 0,
    LastContactedAt DATETIME,
    SuccessfulConversions INT DEFAULT 0,
    ConversionRate DECIMAL(5,4) DEFAULT 0,
    
    FormFieldMap NVARCHAR(MAX), -- JSON object
    
    CreatedAt DATETIME DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME DEFAULT GETUTCDATE(),
    IsActive BIT DEFAULT 1,
    
    INDEX IX_Type (Type),
    INDEX IX_Channel (Channel),
    INDEX IX_Locations (Locations), -- For geo-targeting
    INDEX IX_ConversionRate (ConversionRate DESC)
);

CREATE TABLE OutreachContacts (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CommunityId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES OutreachCommunities(Id),
    CastingCallId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES CastingCalls(Id),
    ClientId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Clients(Id),
    
    Channel NVARCHAR(10) NOT NULL, -- Auto or Script
    Method NVARCHAR(50) NOT NULL, -- Discord, Email, Backstage, etc.
    Status NVARCHAR(20) NOT NULL, -- Queued, Sent, Failed, Clicked, Converted
    
    ShortUrlVariant NVARCHAR(500) NOT NULL, -- Full UTM'd URL
    MessageId NVARCHAR(200),
    PostUrl NVARCHAR(500),
    
    ScheduledAt DATETIME NOT NULL,
    SentAt DATETIME,
    ClickedAt DATETIME,
    ConvertedAt DATETIME,
    
    TotalClicks INT DEFAULT 0,
    DidConvert BIT DEFAULT 0,
    
    Notes NVARCHAR(MAX),
    ErrorMessage NVARCHAR(MAX),
    
    CreatedAt DATETIME DEFAULT GETUTCDATE(),
    
    INDEX IX_CastingCall (CastingCallId),
    INDEX IX_Community (CommunityId),
    INDEX IX_Status (Status),
    INDEX IX_ScheduledAt (ScheduledAt),
    INDEX IX_ShortUrl (ShortUrlVariant)
);

CREATE TABLE OutreachCampaigns (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CastingCallId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES CastingCalls(Id),
    ClientId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Clients(Id),
    Name NVARCHAR(200) NOT NULL,
    
    TierLevel NVARCHAR(50) NOT NULL,
    TargetWitnessCount INT NOT NULL,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME,
    
    CuratedCommunities NVARCHAR(MAX), -- JSON array of IDs
    Scripts NVARCHAR(MAX), -- JSON object
    PostingSchedule NVARCHAR(MAX), -- JSON object
    
    TotalContacts INT DEFAULT 0,
    TotalClicks INT DEFAULT 0,
    TotalConversions INT DEFAULT 0,
    ConversionRate DECIMAL(5,4) DEFAULT 0,
    CostPerWitness DECIMAL(10,2) DEFAULT 0,
    
    CreatedAt DATETIME DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME DEFAULT GETUTCDATE(),
    Status NVARCHAR(20) DEFAULT 'Active',
    
    INDEX IX_CastingCall (CastingCallId),
    INDEX IX_Status (Status)
);
```

#### 11.3.3 API Endpoints (Phase I)

**Outreach Directory Management**

```
POST /api/outreach/communities
Body: { name, type, channel, website, genres[], locations[], postingRules, formFieldMap{} }
Response: OutreachCommunity (created)
Purpose: Add new community to directory (admin-only initially; AI-curated later)

GET /api/outreach/communities?type={FilmCommission|Forum|...}&channel={Auto|Script}&location={Ohio}
Response: List<OutreachCommunity> (filtered)
Purpose: Query directory for targeting

PUT /api/outreach/communities/{id}
Body: { partial update fields }
Response: OutreachCommunity (updated)
Purpose: Update rules, formFieldMap, or disable community

GET /api/outreach/communities/{id}/performance
Response: { totalAttempts, conversions, conversionRate, lastContacted, topPerformingCampaigns[] }
Purpose: Performance analytics per community
```

**Contact Tracking**

```
POST /api/outreach/contacts
Body: { communityId, castingCallId, channel, method, scheduledAt, shortUrlVariant }
Response: OutreachContact (created with Status=Queued)
Purpose: Log planned outreach (auto-scheduled or human-logged)

PUT /api/outreach/contacts/{id}/status
Body: { status, sentAt?, messageId?, postUrl?, notes? }
Response: OutreachContact (updated)
Purpose: Update contact status (e.g., Script channel human confirms "Sent")

GET /api/outreach/contacts?castingCallId={id}&status={Queued|Sent|Clicked|Converted}
Response: List<OutreachContact>
Purpose: Campaign dashboard (see what's pending, what converted)

POST /api/outreach/contacts/{id}/click
Body: { clickedAt }
Response: OutreachContact (updated with ClickedAt, Status=Clicked)
Purpose: Webhook from ShortUrlController when UTM'd link clicked
```

**Campaign Management**

```
POST /api/outreach/campaigns
Body: { castingCallId, name, tierLevel, targetWitnessCount, startDate }
Response: OutreachCampaign (created)
Purpose: Initiate campaign (triggers AI curation if Tier B)

GET /api/outreach/campaigns/{id}
Response: OutreachCampaign with nested contacts[], performance metrics
Purpose: Campaign detail view

PUT /api/outreach/campaigns/{id}/pause
Response: OutreachCampaign (Status=Paused)
Purpose: Pause outreach if witness threshold met early

GET /api/clients/{clientSlug}/campaigns
Response: List<OutreachCampaign> for client
Purpose: Multi-campaign tracking
```

#### 11.3.4 Short URL Attribution Strategy

**UTM Parameter Schema:**

```
Base URL: route4.studio/mary/c/invite
UTM'd Variants:
  - ?utm_source=backstage&utm_medium=forum&utm_campaign=S1E1_casting
  - ?utm_source=discord&utm_medium=bot&utm_campaign=S1E1_casting
  - ?utm_source=cleveland_film_comm&utm_medium=email&utm_campaign=S1E1_casting
  - ?utm_source=reddit_filmmakers&utm_medium=post&utm_campaign=S1E1_casting
```

**ShortUrlController Enhancement:**

```csharp
// Existing: GET /shorturl/{code}
// Enhancement: Log UTM params + associate with OutreachContact

[HttpGet("shorturl/{code}")]
public async Task<IActionResult> RedirectShortUrl(string code, [FromQuery] string? utm_source, [FromQuery] string? utm_medium)
{
    var shortUrl = await _context.ShortUrls.FirstOrDefaultAsync(s => s.Code == code);
    if (shortUrl == null) return NotFound();
    
    // Log click
    shortUrl.ClickCount++;
    
    // NEW: Associate click with OutreachContact (if utm_source provided)
    if (!string.IsNullOrEmpty(utm_source))
    {
        var contact = await _context.OutreachContacts
            .Where(c => c.ShortUrlVariant.Contains($"utm_source={utm_source}"))
            .OrderByDescending(c => c.CreatedAt)
            .FirstOrDefaultAsync();
            
        if (contact != null && contact.ClickedAt == null)
        {
            contact.ClickedAt = DateTime.UtcNow;
            contact.Status = "Clicked";
            contact.TotalClicks++;
        }
    }
    
    await _context.SaveChangesAsync();
    return Redirect(shortUrl.TargetUrl);
}
```

#### 11.3.5 Phase I Workflow: Directory Seeding

**Initial Data Collection (Manual → AI-Assisted)**

```
Step 1: Manual Seed (Route4 staff, Week 1)
├── Research 50 high-value communities
│   ├── 10 film commissions (Ohio, Michigan, Pennsylvania, New York, California)
│   ├── 10 forums (Backstage, Stage 32, FilmFreeway community, IndieWire forums)
│   ├── 10 Facebook groups (Ohio Filmmakers, Indie Film Hustle, Local casting groups)
│   ├── 10 Reddit subs (r/Filmmakers, r/acting, r/LocationSound, city-specific)
│   └── 10 Discord servers (Film Riot, Indie Film Community, regional servers)
├── Document:
│   ├── Website, contact info, submission forms
│   ├── Posting rules (from community guidelines/FAQ)
│   ├── Estimated reach (follower counts, member counts)
│   └── Compliance notes (captchas, approval requirements)
└── Create OutreachCommunity records via API

Step 2: AI Expansion (Week 2-4)
├── AI agent scrapes:
│   ├── Google: "film commission" + [state], "casting call submission" + [city]
│   ├── Facebook: Search public groups with "filmmakers" + [location]
│   ├── Reddit: Scrape /r/Filmmakers sidebar, related subs
│   ├── Discord: Disboard.org search for "film" servers
├── AI validates:
│   ├── Is community active? (recent posts, not abandoned)
│   ├── Do rules allow casting calls? (parse FAQ/pinned posts)
│   ├── Extract form fields (for film commissions)
├── Human reviews AI-curated list (filter hallucinations)
└── Bulk import via POST /api/outreach/communities (batch endpoint)

Step 3: Community Performance Baseline (Ongoing)
├── First outreach to each community (test)
├── Track: Did it convert? How many clicks?
├── Update: ConversionRate, LastContactedAt
└── Prioritize high-performers in future targeting
```

**Example: Greater Cleveland Film Commission Entry**

```json
{
  "name": "Greater Cleveland Film Commission",
  "type": "FilmCommission",
  "channel": "Script",
  "website": "https://clevelandfilm.com",
  "submissionFormUrl": "https://clevelandfilm.com/resources/post-opportunity",
  "contactEmail": "info@clevelandfilm.com",
  "locations": ["Ohio", "Cleveland", "Northeast Ohio"],
  "genres": ["All"], 
  "tags": ["LocalProduction", "CrewOpportunities", "CastingCalls"],
  "estimatedReach": 5000,
  "postingRules": "Free to post crew/casting opportunities. Submissions reviewed within 48 hours.",
  "requiresApproval": true,
  "complianceNotes": "Include project name, dates, compensation details. No explicit content.",
  "hasCaptcha": false,
  "formFieldMap": {
    "project_title": "#opportunity-title",
    "description": "textarea[name='description']",
    "contact_email": "#email",
    "contact_phone": "#phone",
    "opportunity_type": "select[name='type']", 
    "submission_deadline": "#deadline"
  }
}
```

#### 11.3.6 Phase I Deliverables Checklist

**Backend (Route4.Api)**
- [ ] `OutreachCommunity`, `OutreachContact`, `OutreachCampaign` models added to Route4Models.cs
- [ ] EF Core migration created and applied
- [ ] `OutreachController` implemented with all Phase I endpoints
- [ ] ShortUrlController enhanced with UTM tracking + OutreachContact association
- [ ] Seed data: 50 communities manually curated (CSV import or SQL script)

**Database**
- [ ] Tables created with proper indexes
- [ ] Seed script: Insert 50 OutreachCommunity records
- [ ] Views: `vw_CommunityPerformance` (aggregates conversions per community)

**Admin UI (Angular — Voltron Only)**
- [ ] Community directory CRUD (list, create, edit, disable)
- [ ] Contact log viewer (filter by campaign, status, community)
- [ ] Campaign dashboard (active campaigns + performance metrics)
- [ ] UTM builder (generates short URL variants with proper params)

**Testing**
- [ ] Unit tests: OutreachController endpoints
- [ ] Integration test: Create campaign → log contact → track click → verify conversion
- [ ] E2E test: Full flow from casting call creation → campaign → first contact logged

**Documentation**
- [ ] API docs: Swagger annotations for all outreach endpoints
- [ ] Admin guide: How to add new communities, log Script contacts manually
- [ ] UTM tracking guide: Naming conventions, attribution logic

---

### 11.4 Phase II — AI-Curated Targeting & Script Generation

**Goal:** Given a casting call, AI selects 20–50 best-fit communities from directory and generates tone-matched outreach scripts.

#### 11.4.1 AI Agent Input Schema

```json
{
  "castingCallId": "guid",
  "castingCallContent": {
    "title": "Making of MARY - Seeking Ohio-based crew",
    "logline": "Intimate character study, indie drama, shooting Cleveland Feb 2026",
    "tone": "Serious, artistic, character-driven",
    "genre": "Drama",
    "locations": ["Ohio", "Cleveland"],
    "roles": ["Cinematographer", "Sound Designer", "Production Assistant"],
    "compensationModel": "Deferred + backend points",
    "productionDates": "Feb 10-28, 2026"
  },
  "targetWitnessCount": 100,
  "tierLevel": "TierB_Level3"
}
```

#### 11.4.2 AI Agent Output Schema

```json
{
  "curatedCommunities": [
    {
      "communityId": "guid",
      "name": "Greater Cleveland Film Commission",
      "fitScore": 0.95,
      "rationale": "Geographic match (Cleveland), accepts casting calls, high conversion rate (12% historical)",
      "recommendedChannel": "Script",
      "estimatedReach": 5000
    },
    {
      "communityId": "guid",
      "name": "r/Ohio_Filmmakers (Reddit)",
      "fitScore": 0.88,
      "rationale": "Local community, active (200 posts/month), allows casting threads on Fridays",
      "recommendedChannel": "Script",
      "estimatedReach": 3200
    }
    // ... 18-48 more
  ],
  "scripts": {
    "guid_cleveland_film_comm": {
      "subject": "Casting Call: 'Making of MARY' - Cleveland-based Indie Drama",
      "body": "Hi Greater Cleveland Film Commission,\n\nWe're Route4 Studios, currently in pre-production for 'Making of MARY,' an intimate character-driven drama shooting in Cleveland this February...\n\n[Tone: Professional, concise, includes all required fields per formFieldMap]\n\nBest,\nRoute4 on behalf of Making of MARY",
      "compliance": "Includes project name, dates, compensation. No explicit content. Fits 'opportunity_type=Casting'."
    },
    "guid_reddit_ohio": {
      "subject": "[Casting Call] Cleveland Indie Drama - Seeking Crew (Feb 2026)",
      "body": "Hey r/Ohio_Filmmakers! 👋\n\nWe're casting for 'Making of MARY,' a low-budget character study shooting in Cleveland next month...\n\n[Tone: Casual, community-focused, uses emoji sparingly, respects Friday casting thread rule]\n\nInterested? Details here: [shortUrl]",
      "compliance": "Post on Friday only. Flair as 'Casting'. No self-promotion in title."
    }
  },
  "postingSchedule": {
    "guid_cleveland_film_comm": "2026-01-07T10:00:00Z",
    "guid_reddit_ohio": "2026-01-10T14:00:00Z" // Friday 2pm (peak activity)
  },
  "estimatedTotalReach": 125000,
  "projectedConversions": 85, // Based on historical 0.068% average conversion
  "confidenceScore": 0.82
}
```

#### 11.4.3 AI Agent Architecture (Conceptual)

```
Route4.Outreach.AI (Separate service or Azure Function)
├── Input: CastingCall + OutreachCommunity[] (from directory)
├── Step 1: Semantic Matching
│   ├── Embed casting call (genre, tone, location) via OpenAI embeddings
│   ├── Embed each community's metadata (genres, locations, tags)
│   ├── Compute cosine similarity scores
│   └── Rank communities by fit
├── Step 2: Rule-Based Filtering
│   ├── Exclude: communities contacted <7 days ago (rate limit)
│   ├── Exclude: communities with conversion rate <2% (poor performers)
│   ├── Prefer: communities with historical conversions >5
│   └── Respect: MaxContactsPerWeek config (default 3 Script channels)
├── Step 3: Script Generation
│   ├── For each selected community:
│   │   ├── Load community.PostingRules + community.ComplianceNotes
│   │   ├── Prompt GPT-4: "Generate casting call post for {community.Name}. Tone: {community.Tone inferred from Type}. Rules: {PostingRules}. Content: {CastingCall}."
│   │   └── Store generated script in Scripts{}
├── Step 4: Scheduling Optimization
│   ├── Load community.HistoricalPostTimes (if tracked)
│   ├── Default: Weekday mornings 10am-12pm for professional (FilmCommission), Fri evenings for casual (Reddit)
│   └── Spread posts across 2 weeks (avoid spamming all at once)
└── Output: JSON with curatedCommunities[], scripts{}, postingSchedule{}
```

**Human Review Step (Tier B Level 1):**
- Route4 staff reviews AI output before delivery to creator
- Checks: No hallucinated communities, scripts are authentic, compliance accurate
- Adjusts: Removes any risky communities, fixes tone mismatches
- Approves: Delivers final campaign plan to creator or executes (Level 2/3)

---

### 11.5 Phase III — Execution (Auto vs Script)

#### 11.5.1 Auto Channel Execution (API-Driven)

**Discord (Route4 Servers)**

```csharp
// DiscordBotService.PostCastingCallAsync
public async Task PostCastingCallAsync(Guid communityId, string message, string shortUrl)
{
    var community = await _context.OutreachCommunities.FindAsync(communityId);
    if (community.Type != "DiscordServer" || community.Channel != "Auto")
        throw new InvalidOperationException("Not an auto Discord channel");
    
    var channelId = ulong.Parse(community.ApiEndpoint); // Stored as Discord channel ID
    var channel = _discordClient.GetChannel(channelId) as IMessageChannel;
    
    var sentMessage = await channel.SendMessageAsync($"{message}\n\n{shortUrl}");
    
    // Log contact
    var contact = new OutreachContact {
        CommunityId = communityId,
        Channel = "Auto",
        Method = "Discord",
        Status = "Sent",
        ShortUrlVariant = shortUrl,
        MessageId = sentMessage.Id.ToString(),
        SentAt = DateTime.UtcNow
    };
    await _context.OutreachContacts.AddAsync(contact);
    await _context.SaveChangesAsync();
}
```

**Email (Opt-In List)**

```csharp
// EmailService.SendCastingCallEmailAsync
public async Task SendCastingCallEmailAsync(string[] recipients, string subject, string body, string shortUrl)
{
    foreach (var email in recipients)
    {
        var message = new SendGrid.Helpers.Mail.SendGridMessage();
        message.SetFrom(new EmailAddress("casting@route4.studio", "Route4 Casting"));
        message.AddTo(email);
        message.SetSubject(subject);
        message.AddContent(MimeType.Html, $"{body}<br/><br/><a href='{shortUrl}'>Apply Here</a>");
        
        var response = await _sendGridClient.SendEmailAsync(message);
        
        // Log contact
        var contact = new OutreachContact {
            CommunityId = /* lookup email list community */,
            Channel = "Auto",
            Method = "Email",
            Status = response.IsSuccessStatusCode ? "Sent" : "Failed",
            ShortUrlVariant = shortUrl,
            MessageId = response.Headers.GetValues("X-Message-Id").FirstOrDefault(),
            SentAt = DateTime.UtcNow,
            ErrorMessage = response.IsSuccessStatusCode ? null : await response.Body.ReadAsStringAsync()
        };
        await _context.OutreachContacts.AddAsync(contact);
    }
    await _context.SaveChangesAsync();
}
```

**SMS (Opt-In Talent List)**

```csharp
// SMSService.SendCastingCallSMSAsync
public async Task SendCastingCallSMSAsync(string[] phoneNumbers, string message, string shortUrl)
{
    foreach (var phone in phoneNumbers)
    {
        var smsMessage = await _twilioClient.Messages.CreateAsync(
            to: new PhoneNumber(phone),
            from: new PhoneNumber("+14405551234"), // Route4 Twilio number
            body: $"{message}\n{shortUrl}"
        );
        
        // Log contact
        var contact = new OutreachContact {
            CommunityId = /* lookup SMS list community */,
            Channel = "Auto",
            Method = "SMS",
            Status = smsMessage.Status == MessageResource.StatusEnum.Sent ? "Sent" : "Failed",
            ShortUrlVariant = shortUrl,
            MessageId = smsMessage.Sid,
            SentAt = DateTime.UtcNow,
            ErrorMessage = smsMessage.ErrorMessage
        };
        await _context.OutreachContacts.AddAsync(contact);
    }
    await _context.SaveChangesAsync();
}
```

#### 11.5.2 Script Channel Execution (Human-Assisted)

**Backstage Post Pack (Deliverable to Creator)**

```json
{
  "community": "Backstage",
  "channel": "Script",
  "instructions": [
    "1. Log in to Backstage.com",
    "2. Navigate to 'Post a Job' (https://www.backstage.com/casting/post/)",
    "3. Fill form using payload below",
    "4. Click 'Preview' → 'Publish'",
    "5. Copy published URL and paste into Route4 Outreach Contact log"
  ],
  "payload": {
    "jobTitle": "Cinematographer - Indie Drama (Cleveland, OH)",
    "projectType": "Film",
    "productionType": "Independent",
    "description": "Route4 Studios is seeking a talented cinematographer for 'Making of MARY,' an intimate character-driven drama...\n\n[Full AI-generated copy]",
    "compensation": "Deferred + backend points",
    "location": "Cleveland, OH",
    "startDate": "2026-02-10",
    "endDate": "2026-02-28",
    "applyUrl": "https://route4.studio/mary/c/invite?utm_source=backstage&utm_medium=job_post&utm_campaign=S1E1"
  },
  "complianceChecklist": [
    "✅ No offensive language",
    "✅ Compensation clearly stated",
    "✅ Project name included",
    "✅ Apply URL is Route4 short link (not external redirect)"
  ],
  "shortUrlVariant": "https://route4.studio/mary/c/invite?utm_source=backstage&utm_medium=job_post&utm_campaign=S1E1",
  "logInstructions": "After posting, go to Route4 → Outreach → Log Contact. Enter: Community=Backstage, Status=Sent, PostUrl=[paste Backstage job URL]"
}
```

**Film Commission Form-Filler Pack (Chrome Extension)**

```json
{
  "community": "Greater Cleveland Film Commission",
  "channel": "Script",
  "formUrl": "https://clevelandfilm.com/resources/post-opportunity",
  "fillPlan": {
    "#opportunity-title": "Casting Call: 'Making of MARY' - Cleveland Indie Drama",
    "textarea[name='description']": "[AI-generated description, 500 chars]",
    "#email": "casting@route4.studio",
    "#phone": "440-555-1234",
    "select[name='type']": "Casting",
    "#deadline": "2026-01-31",
    "input[name='website']": "https://route4.studio/mary/c/invite?utm_source=cleveland_film_comm&utm_medium=form&utm_campaign=S1E1"
  },
  "instructions": [
    "1. Install Route4 Outreach Helper (Chrome extension)",
    "2. Navigate to form URL",
    "3. Click extension icon → 'Apply Fill Plan'",
    "4. Review pre-filled fields (edit if needed)",
    "5. Complete any captcha/verification",
    "6. Click 'Submit'",
    "7. Extension auto-logs contact in Route4 (Status=Sent)"
  ],
  "compliance": "Requires approval (expect 48hr review). No follow-up needed."
}
```

#### 11.5.3 Chrome Extension Architecture (Form-Filler)

**Manifest (manifest.json)**

```json
{
  "manifest_version": 3,
  "name": "Route4 Outreach Helper",
  "version": "1.0.0",
  "permissions": ["activeTab", "storage"],
  "host_permissions": ["https://route4.studio/*"],
  "background": {
    "service_worker": "background.js"
  },
  "content_scripts": [{
    "matches": ["<all_urls>"],
    "js": ["content.js"]
  }],
  "action": {
    "default_popup": "popup.html",
    "default_icon": "icon.png"
  }
}
```

**Content Script (content.js)**

```javascript
// Listens for fill plan from extension popup
chrome.runtime.onMessage.addListener((request, sender, sendResponse) => {
  if (request.action === 'fillForm') {
    const fillPlan = request.fillPlan;
    
    for (const [selector, value] of Object.entries(fillPlan)) {
      const field = document.querySelector(selector);
      if (field) {
        if (field.tagName === 'SELECT') {
          const option = Array.from(field.options).find(o => o.text === value || o.value === value);
          if (option) field.value = option.value;
        } else if (field.tagName === 'TEXTAREA' || field.tagName === 'INPUT') {
          field.value = value;
          field.dispatchEvent(new Event('input', { bubbles: true })); // Trigger validation
        }
      }
    }
    
    sendResponse({ success: true, filledCount: Object.keys(fillPlan).length });
  }
});
```

**Popup UI (popup.html + popup.js)**

```javascript
// Fetch fill plan from Route4 API based on current URL
async function loadFillPlan() {
  const currentUrl = await getCurrentTabUrl();
  const response = await fetch(`https://route4.studio/api/outreach/fillplan?url=${encodeURIComponent(currentUrl)}`, {
    headers: { 'Authorization': `Bearer ${userToken}` }
  });
  
  if (response.ok) {
    const plan = await response.json();
    document.getElementById('community-name').textContent = plan.community;
    document.getElementById('apply-btn').onclick = () => applyFillPlan(plan.fillPlan);
  } else {
    document.getElementById('status').textContent = 'No fill plan for this page';
  }
}

function applyFillPlan(fillPlan) {
  chrome.tabs.query({ active: true, currentWindow: true }, (tabs) => {
    chrome.tabs.sendMessage(tabs[0].id, { action: 'fillForm', fillPlan }, (response) => {
      if (response.success) {
        document.getElementById('status').textContent = `✅ Filled ${response.filledCount} fields. Review and submit!`;
        logContact(fillPlan.communityId, tabs[0].url); // Auto-log to Route4
      }
    });
  });
}

async function logContact(communityId, postUrl) {
  await fetch('https://route4.studio/api/outreach/contacts', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${userToken}` },
    body: JSON.stringify({
      communityId,
      channel: 'Script',
      method: 'FilmCommission',
      status: 'Sent',
      postUrl,
      sentAt: new Date().toISOString()
    })
  });
}
```

---

### 11.6 Feedback Loop & Conversion Tracking

#### 11.6.1 Click Attribution (Already Implemented in ShortUrlController)

```
User clicks: https://route4.studio/mary/c/invite?utm_source=backstage&utm_campaign=S1E1
  ↓
ShortUrlController.RedirectShortUrl() extracts utm_source=backstage
  ↓
Finds OutreachContact with ShortUrlVariant matching "utm_source=backstage"
  ↓
Updates: ClickedAt, Status=Clicked, TotalClicks++
  ↓
Redirects to: https://route4.studio/making-of-mary/casting (actual landing page)
```

#### 11.6.2 Conversion Tracking (Witness Registration)

```
User submits witness registration form (or joins Discord via invite)
  ↓
WitnessEvent created with ReferralSource = ShortUrl.Code
  ↓
Background job (daily):
  ├── Find OutreachContacts where Status=Clicked
  ├── Check if WitnessEvent exists with matching ShortUrl code
  ├── If match: Update OutreachContact.ConvertedAt, Status=Converted, DidConvert=true
  └── Update OutreachCommunity.SuccessfulConversions++, recalculate ConversionRate
```

**WitnessEvent Enhancement:**

```csharp
public class WitnessEvent
{
    public Guid Id { get; set; }
    // ... existing fields
    
    public string? ReferralSource { get; set; } // ShortUrl code (e.g., "mary/c/invite")
    public string? UtmSource { get; set; } // Extracted from query params
    public string? UtmCampaign { get; set; }
    public Guid? OutreachContactId { get; set; } // FK → OutreachContact (if matched)
}
```

#### 11.6.3 Performance Dashboard (Voltron UI)

**Campaign Performance Card:**

```
Making of MARY - S1E1 Casting Call Campaign
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Status: Active | Started: Jan 5, 2026 | Days Running: 3

Target: 100 witnesses | Current: 12 witnesses | Progress: 12%

Outreach Summary:
├── Total Contacts: 35
├── Auto Channels: 15 (Discord: 5, Email: 10)
├── Script Channels: 20 (Backstage: 5, Film Commissions: 10, Reddit: 5)
├── Pending: 8 (scheduled for next 7 days)
└── Failed: 2 (retry scheduled)

Performance:
├── Total Clicks: 142
├── Conversion Rate: 8.5% (12 witnesses / 142 clicks)
├── Cost Per Witness: $4.17 (assuming $50 Gate 1 / 12 witnesses)
└── Projected Final Witnesses: 98 (based on current trajectory)

Top Performing Communities:
1. Greater Cleveland Film Commission (5 witnesses, 18% conversion)
2. r/Ohio_Filmmakers (3 witnesses, 12% conversion)
3. Backstage Cincinnati (2 witnesses, 8% conversion)

Recommended Actions:
⚠️ Increase outreach to Greater Cleveland Film Comm (high performer)
✅ Continue current pace (on track to hit target by Jan 20)
```

---

### 11.7 Implementation Roadmap

**Phase I — Directory & Tracking (Weeks 1-4)**
- [ ] Week 1: Data models, migrations, API endpoints
- [ ] Week 2: Seed 50 communities (manual curation)
- [ ] Week 3: Admin UI (CRUD for communities, contact log viewer)
- [ ] Week 4: UTM tracking integration, first test campaign

**Phase II — AI Targeting (Weeks 5-8)**
- [ ] Week 5: AI agent architecture (embeddings + rule-based filtering)
- [ ] Week 6: Script generation (GPT-4 integration with community rules)
- [ ] Week 7: Human review workflow (Tier B Level 1 approval UI)
- [ ] Week 8: End-to-end test (AI curates → human reviews → campaign approved)

**Phase III — Execution (Weeks 9-12)**
- [ ] Week 9: Auto channels (Discord, Email, SMS integrations)
- [ ] Week 10: Chrome extension (form-filler for Script channels)
- [ ] Week 11: Feedback loop (click → conversion tracking automation)
- [ ] Week 12: Performance dashboard + analytics

**Maintenance & Optimization (Ongoing)**
- [ ] Monthly: Review community performance, disable low performers
- [ ] Quarterly: Expand directory (add 25 new communities per quarter)
- [ ] Yearly: Retrain AI embeddings with actual conversion data

---

### 11.8 Success Metrics

**Phase I Goals:**
- Directory: 50 communities documented with complete metadata
- Contact Tracking: 100% of outreach attempts logged (auto + script)
- UTM Attribution: 95%+ of clicks correctly attributed to source

**Phase II Goals:**
- AI Curation Accuracy: 80%+ of AI-selected communities rated "good fit" by human reviewers
- Script Quality: 90%+ of AI-generated scripts require <10% human editing
- Time Savings: Reduce campaign setup time from 8 hours (manual) to 1 hour (AI-assisted)

**Phase III Goals:**
- Auto Channel Success: 100% of Discord/Email/SMS sends logged with messageId
- Script Channel Adoption: 70%+ of creators use form-filler extension (vs manual entry)
- Conversion Rate: 5–10% average (witnesses recruited / total clicks)
- Cost Per Witness: <$10 (total outreach cost / witnesses recruited)

**Overall Campaign Goal (Tier B):**
- Reach 100 witnesses per casting call within 30 days
- Maintain <20% spam/ban rate (communities blocking Route4)
- Achieve 15%+ creator retention (creators use Route4 for Episode 2+)
- Year 1 platform earnings: $2,500 (premium positioning from Route4 presence)
- Assuming 25% annual growth (improved by managed outreach): $8,200 cumulative
- Route4 earns (15%): $1,230 over 5 years
- Creator earns (85%): $6,970 over 5 years
- Both benefit from accelerated growth through managed audience building

---

#### Tier Selection Framework

| Factor | Tier A (Submission) | Tier B (Partnership + Outreach) |
|--------|-------------------|----------------------------------|
| **Configuration Timing** | During onboarding | During onboarding |
| **Execution** | Automatic | Automatic (outreach + distribution) |
| **Upfront Cost** | $250 | $0 |
| **Outreach Management** | Creator handles independently | Route4 AI Business Manager (included) |
| **Distribution Management** | Creator | Route4 |
| **Best Fit** | One-off releases | Series + recurring (with audience building) |
| **Platform Complexity** | High (DIY) | Low (Route4 handles all) |
| **Financial Incentive** | None | Aligned (mutual growth + outreach ROI) |
| **Creator Control** | Full independence | Collaborative (execution autonomous) |

---

#### 10.7.1 Automatic Execution Example

**Making of MARY - Episode 1 Completion:**

```
Background Job (runs every 6 hours):
   ↓
Query: SELECT * FROM ReleaseInstances WHERE State = 'ARCHIVE'
   ↓
Found: "making-of-mary-s1e1"
   ↓
Check conditions:
  ✓ ritualAttestations >= 100 (actual: 142 documented witness participations)
  ✓ revenueSignals >= 1000 (actual: $3,200)
  ✓ canonSealed = true
  ✓ client.distributionEnabled = true
  ✓ client.distributionTier = "TierB"
   ↓
Execute: ReleaseManagementService.TriggerFinalDistribution()
   ↓
Call Filmhub API:
  POST /api/submissions
  {
    "title": "Making of MARY - S1E1",
    "description": "...",
    "posterUrl": "...",
    "videoUrl": "...",
    "releaseDate": "2026-01-04",
    "genre": ["Drama", "Indie"],
    "metadata": {
      "createdVia": "Route4",
      "ritualAttestations": 142,
      "revenue": 3200.00
    }
  }
   ↓
Filmhub Response: { "submissionId": "abc123", "status": "pending" }
   ↓
Route4 Updates Database:
  releaseInstance.distributionStatus = "SUBMITTED"
  releaseInstance.filmhubSubmissionId = "abc123"
   ↓
Route4 Notifies Creator (email + Discord announcement):
  "Making of MARY - S1E1 has been automatically submitted to Filmhub.
   Expected platforms: Tubi, Pluto, Roku, Amazon Freevee.
   Timeline: 4-6 weeks for approval + distribution.
   You will receive quarterly earnings reports starting Q2 2026."
   ↓
6 weeks later:
  Filmhub approves → Film goes live on all platforms
   ↓
Route4 receives webhook from Filmhub:
  { "submissionId": "abc123", "status": "live", "platforms": ["Tubi", "Pluto", "Roku", "Freevee"] }
   ↓
Route4 Updates Database:
  releaseInstance.distributionStatus = "LIVE"
   ↓
Route4 Notifies Creator:
  "Making of MARY - S1E1 is now live on Tubi, Pluto, Roku, and Amazon Freevee.
   Direct audience earned. Distribution as outcome."
```

**Creator never approved anything. State + configuration drove execution.**

---

#### 10.7.2 Why This Approach Reaches Bigger Audiences

**Question:** How does Mary reach beyond 100 witnesses?

**Answer:** Route4 automatically escalates her to wider distribution when she's proven success.

**Traditional Model (Approval-Based):**
- Creator completes rituals
- Route4 asks: "Would you like to distribute?"
- Creator must decide (dependent on creator action)
- Distribution delayed or never happens

**Route4 Model (Non-Dependent):**
- Creator configures distribution tier during onboarding
- Creator completes rituals
- System detects: ARCHIVE + thresholds met
- System executes: Auto-submit to Filmhub (no approval needed)
- 6 weeks later: Film live on Tubi, Pluto, Roku (500K potential viewers)

**Mary's film reaches 50K+ viewers without her making a single distribution decision.**

---

#### 10.7.3 Premium Distribution Escalation (Future State)

When Tier B creators hit revenue thresholds, Route4 automatically escalates to premium platforms:

| Revenue Threshold | Auto-Escalation Target | Trigger Logic |
|-------------------|------------------------|---------------|
| $10K cumulative | Amazon Prime Video | Quality bar met + consistent earnings |
| $25K cumulative | Apple TV | Premium audience fit + track record |
| $50K cumulative | HBO Max / Paramount+ | Route4 negotiates bulk catalog deal |

**Creators never pitch platforms. Route4's aggregate success negotiates bulk placement.**

This is Route4's long-term moat: **Route4 becomes the platform that platforms want.**

---

**Key Insight:**
- Distribution is **configured once, executed automatically**
- State + thresholds = inevitable outcome
- No approval gates, no client dependency
- **Non-D principle preserved: state drives behavior, not events**

---

### 10.8 The Perfect Route4 Non-D Automation Flow

Release State: DROP → ARCHIVE (Complete)
   ↓
Background Job Checks:
  ✓ Ritual attestations >= threshold (e.g., 100+ documented witness participations)
  ✓ Revenue signals detected (Stripe payments recorded)
  ✓ Archive immutable (canon sealed)
   ↓
Route4MediaPipelineService.TriggerFinalDistribution()
   ↓
Call Filmhub API:
  POST /api/submissions
  {
    "title": "Making of MARY - S1E1",
    "description": "...",
    "posterUrl": "...",
    "videoUrl": "...", (stored in Frame.io, served via CDN)
    "releaseDate": "2026-01-04",
    "genre": ["Drama", "Indie"],
    "metadata": {
      "createdVia": "Route4",
      "ritualAttestations": 142,
      "revenue": 3200.00
    }
  }
   ↓
Filmhub Auto-Distributes:
  • Tubi ✓
  • Pluto ✓
  • Roku ✓
  • Amazon Freevee ✓
   ↓
Route4 Notifies Creator:
  "Your work is now live on Tubi, Pluto, Roku, and Amazon Freevee.
   Direct audience earned. Distribution as outcome."
```

**Why This Is Powerful:**

✅ **Completely automatable** — No human gatekeeping  
✅ **Scales to all clients** — Filmhub works the same for everyone  
✅ **Genuine value to creators** — Real reach, real audience  
✅ **Proves Route4's thesis** — Restraint → presence → distribution  
✅ **Repeatable success story** — Mary → next client → next  
✅ **Non-D compliant** — State drives behavior, not events  

---

### 10.9 Future State: Route4 Originals (Premium Curation Layer)

**Strategic Vision:**

Route4's ultimate moat is not distributing films. It's becoming **the curation layer that platforms want to carry.**

When Route4 has 20-30 releases live on Tubi with 50K+ views each, platforms compete to feature Route4 releases. Tubi calls Route4, not the other way around.

#### 10.9.1 Route4 Certified Collection (Tubi Partnership)

**Concept:**
Instead of building a standalone streaming app, Route4 creates a curated collection on existing platforms (Tubi, Pluto, Roku).

**What This Looks Like:**

| Element | Description |
|---------|-------------|
| **Route4 Certified Collection** | Dedicated shelf on Tubi: "Route4 Releases—Ritual-Credible Work" |
| **Ritual Journey Documentation** | Casting call → witness process → Drop → distribution (shown alongside film) |
| **Seal of Quality** | "Route4 Certified" badge signals discipline, restraint, authentic presence |
| **Narrative Framing** | Tubi audience sees how the work was made—ritual as artistic value |
| **Infrastructure** | Filmhub/Tubi handle all streaming, licensing, payment—Route4 owns curation |

**Why This Wins:**
- Platforms compete to feature Route4 releases (brand value)
- Route4 controls narrative (ritual as artistic integrity)
- Zero infrastructure burden
- 100% scalable (can curate infinitely)
- Higher positioning than generic aggregation

---

#### 10.9.2 Route4 Originals: Documentary Series (Premium Tier)

**Concept:**
Route4 funds documentaries that capture the ritual journey itself. These become premium Route4 Originals that showcase the process, not just the outcome.

**Funding Model:**
- 100% funded by Route4 production team
- No creator upfront cost
- Shared revenue: Route4 recovers costs + 50% surplus to creator

**The Documentary Structure:**

| Ritual Stage | Documentary Focus | Casting Call Roles (Filled via Route4) |
|--------------|-------------------|---------------------------------------|
| **Signal I (Casting Call)** | "The Invitation" | Lead documentary filmmaker, 2nd camera |
| **Table Read (Process)** | "First Ritual" | Audio engineer, director of photography |
| **Writing Table (Process)** | "Decisions" | Editor (capturing discussion), sound design |
| **Shot Council (Process)** | "Vision Translation" | Cinematographer (documenting discussion), color specialist |
| **The Hold** | "Silence" | Ambient sound recordist, visual archives curator |
| **Signal II (Anonymous Drop)** | "Discovery Without Authorship" | Music supervisor (original score for doc) |
| **The Drop** | "Canon Sealed" | Archive specialist, legal reviewer (doc compliance) |
| **The Echo** | "Reflection" | Interview director, post-production supervisor |
| **Archive** | "Permanence" | Documentary final editor, metadata specialist |

**Example: "Making of MARY - Documentary Series"**

**Episode 1: "Casting Call"** (30 min)
- Route4 funds: Documentary director ($2K), cinematographer ($1K), editor ($1K)
- Captures Mary's team creating Signal I
- Shows who answers the call + why
- Frames witness recruitment as *community formation*

**Episode 2: "The Table"** (45 min)
- Route4 funds: Audio engineer ($1K), director of photography ($1.5K)
- Captures craft discussion at Table Read
- Documents how decisions are made
- Shows witness participation as *credibility building*

**Episode 3: "The Drop"** (60 min)
- Route4 funds: Cinematographer ($2K), editor ($2K)
- Captures primary release + witness reaction
- Documents canon sealing
- Frames publication as *irreversible commitment*

**Episode 4: "The Archive"** (45 min)
- Route4 funds: Archive specialist ($1K), final editor ($1.5K)
- Shows immutability + permanent record
- Frames Route4 as *protector of integrity*
- Connects ritual to distribution outcome

**Total Investment:** ~$15K per creator series (4 episodes)

**Revenue Model:**
- Tubi views on documentary series
- Route4 Originals collection premium positioning
- YouTube channel (behind-the-scenes clips)
- Patreon/membership (full episode access)

---

#### 10.9.3 Why Route4 Originals Matters

**Strategic Value:**

✅ **Creates premium content about content creation** — The ritual itself becomes the story  
✅ **Proves Route4's thesis** — Ritual discipline is credible, audience can see why  
✅ **Positions Route4 as brand** — Not a platform, a curator/credibility marker  
✅ **Increases creator visibility** — Documentary + film = 2x platform presence  
✅ **Attracts premium creators** — "Route4 will fund a documentary about your process"  
✅ **Platforms want this** — Tubi/Pluto compete for Route4 Original documentaries  

**Non-D Compliance:**

The documentary crew roles are integrated into the casting call itself. Creators aren't "being filmed"—they're hiring documentary professionals as part of their crew. This maintains restraint while creating authentic narrative.

**Creator Economics:**

- Creator: 50% of documentary streaming revenue (after Route4 cost recovery)
- Route4: 50% of revenue + positioning as curator
- Both benefit from combined film + documentary visibility

---

#### 10.9.4 Implementation Timeline

**Phase 1 (Now):** Filmhub distribution (Tubi, Pluto, Roku)  
**Phase 2 (When 5-10 releases live):** Negotiate "Route4 Collection" on Tubi  
**Phase 3 (When brand established):** Launch Route4 Originals with first documentary series  
**Phase 4 (Long-term):** Premium curation layer—platforms compete to feature Route4 releases  

---

## 11. Implementation Roadmap

### 11.1 Phase 1: Core Platform
- [ ] ReleaseCycleTemplate model
- [ ] ReleaseInstance state machine
- [ ] Visibility level enforcement
- [ ] WitnessEvent tracking
- [ ] SQL Server migration

### 11.2 Phase 2: Discord Integration
- [ ] Channel template provisioning
- [ ] Role creation and assignment
- [ ] Lock/unlock automation
- [ ] Invite link lifecycle management

### 11.3 Phase 3: Frontend Surface
- [ ] Signal view (anonymous artifact)
- [ ] Release view (episode/primary release)
- [ ] Archive (read-only)
- [ ] Invitations (private viewing)

### 11.4 Phase 4: Admin Dashboard
- [ ] Create/schedule releases
- [ ] Advance release stages
- [ ] Declare canon
- [ ] Issue invitations

### 11.5 Phase 5: Media Pipeline
- [ ] FFmpeg integration
- [ ] Frame.io adapter
- [ ] Artifact generation
- [ ] Asset versioning

---

## 12. Development & Operations

### 12.1 Local Development Setup

#### Backend
```powershell
# Terminal 1 - Navigate to project root
dotnet run --project Route4MoviePlug.Api.csproj

# Listens on http://localhost:5158
# Swagger UI at http://localhost:5158/
# Health check at http://localhost:5158/
```

**Requirements:**
- .NET 9 SDK
- SQL Server (local or configured connection string in appsettings.Development.json)
- Connection string: `DefaultConnection` in appsettings

#### Frontend
```powershell
# Terminal 2 - Navigate to ClientApp directory
cd ClientApp
npm install
ng serve --port 4200

# Listens on http://localhost:4200
# Auto-recompiles on file changes
# Proxy forwards /api requests to http://localhost:5158
```

**Requirements:**
- Node.js 20+
- Angular CLI 19
- npm or yarn

#### Accessing the Application
- **Public Splash Page**: http://localhost:4200/
- **Admin Dashboard**: http://localhost:4200/admin
- **API Documentation**: http://localhost:5158/
- **API Proxy**: Frontend `/api` requests auto-forward to backend

### 12.2 API Endpoints by Ritual

#### Signal I - Casting Call
```
POST /api/clients/{clientSlug}/castingcall
└─ Create casting call invitation (Signal I publication)

GET /api/clients/{clientSlug}/castingcall
└─ Retrieve active casting call (public view)

GET /api/clients/{clientSlug}/castingcall/active
└─ Check if casting call is active
```

#### Process Rituals (Table Read, Writing Table, Shot Council)
```
POST /api/clients/{clientSlug}/castingcall/responses
└─ Accept casting call responses (intake without noise)

GET /api/clients/{clientSlug}/witness/events
└─ Track witnessed ritual participation
```

#### Release Management
```
POST /api/clients/{clientSlug}/releases
└─ Create release instance (DRAFT state)

PUT /api/clients/{clientSlug}/releases/{releaseKey}/state
└─ Advance release state (SIGNAL_I → PROCESS → HOLD → SIGNAL_II → DROP → ECHO → ARCHIVE)

GET /api/clients/{clientSlug}/releases/{releaseKey}
└─ Retrieve release details and visibility level
```

#### Short URLs (Trackable Invitations)
```
POST /api/shorturl/create
Body: { "clientSlug": "making-of-mary", "releaseKey": "S1E1", "ritual": "casting_call" }
Response: { "code": "mary/c/invite", "shortUrl": "https://rt4.io/mary/c/invite" }

GET /shorturl/{code}
└─ Redirect to canonical Route4 URL (with referrer tracking)
```

#### Witness Verification
```
GET /api/clients/{clientSlug}/witness/me
└─ Verify current user's witness status

POST /api/clients/{clientSlug}/witness/verify
Body: { "invitationCode": "..." }
Response: Witness status and archive access grants
```

#### Admin/Voltron Only
```
POST /api/clients/{clientSlug}/releases/{releaseKey}/publish
└─ Publish release (Canon seal)

POST /api/clients/{clientSlug}/discord/provision
└─ Provision Discord channels for ritual

GET /api/clients/{clientSlug}/audit
└─ Audit trail of all state transitions and presence events
```

### 12.3 Asset Management

#### Client Assets Folder Structure
```
ClientApp/public/assets/clients/
└── {clientSlug}/
    ├── cover.jpg               (Splash page hero image)
    ├── cover.svg               (Fallback vector)
    ├── logo.svg                (Client branding)
    └── fragments/              (L2 Fragment assets)
        ├── fragment_001.mp4
        └── fragment_002.mp4
```

#### Media Provider (Frame.io) Folder Structure
```
route4-{clientSlug}/
└── {releaseKey}/
    ├── 00-raw/                 (L0 - Core team only)
    │   └── S1E1_master.mp4
    ├── 10-process/             (L1 - Witnessed process)
    │   ├── S1E1__process-writing__process_preview__v1.mp4
    │   └── S1E1__shot-council__still_set__v1.jpg
    ├── 20-fragments/           (L2 - Public residue)
    │   ├── S1E1__fragment__fragment__v1.mp4
    │   └── S1E1__bts__still__v1.jpg
    ├── 30-release/             (L3 - Public release)
    │   ├── S1E1__drop__release_public__v1.mp4
    │   └── S1E1__drop__thumbnail__v1.jpg
    └── 40-archive/             (L3 - Permanent)
        └── S1E1__archive__master__v1.mp4
```

#### Asset Naming Convention
`{releaseKey}__{stage}__{kind}__v{n}.{ext}`

**Examples:**
- `S1E1__process-writing__process_preview__v1.mp4` (L1 preview)
- `S1E1__drop__release_public__v1.mp4` (L3 public)
- `S1E1__archive__master__v1.mp4` (L3 archive master)

#### Asset Access & Visibility
```
L0 Assets → Voltron only (no CDN, local storage only)
L1 Assets → Witness-only (private CDN with time-limited links)
L2 Assets → Public fragments (public CDN, no context)
L3 Assets → Public release (public CDN, full story context)
```

### 12.4 Deployment Considerations

#### Development Environment
- **Frontend Build**: `ng serve` (unminified, source maps)
- **Backend**: `dotnet run` (debug configuration)
- **Database**: SQL Server (local or configured connection string)
- **Assets**: Static serving from `ClientApp/public/assets`
- **SSR**: Disabled (ng serve uses client-side rendering)

#### Production Environment
- **Frontend Build**: `ng build --configuration production`
  - Output: `dist/client-app/browser` (static files)
  - Deployment: Azure Blob Storage + CDN or S3 + CloudFront
  - SSR: Enable with Node.js server deployment
- **Backend**: `dotnet publish --configuration Release`
  - Deployment: Azure App Service or similar
  - Instance scaling: Horizontal (state-less API design)
- **Database**: Azure SQL Database or AWS RDS
  - Connection string: Environment variable via deployment pipeline
  - Automatic backups: 35-day retention
- **Assets**: 
  - Client cover images → CDN (e.g., Azure CDN)
  - Media artifacts → Frame.io (delegated provider)
- **Security**:
  - SSL/TLS: Enforce HTTPS only
  - CORS: Restrict to production domain(s)
  - Database encryption: Enable Transparent Data Encryption (TDE)

#### CI/CD Pipeline (Recommended)
```
GitHub/Azure DevOps
  ↓
Build Backend (.NET)
  ↓
Build Frontend (Angular SSR)
  ↓
Run Tests
  ↓
Deploy to Staging (appsettings.Staging.json)
  ↓
Smoke Tests
  ↓
Deploy to Production
  ├─ Backend: App Service
  ├─ Frontend: Static hosting + CDN
  └─ Database: SQL Server managed service
```

---

## 13. Quality Assurance & Constraints

### 13.1 Engineering Constraints
- Every feature must map to a ritual or visibility level
- No premature abstraction
- Configuration over customization
- State drives UI, not vice versa

### 12.2 Content Protection Rules
- Story leakage prevention is non-negotiable
- Authorship revealed last, always
- Canon sealed means immutable
- Witness status cannot be gamed

### 12.3 AI Assistant Guidelines
When using AI assistants, reject suggestions that:
- Add engagement mechanics
- Expose story prematurely
- Gamify participation
- Collapse platform/client separation

---

## 14. Project File Mapping (Implementation Guide)

This section maps Route4 design concepts to actual codebase locations.

### 14.1 Backend Files by Concern

#### Multi-Tenancy & State Machine
```
Controllers/
├── ClientsController.cs
│   └─ Enforces multi-tenant data isolation
│   └─ GET /api/clients/{clientSlug} (scoped to tenant)
│
├── ReleaseManagementController.cs
│   └─ Implements release state machine (DRAFT → DROP → ARCHIVE)
│   └─ PUT /api/clients/{clientSlug}/releases/{releaseKey}/state
│
└── Route4DiscordAdminController.cs
    └─ Orchestrates Discord channel automation per state

Middleware/
└── TenantResolutionMiddleware.cs
    └─ Extracts tenant from subdomain/header/path
    └─ Stores in HttpContext for downstream filtering

Data/
├── Route4DbContext.cs
│   ├─ DbSet<Client> (multi-tenant scope)
│   ├─ DbSet<ReleaseInstance> (state machine)
│   ├─ DbSet<WitnessEvent> (presence tracking)
│   ├─ DbSet<CastingCall> (Signal I invitations)
│   ├─ DbSet<ShortUrl> (trackable links)
│   └─ Seed data: Making of MARY client + sample ritual data
│
└── Migrations/
    └─ 00000000000000_InitialCreate.cs
    └─ All subsequent schema versions
```

#### Ritual & Visibility Enforcement
```
Controllers/
├── CastingCallController.cs
│   └─ Signal I (Casting Call)
│   └─ POST /api/clients/{clientSlug}/castingcall (create invitation)
│   └─ GET /api/clients/{clientSlug}/castingcall (L2 public view)
│
├── VipMembershipController.cs
│   └─ Splash pages (L2 Fragment artifacts)
│   └─ GET /api/clients/{clientSlug}/splashpage (public view)
│
└── PaymentsController.cs
    └─ Care-based threshold pricing
    └─ Gate transitions (witness activation, artifact gen, drop)

Services/
├── ReleaseManagementService.cs
│   └─ State machine orchestration
│   └─ Ritual transition logic (lock channels, open ritual windows)
│   └─ Visibility level enforcement
│
├── DiscordBotService.cs (Real/Mock)
│   └─ Channel provisioning per ritual
│   └─ Role assignment (Witness, Member, Admin)
│   └─ Lock/unlock automation based on state
│   └─ Time-limited invite link generation
│
├── StripePaymentService.cs
│   └─ Gate 1: Witness Activation ($50-$100)
│   └─ Gate 2: Artifact Generation ($10-$50)
│   └─ Gate 3: The Drop ($100-$200)
│   └─ Gate 4: Private Viewing ($50-$100)
│   └─ Gate 5: Distribution ($100-$250)
│
└── MediaProvider.cs
    └─ Frame.io abstraction layer
    └─ Asset versioning ({releaseKey}__{stage}__{kind}__v{n})
    └─ Visibility enforcement per folder (L0→L3)
```

#### Presence & Audit
```
Models/
├── WitnessEvent.cs
│   ├─ UserId
│   ├─ RitualName (Table Read, Shot Council, etc.)
│   ├─ ClientSlug
│   ├─ Timestamp
│   └─ Presence acknowledged quietly (no gamification)
│
├── ReleaseStateTransition.cs
│   ├─ From state (DRAFT, SIGNAL_I, etc.)
│   ├─ To state (next ritual)
│   ├─ Timestamp
│   └─ ChangedBy (Voltron actor)
│
└── ShortUrl.cs
    ├─ Code (e.g., "mary/c/invite")
    ├─ FullUrl (canonical Route4 URL)
    ├─ ReferrerTracking (no metrics exposure)
    └─ ExpiresAt (revocable links)
```

### 14.2 Frontend Files by Visibility Level

#### L2 Public (Fragments, Signal I)
```
src/app/pages/
├── splash-page/
│   ├─ splash-page.component.ts
│   │  └─ Loads public splash page data
│   │  └─ GET /api/clients/{clientSlug}/splashpage
│   ├─ splash-page.component.html
│   │  └─ Displays title, subtitle, benefits (no story context)
│   └─ splash-page.component.scss
│      └─ Cinematic styling (gradient text, glass-morphism)
│
└── casting-call/
    ├─ casting-call.component.ts
    │  └─ Displays Signal I (casting call invitation)
    │  └─ GET /api/clients/{clientSlug}/castingcall
    └─ casting-call.component.html
       └─ Shows tone, constraints, intake form (no character details)
```

#### L3 Public (Release, Archive, Echo)
```
src/app/pages/
├── release/
│   ├─ release.component.ts
│   │  └─ Loads release (The Drop)
│   │  └─ GET /api/clients/{clientSlug}/releases/{releaseKey}
│   │  └─ Display rules: 24-hour read-only window after canon seal
│   └─ release.component.html
│      └─ Full story context (episodes, credits locked)
│
├── archive/
│   ├─ archive.component.ts
│   │  └─ Permanent record access (witness + public)
│   │  └─ GET /api/clients/{clientSlug}/archive
│   └─ archive.component.html
│      └─ All L3 artifacts immutable, versioned
│
└── echo/
    ├─ echo.component.ts
    │  └─ Reflection without spoilers
    │  └─ Delayed opening after Drop
    └─ echo.component.html
       └─ Discussion channels (mixed witness/public)
```

#### L1 Witnessed Process (Admin)
```
src/app/pages/admin/
├── admin-dashboard/
│   ├─ admin-dashboard.component.ts
│   │  └─ Quick-action cards for Voltron
│   │  └─ "Create Release", "Advance State", "Provision Discord"
│   └─ admin-dashboard.component.html
│      └─ Read-only summary of ritual schedule
│
├── release-editor/
│   ├─ release-editor.component.ts
│   │  └─ Voltron creates/edits release instances
│   │  └─ POST /api/clients/{clientSlug}/releases (DRAFT state)
│   │  └─ PUT /api/clients/{clientSlug}/releases/{releaseKey} (edit before publish)
│   └─ release-editor.component.html
│      └─ Form: title, description, ritual schedule
│
├── witness-management/
│   ├─ witness-management.component.ts
│   │  └─ Manual witness status assignment
│   │  └─ Audit trail of WitnessEvents
│   └─ witness-management.component.html
│      └─ List: witnessed rituals, invitation codes
│
└── discord-provisioning/
    ├─ discord-provisioning.component.ts
    │  └─ POST /api/clients/{clientSlug}/discord/provision
    │  └─ Voltron triggers channel template creation
    └─ discord-provisioning.component.html
       └─ Channel names, role assignments, permission presets
```

### 14.3 Routing Alignment

#### Public Routes (L2 & L3)
```
app.routes.ts:
├─ '/' → SplashPageComponent (L2 public splash)
├─ '/clients/:clientSlug/casting-call' → CastingCallComponent (L2 Signal I)
├─ '/clients/:clientSlug/releases/:releaseKey' → ReleaseComponent (L3)
├─ '/clients/:clientSlug/archive' → ArchiveComponent (L3)
└─ '/clients/:clientSlug/echo' → EchoComponent (L3 reflection)

Short URL Redirect:
├─ /shorturl/{code} → Route4 canonical URL (with referrer tracking)
└─ E.g., rt4.io/mary/c/invite → localhost:4200/clients/making-of-mary/casting-call
```

#### Admin Routes (L0 & L1)
```
app.routes.ts:
└─ '/admin' → loadChildren: admin.routes (lazy-loaded)
    ├─ '/admin/dashboard' → AdminDashboard
    ├─ '/admin/releases' → ReleaseEditor
    ├─ '/admin/witness' → WitnessManagement
    ├─ '/admin/discord' → DiscordProvisioning
    └─ '/admin/audit' → AuditTrail

Guards:
└─ CanActivate: AuthGuard (Voltron only, future implementation)
```

### 14.4 Database Schema Alignment

```
Clients (multi-tenant key)
├── Id (Guid)
├── Name (string, e.g., "Making of MARY")
├── Slug (string, e.g., "making-of-mary")
└── Relationships:
    ├─ ReleaseInstances[] (FK: ClientId)
    ├─ CastingCalls[] (FK: ClientId)
    ├─ WitnessEvents[] (FK: ClientId)
    └─ ShortUrls[] (FK: ClientId)

ReleaseInstances (state machine)
├── Id (Guid)
├── ClientId (FK)
├── ReleaseKey (string, e.g., "S1E1")
├── Title (string)
├── State (enum: DRAFT, SIGNAL_I, PROCESS, HOLD, SIGNAL_II, DROP, ECHO, ARCHIVE)
├── IsPublished (bool)
├── PublishedAt (DateTime?)
├── CreatedAt (DateTime)
├── UpdatedAt (DateTime)
└── Relationships:
    ├─ StateTransitions[] (FK: ReleaseInstanceId)
    └─ WitnessEvents[] (FK: ReleaseInstanceId)

WitnessEvents (presence tracking)
├── Id (Guid)
├── ClientId (FK)
├── ReleaseInstanceId (FK)
├── UserId (Guid, future auth)
├── RitualName (string: Table Read, Writing Table, Shot Council, The Drop, The Echo, etc.)
├── Timestamp (DateTime)
├── PresenceMode (enum: ReadOnly, SilentWitness, Reflection)
└── Notes (string, context clues only—no story)

CastingCalls (Signal I)
├── Id (Guid)
├── ClientId (FK)
├── Title (string)
├── Message (string, tone & constraints only)
├── IsActive (bool)
├── CreatedAt (DateTime)
├── Relationships:
    └─ CastingCallResponses[] (intake without noise)

ShortUrls (trackable links)
├── Id (Guid)
├── Code (string, e.g., "mary/c/invite")
├── FullUrl (string, canonical Route4 URL)
├── ClientId (FK)
├── ReleaseInstanceId (FK, nullable)
├── RitualName (string)
├── ExpiresAt (DateTime, revocable)
└── ReferrerTracking (bool, no metrics exposure)
```

### 14.5 Discord.NET Implementation Patterns & Constraints

Route4 uses Discord.NET to orchestrate ritual-based channel automation. This section documents critical patterns and known limitations.

#### Persistent DiscordSocketClient Requirement

**Problem:** Creating a new `DiscordSocketClient` per API request causes connection timeouts because the `Ready` event fires asynchronously (2-5 seconds).

**Solution:** Maintain a singleton client with connection reuse:

```csharp
// Services/Discord/DiscordBotService.cs
private DiscordSocketClient? _client;
private readonly SemaphoreSlim _connectionLock = new(1, 1);
private bool _isConnected = false;

// Reuse existing connected client
if (_client != null && _isConnected && _client.ConnectionState == ConnectionState.Connected)
    return _client;

// Only connect once on first request
await _connectionLock.WaitAsync();
try
{
    if (_isConnected) return _client;
    await _client.StartAsync();
    _isConnected = true;
}
finally
{
    _connectionLock.Release();
}
```

**Implementation Location:** `Services/Discord/DiscordBotService.cs` (Real and Mock variants)

#### Gateway Intents Configuration

**Problem:** Requesting privileged intents (e.g., `GatewayIntents.GuildMembers`) without approval causes 4014 error: "Disallowed intent(s)".

**Solution:** Only request necessary intents:

```csharp
var config = new DiscordSocketConfig
{
    GatewayIntents = GatewayIntents.Guilds // Minimum for channel/role management
    // DO NOT REQUEST: GuildMembers (requires privileged approval)
};
```

**Why Guilds Only?**
- Sufficient for: Channel provisioning, role creation, permission management
- NOT sufficient for: Member presence, user cache (not needed for Route4)
- GuildMembers requires: Discord Developer Portal → Bot → Privileged Gateway Intents approval

#### Rate Limiting & Idempotent Operations

**Problem:** Discord API enforces rate limits; re-running setup creates duplicate channels/roles.

**Solution:** Add delays between bulk operations and check for existence:

```csharp
// Rate limit protection
await guild.CreateTextChannelAsync(name);
await Task.Delay(500);

// Idempotent operation (check before create)
var existingChannel = guild.TextChannels.FirstOrDefault(c => c.Name == channelName);
if (existingChannel != null) 
    return existingChannel.Id.ToString(); // Return existing, don't create duplicate

// Create only if doesn't exist
var newChannel = await guild.CreateTextChannelAsync(channelName);
return newChannel.Id.ToString();
```

#### Bot Token Management

**Location:** `appsettings.Development.json` → `DiscordSettings:BotToken`

**Setup Steps:**
1. Create Discord server for client
2. Create bot application in Discord Developer Portal
3. Copy bot token (never commit to source control)
4. Store in `appsettings.Development.json` (local only) or environment variable (production)
5. Add bot to server with OAuth2 URL (Guilds scope + manage channels/roles permissions)

**Token Regeneration:**
- If token expires or leaks: Discord Developer Portal → Bot → Reset Token
- Update `appsettings` before next run

#### Permission Overwrites (Planned)

**Current Status:** TODO - Not yet implemented

**Required for:**
- Locking channels during HOLD state (deny @everyone view)
- Unlocking channels during ritual windows (allow Witness role view)
- Slow mode enforcement during reflection phases

**Implementation Pattern (pseudo-code):**
```csharp
async Task LockChannelAsync(ITextChannel channel, IRole everyoneRole)
{
    await channel.AddPermissionOverwriteAsync(
        everyoneRole, 
        new OverwritePermissions(viewChannel: PermValue.Deny)
    );
}

async Task UnlockChannelAsync(ITextChannel channel, IRole everyoneRole)
{
    await channel.RemovePermissionOverwriteAsync(everyoneRole);
}
```

#### Slow Mode (Planned)

**Current Status:** TODO - Not yet implemented

**Required for:**
- Reflection phases (limit message frequency)
- Preventing spam during high-traffic rituals

**Implementation Pattern (pseudo-code):**
```csharp
async Task SetSlowModeAsync(ITextChannel channel, int slowModeSeconds)
{
    await channel.ModifyAsync(ch => 
        ch.SlowModeInterval = slowModeSeconds // e.g., 60 for 1 minute between messages
    );
}
```

#### Message Sending & Announcements (Planned)

**Current Status:** TODO - Not yet implemented

**Required for:**
- Ritual start/end announcements
- Witness invitations and role assignments
- State transition notifications

**Implementation Pattern (pseudo-code):**
```csharp
async Task SendRitualAnnouncementAsync(ITextChannel channel, string message)
{
    await channel.SendMessageAsync(message);
}

// Embed for richer formatting
var embed = new EmbedBuilder()
    .WithTitle("Signal I — Casting Call")
    .WithDescription("Casting call is now live...")
    .WithColor(Color.Red)
    .Build();

await channel.SendMessageAsync(embed: embed);
```

#### Testing & Validation Checklist

**Setup Verification:**
- [x] SQL Server connection (localhost:1433, DefaultConnection)
- [x] Database seed data (9 RitualMappings for Making of MARY)
- [x] Discord bot connects successfully
- [x] Bot invited to server with proper permissions
- [x] 9 channels created per ritual (signal, process, hold, drop, echo, fragments, interval, private-viewing, archive)
- [x] 3 roles created (Inner Circle, Witnessed, Fragments)
- [x] Setup status endpoint returns correct step progression
- [x] Frontend displays bot invitation URL

**Ritual Automation Validation:**
- [ ] Channel locking on HOLD state (permission overwrites)
- [ ] Channel unlocking on ritual window open
- [ ] Slow mode enabled during reflection phases
- [ ] Ritual announcements sent automatically
- [ ] Witness role assigned on participation
- [ ] Message history archived per ritual

**End-to-End Release Cycle:**
- [ ] Create release instance (DRAFT state)
- [ ] Advance through 9 ritual states (SIGNAL_I → DROP → ARCHIVE)
- [ ] Verify Discord automation triggers at each state transition
- [ ] Confirm witness events logged to database
- [ ] Validate archive immutability

#### Production Readiness Checklist

**Before Shipping:**
- [ ] Replace `EnsureCreated()` with EF Core migrations
- [ ] Implement exponential backoff for transient Discord API failures
- [ ] Add comprehensive error handling for Discord operations
- [ ] Set up monitoring/logging for ritual automations
- [ ] Test with multiple clients (not just Making of MARY)
- [ ] Validate multi-tenancy isolation in Discord operations
- [ ] Document Discord token rotation procedure
- [ ] Implement Discord event logging audit trail

---

## 15. Success Metrics

### 15.1 Platform Health
- Release cycle completion rates
- Ritual participation consistency
- Story leakage incidents (target: zero)
- Client retention

### 15.2 Client Success
- Audience formation without story exposure
- Witness conversion rates
- Revenue generation post-Drop
- Canon protection effectiveness

### 15.3 Sustainability
Route4 should remain small if it cannot support itself without compromising:
- Ritual integrity
- Restraint principles
- Authorship protection
- Intentional silence

**That is acceptable.**

---

## Notes
- Suggested priming prompt to train CoPilot
"This document defines the authoritative architecture of Route4. Do not suggest features or designs
that violate its constraints."

---

## Appendix A: Distribution Automation Implementation Status (Jan 5, 2026)

**Decision Log:**
- ✅ **2026-01-05**: Chose Option A (Transparent Log) for distribution communication
  - State transition: ARCHIVE → ARCHIVE_DISTRIBUTED
  - Creator visibility: Timeline entry with platform list and submission timestamp
  - Philosophy alignment: Restraint (no celebration), transparency (factual record)

**Implementation Checklist:**

### Phase 1: Backend Models (Completed)
- ✅ Updated `ReleaseInstance.Status` to include "ArchiveDistributed"
- ✅ Added `DistributionMetadata` field to `ReleaseInstance` (JSON serialized)
- ✅ Created `DistributionMetadataObject` class with Platforms[], SubmittedAt, SubmissionStatus

### Phase 2: Database Migration (Pending)
- [ ] Create migration: Add DistributionMetadata column to Releases table
- [ ] Migration: `dotnet ef migrations add AddDistributionMetadata`
- [ ] Migration: `dotnet ef database update`

### Phase 3: Backend Services (Pending)
- [ ] Implement `IFilmhubService.SubmitForDistributionAsync()`
- [ ] Update `ReleaseManagementService.TriggerFinalDistribution()`:
  - Verify conditions (State=ARCHIVE, Attestations>=100, Revenue>=$1K)
  - Call `IFilmhubService.SubmitForDistributionAsync()`
  - Update state to ARCHIVE_DISTRIBUTED
  - Serialize DistributionMetadata to JSON
  - Create audit log entry
  - Return success/failure status
- [ ] Add scheduled background job: Daily check for distribution-eligible releases
  - Configuration: Check every 24 hours
  - Filter: releaseInstance.Status == "Archived" AND distributionEnabled == true
  - Execute: `TriggerFinalDistribution()` for each eligible release

### Phase 4: API Endpoints (Pending)
- [ ] `GET /api/clients/{clientSlug}/releases/{releaseKey}/distribution-status`
  - Returns: DistributionMetadata + submission timestamp + platform statuses
  - Visibility: Creator + admin only

### Phase 5: Frontend Display (Pending)
- [ ] **Timeline/Audit Log Component**:
  - Shows all state transitions chronologically
  - Distribution entry: "Submitted to Filmhub (3 platforms: Tubi, Pluto, Roku, Amazon Freevee)"
  - Timestamp: 2026-01-05 14:32 UTC
  - Link to help docs (platform ingestion timelines)
  
- [ ] **Release Card / Details View**:
  - Display status as "ARCHIVE_DISTRIBUTED" (if applicable)
  - Expandable distribution info section:
    - Submission date/time
    - Target platforms
    - Help text: "Platform ingestion times vary (3-6 weeks). Check platforms directly for live status."

### Phase 6: Testing (Pending)
- [ ] **Unit Tests**:
  - Condition verification (state + attestations + revenue)
  - DistributionMetadata serialization
  
- [ ] **Integration Tests**:
  - Filmhub API call (mocked)
  - State transition: ARCHIVE → ARCHIVE_DISTRIBUTED
  - Audit log entry creation
  
- [ ] **E2E Test**:
  - Create release → Complete rituals → Reach ARCHIVE state
  - Verify background job triggers automatically
  - Verify frontend shows distribution state + timeline entry

### Phase 7: Documentation (Pending)
- [ ] Update OpenAPI/Swagger docs for new endpoint
- [ ] Add help article: "How does Route4 handle distribution?"
- [ ] Add help article: "What does ARCHIVE_DISTRIBUTED mean?"
- [ ] Creator onboarding guide: Distribution configuration during setup

**Architecture Decisions Locked:**
- Distribution triggered automatically (no manual approval)
- Communication via state transition (not email/notification)
- Creator discovers via self-directed platform monitoring (earned discovery)
- No celebration language ("Your film is live!")
- Route4 acts as curation layer (no ongoing platform presence)

**Next Immediate Action:**
Phase 2 - Database migration to add DistributionMetadata column to ReleaseInstance table.

---

## Appendix B: Route4.FFmpeg Worker Service Implementation Status (Jan 5, 2026)

**Decision Log:**
- ✅ **2026-01-05**: Documented Route4.FFmpeg as separate Worker Service for multi-tenant media processing
  - Rationale: Isolate CPU-intensive FFmpeg jobs from main API request cycle
  - Architecture: Redis queue (primary) + MSSQL fallback
  - Scaling: Horizontal (2-5 workers, auto-scale based on queue depth)

**Implementation Checklist:**

### Phase 1: Project Setup (Pending)
- [ ] Create `Route4.FFmpeg.sln` or add to existing solution
- [ ] Create `Route4.FFmpeg.Models/` (shared FFmpegJob class)
- [ ] Create `Route4.FFmpeg.Services/` (service interfaces)
- [ ] Create `Route4.FFmpeg.Worker/` (Console app with HostedService)
- [ ] Update `.csproj` files to reference shared models

### Phase 2: Job Queue Service (Pending)
- [ ] Implement `IJobQueueService` interface
- [ ] Create `RedisJobQueueService` implementation
  - Dependencies: `StackExchange.Redis` NuGet
  - Methods: `EnqueueAsync()`, `DequeueAsync()`, `UpdateAsync()`, `GetStatusAsync()`
- [ ] Create `MSSQLJobQueueService` fallback implementation
  - Table: `FFmpegJobs` (Status, CreatedAt, CompletedAt, ProcessingDurationMs)
  - Polling: `SELECT TOP 1 FROM FFmpegJobs WHERE Status = 'Queued'`

### Phase 3: FFmpeg Service (Pending)
- [ ] Implement `IFFmpegService` interface
- [ ] Create `FFmpegCliService` implementation
  - Validates FFmpeg binary path from appsettings
  - Builds CLI arguments based on job type
  - Captures exit codes + stderr for error logging
  - Sets timeout (default: 3600 seconds / 1 hour)
- [ ] Job Type Builders:
  - `BuildCreateFragmentProxyArgs()` - Trim + mute + multi-aspect
  - `BuildCreateProcessPreviewArgs()` - Low-res + watermark + stills
  - `BuildCreateReleaseRenditionsArgs()` - Master archive + 1080p public + thumbnails

### Phase 4: Frame.io Upload Service (Pending)
- [ ] Implement `IFrameioUploadService` interface
- [ ] Create `FrameioUploadService` implementation
  - Dependencies: Frame.io .NET SDK or HTTP client
  - Methods: `UploadAsync(jobOutput)` → returns S3/Frame.io URL
  - Folder structure: `route4-{clientSlug}/{releaseKey}/{stageName}/`

### Phase 5: Worker Host Application (Pending)
- [ ] Create `Program.cs` for Route4.FFmpeg.Worker
  - Host builder: `.UseSerilog()` for structured logging
  - Dependency injection: Register queue service, FFmpeg service, upload service
  - HostedService: `FFmpegWorkerService`
- [ ] Implement `FFmpegWorkerService` (HostedService)
  - Constructor injection: IJobQueueService, IFFmpegService, IFrameioUploadService
  - `ExecuteAsync()`: Main loop (poll queue, dequeue, process, update)
  - Heartbeat logging every 60 seconds
  - Graceful shutdown: Finish current job before stopping

### Phase 6: Status Tracking Endpoints (Pending)
- [ ] Add `IFFmpegJobRepository` to Route4.Api
  - Query method: `GetJobsByReleaseAsync(clientId, releaseKey)`
  - Query method: `GetJobByIdAsync(jobId)`
- [ ] Add controller: `FFmpegJobsController`
  - `GET /api/clients/{clientSlug}/releases/{releaseKey}/ffmpeg-jobs` → List all jobs for release
  - `GET /api/clients/{clientSlug}/releases/{releaseKey}/ffmpeg-jobs/{jobId}` → Single job details + metrics

### Phase 7: Monitoring & Metrics (Pending)
- [ ] Add structured logging to all job transitions
  - Log format: `[JobId] [ClientSlug] [JobType] [Status] Duration: {ms}ms`
  - Include: InputSize, OutputSize, ErrorMessage (if failed)
- [ ] Create dashboard queries (optional)
  - Queue depth over time
  - Average processing time per job type
  - Success/failure rate by job type
- [ ] Set up alerts
  - Dead-letter queue depth > 5 (manual intervention)
  - Queue wait time > 30 minutes (scaling needed)
  - Worker heartbeat missing > 5 minutes (worker crashed)

### Phase 8: Testing (Pending)
- [ ] **Unit Tests**:
  - FFmpeg argument builder (different job types, parameter validation)
  - Job state transitions (Queued → Processing → Completed/Failed)
  - Logging output format
  
- [ ] **Integration Tests**:
  - Enqueue job to Redis → Dequeue in worker → Process → Update status
  - Frame.io upload simulation (mock HTTP responses)
  - Error handling (bad input file, FFmpeg timeout, network failure)
  
- [ ] **E2E Test**:
  - Create release → Trigger SIGNAL ritual → Fragment job enqueued
  - Monitor job status via API endpoint
  - Verify output file stored in Frame.io

### Phase 9: Deployment & Configuration (Pending)
- [ ] Docker setup (optional)
  - Base image: `mcr.microsoft.com/dotnet/runtime:9.0`
  - Add FFmpeg binary: `RUN apt-get install ffmpeg`
  - Copy Route4.FFmpeg.Worker assembly
- [ ] appsettings configuration
  - Development: Local Redis on 6379
  - Production: Azure Redis Cache or managed Redis
  - FFmpeg path: Environment variable (FFMPEG_BIN_PATH)
- [ ] Environment variables
  - `ROUTE4_FFmpeg:BinaryPath`
  - `ROUTE4_JobQueue:ConnectionString`
  - `ROUTE4_Frameio:ApiKey`

### Phase 10: Billing & Cost Allocation (Pending)
- [ ] Process completion logs feed into billing system
  - Job type → Gate mapping: CreateFragmentProxy → Gate 2, CreateReleaseRenditions → Gate 5
  - Cost calculation: Based on ProcessingDurationMs + OutputSize
  - Example log entry: `[S1E1] CreateFragmentProxy completed in 205000ms (3 min 25 sec) → Gate 2: $10–$50 allocation`
- [ ] Create cost report endpoint
  - `GET /api/clients/{clientSlug}/releases/{releaseKey}/ffmpeg-costs` → Total compute cost for release
  - Breaks down by job type + date range

**Architecture Decisions Locked:**
- FFmpeg as separate Worker Service (not embedded in API)
- Redis queue primary, MSSQL fallback
- Job processing is async (API returns 202 Accepted immediately)
- Multi-tenant isolation via ClientId + ReleaseKey
- Processing time logged for audit + billing purposes
- Horizontal scaling supported (multiple workers can run in parallel)

**Implementation Time Estimate:**
- Phase 1-5 (Core worker): 2-3 weeks (depends on Redis setup, Frame.io API learning curve)
- Phase 6-7 (Monitoring): 1 week
- Phase 8 (Testing): 1-2 weeks
- Phase 9-10 (Deployment + Billing): 1 week
- **Total: 5-7 weeks**

**Next Immediate Actions:**
1. Set up Redis instance (local dev or Azure Redis Cache)
2. Create Route4.FFmpeg.Models project with FFmpegJob class
3. Implement IJobQueueService + RedisJobQueueService
4. Build Phase 1 skeleton (console app, basic hosting)
