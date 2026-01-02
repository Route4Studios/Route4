# Route4

**Route4** is a Non-Dependent Release Engine for creators.

It is not a social network.
It is not a marketing tool.
It is a system for releasing work with restraint, ritual, and trust — before, during, and after public release.

Mary is the first client of Route4.

---

## Route4 Studios — The Hybrid Model

Route4 is not purely software.

It is a **physical production studio** (Dayton, OH) paired with a **SaaS release platform**.

### Studio Offering (Dayton, OH - Fox 45 Facility)
- Fully equipped post-production (editing, color, sound)
- Recording/voiceover booths (broadcast-ready)
- Streaming infrastructure (active broadcast channels)
- Equipment available (cameras, audio, grip)
- Team workspace and production support

### Route4 SaaS Offering
- Non-Dependent release methodology
- Discord orchestration
- Witness tracking
- Archive + visibility control
- Sponsorship pipeline integration
- Media artifact generation (Route4-FFmpeg)

### Client Types
1. **Studio Renters**: Traditional facility bookings + equipment
2. **SaaS Clients**: Use Route4 release methodology only (remote)
3. **Full Integration**: Rent studio + use Route4 for release
4. **Voltron Internal**: Mary and future Route4-owned projects

### Voltron (Internal Team)
- 5-person creative team executing Mary and future Route4 projects
- Expertise in sponsorship acquisition and major event production
- Industry connections for A-list musician concerts and partnerships
- Hybrid roles: production, business development, creative direction

### Unique Advantage
Creators don't have to choose between:
- Expensive LA studio + no release control
- Cheap remote production + no infrastructure

Route4 offers: **modern equipment + production guidance + non-dependent release methodology + sponsorship pipeline**.

### Revenue Model (Multi-Stream)
1. **Studio rental + production services** (traditional facility model)
2. **Route4 SaaS threshold fees** (releases, artifacts, witness activation)
3. **Sponsorship revenue sharing** (via Voltron connections; creators benefit)
4. **Partnership facilitation** (lease larger facilities when projects scale)

### Strategic Positioning
Route4 Studios is a Dayton-based production hub where creators build audience before release. We offer integrated production + distribution, with a sponsorship pipeline that benefits projects and partners alike.

This hybrid model creates **defensible differentiation**: we're not competing with every SaaS platform or every local studio. We're offering an **integrated methodology** that marries infrastructure + expertise + ritual-based release strategy.

---

## Core Philosophy

Route4 exists to help creators:

- Build audience *before* release without leaking story
- Reward presence instead of popularity
- Expose **process**, not **outcomes**
- Preserve authorship and canon
- Let capital emerge as a byproduct, not a dependency

Two laws govern everything:

1. **Expose decisions, not outcomes.**
2. **Reveal authorship last.**

---

## Platform vs Client

### Route4 (Platform)
Route4 provides:

- Release cycles
- Ritual stages
- Visibility levels
- Participation tracking (Witness)
- Discord orchestration
- Archive permanence
- Invitation mechanics

Route4 never owns:
- Story
- Lore
- Naming
- Meaning

---

### Mary (Client #1)
Mary defines:

- Ritual names (Signal, Hold, Witness, etc.)
- Language and tone
- Story canon
- Timing
- Credits and revelation

Mary configures Route4. Route4 executes.

---

## Visibility Levels

These levels prevent story leakage while maximizing value.

### L0 — Private (Voltron Only)
- Scripts
- Full edits
- Canon debates

### L1 — Witnessed Process
- Writing sessions
- Shot councils
- Editing / color sessions
- Decisions without outcomes

### L2 — Fragments (Public)
- BTS residue
- Anonymous signals
- Tools, hands, rooms

### L3 — Revelation
- Episode release
- Credits
- Artist reveal

---

## Release Cycle (Abstract)

A Release Cycle is a repeatable sequence of stages.

**Important:** Process rituals that build context and trust (writing, shot, craft) occur **before** the Primary Release. Reflection rituals occur **after**.

Example (Mary):

1. Pre-Artifact (Signal)
2. Process — Table Read (Witness Shadow)
3. Process — Writing Table
4. Process — Shot Council
5. Process — Character Witness Shadow / Crew Participation
6. Silence (Hold)
7. Primary Release (Episode)
8. Reflection (Echo)
9. Residue (Fragments)
10. Meta Reflection (Interval)
11. Private Viewing (Witness-only)
12. Archive

Other clients may configure different stages using the same engine.


---

## Rituals (Mary Implementation)

