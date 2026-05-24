-- ====================================================================
-- Script de Carga Inicial - Paschoalotto Carteira API
-- ====================================================================
-- Descrição: Dados de teste para validação da API
-- Ambiente: Desenvolvimento/Testes
-- Database: PostgreSQL
-- ====================================================================

-- Limpar dados existentes (cuidado: isso apaga todos os dados!)
TRUNCATE TABLE "Boletos" CASCADE;
TRUNCATE TABLE "Acordos" CASCADE;
TRUNCATE TABLE "Parcelas" CASCADE;
TRUNCATE TABLE "Contratos" CASCADE;
TRUNCATE TABLE "Clientes" CASCADE;

-- Resetar sequências
ALTER SEQUENCE "Clientes_Id_seq" RESTART WITH 1;
ALTER SEQUENCE "Contratos_Id_seq" RESTART WITH 1;
ALTER SEQUENCE "Parcelas_Id_seq" RESTART WITH 1;
ALTER SEQUENCE "Acordos_Id_seq" RESTART WITH 1;
ALTER SEQUENCE "Boletos_Id_seq" RESTART WITH 1;

-- ====================================================================
-- CLIENTES (25 PF + 15 PJ = 40 clientes)
-- ====================================================================

-- Pessoas Físicas (TipoPessoa = 0)
INSERT INTO "Clientes" ("TipoPessoa", "Nome", "Documento", "Email", "Telefone", "Endereco", "Cidade", "Estado", "Cep", "DataCadastro", "Ativo")
VALUES
(0, 'João da Silva Santos', '123.456.789-01', 'joao.silva@email.com', '(11) 98765-4321', 'Rua das Flores, 123', 'São Paulo', 'SP', '01234-567', NOW() - INTERVAL '2 years', true),
(0, 'Maria Oliveira Costa', '234.567.890-12', 'maria.oliveira@email.com', '(21) 99876-5432', 'Av. Paulista, 1500', 'Rio de Janeiro', 'RJ', '20040-020', NOW() - INTERVAL '1 year 6 months', true),
(0, 'Pedro Henrique Lima', '345.678.901-23', 'pedro.lima@email.com', '(31) 98765-1234', 'Rua Bahia, 456', 'Belo Horizonte', 'MG', '30160-011', NOW() - INTERVAL '1 year 3 months', true),
(0, 'Ana Paula Ferreira', '456.789.012-34', 'ana.ferreira@email.com', '(41) 97654-3210', 'Rua XV de Novembro, 789', 'Curitiba', 'PR', '80020-310', NOW() - INTERVAL '1 year', true),
(0, 'Carlos Eduardo Souza', '567.890.123-45', 'carlos.souza@email.com', '(51) 96543-2109', 'Av. Ipiranga, 321', 'Porto Alegre', 'RS', '90160-093', NOW() - INTERVAL '10 months', true),
(0, 'Fernanda Rodrigues Alves', '678.901.234-56', 'fernanda.alves@email.com', '(85) 95432-1098', 'Rua Major Facundo, 654', 'Fortaleza', 'CE', '60025-100', NOW() - INTERVAL '8 months', true),
(0, 'Ricardo Pereira Martins', '789.012.345-67', 'ricardo.martins@email.com', '(71) 94321-0987', 'Av. Sete de Setembro, 987', 'Salvador', 'BA', '40060-001', NOW() - INTERVAL '6 months', true),
(0, 'Juliana Santos Oliveira', '890.123.456-78', 'juliana.santos@email.com', '(81) 93210-9876', 'Rua do Imperador, 147', 'Recife', 'PE', '50010-240', NOW() - INTERVAL '5 months', true),
(0, 'Bruno Costa Mendes', '901.234.567-89', 'bruno.mendes@email.com', '(61) 92109-8765', 'SQN 304 Bloco A', 'Brasília', 'DF', '70736-010', NOW() - INTERVAL '4 months', true),
(0, 'Camila Rocha Silva', '012.345.678-90', 'camila.silva@email.com', '(62) 91098-7654', 'Rua T-27, 258', 'Goiânia', 'GO', '74223-060', NOW() - INTERVAL '3 months', true),
(0, 'Rafael Almeida Santos', '111.222.333-44', 'rafael.santos@email.com', '(27) 90987-6543', 'Av. Jerônimo Monteiro, 369', 'Vitória', 'ES', '29010-004', NOW() - INTERVAL '2 months', true),
(0, 'Patrícia Lima Costa', '222.333.444-55', 'patricia.costa@email.com', '(48) 89876-5432', 'Rua Felipe Schmidt, 741', 'Florianópolis', 'SC', '88010-001', NOW() - INTERVAL '2 months', true),
(0, 'Lucas Fernandes Souza', '333.444.555-66', 'lucas.souza@email.com', '(47) 88765-4321', 'Rua XV de Novembro, 852', 'Joinville', 'SC', '89201-600', NOW() - INTERVAL '1 month', true),
(0, 'Mariana Cardoso Oliveira', '444.555.666-77', 'mariana.oliveira@email.com', '(19) 87654-3210', 'Av. Francisco Glicério, 963', 'Campinas', 'SP', '13012-100', NOW() - INTERVAL '1 month', true),
(0, 'Gabriel Ribeiro Lima', '555.666.777-88', 'gabriel.lima@email.com', '(16) 86543-2109', 'Rua General Osório, 159', 'Ribeirão Preto', 'SP', '14015-030', NOW() - INTERVAL '1 month', true),
(0, 'Tatiana Barbosa Santos', '666.777.888-99', 'tatiana.santos@email.com', '(17) 85432-1098', 'Av. Alberto Andaló, 753', 'São José do Rio Preto', 'SP', '15015-000', NOW() - INTERVAL '3 weeks', true),
(0, 'Rodrigo Martins Costa', '777.888.999-00', 'rodrigo.costa@email.com', '(12) 84321-0987', 'Av. São João, 456', 'São José dos Campos', 'SP', '12210-000', NOW() - INTERVAL '2 weeks', true),
(0, 'Aline Souza Pereira', '888.999.000-11', 'aline.pereira@email.com', '(15) 83210-9876', 'Rua Barão de Itapetininga, 321', 'Sorocaba', 'SP', '18035-370', NOW() - INTERVAL '1 week', true),
(0, 'Marcelo Oliveira Silva', '999.000.111-22', 'marcelo.silva@email.com', '(13) 82109-8765', 'Av. Ana Costa, 654', 'Santos', 'SP', '11060-001', NOW() - INTERVAL '5 days', true),
(0, 'Bianca Ferreira Lima', '000.111.222-33', 'bianca.lima@email.com', '(14) 81098-7654', 'Rua Batista de Carvalho, 987', 'Bauru', 'SP', '17015-630', NOW() - INTERVAL '3 days', true),
(0, 'Felipe Santos Rocha', '100.200.300-40', 'felipe.rocha@email.com', '(18) 80987-6543', 'Av. Brasil, 258', 'Presidente Prudente', 'SP', '19010-000', NOW() - INTERVAL '2 days', true),
(0, 'Amanda Costa Almeida', '200.300.400-50', 'amanda.almeida@email.com', '(11) 79876-5432', 'Rua Augusta, 147', 'São Paulo', 'SP', '01305-100', NOW() - INTERVAL '1 day', true),
(0, 'Thiago Lima Mendes', '300.400.500-60', 'thiago.mendes@email.com', '(21) 78765-4321', 'Rua Voluntários da Pátria, 369', 'Rio de Janeiro', 'RJ', '22270-000', NOW(), true),
(0, 'Carolina Rocha Santos', '400.500.600-70', 'carolina.santos@email.com', '(31) 77654-3210', 'Av. Afonso Pena, 741', 'Belo Horizonte', 'MG', '30130-002', NOW(), true),
(0, 'Diego Alves Costa', '500.600.700-80', 'diego.costa@email.com', '(41) 76543-2109', 'Rua Marechal Deodoro, 852', 'Curitiba', 'PR', '80010-010', NOW(), true);

