# Scripts de Banco de Dados

Este diretório contém scripts SQL para configuração e manutenção do banco de dados da **Paschoalotto Carteira API**.

## 📋 Scripts Disponíveis

### `00_Limpeza_Dados.sql`

Script para **limpar todos os dados** das tabelas, mantendo a estrutura do banco de dados intacta.

**⚠️ ATENÇÃO**: Este script **apaga permanentemente todos os registros**. Use apenas em ambientes de **desenvolvimento/teste**.

#### 🎯 Quando Usar

- Antes de executar uma nova carga de dados de teste
- Ao resetar o ambiente de desenvolvimento
- Para limpar dados de testes anteriores

#### 🚀 Como Executar

```bash
# Via psql
psql -h localhost -U postgres -d PaschoalottoCarteira -f 00_Limpeza_Dados.sql
```

O script inclui:
- Pause de 3 segundos como "última chance" de cancelar (Ctrl+C)
- Limpeza em cascata respeitando dependências
- Reset de sequências (IDs voltam para 1)
- Verificação final mostrando contagem de registros

---

### `01_CargaInicial_DadosTeste.sql`

Script de carga inicial com dados de teste para validação da aplicação.

**⚠️ ATENÇÃO**: Este script **limpa todos os dados existentes** antes de inserir os dados de teste. Use apenas em ambientes de **desenvolvimento/teste**.

#### 📊 Conteúdo

- **40 Clientes**:
  - 25 Pessoas Físicas (CPF)
  - 15 Pessoas Jurídicas (CNPJ)
  - Dados completos: nome, documento, email, telefone, endereço

- **30 Contratos** com diferentes status:
  - 10 Contratos **Abertos** (inadimplentes)
  - 5 Contratos **Em Acordo** (renegociados)
  - 5 Contratos **Pagos** (quitados)
  - 3 Contratos **Cancelados**
  - 7 Contratos adicionais para testes variados

- **40+ Parcelas**:
  - Parcelas **Pagas** (Status = 1)
  - Parcelas **Vencidas** (Status = 2) com dias de atraso
  - Parcelas **Em Aberto** (Status = 0)

- **9 Acordos**:
  - 5 Acordos **Ativos** (Status = 0)
  - 2 Acordos **Cancelados** (Status = 1)
  - 2 Acordos **Concluídos** (Status = 2)
  - Com entrada opcional e parcelamentos de 10 a 36x

- **20+ Boletos**:
  - Boletos **Pendentes** (Status = 0)
  - Boletos **Pagos** (Status = 1)
  - Boletos **Vencidos** (Status = 2)
  - Com nosso número, linha digitável e código de barras

#### 🚀 Como Executar

##### Via psql (linha de comando):
```bash
psql -h localhost -U postgres -d PaschoalottoCarteira -f 01_CargaInicial_DadosTeste.sql
```

##### Via pgAdmin:
1. Conecte-se ao banco `PaschoalottoCarteira`
2. Abra o Query Tool
3. Carregue o arquivo `01_CargaInicial_DadosTeste.sql`
4. Execute o script (F5)

##### Via DBeaver:
1. Conecte-se ao banco `PaschoalottoCarteira`
2. Abra um novo SQL Editor
3. Carregue o arquivo `01_CargaInicial_DadosTeste.sql`
4. Execute o script (Ctrl+Enter)

##### Via código (dentro da aplicação):
```csharp
// ⚠️ Apenas para ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
	using var scope = app.Services.CreateScope();
	var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

	var scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
								   "Scripts", "01_CargaInicial_DadosTeste.sql");
	var script = await File.ReadAllTextAsync(scriptPath);

	await context.Database.ExecuteSqlRawAsync(script);
}
```

#### 📈 Estatísticas Geradas

Após a execução, o script exibe:

- Total de registros por tabela
- Distribuição de contratos por status
- Distribuição de acordos por status
- Distribuição de boletos por status

#### 🎯 Casos de Uso para Teste

Este dataset permite testar:

1. **Listagem de Clientes** (PF e PJ)
2. **Consulta de Contratos** por status e cliente
3. **Cálculo de Dívida** com parcelas vencidas
4. **Geração de Acordos** para contratos em aberto
5. **Emissão de Boletos** para acordos ativos
6. **Pagamento de Parcelas** e atualização de status
7. **Cancelamento de Acordos** e Boletos
8. **Relatórios e Dashboards**
9. **Busca por CPF/CNPJ**
10. **Validações de Regras de Negócio**

#### 💡 Exemplos de Queries

##### Clientes com contratos em atraso:
```sql
SELECT c."Nome", c."Documento", ct."NumeroContrato", ct."SaldoDevedor"
FROM "Clientes" c
INNER JOIN "Contratos" ct ON c."Id" = ct."ClienteId"
WHERE ct."Status" = 0
ORDER BY ct."SaldoDevedor" DESC;
```

##### Acordos ativos com boletos vencidos:
```sql
SELECT a."NumeroAcordo", a."ValorTotalAcordo", b."NossoNumero", b."Valor"
FROM "Acordos" a
INNER JOIN "Boletos" b ON a."Id" = b."AcordoId"
WHERE a."Status" = 0 AND b."Status" = 2;
```

##### Parcelas vencidas por contrato:
```sql
SELECT ct."NumeroContrato", COUNT(p."Id") as "ParcelasVencidas", SUM(p."ValorAtualizado") as "TotalDevido"
FROM "Contratos" ct
INNER JOIN "Parcelas" p ON ct."Id" = p."ContratoId"
WHERE p."Status" = 2
GROUP BY ct."NumeroContrato"
ORDER BY "TotalDevido" DESC;
```

## ⚠️ Avisos Importantes

1. **NUNCA execute este script em produção** - ele apaga todos os dados!
2. Os dados são **fictícios** e gerados apenas para testes
3. CPFs e CNPJs são **inválidos** (não passam validação de dígito)
4. Emails e telefones são **fictícios**
5. As datas são calculadas relativamente ao momento da execução
6. Os valores monetários são **realistas** mas não representam dados reais

## 🔧 Manutenção

Para adicionar novos scripts:

1. Crie arquivos numerados sequencialmente: `02_`, `03_`, etc.
2. Documente o propósito do script neste README
3. Inclua comentários no próprio script SQL
4. Sempre inclua tratamento de erros e validações

## 📚 Documentação Adicional

Para mais informações sobre o modelo de dados, consulte:
- Entidades em `Paschoalotto.Carteira.Core.Domain/Entities/`
- Mapeamentos em `Paschoalotto.Carteira.Infrastructure.Persistence/Mappings/`
- Migrations em `Paschoalotto.Carteira.Infrastructure.Persistence/Migrations/`

---

**Banco Paschoalotto** - Sistema de Gestão de Carteira  
**Ambiente**: Desenvolvimento/Testes
