using Paschoalotto.Carteira.Core.Application.DTOs.Boleto;
using Paschoalotto.Carteira.Core.Application.Interfaces;
using Paschoalotto.Carteira.Core.Domain.Entities;
using Paschoalotto.Carteira.Core.Domain.Enums;
using Paschoalotto.Carteira.Core.Domain.Exceptions.Acordo;
using Paschoalotto.Carteira.Core.Domain.Exceptions.Boleto;
using Paschoalotto.Carteira.Core.Domain.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Paschoalotto.Carteira.Core.Application.Services;

public class BoletoService : IBoletoService
{
    private readonly IBoletoRepository _boletoRepository;
    private readonly IAcordoRepository _acordoRepository;
    private readonly IContratoRepository _contratoRepository;
    private readonly IClienteRepository _clienteRepository;

    public BoletoService(
        IBoletoRepository boletoRepository,
        IAcordoRepository acordoRepository,
        IContratoRepository contratoRepository,
        IClienteRepository clienteRepository)
    {
        _boletoRepository = boletoRepository;
        _acordoRepository = acordoRepository;
        _contratoRepository = contratoRepository;
        _clienteRepository = clienteRepository;

        // Configurar licença do QuestPDF (Community license)
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public async Task<BoletoResponseDto> GerarBoletoAsync(BoletoRequestDto request)
    {
        // Validar acordo
        var acordo = await _acordoRepository.GetByIdAsync(request.AcordoId);
        if (acordo == null)
            throw new AcordoNotFoundException(request.AcordoId);

        if (acordo.Status != StatusAcordo.Ativo)
            throw new BoletoInvalidOperationException($"Não é possível gerar boleto para acordo com status {acordo.Status}");

        // Gerar nosso número único
        var nossoNumero = GerarNossoNumero();

        // Gerar linha digitável e código de barras (simplificado)
        var linhaDigitavel = GerarLinhaDigitavel(nossoNumero, acordo.ValorParcela);
        var codigoBarras = GerarCodigoBarras(nossoNumero, acordo.ValorParcela);

        // Criar boleto
        var boleto = new Boleto
        {
            AcordoId = request.AcordoId,
            NossoNumero = nossoNumero,
            LinhaDigitavel = linhaDigitavel,
            CodigoBarras = codigoBarras,
            Valor = acordo.ValorParcela,
            DataVencimento = acordo.DataPrimeiroVencimento,
            Status = StatusBoleto.Pendente,
            DataEmissao = DateTime.Now
        };

        var boletoCriado = await _boletoRepository.AddAsync(boleto);

        return MapToResponseDto(boletoCriado);
    }

    public async Task<BoletoPdfResponseDto> GerarBoletoPdfAsync(int boletoId)
    {
        var boleto = await _boletoRepository.GetByIdAsync(boletoId);
        if (boleto == null)
            throw new BoletoNotFoundException(boletoId);

        var acordo = await _acordoRepository.GetByIdAsync(boleto.AcordoId);
        var contrato = await _contratoRepository.GetByIdAsync(acordo!.ContratoId);
        var cliente = await _clienteRepository.GetByIdAsync(contrato!.ClienteId);

        // Gerar PDF
        var pdfBytes = GerarPdfBoleto(boleto, acordo, contrato, cliente!);
        var pdfBase64 = Convert.ToBase64String(pdfBytes);

        // Atualizar boleto com PDF
        boleto.DocumentoPdfBase64 = pdfBase64;
        boleto.DataAtualizacao = DateTime.Now;
        await _boletoRepository.UpdateAsync(boleto);

        return new BoletoPdfResponseDto
        {
            BoletoId = boleto.Id,
            NossoNumero = boleto.NossoNumero,
            DocumentoPdfBase64 = pdfBase64,
            ContentType = "application/pdf",
            FileName = $"Boleto_{boleto.NossoNumero}.pdf"
        };
    }

    public async Task<IEnumerable<BoletoResponseDto>> GetByAcordoIdAsync(int acordoId)
    {
        var boletos = await _boletoRepository.GetByAcordoIdAsync(acordoId);
        return boletos.Select(MapToResponseDto);
    }

    public async Task<BoletoResponseDto?> GetByIdAsync(int id)
    {
        var boleto = await _boletoRepository.GetByIdAsync(id);
        return boleto != null ? MapToResponseDto(boleto) : null;
    }

    public async Task<BoletoResponseDto?> GetByNossoNumeroAsync(string nossoNumero)
    {
        var boleto = await _boletoRepository.GetByNossoNumeroAsync(nossoNumero);
        return boleto != null ? MapToResponseDto(boleto) : null;
    }

    public async Task<bool> CancelarBoletoAsync(int boletoId)
    {
        var boleto = await _boletoRepository.GetByIdAsync(boletoId);
        if (boleto == null)
            throw new BoletoNotFoundException(boletoId);

        if (boleto.Status == StatusBoleto.Pago)
            throw new BoletoInvalidOperationException("Não é possível cancelar um boleto já pago.");

        boleto.Status = StatusBoleto.Cancelado;
        boleto.DataAtualizacao = DateTime.Now;
        await _boletoRepository.UpdateAsync(boleto);

        return true;
    }

    private string GerarNossoNumero()
    {
        // Formato: BANCO(3) + AGENCIA(4) + CONTA(7) + SEQUENCIAL(8)
        // Simplificado: timestamp + random
        var timestamp = DateTime.Now.ToString("yyMMddHHmmss");
        var random = new Random().Next(1000, 9999);
        return $"001{timestamp}{random}";
    }

    private string GerarLinhaDigitavel(string nossoNumero, decimal valor)
    {
        // Formato simplificado de linha digitável
        // Na realidade, seria necessário calcular dígitos verificadores
        var codigoBanco = "001"; // Banco do Brasil (exemplo)
        var moeda = "9";
        var sequencial = nossoNumero.PadLeft(20, '0');
        var valorStr = ((int)(valor * 100)).ToString().PadLeft(10, '0');

        // Formato: AAAAA.BBBBB CCCCC.DDDDDD EEEEE.FFFFFF G HHHHHHHHHHHHHH
        var parte1 = $"{codigoBanco}{moeda}{sequencial.Substring(0, 5)}";
        var parte2 = sequencial.Substring(5, 10);
        var parte3 = sequencial.Substring(15, 6);
        var dv = "1"; // Dígito verificador simplificado
        var fatorVencimento = "1234"; // Simplificado

        return $"{parte1.Substring(0, 5)}.{parte1.Substring(5)} {parte2.Substring(0, 5)}.{parte2.Substring(5)} {parte3}.{dv} {fatorVencimento}{valorStr}";
    }

    private string GerarCodigoBarras(string nossoNumero, decimal valor)
    {
        // Código de barras simplificado (44 dígitos)
        var codigoBanco = "001";
        var moeda = "9";
        var dv = "1";
        var fatorVencimento = "1234";
        var valorStr = ((int)(valor * 100)).ToString().PadLeft(10, '0');
        var sequencial = nossoNumero.PadLeft(25, '0');

        return $"{codigoBanco}{moeda}{dv}{fatorVencimento}{valorStr}{sequencial}";
    }

    private byte[] GerarPdfBoleto(Boleto boleto, Acordo acordo, Contrato contrato, Cliente cliente)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10));

                page.Header()
                    .Text("BANCO PASCHOALOTTO")
                    .SemiBold().FontSize(16).FontColor(Colors.Blue.Darken3);

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(col =>
                    {
                        // Informações do Cedente
                        col.Item().BorderBottom(1).PaddingBottom(10).Text("DADOS DO CEDENTE").SemiBold();
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Text($"Beneficiário: Banco Paschoalotto - Carteira de Dívidas");
                            row.RelativeItem().Text($"CNPJ: 00.000.000/0001-00");
                        });

                        col.Item().PaddingTop(20).BorderBottom(1).PaddingBottom(10).Text("DADOS DO PAGADOR").SemiBold();
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Text($"Nome: {cliente.Nome}");
                            row.RelativeItem().Text($"Documento: {cliente.Documento}");
                        });

                        if (!string.IsNullOrEmpty(cliente.Endereco))
                        {
                            col.Item().Text($"Endereço: {cliente.Endereco}");
                            col.Item().Text($"Cidade: {cliente.Cidade} - {cliente.Estado}");
                        }

                        col.Item().PaddingTop(20).BorderBottom(1).PaddingBottom(10).Text("DADOS DO DOCUMENTO").SemiBold();
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Column(c =>
                            {
                                c.Item().Text($"Nosso Número: {boleto.NossoNumero}");
                                c.Item().Text($"Número do Acordo: {acordo.NumeroAcordo}");
                                c.Item().Text($"Número do Contrato: {contrato.NumeroContrato}");
                            });
                            row.RelativeItem().Column(c =>
                            {
                                c.Item().Text($"Data Emissão: {boleto.DataEmissao:dd/MM/yyyy}");
                                c.Item().Text($"Data Vencimento: {boleto.DataVencimento:dd/MM/yyyy}");
                                c.Item().Text($"Valor: R$ {boleto.Valor:N2}").SemiBold().FontSize(12);
                            });
                        });

                        col.Item().PaddingTop(20).BorderBottom(1).PaddingBottom(10).Text("INSTRUÇÕES").SemiBold();
                        col.Item().Text("- Não receber após o vencimento");
                        col.Item().Text("- Após o vencimento, multa de 2% e juros de 1% ao mês");
                        col.Item().Text($"- Acordo referente ao contrato {contrato.NumeroContrato}");

                        col.Item().PaddingTop(20).BorderBottom(1).PaddingBottom(10).Text("CÓDIGO DE BARRAS").SemiBold();
                        col.Item().Text(boleto.CodigoBarras).FontFamily("Courier New").FontSize(8);

                        col.Item().PaddingTop(10).Text("LINHA DIGITÁVEL").SemiBold();
                        col.Item().Text(boleto.LinhaDigitavel).FontFamily("Courier New").FontSize(9);
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Banco Paschoalotto - Sistema de Gestão de Carteira | ").FontSize(8);
                        x.Span($"Gerado em {DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(8);
                    });
            });
        });

        return document.GeneratePdf();
    }

    private BoletoResponseDto MapToResponseDto(Boleto boleto)
    {
        return new BoletoResponseDto
        {
            Id = boleto.Id,
            AcordoId = boleto.AcordoId,
            NossoNumero = boleto.NossoNumero,
            LinhaDigitavel = boleto.LinhaDigitavel,
            CodigoBarras = boleto.CodigoBarras,
            Valor = boleto.Valor,
            DataVencimento = boleto.DataVencimento,
            DataPagamento = boleto.DataPagamento,
            ValorPago = boleto.ValorPago,
            Status = boleto.Status,
            StatusDescricao = boleto.Status.ToString(),
            DataEmissao = boleto.DataEmissao
        };
    }
}