-- Pessoas Jurídicas (TipoPessoa = 1)
INSERT INTO "Clientes" ("TipoPessoa", "Nome", "Documento", "Email", "Telefone", "Endereco", "Cidade", "Estado", "Cep", "DataCadastro", "Ativo")
VALUES
(1, 'TechStart Tecnologia Ltda', '12.345.678/0001-90', 'contato@techstart.com.br', '(11) 3456-7890', 'Av. Faria Lima, 1500', 'São Paulo', 'SP', '01451-001', NOW() - INTERVAL '3 years', true),
(1, 'Comercial Silva & Cia', '23.456.789/0001-01', 'financeiro@silvaecia.com.br', '(21) 2234-5678', 'Rua do Comércio, 789', 'Rio de Janeiro', 'RJ', '20031-040', NOW() - INTERVAL '2 years 6 months', true),
(1, 'Indústria MG Peças Ltda', '34.567.890/0001-12', 'cobranca@mgpecas.com.br', '(31) 3345-6789', 'Av. Cristiano Machado, 2500', 'Belo Horizonte', 'MG', '31160-900', NOW() - INTERVAL '2 years', true),
(1, 'Restaurante Sabor & Arte', '45.678.901/0001-23', 'admin@saborarte.com.br', '(41) 3456-7890', 'Rua 24 Horas, 321', 'Curitiba', 'PR', '80050-000', NOW() - INTERVAL '1 year 9 months', true),
(1, 'Construtora Alicerce S/A', '56.789.012/0001-34', 'contabilidade@alicerce.com.br', '(51) 3567-8901', 'Av. Assis Brasil, 4500', 'Porto Alegre', 'RS', '91010-001', NOW() - INTERVAL '1 year 6 months', true),
(1, 'Farmácia Vida Saudável', '67.890.123/0001-45', 'gerencia@vidasaudavel.com.br', '(85) 3678-9012', 'Av. Santos Dumont, 1234', 'Fortaleza', 'CE', '60150-161', NOW() - INTERVAL '1 year 3 months', true),
(1, 'Autopeças Rápido Ltda', '78.901.234/0001-56', 'vendas@autopecasrapido.com.br', '(71) 3789-0123', 'Av. ACM, 5678', 'Salvador', 'BA', '41820-000', NOW() - INTERVAL '1 year', true),
(1, 'Padaria e Confeitaria Pão Quentinho', '89.012.345/0001-67', 'contato@paoquentinho.com.br', '(81) 3890-1234', 'Rua da Aurora, 987', 'Recife', 'PE', '50050-000', NOW() - INTERVAL '10 months', true),
(1, 'Livraria Saber & Cultura', '90.123.456/0001-78', 'financeiro@sabercultura.com.br', '(61) 3901-2345', 'SCN Quadra 2', 'Brasília', 'DF', '70712-903', NOW() - INTERVAL '8 months', true),
(1, 'Oficina Mecânica Turbo Motors', '01.234.567/0001-89', 'atendimento@turbomotors.com.br', '(62) 3012-3456', 'Av. Goiás, 1478', 'Goiânia', 'GO', '74063-010', NOW() - INTERVAL '6 months', true),
(1, 'Clínica Odontológica Sorriso Perfeito', '11.111.222/0001-33', 'agendamento@sorrisoperfeito.com.br', '(27) 3111-2222', 'Rua Sete de Setembro, 456', 'Vitória', 'ES', '29010-911', NOW() - INTERVAL '5 months', true),
(1, 'Hotel Praia & Sol', '22.222.333/0001-44', 'reservas@praiaesol.com.br', '(48) 3222-3333', 'Av. Beira Mar Norte, 2000', 'Florianópolis', 'SC', '88015-700', NOW() - INTERVAL '4 months', true),
(1, 'Academia FitLife', '33.333.444/0001-55', 'contato@fitlife.com.br', '(47) 3333-4444', 'Rua Princesa Isabel, 789', 'Joinville', 'SC', '89201-270', NOW() - INTERVAL '3 months', true),
(1, 'Supermercado Economia & Cia', '44.444.555/0001-66', 'compras@economiaecia.com.br', '(19) 3444-5555', 'Av. Norte Sul, 3456', 'Campinas', 'SP', '13035-000', NOW() - INTERVAL '2 months', true),
(1, 'Pet Shop Amigo Fiel', '55.555.666/0001-77', 'vendas@amigofiel.com.br', '(16) 3555-6666', 'Rua Amador Bueno, 852', 'Ribeirão Preto', 'SP', '14025-670', NOW() - INTERVAL '1 month', true);

