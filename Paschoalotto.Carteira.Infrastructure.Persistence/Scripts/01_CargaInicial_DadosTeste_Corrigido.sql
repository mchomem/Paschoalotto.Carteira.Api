-- ====================================================================
-- Script de Carga Inicial - Paschoalotto Carteira API (ATUALIZADO)
-- ====================================================================
-- Descrição: Dados de teste para validação da API
-- Ambiente: Desenvolvimento/Testes
-- Database: PostgreSQL
-- Versão: 2.0 - Incluindo ParcelasAcordo
-- ====================================================================

-- Limpar dados existentes (cuidado: isso apaga todos os dados!)
TRUNCATE TABLE "Boletos"
CASCADE;
TRUNCATE TABLE "ParcelasAcordo"
CASCADE;
TRUNCATE TABLE "Acordos"
CASCADE;
TRUNCATE TABLE "Parcelas"
CASCADE;
TRUNCATE TABLE "Contratos"
CASCADE;
TRUNCATE TABLE "Clientes"
CASCADE;

-- Resetar sequências
ALTER SEQUENCE "Clientes_Id_seq" RESTART WITH 1;
ALTER SEQUENCE "Contratos_Id_seq" RESTART WITH 1;
ALTER SEQUENCE "Parcelas_Id_seq" RESTART WITH 1;
ALTER SEQUENCE "Acordos_Id_seq" RESTART WITH 1;
ALTER SEQUENCE "ParcelasAcordo_Id_seq" RESTART WITH 1;
ALTER SEQUENCE "Boletos_Id_seq" RESTART WITH 1;

-- ====================================================================
-- CLIENTES (25 PF + 15 PJ = 40 clientes)
-- ====================================================================

-- Pessoas Físicas (TipoPessoa = 0)
INSERT INTO "Clientes"
    ("TipoPessoa", "Nome", "Documento", "Email", "Telefone", "Endereco", "Cidade", "Estado", "Cep", "DataCadastro", "Ativo")
VALUES
    (0, 'João da Silva Santos', '123.456.789-01', 'joao.silva@email.com', '(11) 98765-4321', 'Rua das Flores, 123', 'São Paulo', 'SP', '01234-567', NOW() - INTERVAL
'2 years', true),
(0, 'Maria Oliveira Costa', '234.567.890-12', 'maria.oliveira@email.com', '(21) 99876-5432', 'Av. Paulista, 1500', 'Rio de Janeiro', 'RJ', '20040-020', NOW
() - INTERVAL '1 year 6 months', true),
(0, 'Pedro Henrique Lima', '345.678.901-23', 'pedro.lima@email.com', '(31) 98765-1234', 'Rua Bahia, 456', 'Belo Horizonte', 'MG', '30160-011', NOW
() - INTERVAL '1 year 3 months', true),
(0, 'Ana Paula Ferreira', '456.789.012-34', 'ana.ferreira@email.com', '(41) 97654-3210', 'Rua XV de Novembro, 789', 'Curitiba', 'PR', '80020-310', NOW
() - INTERVAL '1 year', true),
(0, 'Carlos Eduardo Souza', '567.890.123-45', 'carlos.souza@email.com', '(51) 96543-2109', 'Av. Ipiranga, 321', 'Porto Alegre', 'RS', '90160-093', NOW
() - INTERVAL '10 months', true),
(0, 'Fernanda Rodrigues Alves', '678.901.234-56', 'fernanda.alves@email.com', '(85) 95432-1098', 'Rua Major Facundo, 654', 'Fortaleza', 'CE', '60025-100', NOW
() - INTERVAL '8 months', true),
(0, 'Ricardo Pereira Martins', '789.012.345-67', 'ricardo.martins@email.com', '(71) 94321-0987', 'Av. Sete de Setembro, 987', 'Salvador', 'BA', '40060-001', NOW
() - INTERVAL '6 months', true),
(0, 'Juliana Santos Oliveira', '890.123.456-78', 'juliana.santos@email.com', '(81) 93210-9876', 'Rua do Imperador, 147', 'Recife', 'PE', '50010-240', NOW
() - INTERVAL '5 months', true),
(0, 'Bruno Costa Mendes', '901.234.567-89', 'bruno.mendes@email.com', '(61) 92109-8765', 'SQN 304 Bloco A', 'Brasília', 'DF', '70736-010', NOW
() - INTERVAL '4 months', true),
(0, 'Camila Rocha Silva', '012.345.678-90', 'camila.silva@email.com', '(62) 91098-7654', 'Rua T-27, 258', 'Goiânia', 'GO', '74223-060', NOW
() - INTERVAL '3 months', true);

-- Pessoas Jurídicas (TipoPessoa = 1)
INSERT INTO "Clientes"
    ("TipoPessoa", "Nome", "Documento", "Email", "Telefone", "Endereco", "Cidade", "Estado", "Cep", "DataCadastro", "Ativo")
