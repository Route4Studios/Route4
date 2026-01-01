# Route4 Discord Setup - Quick Start
# This script helps your team get started quickly

Write-Output ""
Write-Output "🚀 ROUTE4 DISCORD SETUP - QUICK START"
Write-Output "======================================"
Write-Output ""

Write-Output "📋 AVAILABLE SCRIPTS:"
Write-Output ""
Write-Output "1. .\step1-get-bot-invitation.ps1 -ClientSlug 'making-of-mary'"
Write-Output "   → Gets the bot invitation URL for Discord setup"
Write-Output ""
Write-Output "2. .\step2-configure-server.ps1 -ClientSlug 'making-of-mary' -GuildId 'YOUR_GUILD_ID'"
Write-Output "   → Configures your Discord server with Route4 templates"
Write-Output ""

Write-Output "🎯 QUICK SETUP FOR MAKING OF MARY:"
Write-Output ""
Write-Output "Step 1 - Copy and paste this:"
Write-Output ".\step1-get-bot-invitation.ps1 -ClientSlug 'making-of-mary'"
Write-Output ""
Write-Output "Step 2 - After creating Discord server and inviting bot:"
Write-Output ".\step2-configure-server.ps1 -ClientSlug 'making-of-mary' -GuildId 'PASTE_YOUR_GUILD_ID_HERE'"
Write-Output ""

Write-Output "📖 FULL MANUAL: See TEAM-DISCORD-SETUP-MANUAL.md"
Write-Output ""

# Check if API is running
try {
    $healthCheck = Invoke-RestMethod -Uri "http://localhost:5158/api/health" -Method GET -TimeoutSec 2
    Write-Output "✅ Route4 API is running and ready"
} catch {
    Write-Output "❌ Route4 API is not running"
    Write-Output "   To start: dotnet run --project Route4MoviePlug.Api.csproj"
}

Write-Output ""
Write-Output "👥 NEED TO TRAIN 4 TEAM MEMBERS?"
Write-Output "   Share these files:"
Write-Output "   • TEAM-DISCORD-SETUP-MANUAL.md (full instructions)"
Write-Output "   • step1-get-bot-invitation.ps1 (automated script)"
Write-Output "   • step2-configure-server.ps1 (automated script)"
Write-Output ""