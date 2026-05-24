# Paschoalotto Carteira API

API REST para gestão e negociação de contratos de dívidas de pessoas físicas e jurídicas do Banco Paschoalotto.

## 🏗️ Arquitetura

Este projeto foi desenvolvido utilizando **Arquitetura Limpa** (Clean Architecture) com separação clara de responsabilidades:

```
Paschoalotto.Carteira.Api/
├── Paschoalotto.Carteira.Core.Domain/          # Entidades, Enums, Interfaces e Exceções
├── Paschoalotto.Carteira.Core.Application/     # DTOs, Services e Lógica de Negócio
├── Paschoalotto.Carteira.Infrastructure.Persistence/ # Entity Framework, Repositories e Mappings
├── Paschoalotto.Carteira.Infrastructure.IoC/   # Dependency Injection, Swagger, JWT, CORS
└── Paschoalotto.Carteira.Api/                  # Controllers, Middlewares e Configuração
```

## 🚀 Tecnologias

- **.NET 9.0**
- **ASP.NET Core Web API**
- **Entity Framework Core 9.0**
- **PostgreSQL** (Npgsql 9.0.2)
- **Swagger/Swashbuckle** (Documentação da API)
- **JWT Bearer Authentication**
- **Mapster** (Mapeamento de objetos)
- **QuestPDF** (Geração de PDF de boletos)

## 📋 Funcionalidades

### 1. Gestão de Clientes
- Cadastro de Pessoa Física e Jurídica
- Consulta por ID, documento (CPF/CNPJ) e pesquisa
- Atualização e exclusão

### 2. Gestão de Contratos
- Criação de contratos de dívida
- Consulta de contratos por cliente
- Visualização de parcelas
- Atualização de status

### 3. Cálculo de Dívida
- Cálculo automático com juros compostos
- Aplicação de multa e correção monetária
- Histórico de cálculos
- Rastreabilidade de valores

### 4. Geração de Acordos
- Negociação de dívidas com desconto
- Parcelamento de 1 a 60x
- Entrada opcional
- Validações de regras de negócio

### 5. Emissão de Boletos
- Geração de boleto bancário
- Linha digitável e código de barras
- PDF com layout profissional
- Controle de vencimento e status

## ⚙️ Configuração

### Pré-requisitos

- .NET 9.0 SDK
- PostgreSQL 12+
- Visual Studio 2022+ ou VS Code

### Configuração do Banco de Dados

1. Certifique-se de que o PostgreSQL está rodando
2. A connection string padrão está em `appsettings.json`:

```json
{
  "ConnectionStrings": {
	"DefaultConnection": "Host=localhost;Port=5432;Database=PaschoalottoCarteira;Username=postgres;Password=postgres"
  }
}
```

3. Execute as migrations para criar o banco de dados:

```bash
cd Paschoalotto.Carteira.Infrastructure.Persistence
dotnet ef database update --startup-project ..\Paschoalotto.Carteira.Api\Paschoalotto.Carteira.Api.csproj
```

4. **(Opcional)** Carregue dados de teste:

```bash
# Via psql
psql -h localhost -U postgres -d PaschoalottoCarteira -f Scripts/01_CargaInicial_DadosTeste.sql

# Ou via pgAdmin/DBeaver abrindo o arquivo Scripts/01_CargaInicial_DadosTeste.sql
```

Este script insere **40 clientes**, **30 contratos**, **40+ parcelas**, **9 acordos** e **20+ boletos** com dados realistas para testes.

⚠️ **Atenção**: O script de carga limpa todos os dados existentes. Use apenas em desenvolvimento!

### CORS

A API está configurada para aceitar requisições apenas de **http://localhost:4200** (Angular frontend).

Para alterar as origens permitidas, edite o `appsettings.json`:

```json
{
  "Cors": {
	"PolicyName": "PaschoalottoCarteira",
	"AllowedOrigins": "http://localhost:4200;http://localhost:3000"
  }
}
```

### JWT Authentication

A chave JWT está configurada no `appsettings.json`. Em produção, use uma chave segura em variável de ambiente:

```json
{
  "Jwt": {
	"Key": "sua-chave-secreta-aqui",
	"Issuer": "BancoPaschoalotto",
	"Audience": "PaschoalottoCarteiraApi"
  }
}
```

## 🏃 Executando a Aplicação