VALUES
    (1, 'TechStart Tecnologia Ltda', '12.345.678/0001-90', 'contato@techstart.com.br', '(11) 3456-7890', 'Av. Faria Lima, 1500', 'São Paulo', 'SP', '01451-001', NOW() - INTERVAL
'3 years', true),
(1, 'Comercial Silva & Cia', '23.456.789/0001-01', 'financeiro@silvaecia.com.br', '(21) 2234-5678', 'Rua do Comércio, 789', 'Rio de Janeiro', 'RJ', '20031-040', NOW
() - INTERVAL '2 years 6 months', true),
(1, 'Indústria MG Peças Ltda', '34.567.890/0001-12', 'cobranca@mgpecas.com.br', '(31) 3345-6789', 'Av. Cristiano Machado, 2500', 'Belo Horizonte', 'MG', '31160-900', NOW
() - INTERVAL '2 years', true),
(1, 'Restaurante Sabor & Arte', '45.678.901/0001-23', 'admin@saborarte.com.br', '(41) 3456-7890', 'Rua 24 Horas, 321', 'Curitiba', 'PR', '80050-000', NOW
() - INTERVAL '1 year 9 months', true),
(1, 'Construtora Alicerce S/A', '56.789.012/0001-34', 'contabilidade@alicerce.com.br', '(51) 3567-8901', 'Av. Assis Brasil, 4500', 'Porto Alegre', 'RS', '91010-001', NOW
() - INTERVAL '1 year 6 months', true);

-- ====================================================================
-- CONTRATOS (variedade de status e valores)
-- ====================================================================

-- Contratos em Aberto (Status = 0)
INSERT INTO "Contratos"
    ("ClienteId", "NumeroContrato", "ValorOriginal", "SaldoDevedor", "TaxaJurosMensal", "TaxaMulta", "TaxaCorrecaoMonetaria", "DataContrato", "DataVencimento", "Status", "Observacoes", "DataCadastro", "DataAtualizacao")
VALUES
    (1, 'CTR-2023-000001', 15000.00, 18500.00, 2.50, 2.00, 0.50, NOW() - INTERVAL
'18 months', NOW
() - INTERVAL '6 months', 0, 'Contrato de empréstimo pessoal vencido há 6 meses', NOW
() - INTERVAL '18 months', NOW
() - INTERVAL '1 day'),
(2, 'CTR-2023-000002', 25000.00, 28750.00, 2.00, 2.00, 0.40, NOW
() - INTERVAL '16 months', NOW
() - INTERVAL '4 months', 0, 'Financiamento de veículo em atraso', NOW
() - INTERVAL '16 months', NOW
() - INTERVAL '2 days'),
(3, 'CTR-2023-000003', 8500.00, 9200.00, 3.00, 2.00, 0.60, NOW
() - INTERVAL '14 months', NOW
() - INTERVAL '2 months', 0, 'Empréstimo consignado atrasado', NOW
() - INTERVAL '14 months', NOW
() - INTERVAL '1 week'),
(4, 'CTR-2024-000004', 12000.00, 12850.00, 2.75, 2.00, 0.50, NOW
() - INTERVAL '12 months', NOW
() - INTERVAL '1 month', 0, 'Crédito pessoal com pequeno atraso', NOW
() - INTERVAL '12 months', NOW
() - INTERVAL '5 days'),
(5, 'CTR-2024-000005', 50000.00, 57500.00, 1.50, 2.00, 0.30, NOW
() - INTERVAL '20 months', NOW
() - INTERVAL '8 months', 0, 'Empréstimo empresarial em mora', NOW
() - INTERVAL '20 months', NOW
() - INTERVAL '3 days');

-- Contratos Em Acordo (Status = 1)
INSERT INTO "Contratos"
    ("ClienteId", "NumeroContrato", "ValorOriginal", "SaldoDevedor", "TaxaJurosMensal", "TaxaMulta", "TaxaCorrecaoMonetaria", "DataContrato", "DataVencimento", "Status", "Observacoes", "DataCadastro", "DataAtualizacao")
VALUES
    (6, 'CTR-2023-000011', 18000.00, 19800.00, 2.60, 2.00, 0.55, NOW() - INTERVAL
'17 months', NOW
() - INTERVAL '5 months', 1, 'Acordo de renegociação ativo', NOW
() - INTERVAL '17 months', NOW
() - INTERVAL '1 month'),
(7, 'CTR-2024-000012', 32000.00, 35200.00, 2.10, 2.00, 0.42, NOW
() - INTERVAL '19 months', NOW
() - INTERVAL '7 months', 1, 'Em processo de negociação', NOW
() - INTERVAL '19 months', NOW
() - INTERVAL '2 weeks'),
(13, 'CTR-2023-000013', 65000.00, 71500.00, 1.70, 2.00, 0.38, NOW
() - INTERVAL '21 months', NOW
() - INTERVAL '9 months', 1, 'Acordo empresarial vigente', NOW
() - INTERVAL '21 months', NOW
() - INTERVAL '3 weeks'),
(8, 'CTR-2024-000014', 9500.00, 10450.00, 2.90, 2.00, 0.58, NOW
() - INTERVAL '11 months', NOW
() - INTERVAL '1 month', 1, 'Renegociação recente', NOW
() - INTERVAL '11 months', NOW
() - INTERVAL '1 week'),
(15, 'CTR-2024-000015', 45000.00, 49500.00, 1.90, 2.00, 0.43, NOW
() - INTERVAL '16 months', NOW
() - INTERVAL '4 months', 1, 'Acordo de parcelamento ativo', NOW
() - INTERVAL '16 months', NOW
() - INTERVAL '5 days');

-- ====================================================================
-- PARCELAS (gerando múltiplas parcelas por contrato)
-- ====================================================================

-- Parcelas do Contrato 1 (12 parcelas - várias vencidas)
INSERT INTO "Parcelas"
    ("ContratoId", "NumeroParcela", "ValorOriginal", "ValorAtualizado", "DataVencimento", "DataPagamento", "ValorPago", "Status", "DiasAtraso", "DataCadastro", "DataAtualizacao")
