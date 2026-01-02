# Fox 45 Studio Collage Background Setup

## Overview
The home page now has a subtle 5-image collage background showing the filmmaking process stages at low opacity (8%) behind the white content.

## Images Required

Create a folder at: `ClientApp/public/assets/studio-collage/`

Place these 5 images with the following naming:

### 1. Recording Studio (0% - Left)
**File:** `1-recording.jpg`
- **Purpose:** Audio booth, microphone, recording equipment
- **Search terms:** "recording studio microphone" OR "podcast booth"
- **Best source:** Unsplash, Pexels
- **Suggested links:**
  - https://unsplash.com/photos/QK7DCb6B0Yg (microphone + monitor)
  - https://unsplash.com/photos/a-microphone-and-sound-equipment-in-front-of-a-monitor-QK7DCb6B0Yg

### 2. Film Crew on Set (20%)
**File:** `2-film-crew.jpg`
- **Purpose:** Silhouette of production team with camera, lights, dolly
- **Search terms:** "film production crew" OR "video camera filming"
- **Best source:** Unsplash, Pexels
- **Suggested links:**
  - https://unsplash.com/search/photos/film-crew
  - Search for images showing multiple people around professional camera

### 3. Editing Suite (40% - Center)
**File:** `3-editing-suite.jpg`
- **Purpose:** Post-production editing, color grading, multi-monitor setup
- **Search terms:** "video editing" OR "color grading" OR "editing suite"
- **Best source:** Unsplash, Pexels
- **Suggested links:**
  - https://unsplash.com/search/photos/video-editing
  - Look for images with monitors/computers for editing

### 4. Collaborative Team (60%)
**File:** `4-collaboration.jpg`
- **Purpose:** Multiple people working together in studio environment
- **Search terms:** "production team" OR "creative team collaborating" OR "studio team"
- **Best source:** Unsplash, Pexels
- **Suggested links:**
  - https://unsplash.com/search/photos/team-collaboration
  - https://unsplash.com/search/photos/creative-team

### 5. Professional Equipment (100% - Right)
**File:** `5-equipment.jpg`
- **Purpose:** Close-up of professional gear: lights, cables, control panels
- **Search terms:** "professional lighting equipment" OR "studio equipment" OR "broadcast equipment"
- **Best source:** Unsplash, Pexels
- **Suggested links:**
  - https://unsplash.com/search/photos/studio-lighting
  - Look for images showing professional rigs and equipment detail

## Image Specifications

- **Format:** JPG or PNG (JPG recommended for file size)
- **Resolution:** Minimum 1920x1200px (landscape orientation recommended)
- **Aspect ratio:** 16:9 or 4:3 (wider is better)
- **File size:** Keep under 500KB per image for performance
- **Quality:** High contrast, well-lit images work best with low opacity

## Opacity Setting

Current opacity in SCSS: `opacity: 0.08;` (8%)

To adjust visibility:
- `0.05` = Very subtle (5%)
- `0.08` = Current (8%)
- `0.12` = More visible (12%)
- `0.15` = Much more visible (15%)

Edit [home.component.scss](home.component.scss#L32) if you want to adjust.

## Implementation Steps

1. Create folder: `ClientApp/public/assets/studio-collage/`
2. Download 5 images from sources above
3. Rename them to: `1-recording.jpg`, `2-film-crew.jpg`, etc.
4. Place them in the folder
5. Run `npm start` (frontend will reload)
6. Background should appear behind all home page content

## How It Works

The CSS uses a fixed background with:
- Linear gradient across 5 images (0%, 20%, 40%, 60%, 100%)
- Positioned at top-left and covers entire viewport
- 8% opacity for subtle effect
- Fixed attachment so it stays behind as you scroll
- z-index: -1 keeps it behind all content

## Route4 Context

This background subtly suggests the physical production studio to local creators while keeping focus on the SaaS platform (home page content sits above with white background).

Shows the **complete filmmaking pipeline**:
1. Recording (audio/content capture)
2. Production (on-set collaboration)
3. Post-production (editing & color)
4. Team collaboration (process visibility)
5. Equipment (professional infrastructure)

## Troubleshooting

If images don't appear:
1. Check file paths are correct: `public/assets/studio-collage/[filename]`
2. Verify file names match exactly (case-sensitive)
3. Check browser console for 404 errors
4. Try `.png` format instead of `.jpg`
5. Ensure images are at least 1920px wide

If you want to adjust styling further, see [home.component.scss](home.component.scss#L27-L43).
