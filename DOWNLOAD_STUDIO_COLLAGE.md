# Studio Collage - Image Download & Assembly Guide

## Quick Start - 5 Cinematic Production Images

I've selected 5 high-quality, royalty-free images that show the complete filmmaking pipeline. All are CC0 (free to use, no attribution required).

### Images to Download

Download these images and save them to: `ClientApp/public/assets/studio-collage/`

#### 1. Recording Studio - Audio Production
**File name:** `1-recording.jpg`
**Source:** Unsplash
**Download URL:** https://unsplash.com/photos/QK7DCb6B0Yg
**Description:** Professional microphone with audio equipment and monitor - shows audio capture stage
**Alt:** Search Unsplash for "microphone studio equipment"

#### 2. Film Crew on Set - Production
**File name:** `2-film-crew.jpg`
**Source:** Unsplash
**Download URL:** https://unsplash.com/photos/a-couple-of-people-that-are-standing-in-the-dark-d0q9au0dnN4
**Description:** Silhouette of crew members on film set with professional lighting
**Alt:** Search Unsplash for "film crew production set"

#### 3. Editing Suite - Post-Production
**File name:** `3-editing-suite.jpg`
**Source:** Unsplash  
**Download URL:** https://unsplash.com/search/photos/video-editing
**Description:** Editor at workstation with multi-monitor setup for color grading/editing
**Alt:** Search for "video editing suite" or "color grading"

#### 4. Studio Lighting Setup - Equipment
**File name:** `4-collaboration.jpg`
**Source:** Unsplash
**Download URL:** https://unsplash.com/photos/a-tripod-light-with-a-white-screen-on-it-ALZNEvw8GBY
**Description:** Professional lighting rig showing studio infrastructure
**Alt:** Search for "studio lighting setup" or "professional light equipment"

#### 5. Production Team - Collaboration
**File name:** `5-equipment.jpg`
**Source:** Unsplash
**Download URL:** https://unsplash.com/search/photos/film-production-cinematic
**Description:** Team working together on production - shows collaborative craft
**Alt:** Search for "film production team" or "production collaboration"

---

## Download Instructions

### Method 1: Direct Download (Easiest)
1. Visit each Unsplash URL above
2. Click the **Download button** (usually top right or bottom)
3. Save to: `ClientApp/public/assets/studio-collage/`
4. Rename to the filename above (e.g., `1-recording.jpg`)

### Method 2: Using Unsplash API/Direct Links
All Unsplash images have download endpoints. Replace `[ID]` with the photo ID:
```
https://unsplash.com/photos/[ID]/download
```

### Method 3: Via PowerShell (Batch Download)
Create the folder and download all at once:

```powershell
# Create folder
New-Item -ItemType Directory -Path "C:\Users\rodne\Src\route4-movieplug\ClientApp\public\assets\studio-collage" -Force

# Download images (copy-paste these one at a time into PowerShell)
Invoke-WebRequest -Uri "https://unsplash.com/photos/QK7DCb6B0Yg/download" -OutFile "C:\Users\rodne\Src\route4-movieplug\ClientApp\public\assets\studio-collage\1-recording.jpg"
```

---

## Image Specifications

- **Format:** JPG (recommended for web)
- **Minimum width:** 1920px (landscape)
- **Aspect ratio:** 16:9 or 4:3
- **File size:** Keep under 500KB each for performance
- **Quality:** High contrast, well-lit images work best at low opacity

---

## Once Downloaded

1. Place all 5 images in `ClientApp/public/assets/studio-collage/`
2. Run `npm start` from root folder
3. Frontend will auto-load the collage
4. Commit to git

---

## Alternative: AI Image Generation Option

If you prefer, I can help generate 5 cinematic production images using AI tools:
- **Midjourney:** "Cinematic film production..."
- **Stable Diffusion:** High-quality prompts for each stage
- **Adobe Firefly:** Professional production imagery

Let me know if you'd like to go this route instead.

---

## Quick Reference - What Each Image Represents

| Image | Stage | Shows |
|-------|-------|-------|
| 1 | **Recording** | Audio booth, microphone, sound engineering |
| 2 | **Production** | Film crew on set, silhouette, professional lighting |
| 3 | **Post-Production** | Editing suite, color grading, technical work |
| 4 | **Equipment** | Professional rigs, lighting, studio infrastructure |
| 5 | **Collaboration** | Team working together, creative environment |

---

## Transparency Setting

Current SCSS opacity: **8%** (very subtle, won't interfere with readability)

The background appears behind all home page content at low opacity.

---

## Next Steps

1. **Download** the 5 images from Unsplash links above
2. **Save** to `ClientApp/public/assets/studio-collage/` with exact filenames
3. **Start** frontend (`npm start`)
4. **Commit** to git

The collage will appear as a subtle, professional backdrop showing the complete Route4 production pipeline.

---

**This visual tribute to Fox 45 Studio shows creators the full production journey you make available to them.**