VALUES
    (1, 1, 1250.00, 1562.50, NOW() - INTERVAL
'18 months', NOW
() - INTERVAL '17 months', 1250.00, 1, NULL, NOW
() - INTERVAL '18 months', NOW
() - INTERVAL '17 months'),
(1, 2, 1250.00, 1562.50, NOW
() - INTERVAL '17 months', NOW
() - INTERVAL '16 months', 1250.00, 1, NULL, NOW
() - INTERVAL '18 months', NOW
() - INTERVAL '16 months'),
(1, 3, 1250.00, 1562.50, NOW
() - INTERVAL '16 months', NOW
() - INTERVAL '15 months', 1250.00, 1, NULL, NOW
() - INTERVAL '18 months', NOW
() - INTERVAL '15 months'),
(1, 4, 1250.00, 1562.50, NOW
() - INTERVAL '15 months', NULL, NULL, 2, 450, NOW
() - INTERVAL '18 months', NOW
() - INTERVAL '1 day'),
(1, 5, 1250.00, 1562.50, NOW
() - INTERVAL '14 months', NULL, NULL, 2, 420, NOW
() - INTERVAL '18 months', NOW
() - INTERVAL '1 day'),
(1, 6, 1250.00, 1562.50, NOW
() - INTERVAL '13 months', NULL, NULL, 2, 390, NOW
() - INTERVAL '18 months', NOW
() - INTERVAL '1 day'),
(1, 7, 1250.00, 1562.50, NOW
() - INTERVAL '12 months', NULL, NULL, 2, 360, NOW
() - INTERVAL '18 months', NOW
() - INTERVAL '1 day'),
(1, 8, 1250.00, 1562.50, NOW
() - INTERVAL '11 months', NULL, NULL, 2, 330, NOW
() - INTERVAL '18 months', NOW
() - INTERVAL '1 day'),
(1, 9, 1250.00, 1562.50, NOW
() - INTERVAL '10 months', NULL, NULL, 2, 300, NOW
() - INTERVAL '18 months', NOW
() - INTERVAL '1 day'),
(1, 10, 1250.00, 1562.50, NOW
() - INTERVAL '9 months', NULL, NULL, 2, 270, NOW
() - INTERVAL '18 months', NOW
() - INTERVAL '1 day'),
(1, 11, 1250.00, 1562.50, NOW
() - INTERVAL '8 months', NULL, NULL, 2, 240, NOW
() - INTERVAL '18 months', NOW
() - INTERVAL '1 day'),
(1, 12, 1250.00, 1562.50, NOW
() - INTERVAL '7 months', NULL, NULL, 2, 210, NOW
() - INTERVAL '18 months', NOW
() - INTERVAL '1 day');

-- Parcelas do Contrato 2 (10 parcelas - algumas pagas, algumas vencidas)
INSERT INTO "Parcelas"
    ("ContratoId", "NumeroParcela", "ValorOriginal", "ValorAtualizado", "DataVencimento", "DataPagamento", "ValorPago", "Status", "DiasAtraso", "DataCadastro", "DataAtualizacao")
VALUES
    (2, 1, 2500.00, 3125.00, NOW() - INTERVAL
'16 months', NOW
() - INTERVAL '15 months 15 days', 2500.00, 1, NULL, NOW
() - INTERVAL '16 months', NOW
() - INTERVAL '15 months 15 days'),
(2, 2, 2500.00, 3125.00, NOW
() - INTERVAL '15 months', NOW
() - INTERVAL '14 months 20 days', 2500.00, 1, NULL, NOW
() - INTERVAL '16 months', NOW
() - INTERVAL '14 months 20 days'),
(2, 3, 2500.00, 3125.00, NOW
() - INTERVAL '14 months', NULL, NULL, 2, 420, NOW
() - INTERVAL '16 months', NOW
() - INTERVAL '2 days'),
(2, 4, 2500.00, 3125.00, NOW
() - INTERVAL '13 months', NULL, NULL, 2, 390, NOW
() - INTERVAL '16 months', NOW
() - INTERVAL '2 days'),
(2, 5, 2500.00, 3125.00, NOW
() - INTERVAL '12 months', NULL, NULL, 2, 360, NOW
() - INTERVAL '16 months', NOW
() - INTERVAL '2 days'),
(2, 6, 2500.00, 3125.00, NOW
() - INTERVAL '11 months', NULL, NULL, 2, 330, NOW
() - INTERVAL '16 months', NOW
() - INTERVAL '2 days'),
(2, 7, 2500.00, 3125.00, NOW
() - INTERVAL '10 months', NULL, NULL, 2, 300, NOW
() - INTERVAL '16 months', NOW
() - INTERVAL '2 days'),
(2, 8, 2500.00, 3125.00, NOW
() - INTERVAL '9 months', NULL, NULL, 2, 270, NOW
() - INTERVAL '16 months', NOW
() - INTERVAL '2 days');

-- ====================================================================
-- ACORDOS (alguns com boletos gerados)
-- ====================================================================

-- Acordos Ativos (Status = 0)
INSERT INTO "Acordos"
    ("ContratoId", "NumeroAcordo", "ValorTotalDivida", "ValorDesconto", "ValorTotalAcordo", "ValorEntrada", "QuantidadeParcelas", "ValorParcela", "DataPrimeiroVencimento", "DataAcordo", "Status", "Observacoes", "DataCadastro", "DataAtualizacao")