-- ====================================================================
-- CONTRATOS (variedade de status e valores)
-- ====================================================================

-- Contratos em Aberto (Status = 0)
INSERT INTO "Contratos" ("ClienteId", "NumeroContrato", "ValorOriginal", "SaldoDevedor", "TaxaJurosMensal", "TaxaMulta", "TaxaCorrecaoMonetaria", "DataContrato", "DataVencimento", "Status", "Observacoes", "DataCadastro", "DataAtualizacao")
VALUES
(1, 'CTR-2023-000001', 15000.00, 18500.00, 2.50, 2.00, 0.50, NOW() - INTERVAL '18 months', NOW() - INTERVAL '6 months', 0, 'Contrato de empréstimo pessoal vencido há 6 meses', NOW() - INTERVAL '18 months', NOW() - INTERVAL '1 day'),
(2, 'CTR-2023-000002', 25000.00, 28750.00, 2.00, 2.00, 0.40, NOW() - INTERVAL '16 months', NOW() - INTERVAL '4 months', 0, 'Financiamento de veículo em atraso', NOW() - INTERVAL '16 months', NOW() - INTERVAL '2 days'),
(3, 'CTR-2023-000003', 8500.00, 9200.00, 3.00, 2.00, 0.60, NOW() - INTERVAL '14 months', NOW() - INTERVAL '2 months', 0, 'Empréstimo consignado atrasado', NOW() - INTERVAL '14 months', NOW() - INTERVAL '1 week'),
(4, 'CTR-2024-000004', 12000.00, 12850.00, 2.75, 2.00, 0.50, NOW() - INTERVAL '12 months', NOW() - INTERVAL '1 month', 0, 'Crédito pessoal com pequeno atraso', NOW() - INTERVAL '12 months', NOW() - INTERVAL '5 days'),
(5, 'CTR-2024-000005', 50000.00, 57500.00, 1.50, 2.00, 0.30, NOW() - INTERVAL '20 months', NOW() - INTERVAL '8 months', 0, 'Empréstimo empresarial em mora', NOW() - INTERVAL '20 months', NOW() - INTERVAL '3 days'),
(26, 'CTR-2023-000006', 80000.00, 96000.00, 1.80, 2.00, 0.40, NOW() - INTERVAL '24 months', NOW() - INTERVAL '12 months', 0, 'Financiamento de equipamentos industriais', NOW() - INTERVAL '24 months', NOW() - INTERVAL '1 day'),
(27, 'CTR-2023-000007', 35000.00, 39200.00, 2.20, 2.00, 0.50, NOW() - INTERVAL '15 months', NOW() - INTERVAL '3 months', 0, 'Capital de giro em atraso', NOW() - INTERVAL '15 months', NOW() - INTERVAL '2 days'),
(28, 'CTR-2024-000008', 120000.00, 138000.00, 1.60, 2.00, 0.35, NOW() - INTERVAL '22 months', NOW() - INTERVAL '10 months', 0, 'Expansão de negócio - inadimplente', NOW() - INTERVAL '22 months', NOW() - INTERVAL '4 days'),
(10, 'CTR-2024-000009', 6500.00, 7150.00, 3.50, 2.00, 0.70, NOW() - INTERVAL '10 months', NOW() - INTERVAL '1 month', 0, 'Empréstimo pessoal recente', NOW() - INTERVAL '10 months', NOW() - INTERVAL '1 week'),
(15, 'CTR-2024-000010', 22000.00, 24860.00, 2.30, 2.00, 0.45, NOW() - INTERVAL '13 months', NOW() - INTERVAL '1 month', 0, 'Financiamento mobiliário', NOW() - INTERVAL '13 months', NOW() - INTERVAL '3 days');

-- Contratos Em Acordo (Status = 1)
INSERT INTO "Contratos" ("ClienteId", "NumeroContrato", "ValorOriginal", "SaldoDevedor", "TaxaJurosMensal", "TaxaMulta", "TaxaCorrecaoMonetaria", "DataContrato", "DataVencimento", "Status", "Observacoes", "DataCadastro", "DataAtualizacao")
VALUES
(6, 'CTR-2023-000011', 18000.00, 19800.00, 2.60, 2.00, 0.55, NOW() - INTERVAL '17 months', NOW() - INTERVAL '5 months', 1, 'Acordo de renegociação ativo', NOW() - INTERVAL '17 months', NOW() - INTERVAL '1 month'),
(7, 'CTR-2024-000012', 32000.00, 35200.00, 2.10, 2.00, 0.42, NOW() - INTERVAL '19 months', NOW() - INTERVAL '7 months', 1, 'Em processo de negociação', NOW() - INTERVAL '19 months', NOW() - INTERVAL '2 weeks'),
(29, 'CTR-2023-000013', 65000.00, 71500.00, 1.70, 2.00, 0.38, NOW() - INTERVAL '21 months', NOW() - INTERVAL '9 months', 1, 'Acordo empresarial vigente', NOW() - INTERVAL '21 months', NOW() - INTERVAL '3 weeks'),
(8, 'CTR-2024-000014', 9500.00, 10450.00, 2.90, 2.00, 0.58, NOW() - INTERVAL '11 months', NOW() - INTERVAL '1 month', 1, 'Renegociação recente', NOW() - INTERVAL '11 months', NOW() - INTERVAL '1 week'),
(30, 'CTR-2024-000015', 45000.00, 49500.00, 1.90, 2.00, 0.43, NOW() - INTERVAL '16 months', NOW() - INTERVAL '4 months', 1, 'Acordo de parcelamento ativo', NOW() - INTERVAL '16 months', NOW() - INTERVAL '5 days');

