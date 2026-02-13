#!/bin/bash
# Script para resetar o banco de dados e executar o seed
# Execute: chmod +x ResetDatabase.sh && ./ResetDatabase.sh

echo "🔄 Resetando banco de dados..."

# Remover todas as migrações (exceto a inicial se necessário)
echo "📦 Removendo banco de dados..."
dotnet ef database drop --force --context TrabukaDbContext

# Criar banco novamente
echo "📦 Criando banco de dados..."
dotnet ef database update --context TrabukaDbContext

if [ $? -eq 0 ]; then
    echo "✅ Banco de dados resetado com sucesso!"
    echo "🌱 O seed será executado automaticamente quando você iniciar a aplicação."
    echo ""
    echo "Para iniciar a aplicação, execute:"
    echo "  dotnet run"
else
    echo "❌ Erro ao resetar banco de dados!"
    exit 1
fi