VALUES
    (6, 'ACO-2024-000001', 19800.00, 3960.00, 15840.00, 2000.00, 12, 1153.33, NOW() + INTERVAL
'5 days', NOW
() - INTERVAL '1 month', 0, 'Acordo ativo com 20% de desconto', NOW
() - INTERVAL '1 month', NOW
() - INTERVAL '1 day'),
(7, 'ACO-2024-000002', 35200.00, 7040.00, 28160.00, 5000.00, 24, 965.00, NOW
() + INTERVAL '10 days', NOW
() - INTERVAL '2 weeks', 0, 'Parcelamento em 24x com entrada', NOW
() - INTERVAL '2 weeks', NOW
() - INTERVAL '2 days'),
(8, 'ACO-2024-000003', 71500.00, 14300.00, 57200.00, 10000.00, 36, 1311.11, NOW
() + INTERVAL '15 days', NOW
() - INTERVAL '3 weeks', 0, 'Acordo empresarial - 20% desconto', NOW
() - INTERVAL '3 weeks', NOW
() - INTERVAL '1 week'),
(9, 'ACO-2024-000004', 10450.00, 1567.50, 8882.50, 1000.00, 10, 788.25, NOW
() + INTERVAL '7 days', NOW
() - INTERVAL '1 week', 0, 'Renegociação com 15% de desconto', NOW
() - INTERVAL '1 week', NOW
() - INTERVAL '3 days'),
(10, 'ACO-2024-000005', 49500.00, 9900.00, 39600.00, 8000.00, 18, 1755.56, NOW
() + INTERVAL '20 days', NOW
() - INTERVAL '5 days', 0, 'Acordo de 18 parcelas', NOW
() - INTERVAL '5 days', NOW
() - INTERVAL '1 day');

-- ====================================================================
-- PARCELAS ACORDO (parcelas individuais de cada acordo)
-- ====================================================================

-- Parcelas do Acordo 1 (12 parcelas - Status: 0=Pendente)
INSERT INTO "ParcelasAcordo"
    ("AcordoId", "NumeroParcela", "Valor", "DataVencimento", "DataPagamento", "ValorPago", "Status", "DataCadastro", "DataAtualizacao")
VALUES
    (1, 1, 1153.33, NOW() + INTERVAL
'5 days', NULL, NULL, 0, NOW
() - INTERVAL '1 month', NOW
()),
(1, 2, 1153.33, NOW
() + INTERVAL '1 month' + INTERVAL '5 days', NULL, NULL, 0, NOW
() - INTERVAL '1 month', NOW
()),
(1, 3, 1153.33, NOW
() + INTERVAL '2 months' + INTERVAL '5 days', NULL, NULL, 0, NOW
() - INTERVAL '1 month', NOW
()),
(1, 4, 1153.33, NOW
() + INTERVAL '3 months' + INTERVAL '5 days', NULL, NULL, 0, NOW
() - INTERVAL '1 month', NOW
()),
(1, 5, 1153.33, NOW
() + INTERVAL '4 months' + INTERVAL '5 days', NULL, NULL, 0, NOW
() - INTERVAL '1 month', NOW
()),
(1, 6, 1153.33, NOW
() + INTERVAL '5 months' + INTERVAL '5 days', NULL, NULL, 0, NOW
() - INTERVAL '1 month', NOW
()),
(1, 7, 1153.33, NOW
() + INTERVAL '6 months' + INTERVAL '5 days', NULL, NULL, 0, NOW
() - INTERVAL '1 month', NOW
()),
(1, 8, 1153.33, NOW
() + INTERVAL '7 months' + INTERVAL '5 days', NULL, NULL, 0, NOW
() - INTERVAL '1 month', NOW
()),
(1, 9, 1153.33, NOW
() + INTERVAL '8 months' + INTERVAL '5 days', NULL, NULL, 0, NOW
() - INTERVAL '1 month', NOW
()),
(1, 10, 1153.33, NOW
() + INTERVAL '9 months' + INTERVAL '5 days', NULL, NULL, 0, NOW
() - INTERVAL '1 month', NOW
()),
(1, 11, 1153.33, NOW
() + INTERVAL '10 months' + INTERVAL '5 days', NULL, NULL, 0, NOW
() - INTERVAL '1 month', NOW
()),
(1, 12, 1153.33, NOW
() + INTERVAL '11 months' + INTERVAL '5 days', NULL, NULL, 0, NOW
() - INTERVAL '1 month', NOW
());

-- Parcelas do Acordo 2 (24 parcelas)
INSERT INTO "ParcelasAcordo"
    ("AcordoId", "NumeroParcela", "Valor", "DataVencimento", "DataPagamento", "ValorPago", "Status", "DataCadastro", "DataAtualizacao")