-- Contratos Pagos (Status = 2)
INSERT INTO "Contratos" ("ClienteId", "NumeroContrato", "ValorOriginal", "SaldoDevedor", "TaxaJurosMensal", "TaxaMulta", "TaxaCorrecaoMonetaria", "DataContrato", "DataVencimento", "Status", "Observacoes", "DataCadastro", "DataAtualizacao")
VALUES
(11, 'CTR-2022-000016', 10000.00, 0.00, 2.40, 2.00, 0.48, NOW() - INTERVAL '30 months', NOW() - INTERVAL '18 months', 2, 'Quitado integralmente', NOW() - INTERVAL '30 months', NOW() - INTERVAL '6 months'),
(12, 'CTR-2022-000017', 15500.00, 0.00, 2.20, 2.00, 0.44, NOW() - INTERVAL '28 months', NOW() - INTERVAL '16 months', 2, 'Liquidado com desconto', NOW() - INTERVAL '28 months', NOW() - INTERVAL '8 months'),
(31, 'CTR-2022-000018', 28000.00, 0.00, 1.85, 2.00, 0.40, NOW() - INTERVAL '32 months', NOW() - INTERVAL '20 months', 2, 'Empréstimo quitado', NOW() - INTERVAL '32 months', NOW() - INTERVAL '12 months'),
(13, 'CTR-2023-000019', 7200.00, 0.00, 3.10, 2.00, 0.62, NOW() - INTERVAL '26 months', NOW() - INTERVAL '14 months', 2, 'Pago antecipadamente', NOW() - INTERVAL '26 months', NOW() - INTERVAL '7 months'),
(14, 'CTR-2023-000020', 19800.00, 0.00, 2.35, 2.00, 0.47, NOW() - INTERVAL '24 months', NOW() - INTERVAL '12 months', 2, 'Quitação total realizada', NOW() - INTERVAL '24 months', NOW() - INTERVAL '5 months');

-- Contratos Cancelados (Status = 3)
INSERT INTO "Contratos" ("ClienteId", "NumeroContrato", "ValorOriginal", "SaldoDevedor", "TaxaJurosMensal", "TaxaMulta", "TaxaCorrecaoMonetaria", "DataContrato", "DataVencimento", "Status", "Observacoes", "DataCadastro", "DataAtualizacao")
VALUES
(16, 'CTR-2023-000021', 5500.00, 0.00, 3.20, 2.00, 0.64, NOW() - INTERVAL '15 months', NOW() - INTERVAL '3 months', 3, 'Cancelado por fraude', NOW() - INTERVAL '15 months', NOW() - INTERVAL '2 months'),
(32, 'CTR-2023-000022', 42000.00, 0.00, 1.95, 2.00, 0.42, NOW() - INTERVAL '18 months', NOW() - INTERVAL '6 months', 3, 'Cancelamento por acordo judicial', NOW() - INTERVAL '18 months', NOW() - INTERVAL '4 months'),
(17, 'CTR-2024-000023', 8800.00, 0.00, 2.85, 2.00, 0.57, NOW() - INTERVAL '12 months', NOW() - INTERVAL '2 months', 3, 'Contrato anulado', NOW() - INTERVAL '12 months', NOW() - INTERVAL '1 month');

-- Contratos adicionais para teste
INSERT INTO "Contratos" ("ClienteId", "NumeroContrato", "ValorOriginal", "SaldoDevedor", "TaxaJurosMensal", "TaxaMulta", "TaxaCorrecaoMonetaria", "DataContrato", "DataVencimento", "Status", "Observacoes", "DataCadastro", "DataAtualizacao")
VALUES
(18, 'CTR-2024-000024', 13500.00, 14580.00, 2.45, 2.00, 0.49, NOW() - INTERVAL '9 months', NOW() - INTERVAL '2 weeks', 0, 'Atraso recente', NOW() - INTERVAL '9 months', NOW() - INTERVAL '2 days'),
(19, 'CTR-2024-000025', 27500.00, 29975.00, 2.15, 2.00, 0.43, NOW() - INTERVAL '11 months', NOW() - INTERVAL '3 weeks', 0, 'Inadimplência inicial', NOW() - INTERVAL '11 months', NOW() - INTERVAL '1 day'),
(20, 'CTR-2024-000026', 16200.00, 17496.00, 2.55, 2.00, 0.51, NOW() - INTERVAL '10 months', NOW() - INTERVAL '1 month', 0, 'Pendente de negociação', NOW() - INTERVAL '10 months', NOW()),
(33, 'CTR-2023-000027', 95000.00, 108100.00, 1.65, 2.00, 0.37, NOW() - INTERVAL '25 months', NOW() - INTERVAL '13 months', 0, 'Inadimplência grave', NOW() - INTERVAL '25 months', NOW() - INTERVAL '1 week'),
(34, 'CTR-2024-000028', 38000.00, 41800.00, 2.05, 2.00, 0.41, NOW() - INTERVAL '14 months', NOW() - INTERVAL '2 months', 1, 'Acordo recente', NOW() - INTERVAL '14 months', NOW() - INTERVAL '2 weeks'),
(21, 'CTR-2024-000029', 11300.00, 12373.00, 2.70, 2.00, 0.54, NOW() - INTERVAL '8 months', NOW() - INTERVAL '3 weeks', 0, 'Atraso menor', NOW() - INTERVAL '8 months', NOW() - INTERVAL '3 days'),
(22, 'CTR-2024-000030', 20500.00, 22140.00, 2.25, 2.00, 0.45, NOW() - INTERVAL '12 months', NOW() - INTERVAL '2 months', 0, 'Em processo de cobrança', NOW() - INTERVAL '12 months', NOW() - INTERVAL '5 days');

-- ====================================================================
-- PARCELAS (gerando múltiplas parcelas por contrato)
-- ====================================================================

