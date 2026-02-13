# Script para resetar o banco de dados e executar o seed
# Execute: .\ResetDatabase.ps1

Write-Host "🔄 Resetando banco de dados..." -ForegroundColor Yellow

# Remover todas as migrações (exceto a inicial se necessário)
Write-Host "📦 Removendo migrações..." -ForegroundColor Cyan
dotnet ef database drop --force --context TrabukaDbContext
if ($LASTEXITCODE -ne 0) {
    Write-Host "⚠️  Erro ao dropar banco. Continuando..." -ForegroundColor Yellow
}

# Criar banco novamente
Write-Host "📦 Criando banco de dados..." -ForegroundColor Cyan
dotnet ef database update --context TrabukaDbContext
if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Erro ao criar banco de dados!" -ForegroundColor Red
    exit 1
}

Write-Host "✅ Banco de dados resetado com sucesso!" -ForegroundColor Green
Write-Host "🌱 O seed será executado automaticamente quando você iniciar a aplicação." -ForegroundColor Green
Write-Host ""
Write-Host "Para iniciar a aplicação, execute:" -ForegroundColor Cyan
Write-Host "  dotnet run" -ForegroundColor White