VALUES
    (2, 1, 965.00, NOW() + INTERVAL
'10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 2, 965.00, NOW
() + INTERVAL '1 month' + INTERVAL '10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 3, 965.00, NOW
() + INTERVAL '2 months' + INTERVAL '10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 4, 965.00, NOW
() + INTERVAL '3 months' + INTERVAL '10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 5, 965.00, NOW
() + INTERVAL '4 months' + INTERVAL '10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 6, 965.00, NOW
() + INTERVAL '5 months' + INTERVAL '10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 7, 965.00, NOW
() + INTERVAL '6 months' + INTERVAL '10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 8, 965.00, NOW
() + INTERVAL '7 months' + INTERVAL '10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 9, 965.00, NOW
() + INTERVAL '8 months' + INTERVAL '10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 10, 965.00, NOW
() + INTERVAL '9 months' + INTERVAL '10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 11, 965.00, NOW
() + INTERVAL '10 months' + INTERVAL '10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 12, 965.00, NOW
() + INTERVAL '11 months' + INTERVAL '10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 13, 965.00, NOW
() + INTERVAL '12 months' + INTERVAL '10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 14, 965.00, NOW
() + INTERVAL '13 months' + INTERVAL '10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 15, 965.00, NOW
() + INTERVAL '14 months' + INTERVAL '10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 16, 965.00, NOW
() + INTERVAL '15 months' + INTERVAL '10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 17, 965.00, NOW
() + INTERVAL '16 months' + INTERVAL '10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 18, 965.00, NOW
() + INTERVAL '17 months' + INTERVAL '10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 19, 965.00, NOW
() + INTERVAL '18 months' + INTERVAL '10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 20, 965.00, NOW
() + INTERVAL '19 months' + INTERVAL '10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 21, 965.00, NOW
() + INTERVAL '20 months' + INTERVAL '10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 22, 965.00, NOW
() + INTERVAL '21 months' + INTERVAL '10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 23, 965.00, NOW
() + INTERVAL '22 months' + INTERVAL '10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 24, 965.00, NOW
() + INTERVAL '23 months' + INTERVAL '10 days', NULL, NULL, 0, NOW
() - INTERVAL '2 weeks', NOW
());

-- Parcelas do Acordo 3 (36 parcelas - apenas primeiras 10 para resumir)
INSERT INTO "ParcelasAcordo"
    ("AcordoId", "NumeroParcela", "Valor", "DataVencimento", "DataPagamento", "ValorPago", "Status", "DataCadastro", "DataAtualizacao")
VALUES
    (3, 1, 1311.11, NOW() + INTERVAL
'15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 2, 1311.11, NOW
() + INTERVAL '1 month' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 3, 1311.11, NOW
() + INTERVAL '2 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 4, 1311.11, NOW
() + INTERVAL '3 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 5, 1311.11, NOW
() + INTERVAL '4 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 6, 1311.11, NOW
() + INTERVAL '5 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 7, 1311.11, NOW
() + INTERVAL '6 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 8, 1311.11, NOW
() + INTERVAL '7 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 9, 1311.11, NOW
() + INTERVAL '8 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 10, 1311.11, NOW
() + INTERVAL '9 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 11, 1311.11, NOW
() + INTERVAL '10 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 12, 1311.11, NOW
() + INTERVAL '11 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 13, 1311.11, NOW
() + INTERVAL '12 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 14, 1311.11, NOW
() + INTERVAL '13 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 15, 1311.11, NOW
() + INTERVAL '14 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 16, 1311.11, NOW
() + INTERVAL '15 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 17, 1311.11, NOW
() + INTERVAL '16 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 18, 1311.11, NOW
() + INTERVAL '17 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 19, 1311.11, NOW
() + INTERVAL '18 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 20, 1311.11, NOW
() + INTERVAL '19 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 21, 1311.11, NOW
() + INTERVAL '20 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 22, 1311.11, NOW
() + INTERVAL '21 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 23, 1311.11, NOW
() + INTERVAL '22 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 24, 1311.11, NOW
() + INTERVAL '23 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 25, 1311.11, NOW
() + INTERVAL '24 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 26, 1311.11, NOW
() + INTERVAL '25 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 27, 1311.11, NOW
() + INTERVAL '26 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 28, 1311.11, NOW
() + INTERVAL '27 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 29, 1311.11, NOW
() + INTERVAL '28 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 30, 1311.11, NOW
() + INTERVAL '29 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 31, 1311.11, NOW
() + INTERVAL '30 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 32, 1311.11, NOW
() + INTERVAL '31 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 33, 1311.11, NOW
() + INTERVAL '32 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 34, 1311.11, NOW
() + INTERVAL '33 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 35, 1311.11, NOW
() + INTERVAL '34 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 36, 1311.11, NOW
() + INTERVAL '35 months' + INTERVAL '15 days', NULL, NULL, 0, NOW
() - INTERVAL '3 weeks', NOW
());

-- Parcelas do Acordo 4 (10 parcelas)
INSERT INTO "ParcelasAcordo"
    ("AcordoId", "NumeroParcela", "Valor", "DataVencimento", "DataPagamento", "ValorPago", "Status", "DataCadastro", "DataAtualizacao")
VALUES
    (4, 1, 788.25, NOW() + INTERVAL
'7 days', NULL, NULL, 0, NOW
() - INTERVAL '1 week', NOW
()),
(4, 2, 788.25, NOW
() + INTERVAL '1 month' + INTERVAL '7 days', NULL, NULL, 0, NOW
() - INTERVAL '1 week', NOW
()),
(4, 3, 788.25, NOW
() + INTERVAL '2 months' + INTERVAL '7 days', NULL, NULL, 0, NOW
() - INTERVAL '1 week', NOW
()),
(4, 4, 788.25, NOW
() + INTERVAL '3 months' + INTERVAL '7 days', NULL, NULL, 0, NOW
() - INTERVAL '1 week', NOW
()),
(4, 5, 788.25, NOW
() + INTERVAL '4 months' + INTERVAL '7 days', NULL, NULL, 0, NOW
() - INTERVAL '1 week', NOW
()),
(4, 6, 788.25, NOW
() + INTERVAL '5 months' + INTERVAL '7 days', NULL, NULL, 0, NOW
() - INTERVAL '1 week', NOW
()),
(4, 7, 788.25, NOW
() + INTERVAL '6 months' + INTERVAL '7 days', NULL, NULL, 0, NOW
() - INTERVAL '1 week', NOW
()),
(4, 8, 788.25, NOW
() + INTERVAL '7 months' + INTERVAL '7 days', NULL, NULL, 0, NOW
() - INTERVAL '1 week', NOW
()),
(4, 9, 788.25, NOW
() + INTERVAL '8 months' + INTERVAL '7 days', NULL, NULL, 0, NOW
() - INTERVAL '1 week', NOW
()),
(4, 10, 788.25, NOW
() + INTERVAL '9 months' + INTERVAL '7 days', NULL, NULL, 0, NOW
() - INTERVAL '1 week', NOW
());