-- Parcelas do Contrato 1 (12 parcelas - várias vencidas)
INSERT INTO "Parcelas" ("ContratoId", "NumeroParcela", "ValorOriginal", "ValorAtualizado", "DataVencimento", "DataPagamento", "ValorPago", "Status", "DiasAtraso", "DataCadastro", "DataAtualizacao")
VALUES
(1, 1, 1250.00, 1562.50, NOW() - INTERVAL '18 months', NOW() - INTERVAL '17 months', 1250.00, 1, NULL, NOW() - INTERVAL '18 months', NOW() - INTERVAL '17 months'),
(1, 2, 1250.00, 1562.50, NOW() - INTERVAL '17 months', NOW() - INTERVAL '16 months', 1250.00, 1, NULL, NOW() - INTERVAL '18 months', NOW() - INTERVAL '16 months'),
(1, 3, 1250.00, 1562.50, NOW() - INTERVAL '16 months', NOW() - INTERVAL '15 months', 1250.00, 1, NULL, NOW() - INTERVAL '18 months', NOW() - INTERVAL '15 months'),
(1, 4, 1250.00, 1562.50, NOW() - INTERVAL '15 months', NULL, NULL, 2, 450, NOW() - INTERVAL '18 months', NOW() - INTERVAL '1 day'),
(1, 5, 1250.00, 1562.50, NOW() - INTERVAL '14 months', NULL, NULL, 2, 420, NOW() - INTERVAL '18 months', NOW() - INTERVAL '1 day'),
(1, 6, 1250.00, 1562.50, NOW() - INTERVAL '13 months', NULL, NULL, 2, 390, NOW() - INTERVAL '18 months', NOW() - INTERVAL '1 day'),
(1, 7, 1250.00, 1562.50, NOW() - INTERVAL '12 months', NULL, NULL, 2, 360, NOW() - INTERVAL '18 months', NOW() - INTERVAL '1 day'),
(1, 8, 1250.00, 1562.50, NOW() - INTERVAL '11 months', NULL, NULL, 2, 330, NOW() - INTERVAL '18 months', NOW() - INTERVAL '1 day'),
(1, 9, 1250.00, 1562.50, NOW() - INTERVAL '10 months', NULL, NULL, 2, 300, NOW() - INTERVAL '18 months', NOW() - INTERVAL '1 day'),
(1, 10, 1250.00, 1562.50, NOW() - INTERVAL '9 months', NULL, NULL, 2, 270, NOW() - INTERVAL '18 months', NOW() - INTERVAL '1 day'),
(1, 11, 1250.00, 1562.50, NOW() - INTERVAL '8 months', NULL, NULL, 2, 240, NOW() - INTERVAL '18 months', NOW() - INTERVAL '1 day'),
(1, 12, 1250.00, 1562.50, NOW() - INTERVAL '7 months', NULL, NULL, 2, 210, NOW() - INTERVAL '18 months', NOW() - INTERVAL '1 day');

-- Parcelas do Contrato 2 (24 parcelas - várias pagas, algumas vencidas)
INSERT INTO "Parcelas" ("ContratoId", "NumeroParcela", "ValorOriginal", "ValorAtualizado", "DataVencimento", "DataPagamento", "ValorPago", "Status", "DiasAtraso", "DataCadastro", "DataAtualizacao")
VALUES
(2, 1, 1041.67, 1302.09, NOW() - INTERVAL '16 months', NOW() - INTERVAL '15 months 15 days', 1041.67, 1, NULL, NOW() - INTERVAL '16 months', NOW() - INTERVAL '15 months 15 days'),
(2, 2, 1041.67, 1302.09, NOW() - INTERVAL '15 months', NOW() - INTERVAL '14 months 20 days', 1041.67, 1, NULL, NOW() - INTERVAL '16 months', NOW() - INTERVAL '14 months 20 days'),
(2, 3, 1041.67, 1302.09, NOW() - INTERVAL '14 months', NOW() - INTERVAL '13 months 10 days', 1041.67, 1, NULL, NOW() - INTERVAL '16 months', NOW() - INTERVAL '13 months 10 days'),
(2, 4, 1041.67, 1302.09, NOW() - INTERVAL '13 months', NOW() - INTERVAL '12 months 25 days', 1041.67, 1, NULL, NOW() - INTERVAL '16 months', NOW() - INTERVAL '12 months 25 days'),
(2, 5, 1041.67, 1302.09, NOW() - INTERVAL '12 months', NOW() - INTERVAL '11 months 18 days', 1041.67, 1, NULL, NOW() - INTERVAL '16 months', NOW() - INTERVAL '11 months 18 days'),
(2, 6, 1041.67, 1302.09, NOW() - INTERVAL '11 months', NOW() - INTERVAL '10 months 22 days', 1041.67, 1, NULL, NOW() - INTERVAL '16 months', NOW() - INTERVAL '10 months 22 days'),
(2, 7, 1041.67, 1302.09, NOW() - INTERVAL '10 months', NULL, NULL, 2, 300, NOW() - INTERVAL '16 months', NOW() - INTERVAL '2 days'),
(2, 8, 1041.67, 1302.09, NOW() - INTERVAL '9 months', NULL, NULL, 2, 270, NOW() - INTERVAL '16 months', NOW() - INTERVAL '2 days'),
(2, 9, 1041.67, 1302.09, NOW() - INTERVAL '8 months', NULL, NULL, 2, 240, NOW() - INTERVAL '16 months', NOW() - INTERVAL '2 days'),
(2, 10, 1041.67, 1302.09, NOW() - INTERVAL '7 months', NULL, NULL, 2, 210, NOW() - INTERVAL '16 months', NOW() - INTERVAL '2 days'),
(2, 11, 1041.67, 1302.09, NOW() - INTERVAL '6 months', NULL, NULL, 2, 180, NOW() - INTERVAL '16 months', NOW() - INTERVAL '2 days'),
(2, 12, 1041.67, 1302.09, NOW() - INTERVAL '5 months', NULL, NULL, 2, 150, NOW() - INTERVAL '16 months', NOW() - INTERVAL '2 days');

