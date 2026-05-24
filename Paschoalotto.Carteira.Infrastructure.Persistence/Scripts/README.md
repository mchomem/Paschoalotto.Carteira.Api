# Scripts de Carga de Dados de Teste

## 🎉 Atualização Importante

**✅ Agora você precisa executar APENAS 1 script!**

Os 3 scripts anteriores foram **unificados** em um único arquivo para simplificar o processo de carga de dados.

---

## 📋 Descrição

Script SQL unificado para popular o banco de dados PostgreSQL com dados de teste completos, incluindo dados específicos para o cliente **João da Silva Santos** (CPF: **123.456.789-01**).

## 🎯 Script Unificado

### `01_CargaCompleta_DadosTeste.sql`

**Script único e completo** que contém toda a carga de dados:

**✅ Parte 1: Base de Dados**
- 15 clientes (10 PF + 5 PJ)
- 10 contratos diversos
- Parcelas de contratos
- 5 acordos (para outros clientes)
- Parcelas de acordos
- Boletos

**✅ Parte 2: Dados do Cliente CPF 123.456.789-01**
- 2 contratos adicionais (totalizando 3 contratos)
- 18 parcelas adicionais
- 2 acordos ativos
- 27 parcelas de acordo
- 13 boletos vinculados aos acordos

**✅ Parte 3: Atualizações de Status**
- Atualiza Contrato 1 para status "Em Acordo"
- Atualiza parcelas vencidas para status "Em Acordo"

## 📊 Resultado Final - Cliente CPF 123.456.789-01

Após executar os 3 scripts, o cliente terá:

| Tipo | Quantidade | Detalhes |
|------|-----------|----------|
| **Contratos** | 3 | CTR-2023-000001 (Em Acordo)<br>CTR-2024-000016 (Em Acordo)<br>CTR-2024-000017 (Aberto) |
| **Acordos** | 2 | ACO-2024-000006 (15 parcelas)<br>ACO-2024-000007 (12 parcelas) |
| **Boletos** | 13 | 2 entradas + 11 parcelas |
| **Parcelas Contrato** | 30 | 5 pagas, 16 em acordo, 9 vencidas |
| **Parcelas Acordo** | 27 | Todas pendentes |

### 💰 Valores Totais

- **Total em Dívida**: R$ 49.850,00
- **Valor com Desconto**: R$ 39.243,12
- **Desconto Total**: R$ 5.784,38 (~14,7%)

## 🚀 Como Executar

### Opção 1: Via pgAdmin ou DBeaver
1. Conecte-se ao banco de dados
2. Abra o script `01_CargaCompleta_DadosTeste.sql`
3. Execute (F5 ou botão Execute)

### Opção 2: Via linha de comando (psql)
```bash
psql -U postgres -d paschoalotto_carteira -f "01_CargaCompleta_DadosTeste.sql"
```

### Opção 3: Via Windows PowerShell
```powershell
cd "D:\Projects\mchomem\Paschoalotto.Carteira.Api\Paschoalotto.Carteira.Infrastructure.Persistence\Scripts"

# Executar script unificado
$env:PGPASSWORD="sua_senha"
psql -U postgres -d paschoalotto_carteira -f "01_CargaCompleta_DadosTeste.sql"
$env:PGPASSWORD=""
```

**✅ Pronto! O banco estará completamente populado com apenas 1 comando.**

## ⚠️ Avisos Importantes

### 🗑️ Limpeza de Dados
O primeiro script **TRUNCA TODAS AS TABELAS** com `CASCADE`. Isso apaga **TODOS OS DADOS EXISTENTES**.

### 🔒 Ambiente
Scripts destinados **APENAS** para ambiente de **desenvolvimento/testes**.
**NÃO EXECUTE EM PRODUÇÃO!**

### 📝 Sequências
Os scripts resetam as sequências de IDs. Certifique-se de que não há dependências externas.

## 🧪 Testes no Frontend

Após executar os scripts, teste no frontend Angular:

1. **Login**: Use o CPF `123.456.789-01`
2. **Dashboard**: Verifique se aparecem:
   - ✅ 4 cards com totalizadores
   - ✅ 3 contratos
   - ✅ 2 acordos
   - ✅ 13 boletos
   - ✅ 27 parcelas de acordo

## 🐛 Troubleshooting

### Erro: "duplicate key value violates unique constraint"
- **Causa**: Já existem dados no banco
- **Solução**: Os scripts já fazem TRUNCATE, mas se persistir, limpe manualmente as tabelas na ordem inversa

### Erro: "relation does not exist"
- **Causa**: Migrations não foram aplicadas
- **Solução**: Execute `dotnet ef database update` no projeto API

### Dados não aparecem no frontend
- **Causa**: Cache do navegador
- **Solução**: Pressione `Ctrl+Shift+R` para limpar cache e recarregar

## 📁 Localização

```
D:\Projects\mchomem\Paschoalotto.Carteira.Api\
└── Paschoalotto.Carteira.Infrastructure.Persistence\
    └── Scripts\
        ├── 01_CargaCompleta_DadosTeste.sql  ⭐ Script Unificado
        ├── backup\
        │   ├── 01_CargaInicial_DadosTeste_Corrigido.sql
        │   ├── 02_DadosTeste_Cliente_CPF_123456789-01.sql
        │   └── 03_Atualizar_Status_Contrato1.sql
        └── README.md
```

> **💡 Nota**: Os scripts antigos foram movidos para a pasta `backup/` para referência histórica.

## 📞 Suporte

Para dúvidas ou problemas, verifique:
1. Logs da aplicação backend
2. Console do navegador (F12)
3. Queries de verificação ao final de cada script
