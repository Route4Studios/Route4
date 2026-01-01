# Route4 Discord Setup - Step 1: Get Bot Invitation
# Copy and paste this entire script into PowerShell

param(
    [Parameter(Mandatory=$true)]
    [string]$ClientSlug
)

Write-Output "🚀 Route4 Discord Setup - Step 1"
Write-Output "================================="

try {
    $response = Invoke-RestMethod -Uri "http://localhost:5158/api/admin/clients/$ClientSlug/release-engine/bot-invitation" -Method GET
    
    Write-Output ""
    Write-Output "✅ SUCCESS! Here's what you need:"
    Write-Output ""
    Write-Output "🔗 BOT INVITATION URL (copy this):"
    Write-Output $response.invitationUrl
    Write-Output ""
    Write-Output "📋 NEXT STEPS:"
    $response.instructions | ForEach-Object { Write-Output "  $_" }
    Write-Output ""
    Write-Output "⏭️  After completing manual steps, run: .\step2-configure-server.ps1 -ClientSlug '$ClientSlug' -GuildId 'YOUR_GUILD_ID'"
} catch {
    Write-Output "❌ ERROR: $($_.Exception.Message)"
    Write-Output ""
    Write-Output "🔍 TROUBLESHOOTING:"
    Write-Output "  1. Make sure Route4 API is running (http://localhost:5158)"
    Write-Output "  2. Check client slug is correct: '$ClientSlug'"
    Write-Output "  3. Try: Get-Process *Route4* to see if API is running"
}

Write-Output ""
Write-Output "📞 Need help? Check TEAM-DISCORD-SETUP-MANUAL.md"