### Usando Visual Studio
1. Abra a solução `Paschoalotto.Carteira.sln`
2. Defina `Paschoalotto.Carteira.Api` como projeto de inicialização
3. Pressione F5 para executar

### Usando CLI
```bash
cd Paschoalotto.Carteira.Api
dotnet run
```

A API estará disponível em:
- **HTTP**: `http://localhost:5182`
- **HTTPS**: `https://localhost:7183`
- **Swagger UI**: `http://localhost:5182` ou `https://localhost:7183` (raiz - abre automaticamente)

## 📖 Documentação da API

A documentação interativa está disponível via **Swagger** na raiz da aplicação quando executada em modo Development.

### Principais Endpoints

#### Clientes
- `POST /api/Cliente` - Criar cliente
- `GET /api/Cliente/{id}` - Buscar por ID
- `GET /api/Cliente/documento/{documento}` - Buscar por CPF/CNPJ
- `GET /api/Cliente/search?term=` - Pesquisar clientes
- `PUT /api/Cliente/{id}` - Atualizar cliente
- `DELETE /api/Cliente/{id}` - Remover cliente

#### Contratos
- `POST /api/Contrato` - Criar contrato
- `GET /api/Contrato/{id}` - Buscar por ID
- `GET /api/Contrato/cliente/{clienteId}` - Contratos de um cliente
- `GET /api/Contrato/{contratoId}/parcelas` - Parcelas do contrato
- `PUT /api/Contrato/{id}` - Atualizar contrato

#### Cálculo de Dívida
- `POST /api/CalculoDivida` - Calcular dívida com request customizado
- `GET /api/CalculoDivida/{contratoId}` - Calcular dívida de um contrato

#### Acordos
- `POST /api/Acordo` - Criar acordo de negociação
- `GET /api/Acordo/{id}` - Buscar acordo
- `GET /api/Acordo/contrato/{contratoId}` - Acordos de um contrato
- `POST /api/Acordo/{id}/cancelar` - Cancelar acordo

#### Boletos
- `POST /api/Boleto` - Gerar boleto
- `GET /api/Boleto/{id}` - Buscar boleto
- `GET /api/Boleto/{id}/pdf` - Gerar PDF do boleto
- `GET /api/Boleto/acordo/{acordoId}` - Boletos de um acordo
- `POST /api/Boleto/{id}/cancelar` - Cancelar boleto

## 🔒 Segurança

- **JWT Bearer Authentication** para endpoints protegidos
- **CORS** restrito a origens específicas
- **Exception Handling Middleware** para tratamento centralizado de erros
- **Validações de negócio** em todos os serviços

## 📦 Estrutura de Pacotes

### Core.Domain
- Entidades principais (Cliente, Contrato, Parcela, Acordo, Boleto)
- Enums (TipoPessoa, StatusContrato, StatusParcela, etc.)
- Interfaces de repositórios
- Exceções customizadas

### Core.Application
- DTOs (Request/Response)
- Interfaces de serviços
- Serviços de aplicação (ClienteService, ContratoService, DebtCalculationService, AgreementService, BoletoService)

### Infrastructure.Persistence
- AppDbContext
- Mapeamentos do EF Core
- Implementação de repositórios

### Infrastructure.IoC
- DependencyInjection (registro de serviços, repositórios, Swagger, JWT, CORS)

### API
- Controllers
- Middlewares (ExceptionHandlingMiddleware)
- Responses (ApiResponse<T>)
- Using.cs (global usings)
- Program.cs (configuração do pipeline)

## 🧪 Regras de Negócio

### Cálculo de Dívida
- Juros compostos mensais
- Multa aplicada sobre o valor original
- Correção monetária opcional
- Cálculo baseado em dias de atraso

### Acordos
- Desconto máximo de 50%
- Parcelamento de 1 a 60 meses
- Valor mínimo de parcela: R$ 50,00
- Entrada opcional
- Contrato bloqueado durante acordo ativo

### Boletos
- Geração automática de nosso número
- Código de barras e linha digitável
- PDF com layout padrão bancário
- Controle de vencimento

## 📝 Licença

Este projeto é proprietário do Banco Paschoalotto.

## 👨‍💻 Desenvolvedor

Desenvolvido seguindo as melhores práticas de Clean Architecture, SOLID e Domain-Driven Design.

---

**Banco Paschoalotto** - Sistema de Gestão de Carteira
