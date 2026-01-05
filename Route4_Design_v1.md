# Route4 Non-Dependent Release Engine
## Design Document - v1
--

## 1. Executive Summary

**Route4** is a multi-tenant RdaS platform that enables creators to release work with restraint, ritual, and trust. It builds audience through documented care, protects authorship, and treats capital as an outcome rather than a driver.

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

**Ritual discipline as service.**

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

### 9.1 Route4-FFmpeg API
Auto-generates ritual-required artifacts:

#### 9.1.1 Primary Jobs
1. **CreateFragmentProxy (L2)**
  - Auto-trim 15-90 seconds
  - Optional mute/room-tone
  - Safe crop/blur/mask
  - Multi-aspect outputs

2. **CreateProcessPreview (L1)**
  - Muted previews for craft sessions
  - Low-res proxies
  - Burn-in watermarks
  - Still extraction

3. **CreateReleaseRenditions (L3)**
  - Public release encodes
  - Private master/archive
  - Thumbnail sets
  - Credits roll variants

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