### The Signal
- Anonymous song drop
- No artist, producer, or genre
- Discovery-first listening

### The Hold
- Intentional silence
- No explanation
- No engagement farming

### The Drop
- Episode release
- Read-only window (24h)

### The Echo
- Reflection after 24h
- No theories, no spoilers

### Fragments
- BTS residue
- No explanations
- Short, incomplete artifacts

### The Interval
- Podcast as consequence
- Post-event reflection only

### Writing Table
- Mary-only writing sessions
- Craft revealed through story

### Private Viewing
- Witness-level only
- Private location
- No recording
- QR-based access

---

## Witness

**Witness** is a role, a verb, and a status.

- Witnessing = presence, not performance
- No leveling
- No gamification
- Presence is acknowledged quietly

Witness status may unlock:
- Reflection channels
- Invitations
- Private Viewings

---

## Discord as Execution Layer

Discord is not the system of record.
It is where rituals occur.

Most channels are read-only most of the time.
Channels open only during rituals.

---

## Default Route4 Discord Channel Templates

Route4 provides **neutral, reusable Discord channel templates**. These are *platform defaults*, not hard requirements. Each client may rename channels via configuration, but the **intent and permissions** remain consistent.

### Orientation (Read-only)
- `#signal` — Pre-release artifacts and announcements
- `#how-to-witness` — Participation rules and expectations
- `#start-here` — Entry point for new members

### Releases
- `#releases` — Primary release drops (read-only during first window)
- `#soundtrack` — Companion audio artifacts

### Reflection
- `#after-the-drop` — Opens after delay; reflection-only discussion
- `#interval` — Podcast and meta reflection drops (read-only)

### Residue
- `#fragments` — BTS residue and artifacts (post-only by approved roles)

### Invitations
- `#private-viewing` — Witness-only invitation notices

### Process Rooms (Opened Only During Sessions)
- `#writing-table`
- `#shot-council`
- `#cut-room`
- `#color-before-meaning`

### Private (Client Team Only)
- `#core-team`
- `#canon-drafts`

These templates allow Route4 to onboard new clients quickly while preserving confidentiality, restraint, and ritual integrity.

---

## Web App (Angular)

The Route4 app is the **spine**.

It provides:
- Official releases
- Witness identity
- Archive
- Invitations
- Deep links to Discord

It does NOT:
- Host conversation
- Replace Discord
- Optimize engagement

### App IA

- Signal
- Episodes
- Archive
- Voltron
- Profile

---

## Route4 Media Pipeline (Route4-FFmpeg API) — Law

Route4 includes a **Route4-FFmpeg API** and worker pipeline that generates media artifacts required by rituals and visibility levels.

This is **platform law**:

- FFmpeg capabilities exist to **enforce restraint**, **protect confidentiality**, and **automate ritual artifacts**.
- Route4 does **not** expose codec/FFmpeg knobs to creators.
- Creators choose **intent** (Fragment / Process Preview / Release Renditions), Route4 chooses the safe implementation.
- If a media feature does not support a ritual, visibility level, or release stage, it does not belong.

### Primary Jobs (Only 3 at MVP)

1. **CreateFragmentProxy** (L2)
   - Auto-trim to 15–90 seconds
   - Optional mute / room-tone
   - Safe crop/blur/mask to avoid spoilers
   - Multi-aspect outputs (9:16, 1:1, 16:9)
   - Optional subtle watermark (client-configurable)

2. **CreateProcessPreview** (L1)
   - Muted previews for craft sessions (edit/color/shot)
   - Low-res proxies
   - Optional burn-in label: "WITNESSED PROCESS — NO OUTCOMES"
   - Crop/mask regions that leak dialogue/plot
   - Still extraction for color/scopes discussion

3. **CreateReleaseRenditions** (L3 + operational)
   - Public release encode(s)
   - Private master / archive encode
   - Private Viewing encode (offline-safe)
   - Thumbnail set
   - Credits roll variant where authorship can be revealed

### Architectural Guardrails

- FFmpeg runs as a **queue-based worker**, never synchronous on request threads.
- No user-supplied FFmpeg arguments. Ever.
- Outputs stored in object storage; metadata stored in Route4 DB.
- Job states: `pending → processing → complete → failed`.
- Hard caps on duration, resolution, and concurrency.

### Creator/Admin UX (Intent, Not Settings)

Buttons / actions Route4 exposes:
- **Generate Fragments** (Short / Silent / Cropped)
- **Generate Process Preview** (Muted / Watermarked)
- **Generate Release Renditions** (Public / Private / Private Viewing)

---