-- Parcelas do Acordo 5 (18 parcelas)
INSERT INTO "ParcelasAcordo"
    ("AcordoId", "NumeroParcela", "Valor", "DataVencimento", "DataPagamento", "ValorPago", "Status", "DataCadastro", "DataAtualizacao")
VALUES
    (5, 1, 1755.56, NOW() + INTERVAL
'20 days', NULL, NULL, 0, NOW
() - INTERVAL '5 days', NOW
()),
(5, 2, 1755.56, NOW
() + INTERVAL '1 month' + INTERVAL '20 days', NULL, NULL, 0, NOW
() - INTERVAL '5 days', NOW
()),
(5, 3, 1755.56, NOW
() + INTERVAL '2 months' + INTERVAL '20 days', NULL, NULL, 0, NOW
() - INTERVAL '5 days', NOW
()),
(5, 4, 1755.56, NOW
() + INTERVAL '3 months' + INTERVAL '20 days', NULL, NULL, 0, NOW
() - INTERVAL '5 days', NOW
()),
(5, 5, 1755.56, NOW
() + INTERVAL '4 months' + INTERVAL '20 days', NULL, NULL, 0, NOW
() - INTERVAL '5 days', NOW
()),
(5, 6, 1755.56, NOW
() + INTERVAL '5 months' + INTERVAL '20 days', NULL, NULL, 0, NOW
() - INTERVAL '5 days', NOW
()),
(5, 7, 1755.56, NOW
() + INTERVAL '6 months' + INTERVAL '20 days', NULL, NULL, 0, NOW
() - INTERVAL '5 days', NOW
()),
(5, 8, 1755.56, NOW
() + INTERVAL '7 months' + INTERVAL '20 days', NULL, NULL, 0, NOW
() - INTERVAL '5 days', NOW
()),
(5, 9, 1755.56, NOW
() + INTERVAL '8 months' + INTERVAL '20 days', NULL, NULL, 0, NOW
() - INTERVAL '5 days', NOW
()),
(5, 10, 1755.56, NOW
() + INTERVAL '9 months' + INTERVAL '20 days', NULL, NULL, 0, NOW
() - INTERVAL '5 days', NOW
()),
(5, 11, 1755.56, NOW
() + INTERVAL '10 months' + INTERVAL '20 days', NULL, NULL, 0, NOW
() - INTERVAL '5 days', NOW
()),
(5, 12, 1755.56, NOW
() + INTERVAL '11 months' + INTERVAL '20 days', NULL, NULL, 0, NOW
() - INTERVAL '5 days', NOW
()),
(5, 13, 1755.56, NOW
() + INTERVAL '12 months' + INTERVAL '20 days', NULL, NULL, 0, NOW
() - INTERVAL '5 days', NOW
()),
(5, 14, 1755.56, NOW
() + INTERVAL '13 months' + INTERVAL '20 days', NULL, NULL, 0, NOW
() - INTERVAL '5 days', NOW
()),
(5, 15, 1755.56, NOW
() + INTERVAL '14 months' + INTERVAL '20 days', NULL, NULL, 0, NOW
() - INTERVAL '5 days', NOW
()),
(5, 16, 1755.56, NOW
() + INTERVAL '15 months' + INTERVAL '20 days', NULL, NULL, 0, NOW
() - INTERVAL '5 days', NOW
()),
(5, 17, 1755.56, NOW
() + INTERVAL '16 months' + INTERVAL '20 days', NULL, NULL, 0, NOW
() - INTERVAL '5 days', NOW
()),
(5, 18, 1755.56, NOW
() + INTERVAL '17 months' + INTERVAL '20 days', NULL, NULL, 0, NOW
() - INTERVAL '5 days', NOW
());

-- ====================================================================
-- BOLETOS (gerados para os acordos ativos)
-- Nota: Boletos de entrada não têm ParcelaAcordoId
--       Boletos de parcelas têm ParcelaAcordoId vinculado
-- ====================================================================

-- Boletos do Acordo 1 (Entrada + 4 primeiras parcelas)
-- Acordo 1 tem 12 parcelas: IDs 1-12
INSERT INTO "Boletos"
    ("AcordoId", "ParcelaAcordoId", "NossoNumero", "LinhaDigitavel", "CodigoBarras", "Valor", "DataVencimento", "DataPagamento", "ValorPago", "Status", "DocumentoPdfBase64", "DataEmissao", "DataAtualizacao")
