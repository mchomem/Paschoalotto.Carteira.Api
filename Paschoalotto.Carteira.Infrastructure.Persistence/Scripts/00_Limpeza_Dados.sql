-- ====================================================================
-- Script de Limpeza de Dados - Paschoalotto Carteira API
-- ====================================================================
-- Descrição: Remove todos os dados das tabelas mantendo a estrutura
-- Ambiente: Desenvolvimento/Testes
-- Database: PostgreSQL
-- ====================================================================
-- ⚠️ ATENÇÃO: Este script apaga TODOS os dados do banco!
-- Use apenas em ambientes de desenvolvimento/testes
-- ====================================================================

-- Confirmar antes de executar
DO $$ 
BEGIN 
	RAISE NOTICE '⚠️  ATENÇÃO: Este script irá APAGAR TODOS OS DADOS!';
	RAISE NOTICE 'Aguardando 3 segundos...';
	PERFORM pg_sleep
(3);
	RAISE NOTICE 'Iniciando limpeza...';
END $$;

-- Desabilitar verificação de chaves estrangeiras temporariamente
SET session_replication_role
= 'replica';

-- Limpar dados em ordem (respeitando dependências)
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

-- Reabilitar verificação de chaves estrangeiras
SET session_replication_role
= 'origin';

-- Resetar sequências (auto-increment)
ALTER SEQUENCE "Clientes_Id_seq" RESTART WITH 1;
ALTER SEQUENCE "Contratos_Id_seq" RESTART WITH 1;
ALTER SEQUENCE "Parcelas_Id_seq" RESTART WITH 1;
ALTER SEQUENCE "Acordos_Id_seq" RESTART WITH 1;
ALTER SEQUENCE "ParcelasAcordo_Id_seq" RESTART WITH 1;
ALTER SEQUENCE "Boletos_Id_seq" RESTART WITH 1;

-- Verificar limpeza
	SELECT
		'Clientes' AS "Tabela", COUNT(*) AS "Registros"
	FROM "Clientes"
UNION ALL
	SELECT
		'Contratos', COUNT(*)
	FROM "Contratos"
UNION ALL
	SELECT
		'Parcelas', COUNT(*)
	FROM "Parcelas"
UNION ALL
	SELECT
		'Acordos', COUNT(*)
	FROM "Acordos"
UNION ALL
	SELECT
		'ParcelasAcordo', COUNT(*)
	FROM "ParcelasAcordo"
UNION ALL
	SELECT
		'Boletos', COUNT(*)
	FROM "Boletos";

-- Mensagem final
DO $$ 
BEGIN 
	RAISE NOTICE '✅ Limpeza concluída com sucesso!';
	RAISE NOTICE 'Todas as tabelas foram esvaziadas e sequências resetadas.';
END $$;

-- ====================================================================
-- FIM DO SCRIPT
-- ====================================================================