## Ritual Automation Map (Where Route4-FFmpeg Automates Rituals)

Route4-FFmpeg does not change rituals — it **automates the safe artifacts** each ritual requires.

### The Signal (Anonymous Pre-Artifact)
- Automates: optional audio normalization / consistent loudness (safe defaults)
- Enforces: no embedded credit metadata surfaced in Route4 UI

### Table Read (Witness Shadow)
- Automates (optional): audio-only capture proxy (no video), trimmed and leveled for archive
- Enforces: L1 distribution (private/role-gated)

### Writing Table
- Automates (optional): short excerpt clips for Archive (L1), cropped to hands/page, muted if needed

### Shot Council
- Automates: safe still extraction and cropped boards (no context frames)
- Automates: preview snippets that show *intent* without revealing outcomes (L1)

### Character Witness Shadow / Crew Participation
- Automates: Fragment creation from BTS clips (CreateFragmentProxy) with spoiler masking (L2)

### The Hold
- Automates: nothing by design (silence is the feature)

### The Drop (Primary Release)
- Automates: Release packaging (CreateReleaseRenditions)
  - public encode(s)
  - archive encode
  - credits variant (authorship reveal)
  - thumbnails

### The Echo (Reflection)
- Automates: none (conversation is off-platform; Route4 only deep-links)

### Fragments (Residue)
- Automates: CreateFragmentProxy outputs across aspect ratios + safe audio handling

### The Interval (Podcast)
- Automates: audio mastering proxy + segment trimming; consistent levels

### Private Viewing (Witness-only)
- Automates: Private Viewing encode (offline-safe) + optional burn-in policies

### Archive
- Automates: durable proxy formats for long-term access + versioned renditions

---

## Media Providers (Frame.io Integration)

Route4 does not replace media hosting and review infrastructure. Route4 **domesticates** it.

**Rule:** If a client has to learn hosting tools or permissions, Route4 has failed.

### Responsibilities

**Route4 owns (control plane):**
- Release timing and stage state machine
- Visibility levels (L0–L3)
- Audience gating (Witness) and invitations
- Canon and archive links
- What appears, when, to whom, and why

**Media Provider owns (data plane):**
- Storage and streaming
- Playback reliability
- Asset versioning
- Low-level delivery performance

Frame.io is Provider #1.

### Provider Abstraction (Internal Interface)

Route4 integrates providers through an internal adapter. Provider names stay invisible to end users.

```ts
// route4/media/MediaProvider.ts
export type VisibilityLevel = "L0" | "L1" | "L2" | "L3";

export type AssetKind =
  | "raw"
  | "proxy"
  | "fragment"
  | "process_preview"
  | "release_public"
  | "release_archive"
  | "release_private_viewing"
  | "thumbnail"
  | "still";

export interface MediaProviderAsset {
  provider: string;           // e.g. "frameio" (internal only)
  providerAssetId: string;    // stable id from provider
  playbackEmbedUrl?: string;  // for Route4 UI embedding
  downloadUrl?: string;       // optional; usually role-gated
  metadata?: Record<string, any>;
}

export interface UploadRequest {
  clientId: string;
  releaseKey: string;         // e.g. S1E1, or generic cycle key
  stage: string;              // e.g. "process/writing", "release/drop"
  visibility: VisibilityLevel;
  kind: AssetKind;
  fileName: string;
  contentType: string;
}

export interface MediaProvider {
  ensureClientSpace(clientId: string): Promise<void>; // project/workspace
  ensureReleaseSpace(clientId: string, releaseKey: string): Promise<void>; // folder

  upload(req: UploadRequest, sourceUrlOrPath: string): Promise<MediaProviderAsset>;
  createVersion(baseProviderAssetId: string, sourceUrlOrPath: string): Promise<MediaProviderAsset>;

  getEmbed(providerAssetId: string, visibility: VisibilityLevel): Promise<string>;
  setAccessPolicy(providerAssetId: string, visibility: VisibilityLevel): Promise<void>;

  createShareLink(providerAssetId: string, opts: { expiresAt?: string; password?: string }): Promise<string>;
}
```

### Folder + Naming Strategy (Frame.io)

Route4 enforces a deterministic structure so creators never manage folders manually.

**Per Client (Frame.io Project):**
- `route4-{clientSlug}`

**Per ReleaseInstance (Folder):**
- `{releaseKey}/`

**Inside each release folder:**
- `00-raw/` (L0)
- `10-process/` (L1)
- `20-fragments/` (L2)
- `30-release/` (L3)
- `40-archive/` (L3)