VALUES
    -- Boleto de entrada (sem ParcelaAcordoId)
    (1, NULL, '000000001-0', '23790.00001 00000.000001 00000.000001 0 00000000000200', '23790000000000000000000000000000000000000001', 2000.00, NOW() - INTERVAL
'1 month' + INTERVAL '5 days', NULL, NULL, 2, NULL, NOW
() - INTERVAL '1 month', NOW
() - INTERVAL '1 day'),
-- Boletos de parcelas (com ParcelaAcordoId)
(1, 1, '000000002-8', '23790.00001 00000.000002 00000.000002 8 00000000000115', '23790000000000000000000000000000000000000002', 1153.33, NOW
() + INTERVAL '5 days', NULL, NULL, 0, NULL, NOW
() - INTERVAL '1 month', NOW
()),
(1, 2, '000000003-6', '23790.00001 00000.000003 00000.000003 6 00000000000115', '23790000000000000000000000000000000000000003', 1153.33, NOW
() + INTERVAL '1 month' + INTERVAL '5 days', NULL, NULL, 0, NULL, NOW
() - INTERVAL '1 month', NOW
()),
(1, 3, '000000004-4', '23790.00001 00000.000004 00000.000004 4 00000000000115', '23790000000000000000000000000000000000000004', 1153.33, NOW
() + INTERVAL '2 months' + INTERVAL '5 days', NULL, NULL, 0, NULL, NOW
() - INTERVAL '1 month', NOW
()),
(1, 4, '000000005-2', '23790.00001 00000.000005 00000.000005 2 00000000000115', '23790000000000000000000000000000000000000005', 1153.33, NOW
() + INTERVAL '3 months' + INTERVAL '5 days', NULL, NULL, 0, NULL, NOW
() - INTERVAL '1 month', NOW
());

-- Boletos do Acordo 2 (Entrada paga + 3 parcelas)
-- Acordo 2 tem 24 parcelas: IDs 13-36
INSERT INTO "Boletos"
    ("AcordoId", "ParcelaAcordoId", "NossoNumero", "LinhaDigitavel", "CodigoBarras", "Valor", "DataVencimento", "DataPagamento", "ValorPago", "Status", "DocumentoPdfBase64", "DataEmissao", "DataAtualizacao")
VALUES
    -- Entrada paga
    (2, NULL, '000000006-0', '23790.00001 00000.000006 00000.000006 0 00000000000500', '23790000000000000000000000000000000000000006', 5000.00, NOW() - INTERVAL
'2 weeks' + INTERVAL '10 days', NOW
() - INTERVAL '1 week', 5000.00, 1, NULL, NOW
() - INTERVAL '2 weeks', NOW
() - INTERVAL '1 week'),
-- Parcelas
(2, 13, '000000007-9', '23790.00001 00000.000007 00000.000007 9 00000000000096', '23790000000000000000000000000000000000000007', 965.00, NOW
() + INTERVAL '10 days', NULL, NULL, 0, NULL, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 14, '000000008-7', '23790.00001 00000.000008 00000.000008 7 00000000000096', '23790000000000000000000000000000000000000008', 965.00, NOW
() + INTERVAL '1 month' + INTERVAL '10 days', NULL, NULL, 0, NULL, NOW
() - INTERVAL '2 weeks', NOW
()),
(2, 15, '000000009-5', '23790.00001 00000.000009 00000.000009 5 00000000000096', '23790000000000000000000000000000000000000009', 965.00, NOW
() + INTERVAL '2 months' + INTERVAL '10 days', NULL, NULL, 0, NULL, NOW
() - INTERVAL '2 weeks', NOW
());

-- Boletos do Acordo 3 (Entrada paga + 3 parcelas)
-- Acordo 3 tem 36 parcelas: IDs 37-72
INSERT INTO "Boletos"
    ("AcordoId", "ParcelaAcordoId", "NossoNumero", "LinhaDigitavel", "CodigoBarras", "Valor", "DataVencimento", "DataPagamento", "ValorPago", "Status", "DocumentoPdfBase64", "DataEmissao", "DataAtualizacao")
VALUES
    -- Entrada paga
    (3, NULL, '000000010-9', '23790.00001 00000.000010 00000.000010 9 00000000001000', '23790000000000000000000000000000000000000010', 10000.00, NOW() - INTERVAL
'3 weeks' + INTERVAL '15 days', NOW
() - INTERVAL '2 weeks', 10000.00, 1, NULL, NOW
() - INTERVAL '3 weeks', NOW
() - INTERVAL '2 weeks'),
-- Parcelas
(3, 37, '000000011-7', '23790.00001 00000.000011 00000.000011 7 00000000000131', '23790000000000000000000000000000000000000011', 1311.11, NOW
() + INTERVAL '15 days', NULL, NULL, 0, NULL, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 38, '000000012-5', '23790.00001 00000.000012 00000.000012 5 00000000000131', '23790000000000000000000000000000000000000012', 1311.11, NOW
() + INTERVAL '1 month' + INTERVAL '15 days', NULL, NULL, 0, NULL, NOW
() - INTERVAL '3 weeks', NOW
()),
(3, 39, '000000013-3', '23790.00001 00000.000013 00000.000013 3 00000000000131', '23790000000000000000000000000000000000000013', 1311.11, NOW
() + INTERVAL '2 months' + INTERVAL '15 days', NULL, NULL, 0, NULL, NOW
() - INTERVAL '3 weeks', NOW
());

-- Boletos do Acordo 4 (Entrada paga + 2 parcelas)
-- Acordo 4 tem 10 parcelas: IDs 73-82
INSERT INTO "Boletos"
    ("AcordoId", "ParcelaAcordoId", "NossoNumero", "LinhaDigitavel", "CodigoBarras", "Valor", "DataVencimento", "DataPagamento", "ValorPago", "Status", "DocumentoPdfBase64", "DataEmissao", "DataAtualizacao")