-- Parcelas do Contrato 3 (10 parcelas - algumas pagas, algumas em aberto)
INSERT INTO "Parcelas" ("ContratoId", "NumeroParcela", "ValorOriginal", "ValorAtualizado", "DataVencimento", "DataPagamento", "ValorPago", "Status", "DiasAtraso", "DataCadastro", "DataAtualizacao")
VALUES
(3, 1, 850.00, 1105.00, NOW() - INTERVAL '14 months', NOW() - INTERVAL '13 months 5 days', 850.00, 1, NULL, NOW() - INTERVAL '14 months', NOW() - INTERVAL '13 months 5 days'),
(3, 2, 850.00, 1105.00, NOW() - INTERVAL '13 months', NOW() - INTERVAL '12 months 10 days', 850.00, 1, NULL, NOW() - INTERVAL '14 months', NOW() - INTERVAL '12 months 10 days'),
(3, 3, 850.00, 1105.00, NOW() - INTERVAL '12 months', NOW() - INTERVAL '11 months 15 days', 850.00, 1, NULL, NOW() - INTERVAL '14 months', NOW() - INTERVAL '11 months 15 days'),
(3, 4, 850.00, 1105.00, NOW() - INTERVAL '11 months', NOW() - INTERVAL '10 months 8 days', 850.00, 1, NULL, NOW() - INTERVAL '14 months', NOW() - INTERVAL '10 months 8 days'),
(3, 5, 850.00, 1105.00, NOW() - INTERVAL '10 months', NULL, NULL, 2, 300, NOW() - INTERVAL '14 months', NOW() - INTERVAL '1 week'),
(3, 6, 850.00, 1105.00, NOW() - INTERVAL '9 months', NULL, NULL, 2, 270, NOW() - INTERVAL '14 months', NOW() - INTERVAL '1 week'),
(3, 7, 850.00, 1105.00, NOW() - INTERVAL '8 months', NULL, NULL, 2, 240, NOW() - INTERVAL '14 months', NOW() - INTERVAL '1 week'),
(3, 8, 850.00, 1105.00, NOW() - INTERVAL '7 months', NULL, NULL, 0, 0, NOW() - INTERVAL '14 months', NOW() - INTERVAL '1 week'),
(3, 9, 850.00, 850.00, NOW() - INTERVAL '6 months', NULL, NULL, 0, 0, NOW() - INTERVAL '14 months', NOW() - INTERVAL '1 week'),
(3, 10, 850.00, 850.00, NOW() - INTERVAL '5 months', NULL, NULL, 0, 0, NOW() - INTERVAL '14 months', NOW() - INTERVAL '1 week');

-- Parcelas do Contrato 6 (Pessoa Jurídica - 36 parcelas)
INSERT INTO "Parcelas" ("ContratoId", "NumeroParcela", "ValorOriginal", "ValorAtualizado", "DataVencimento", "DataPagamento", "ValorPago", "Status", "DiasAtraso", "DataCadastro", "DataAtualizacao")
VALUES
(6, 1, 2222.22, 2800.00, NOW() - INTERVAL '24 months', NOW() - INTERVAL '23 months', 2222.22, 1, NULL, NOW() - INTERVAL '24 months', NOW() - INTERVAL '23 months'),
(6, 2, 2222.22, 2800.00, NOW() - INTERVAL '23 months', NOW() - INTERVAL '22 months', 2222.22, 1, NULL, NOW() - INTERVAL '24 months', NOW() - INTERVAL '22 months'),
(6, 3, 2222.22, 2800.00, NOW() - INTERVAL '22 months', NOW() - INTERVAL '21 months', 2222.22, 1, NULL, NOW() - INTERVAL '24 months', NOW() - INTERVAL '21 months'),
(6, 4, 2222.22, 2800.00, NOW() - INTERVAL '21 months', NOW() - INTERVAL '20 months', 2222.22, 1, NULL, NOW() - INTERVAL '24 months', NOW() - INTERVAL '20 months'),
(6, 5, 2222.22, 2800.00, NOW() - INTERVAL '20 months', NOW() - INTERVAL '19 months', 2222.22, 1, NULL, NOW() - INTERVAL '24 months', NOW() - INTERVAL '19 months'),
(6, 6, 2222.22, 2800.00, NOW() - INTERVAL '19 months', NULL, NULL, 2, 570, NOW() - INTERVAL '24 months', NOW() - INTERVAL '1 day'),
(6, 7, 2222.22, 2800.00, NOW() - INTERVAL '18 months', NULL, NULL, 2, 540, NOW() - INTERVAL '24 months', NOW() - INTERVAL '1 day'),
(6, 8, 2222.22, 2800.00, NOW() - INTERVAL '17 months', NULL, NULL, 2, 510, NOW() - INTERVAL '24 months', NOW() - INTERVAL '1 day'),
(6, 9, 2222.22, 2800.00, NOW() - INTERVAL '16 months', NULL, NULL, 2, 480, NOW() - INTERVAL '24 months', NOW() - INTERVAL '1 day'),
(6, 10, 2222.22, 2800.00, NOW() - INTERVAL '15 months', NULL, NULL, 2, 450, NOW() - INTERVAL '24 months', NOW() - INTERVAL '1 day');

-- ====================================================================
-- ACORDOS (alguns com boletos gerados)
-- ====================================================================