**Asset naming convention:**
- `{releaseKey}__{stage}__{kind}__v{n}.{ext}`

Examples:
- `S1E1__process-writing__process_preview__v1.mp4`
- `S1E1__residue-fragments__fragment__v3.mp4`
- `S1E1__drop__release_public__v1.mp4`

### Visibility Mapping (Route4 → Provider)

- **L0 (Private):** provider assets remain private; no embeds for audience
- **L1 (Witnessed Process):** embed proxies only; muted/masked previews
- **L2 (Fragments):** short masked proxies; multi-aspect; no context
- **L3 (Revelation):** final encodes + credits variants + thumbnails

Route4 always decides *when* an embed becomes visible.

### Failure Modes + Defaults

Route4 must degrade gracefully:

- Provider outage → Route4 shows "Artifact processing" state; no broken links
- Missing embed → Route4 falls back to a role-gated download link (if allowed)
- Misconfigured permissions → Route4 re-applies policies on publish/unlock events
- Version confusion → Route4 uses deterministic `kind` + `v{n}` and records the active asset pointer

Route4 never asks creators to fix hosting.

---

## New Client Discord Bootstrap (Route4)

This checklist defines how Route4 **provisions, configures, and governs Discord** for a new client — without requiring the client to understand Discord mechanics.

> The client never needs to "learn Discord."  
> Route4 treats Discord as an execution surface, not a product feature.

---

### Phase 1 — Client Intake (Route4 Admin)

- [ ] Client name + short description
- [ ] Primary creator / core team members
- [ ] Preferred language pack (terms like Witness, Signal, etc.)
- [ ] Release cadence (episodic, seasonal, one-off)
- [ ] Desired rituals (enable / disable per client)
- [ ] Confidentiality sensitivity (low / medium / high)

---

### Phase 2 — Discord Provisioning (Automated or Assisted)

Route4 handles the following:

- [ ] Create Discord server (or connect to existing one)
- [ ] Apply Route4 default channel templates
- [ ] Create role set:
  - Core Team
  - Witness
  - Member (default)
- [ ] Apply read-only defaults to most channels
- [ ] Lock process rooms (closed by default)

Client only confirms names and access — no setup required.

---

### Phase 3 — Permission & Safety Defaults

Route4 enforces safe defaults:

- [ ] @everyone posting disabled in ritual channels
- [ ] Slow mode enabled in reflection channels
- [ ] File uploads disabled in discussion channels
- [ ] Voice/stage rooms locked by role
- [ ] Invite links disabled or time-limited

These prevent chaos before it can happen.

---

### Phase 4 — Ritual Mapping

For each enabled ritual:

- [ ] Map ritual → channel
- [ ] Define open/close behavior
- [ ] Define visibility level (L0–L3)
- [ ] Define Discord automation (lock/unlock)
- [ ] Define app deep links

Example:
- *Pre-Artifact* → `#signal` → read-only
- *Primary Release* → `#releases` → read-only 24h

---

### Phase 5 — Client Review (15 minutes)

Client reviews:

- [ ] Channel names (optional rename)
- [ ] Role labels (optional rename)
- [ ] Ritual enablement

No Discord training required.

---

### Phase 6 — Go Live

- [ ] Publish invite link via Route4 app
- [ ] Lock unused channels
- [ ] Set first release schedule
- [ ] Run first ritual dry test

---

### Phase 7 — Ongoing Governance (Route4-Owned)

- [ ] Channel state managed by release cycle
- [ ] Roles assigned via Witness events
- [ ] Ritual rooms open only when needed
- [ ] Archive maintained in Route4 app

Clients focus on creation. Route4 handles structure.

---

## Design Principle

> If a client asks how Discord works, Route4 has already failed.

Route4’s job is to **remove cognitive load**, enforce restraint, and preserve trust — not to teach tooling.

---

## What Route4 Never Asks a Client to Do

- Configure permissions
- Decide channel architecture
- Moderate chaos
- Learn Discord bots
- Gamify engagement

Route4 absorbs that complexity.

---

---

## Engineer Implementation Notes (Authoritative)

This document is not conceptual only. It is **implementation-authoritative**.

The primary engineer implementing Route4 is also the system designer. There is no handoff gap between vision and execution.

All tooling, code, and architecture decisions should defer to this document when ambiguity exists.

---

## Engineering Principles

1. **Configuration over customization**  
   Route4 must support multiple clients through configuration (language packs, ritual enablement, schedules), not forks.

2. **Discord is infrastructure, not product**  
   Discord APIs, permissions, and automation should remain invisible to clients.