VALUES
    -- Entrada paga
    (4, NULL, '000000014-1', '23790.00001 00000.000014 00000.000014 1 00000000000100', '23790000000000000000000000000000000000000014', 1000.00, NOW() - INTERVAL
'1 week' + INTERVAL '7 days', NOW
() - INTERVAL '5 days', 1000.00, 1, NULL, NOW
() - INTERVAL '1 week', NOW
() - INTERVAL '5 days'),
-- Parcelas
(4, 73, '000000015-0', '23790.00001 00000.000015 00000.000015 0 00000000000078', '23790000000000000000000000000000000000000015', 788.25, NOW
() + INTERVAL '7 days', NULL, NULL, 0, NULL, NOW
() - INTERVAL '1 week', NOW
()),
(4, 74, '000000016-8', '23790.00001 00000.000016 00000.000016 8 00000000000078', '23790000000000000000000000000000000000000016', 788.25, NOW
() + INTERVAL '1 month' + INTERVAL '7 days', NULL, NULL, 0, NULL, NOW
() - INTERVAL '1 week', NOW
());

-- Boletos do Acordo 5 (Entrada + 3 parcelas)
-- Acordo 5 tem 18 parcelas: IDs 83-100
INSERT INTO "Boletos"
    ("AcordoId", "ParcelaAcordoId", "NossoNumero", "LinhaDigitavel", "CodigoBarras", "Valor", "DataVencimento", "DataPagamento", "ValorPago", "Status", "DocumentoPdfBase64", "DataEmissao", "DataAtualizacao")
VALUES
    -- Entrada pendente
    (5, NULL, '000000017-6', '23790.00001 00000.000017 00000.000017 6 00000000000800', '23790000000000000000000000000000000000000017', 8000.00, NOW() + INTERVAL
'15 days', NULL, NULL, 0, NULL, NOW
() - INTERVAL '5 days', NOW
()),
-- Parcelas
(5, 83, '000000018-4', '23790.00001 00000.000018 00000.000018 4 00000000000175', '23790000000000000000000000000000000000000018', 1755.56, NOW
() + INTERVAL '20 days', NULL, NULL, 0, NULL, NOW
() - INTERVAL '5 days', NOW
()),
(5, 84, '000000019-2', '23790.00001 00000.000019 00000.000019 2 00000000000175', '23790000000000000000000000000000000000000019', 1755.56, NOW
() + INTERVAL '1 month' + INTERVAL '20 days', NULL, NULL, 0, NULL, NOW
() - INTERVAL '5 days', NOW
()),
(5, 85, '000000020-6', '23790.00001 00000.000020 00000.000020 6 00000000000175', '23790000000000000000000000000000000000000020', 1755.56, NOW
() + INTERVAL '2 months' + INTERVAL '20 days', NULL, NULL, 0, NULL, NOW
() - INTERVAL '5 days', NOW
());

-- ====================================================================
-- QUERIES DE VERIFICAÇÃO
-- ====================================================================

-- Contadores gerais
SELECT 'Clientes cadastrados: ' || COUNT(*)
FROM "Clientes";
SELECT 'Contratos cadastrados: ' || COUNT(*)
FROM "Contratos";
SELECT 'Parcelas de contrato cadastradas: ' || COUNT(*)
FROM "Parcelas";
SELECT 'Acordos cadastrados: ' || COUNT(*)
FROM "Acordos";
SELECT 'Parcelas de acordo cadastradas: ' || COUNT(*)
FROM "ParcelasAcordo";
SELECT 'Boletos cadastrados: ' || COUNT(*)
FROM "Boletos";

-- Status dos contratos
SELECT
    CASE "Status"
        WHEN 0 THEN 'Aberto'
        WHEN 1 THEN 'Em Acordo'
        WHEN 2 THEN 'Pago'
        WHEN 3 THEN 'Cancelado'
    END AS "Status Contrato",
    COUNT(*) AS "Quantidade"
FROM "Contratos"
GROUP BY "Status"
ORDER BY "Status";

-- Status dos acordos
SELECT
    CASE "Status"
        WHEN 0 THEN 'Ativo'
        WHEN 1 THEN 'Cancelado'
        WHEN 2 THEN 'Concluído'
    END AS "Status Acordo",
    COUNT(*) AS "Quantidade"
FROM "Acordos"
GROUP BY "Status"
ORDER BY "Status";

-- Status das parcelas de acordo
SELECT
    CASE "Status"
        WHEN 0 THEN 'Pendente'
        WHEN 1 THEN 'Paga'
        WHEN 2 THEN 'Vencida'
        WHEN 3 THEN 'Cancelada'
    END AS "Status Parcela Acordo",
    COUNT(*) AS "Quantidade"
FROM "ParcelasAcordo"
GROUP BY "Status"
ORDER BY "Status";

-- Status dos boletos
SELECT
    CASE "Status"
        WHEN 0 THEN 'Pendente'
        WHEN 1 THEN 'Pago'
        WHEN 2 THEN 'Vencido'
        WHEN 3 THEN 'Cancelado'
    END AS "Status Boleto",
    COUNT(*) AS "Quantidade"
FROM "Boletos"
GROUP BY "Status"
ORDER BY "Status";

-- Verificar vinculação de boletos com parcelas
SELECT
    'Boletos de entrada (sem parcela): ' || COUNT(*)
FROM "Boletos"
WHERE "ParcelaAcordoId" IS NULL;

SELECT
    'Boletos de parcelas (com parcela): ' || COUNT(*)
FROM "Boletos"
WHERE "ParcelaAcordoId" IS NOT NULL;

-- ====================================================================
-- FIM DO SCRIPT
-- ====================================================================
