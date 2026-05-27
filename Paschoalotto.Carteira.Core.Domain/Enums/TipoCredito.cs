namespace Paschoalotto.Carteira.Core.Domain.Enums;

/// <summary>
/// Define o tipo/natureza do crédito originador do contrato de cobrança.
/// Usado internamente no sistema para indicar a origem da dívida.
/// </summary>
public enum TipoCredito
{
    /// <summary>
    /// Venda de produtos/mercadorias via Nota Fiscal
    /// </summary>
    NotaFiscal = 1,

    /// <summary>
    /// Empréstimo pessoal ou empresarial
    /// </summary>
    Emprestimo = 2,

    /// <summary>
    /// Fatura de cartão de crédito
    /// </summary>
    CartaoCredito = 3,

    /// <summary>
    /// Financiamento de bens (veículo, imóvel, etc.)
    /// </summary>
    Financiamento = 4,

    /// <summary>
    /// Prestação de serviços
    /// </summary>
    Servico = 5,

    /// <summary>
    /// Aluguel/locação de imóveis ou equipamentos
    /// </summary>
    Aluguel = 6,

    /// <summary>
    /// Cheque devolvido/sustado
    /// </summary>
    Cheque = 7,

    /// <summary>
    /// Contrato comercial genérico
    /// </summary>
    ContratoComercial = 8,

    /// <summary>
    /// Duplicata de serviço ou mercadoria
    /// </summary>
    Duplicata = 9,

    /// <summary>
    /// Mensalidade de serviços (escola, clube, etc.)
    /// </summary>
    Mensalidade = 10,

    /// <summary>
    /// Outros tipos não especificados
    /// </summary>
    Outros = 99
}