3. **State > events > UI**  
   Release state drives Discord behavior and UI rendering, not the other way around.

4. **Minimal surface area**  
   Every feature must map directly to a ritual or visibility level.

5. **No premature abstraction**  
   Abstractions should emerge from Mary + one additional client, not speculation.

---

## Recommended Build Order (Engineer-Focused)

### Phase 1 — Stabilize the Spine
- [ ] ReleaseCycleTemplate model
- [ ] ReleaseInstance state machine
- [ ] Visibility level enforcement
- [ ] WitnessEvent tracking

### Phase 2 — Discord Integration
- [ ] Channel template provisioning
- [ ] Role creation and assignment
- [ ] Lock/unlock automation per stage
- [ ] Invite link lifecycle management

### Phase 3 — App Surface (Angular)
- [ ] Signal view (anonymous artifact)
- [ ] Release view (episode / primary release)
- [ ] Archive (read-only)
- [ ] Invitations (private viewing)

### Phase 4 — Admin Surface (Internal)
- [ ] Create / schedule releases
- [ ] Advance release stages
- [ ] Declare canon
- [ ] Issue invitations

---

## Copilot / AI Assistant Usage (Guardrails)

When using Copilot, Claude Sonnet, or any AI assistant:

- Treat this README as **source-of-truth**
- Reject suggestions that:
  - Add engagement mechanics
  - Expose story prematurely
  - Gamify participation
  - Collapse platform/client separation

Suggested priming prompt:

> "This document defines the authoritative architecture of Route4. Do not suggest features or designs that violate its constraints."

---

## Final Engineering Constraint

> If a feature cannot be explained as supporting a ritual, visibility level, or release stage, it does not belong in Route4.

This constraint is non-negotiable.

---

## Care-Based Threshold Pricing (Internal Model)

This section defines Route4’s **Care-based pricing methodology**. It replaces flat subscriptions with **threshold-based fees** tied to irreversible moments in the creative lifecycle.

This model exists for **internal evaluation and sustainability planning only**. It is not public pricing, and it is **not applied to Mary**.

Mary remains above-the-line work. Route4 is being built alongside it.

---

### Pricing Philosophy — Care, Not Consumption

Route4 does not charge for:
- time spent thinking
- drafts
- silence
- planning
- iteration

Route4 only assigns value when a creator **crosses a threshold** — a moment that creates permanence, allocates resources, or opens access to others.

This methodology is called **Care**.

> Care means fewer actions, taken seriously.

---

## Threshold Gates & Plausible Internal Fees

These prices are **reference points** to understand sustainability, cost recovery, and long-term viability. They are not promises or requirements.

### 🔐 Gate 1 — Signal Publication
**Moment:** An anonymous pre-artifact is released and archived.

- Audio normalization
- Timestamped entry
- Immutable archive record

**Reference Fee:** $25–$50 per Signal

---

### 🔐 Gate 2 — Witness Activation
**Moment:** The process opens to others.

- Witness role enabled
- Table Read or Process access window
- Presence tracking begins

**Reference Fee:** $50–$100 per activation window

---

### 🔐 Gate 3 — Artifact Generation (Route4-FFmpeg)
**Moment:** Raw material becomes canonical artifacts.

- Fragment batches
- Process previews
- Release renditions

**Reference Fees:**
- $10–$20 per Fragment batch
- $30–$50 per Process Preview set
- $75–$150 per Release Packaging

---

### 🔐 Gate 4 — The Drop (Primary Release)
**Moment:** The work becomes real.

- Release published
- Credits locked
- Canon sealed
- Archive entry created

**Reference Fee:** $100–$200 per Drop

---

### 🔐 Gate 5 — Private Viewing
**Moment:** Scarce, protected access is created.

- Offline-safe encode
- Invitations issued
- Optional burn-in policies

**Reference Fee:** $50–$100 per Private Viewing event

---

## Why This Model Fits Route4

- Encourages restraint
- Discourages noise
- Aligns money with meaning
- Makes each ritual intentional
- Prevents platform abuse

Route4 never charges for presence — only for **crossing**.

---

## Mary-Specific Clarification

Mary is **not charged** under this model.

Route4 is being developed *alongside* Mary, not *on top of* it.

These thresholds exist to:
- understand operating costs
- validate sustainability
- guide future pricing decisions

They are part of Route4’s **self-discipline**, not monetization pressure.

---

## Final Note on Sustainability

If Route4 cannot support itself without compromising:
- ritual
- restraint
- authorship
- silence

then Route4 should remain small.

That is acceptable.