-- Acordos Ativos (Status = 0)
INSERT INTO "Acordos" ("ContratoId", "NumeroAcordo", "ValorTotalDivida", "ValorDesconto", "ValorTotalAcordo", "ValorEntrada", "QuantidadeParcelas", "ValorParcela", "DataPrimeiroVencimento", "DataAcordo", "Status", "Observacoes", "DataCadastro", "DataAtualizacao")
VALUES
(11, 'ACO-2024-000001', 19800.00, 3960.00, 15840.00, 2000.00, 12, 1153.33, NOW() + INTERVAL '5 days', NOW() - INTERVAL '1 month', 0, 'Acordo ativo com 20% de desconto', NOW() - INTERVAL '1 month', NOW() - INTERVAL '1 day'),
(12, 'ACO-2024-000002', 35200.00, 7040.00, 28160.00, 5000.00, 24, 965.00, NOW() + INTERVAL '10 days', NOW() - INTERVAL '2 weeks', 0, 'Parcelamento em 24x com entrada', NOW() - INTERVAL '2 weeks', NOW() - INTERVAL '2 days'),
(13, 'ACO-2024-000003', 71500.00, 14300.00, 57200.00, 10000.00, 36, 1311.11, NOW() + INTERVAL '15 days', NOW() - INTERVAL '3 weeks', 0, 'Acordo empresarial - 20% desconto', NOW() - INTERVAL '3 weeks', NOW() - INTERVAL '1 week'),
(14, 'ACO-2024-000004', 10450.00, 1567.50, 8882.50, 1000.00, 10, 788.25, NOW() + INTERVAL '7 days', NOW() - INTERVAL '1 week', 0, 'Renegociação com 15% de desconto', NOW() - INTERVAL '1 week', NOW() - INTERVAL '3 days'),
(15, 'ACO-2024-000005', 49500.00, 9900.00, 39600.00, 8000.00, 18, 1755.56, NOW() + INTERVAL '20 days', NOW() - INTERVAL '5 days', 0, 'Acordo de 18 parcelas', NOW() - INTERVAL '5 days', NOW() - INTERVAL '1 day');

-- Acordos Cancelados (Status = 1)
INSERT INTO "Acordos" ("ContratoId", "NumeroAcordo", "ValorTotalDivida", "ValorDesconto", "ValorTotalAcordo", "ValorEntrada", "QuantidadeParcelas", "ValorParcela", "DataPrimeiroVencimento", "DataAcordo", "Status", "Observacoes", "DataCadastro", "DataAtualizacao")
VALUES
(7, 'ACO-2023-000006', 39200.00, 5880.00, 33320.00, 0.00, 20, 1666.00, NOW() - INTERVAL '5 months', NOW() - INTERVAL '6 months', 1, 'Cancelado por não cumprimento', NOW() - INTERVAL '6 months', NOW() - INTERVAL '4 months'),
(28, 'ACO-2024-000007', 41800.00, 6270.00, 35530.00, 5000.00, 15, 2035.33, NOW() - INTERVAL '2 months', NOW() - INTERVAL '3 months', 1, 'Acordo cancelado - cliente inadimplente', NOW() - INTERVAL '3 months', NOW() - INTERVAL '1 month');

-- Acordos Concluídos (Status = 2)
INSERT INTO "Acordos" ("ContratoId", "NumeroAcordo", "ValorTotalDivida", "ValorDesconto", "ValorTotalAcordo", "ValorEntrada", "QuantidadeParcelas", "ValorParcela", "DataPrimeiroVencimento", "DataAcordo", "Status", "Observacoes", "DataCadastro", "DataAtualizacao")
VALUES
(16, 'ACO-2023-000008', 12000.00, 2400.00, 9600.00, 0.00, 12, 800.00, NOW() - INTERVAL '18 months', NOW() - INTERVAL '19 months', 2, 'Acordo quitado integralmente', NOW() - INTERVAL '19 months', NOW() - INTERVAL '6 months'),
(17, 'ACO-2023-000009', 18500.00, 3700.00, 14800.00, 2000.00, 10, 1280.00, NOW() - INTERVAL '16 months', NOW() - INTERVAL '17 months', 2, 'Liquidado com sucesso', NOW() - INTERVAL '17 months', NOW() - INTERVAL '8 months');

-- ====================================================================
-- BOLETOS (gerados para os acordos ativos)
-- ====================================================================

-- Boletos do Acordo 1 (12 boletos - alguns já vencidos)
INSERT INTO "Boletos" ("AcordoId", "NossoNumero", "LinhaDigitavel", "CodigoBarras", "Valor", "DataVencimento", "DataPagamento", "ValorPago", "Status", "DocumentoPdfBase64", "DataEmissao", "DataAtualizacao")
VALUES
(1, '000000001-0', '23790.00001 00000.000001 00000.000001 0 00000000000000', '23790000000000000000000000000000000000000001', 2000.00, NOW() - INTERVAL '1 month' + INTERVAL '5 days', NULL, NULL, 2, NULL, NOW() - INTERVAL '1 month', NOW() - INTERVAL '1 day'),
(1, '000000002-8', '23790.00001 00000.000002 00000.000002 8 00000000000115', '23790000000000000000000000000000000000000002', 1153.33, NOW() + INTERVAL '5 days', NULL, NULL, 0, NULL, NOW() - INTERVAL '1 month', NOW()),
(1, '000000003-6', '23790.00001 00000.000003 00000.000003 6 00000000000115', '23790000000000000000000000000000000000000003', 1153.33, NOW() + INTERVAL '1 month' + INTERVAL '5 days', NULL, NULL, 0, NULL, NOW() - INTERVAL '1 month', NOW()),
(1, '000000004-4', '23790.00001 00000.000004 00000.000004 4 00000000000115', '23790000000000000000000000000000000000000004', 1153.33, NOW() + INTERVAL '2 months' + INTERVAL '5 days', NULL, NULL, 0, NULL, NOW() - INTERVAL '1 month', NOW()),
(1, '000000005-2', '23790.00001 00000.000005 00000.000005 2 00000000000115', '23790000000000000000000000000000000000000005', 1153.33, NOW() + INTERVAL '3 months' + INTERVAL '5 days', NULL, NULL, 0, NULL, NOW() - INTERVAL '1 month', NOW());

-- Boletos do Acordo 2 (24 boletos - entrada paga, demais em aberto)
INSERT INTO "Boletos" ("AcordoId", "NossoNumero", "LinhaDigitavel", "CodigoBarras", "Valor", "DataVencimento", "DataPagamento", "ValorPago", "Status", "DocumentoPdfBase64", "DataEmissao", "DataAtualizacao")
VALUES
(2, '000000006-0', '23790.00001 00000.000006 00000.000006 0 00000000000500', '23790000000000000000000000000000000000000006', 5000.00, NOW() - INTERVAL '2 weeks' + INTERVAL '10 days', NOW() - INTERVAL '1 week', 5000.00, 1, NULL, NOW() - INTERVAL '2 weeks', NOW() - INTERVAL '1 week'),
(2, '000000007-9', '23790.00001 00000.000007 00000.000007 9 00000000000096', '23790000000000000000000000000000000000000007', 965.00, NOW() + INTERVAL '10 days', NULL, NULL, 0, NULL, NOW() - INTERVAL '2 weeks', NOW()),
(2, '000000008-7', '23790.00001 00000.000008 00000.000008 7 00000000000096', '23790000000000000000000000000000000000000008', 965.00, NOW() + INTERVAL '1 month' + INTERVAL '10 days', NULL, NULL, 0, NULL, NOW() - INTERVAL '2 weeks', NOW()),
(2, '000000009-5', '23790.00001 00000.000009 00000.000009 5 00000000000096', '23790000000000000000000000000000000000000009', 965.00, NOW() + INTERVAL '2 months' + INTERVAL '10 days', NULL, NULL, 0, NULL, NOW() - INTERVAL '2 weeks', NOW());

-- Boletos do Acordo 3 (36 boletos - entrada paga, primeiros em aberto)
INSERT INTO "Boletos" ("AcordoId", "NossoNumero", "LinhaDigitavel", "CodigoBarras", "Valor", "DataVencimento", "DataPagamento", "ValorPago", "Status", "DocumentoPdfBase64", "DataEmissao", "DataAtualizacao")
VALUES
(3, '000000010-9', '23790.00001 00000.000010 00000.000010 9 00000000001000', '23790000000000000000000000000000000000000010', 10000.00, NOW() - INTERVAL '3 weeks' + INTERVAL '15 days', NOW() - INTERVAL '2 weeks', 10000.00, 1, NULL, NOW() - INTERVAL '3 weeks', NOW() - INTERVAL '2 weeks'),
(3, '000000011-7', '23790.00001 00000.000011 00000.000011 7 00000000000131', '23790000000000000000000000000000000000000011', 1311.11, NOW() + INTERVAL '15 days', NULL, NULL, 0, NULL, NOW() - INTERVAL '3 weeks', NOW()),
(3, '000000012-5', '23790.00001 00000.000012 00000.000012 5 00000000000131', '23790000000000000000000000000000000000000012', 1311.11, NOW() + INTERVAL '1 month' + INTERVAL '15 days', NULL, NULL, 0, NULL, NOW() - INTERVAL '3 weeks', NOW()),
(3, '000000013-3', '23790.00001 00000.000013 00000.000013 3 00000000000131', '23790000000000000000000000000000000000000013', 1311.11, NOW() + INTERVAL '2 months' + INTERVAL '15 days', NULL, NULL, 0, NULL, NOW() - INTERVAL '3 weeks', NOW());

-- Boletos do Acordo 4 (10 boletos - entrada + 9 parcelas)
INSERT INTO "Boletos" ("AcordoId", "NossoNumero", "LinhaDigitavel", "CodigoBarras", "Valor", "DataVencimento", "DataPagamento", "ValorPago", "Status", "DocumentoPdfBase64", "DataEmissao", "DataAtualizacao")
VALUES
(4, '000000014-1', '23790.00001 00000.000014 00000.000014 1 00000000000100', '23790000000000000000000000000000000000000014', 1000.00, NOW() - INTERVAL '1 week' + INTERVAL '7 days', NOW() - INTERVAL '5 days', 1000.00, 1, NULL, NOW() - INTERVAL '1 week', NOW() - INTERVAL '5 days'),
(4, '000000015-0', '23790.00001 00000.000015 00000.000015 0 00000000000078', '23790000000000000000000000000000000000000015', 788.25, NOW() + INTERVAL '7 days', NULL, NULL, 0, NULL, NOW() - INTERVAL '1 week', NOW()),
(4, '000000016-8', '23790.00001 00000.000016 00000.000016 8 00000000000078', '23790000000000000000000000000000000000000016', 788.25, NOW() + INTERVAL '1 month' + INTERVAL '7 days', NULL, NULL, 0, NULL, NOW() - INTERVAL '1 week', NOW());

-- Boletos do Acordo 5 (18 boletos - entrada + 17 parcelas)
INSERT INTO "Boletos" ("AcordoId", "NossoNumero", "LinhaDigitavel", "CodigoBarras", "Valor", "DataVencimento", "DataPagamento", "ValorPago", "Status", "DocumentoPdfBase64", "DataEmissao", "DataAtualizacao")
VALUES
(5, '000000017-6', '23790.00001 00000.000017 00000.000017 6 00000000000800', '23790000000000000000000000000000000000000017', 8000.00, NOW() - INTERVAL '5 days' + INTERVAL '20 days', NULL, NULL, 0, NULL, NOW() - INTERVAL '5 days', NOW()),
(5, '000000018-4', '23790.00001 00000.000018 00000.000018 4 00000000000175', '23790000000000000000000000000000000000000018', 1755.56, NOW() + INTERVAL '20 days', NULL, NULL, 0, NULL, NOW() - INTERVAL '5 days', NOW()),
(5, '000000019-2', '23790.00001 00000.000019 00000.000019 2 00000000000175', '23790000000000000000000000000000000000000019', 1755.56, NOW() + INTERVAL '1 month' + INTERVAL '20 days', NULL, NULL, 0, NULL, NOW() - INTERVAL '5 days', NOW()),
(5, '000000020-6', '23790.00001 00000.000020 00000.000020 6 00000000000175', '23790000000000000000000000000000000000000020', 1755.56, NOW() + INTERVAL '2 months' + INTERVAL '20 days', NULL, NULL, 0, NULL, NOW() - INTERVAL '5 days', NOW());

-- ====================================================================
-- FIM DO SCRIPT
-- ====================================================================

-- Verificação final - contadores
SELECT 'Clientes cadastrados: ' || COUNT(*) FROM "Clientes";
SELECT 'Contratos cadastrados: ' || COUNT(*) FROM "Contratos";
SELECT 'Parcelas cadastradas: ' || COUNT(*) FROM "Parcelas";
SELECT 'Acordos cadastrados: ' || COUNT(*) FROM "Acordos";
SELECT 'Boletos cadastrados: ' || COUNT(*) FROM "Boletos";

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
