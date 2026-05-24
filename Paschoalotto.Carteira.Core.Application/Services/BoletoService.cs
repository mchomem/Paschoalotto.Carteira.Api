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
using SkiaSharp;

namespace Paschoalotto.Carteira.Core.Application.Services;

public class BoletoService : IBoletoService
{
    private readonly IBoletoRepository _boletoRepository;
    private readonly IAcordoRepository _acordoRepository;
    private readonly IParcelaAcordoRepository _parcelaAcordoRepository;
    private readonly IContratoRepository _contratoRepository;
    private readonly IClienteRepository _clienteRepository;

    public BoletoService(
        IBoletoRepository boletoRepository,
        IAcordoRepository acordoRepository,
        IParcelaAcordoRepository parcelaAcordoRepository,
        IContratoRepository contratoRepository,
        IClienteRepository clienteRepository)
    {
        _boletoRepository = boletoRepository;
        _acordoRepository = acordoRepository;
        _parcelaAcordoRepository = parcelaAcordoRepository;
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

        // Buscar parcela específica se informada
        ParcelaAcordo? parcelaAcordo = null;
        decimal valorBoleto = acordo.ValorParcela;
        DateTime dataVencimento = acordo.DataPrimeiroVencimento;

        if (request.ParcelaAcordoId.HasValue)
        {
            parcelaAcordo = await _parcelaAcordoRepository.GetByIdAsync(request.ParcelaAcordoId.Value);
            if (parcelaAcordo == null)
                throw new BoletoInvalidOperationException("Parcela do acordo não encontrada.");

            if (parcelaAcordo.AcordoId != request.AcordoId)
                throw new BoletoInvalidOperationException("A parcela não pertence ao acordo informado.");

            if (parcelaAcordo.Status != StatusParcelaAcordo.Pendente)
                throw new BoletoInvalidOperationException($"Não é possível gerar boleto para parcela com status {parcelaAcordo.Status}");

            // Verificar se já existe boleto para esta parcela
            var boletosExistentes = await _boletoRepository.GetByAcordoIdAsync(request.AcordoId);
            if (boletosExistentes.Any(b => b.ParcelaAcordoId == request.ParcelaAcordoId && b.Status != StatusBoleto.Cancelado))
                throw new BoletoInvalidOperationException("Já existe um boleto ativo para esta parcela.");

            valorBoleto = parcelaAcordo.Valor;
            dataVencimento = parcelaAcordo.DataVencimento;
        }

        // Gerar nosso número único
        var nossoNumero = GerarNossoNumero();

        // Gerar linha digitável e código de barras (simplificado)
        var linhaDigitavel = GerarLinhaDigitavel(nossoNumero, valorBoleto);
        var codigoBarras = GerarCodigoBarras(nossoNumero, valorBoleto);

        // Criar boleto
        var boleto = new Boleto
        {
            AcordoId = request.AcordoId,
            ParcelaAcordoId = request.ParcelaAcordoId,
            NossoNumero = nossoNumero,
            LinhaDigitavel = linhaDigitavel,
            CodigoBarras = codigoBarras,
            Valor = valorBoleto,
            DataVencimento = dataVencimento,
            Status = StatusBoleto.Pendente,
            DataEmissao = DateTime.Now
        };

        var boletoCriado = await _boletoRepository.AddAsync(boleto);

        return MapToResponseDto(boletoCriado, parcelaAcordo);
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
        boleto.DataAtualizacao = DateTime.UtcNow;
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
        return boletos.Select(b => MapToResponseDto(b));
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

                        // Gerar imagem do código de barras
                        var codigoBarrasImagem = GerarImagemCodigoBarras(boleto.CodigoBarras);
                        col.Item().Image(codigoBarrasImagem).FitWidth();

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

    private byte[] GerarImagemCodigoBarras(string codigoBarras)
    {
        try
        {
            // Validar que temos 44 dígitos (padrão FEBRABAN)
            if (codigoBarras.Length != 44 || !codigoBarras.All(char.IsDigit))
            {
                return GerarImagemFallback(codigoBarras);
            }

            // Codificar usando ITF-14 (Intercalado 2 de 5)
            var encodedBars = EncodeITF(codigoBarras);

            // Dimensões da imagem
            const float moduleWidth = 1.0f; // Largura do módulo mais fino
            const int barcodeHeight = 20; // Reduzido para 1/3 (de 60 para 20)
            const int margin = 10;
            const int textHeight = 25;

            var totalWidth = (int)((encodedBars.Length * moduleWidth) + (2 * margin));
            var totalHeight = barcodeHeight + textHeight + (2 * margin);

            using var bitmap = new SKBitmap(totalWidth, totalHeight);
            using var canvas = new SKCanvas(bitmap);
            canvas.Clear(SKColors.White);

            using var blackPaint = new SKPaint
            {
                Color = SKColors.Black,
                Style = SKPaintStyle.Fill,
                IsAntialias = false
            };

            using var textPaint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = 10,
                IsAntialias = true,
                TextAlign = SKTextAlign.Center,
                Typeface = SKTypeface.FromFamilyName("Courier New", SKFontStyle.Normal)
            };

            // Desenhar as barras
            float x = margin;
            for (int i = 0; i < encodedBars.Length; i++)
            {
                if (encodedBars[i] == '1') // Barra preta
                {
                    var rect = new SKRect(x, margin, x + moduleWidth, margin + barcodeHeight);
                    canvas.DrawRect(rect, blackPaint);
                }
                x += moduleWidth;
            }

            // Desenhar o código de barras como texto abaixo
            canvas.DrawText(codigoBarras, totalWidth / 2, totalHeight - 8, textPaint);

            using var image = SKImage.FromBitmap(bitmap);
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            return data.ToArray();
        }
        catch
        {
            return GerarImagemFallback(codigoBarras);
        }
    }

    private string EncodeITF(string data)
    {
        // Tabela de codificação ITF (Intercalado 2 de 5)
        // Cada dígito é representado por 5 barras (2 largas, 3 estreitas)
        var encodingTable = new Dictionary<char, string>
        {
            {'0', "00110"}, // NNWWN
            {'1', "10001"}, // WNNNW
            {'2', "01001"}, // NWNNW
            {'3', "11000"}, // WWNNN
            {'4', "00101"}, // NNWNW
            {'5', "10100"}, // WNWNN
            {'6', "01100"}, // NWWNN
            {'7', "00011"}, // NNNWW
            {'8', "10010"}, // WNNWN
            {'9', "01010"}  // NWNWN
        };

        var result = new System.Text.StringBuilder();

        // Start pattern: 0000 (4 narrow bars)
        result.Append("0000");

        // Processar pares de dígitos (intercalado)
        for (int i = 0; i < data.Length; i += 2)
        {
            var digit1 = encodingTable[data[i]];
            var digit2 = encodingTable[data[i + 1]];

            // Intercalar os dígitos (barras do primeiro, espaços do segundo)
            for (int j = 0; j < 5; j++)
            {
                // Barra do primeiro dígito
                result.Append(digit1[j] == '1' ? "11" : "1");

                // Espaço do segundo dígito
                result.Append(digit2[j] == '1' ? "00" : "0");
            }
        }

        // Stop pattern: 100 (wide bar, narrow space, narrow bar)
        result.Append("1100");

        return result.ToString();
    }

    private byte[] GerarImagemFallback(string codigoBarras)
    {
        using var bitmap = new SKBitmap(600, 85);
        using var canvas = new SKCanvas(bitmap);
        canvas.Clear(SKColors.White);

        using var paint = new SKPaint
        {
            Color = SKColors.Black,
            TextSize = 11,
            IsAntialias = true,
            TextAlign = SKTextAlign.Center,
            Typeface = SKTypeface.FromFamilyName("Courier New", SKFontStyle.Normal)
        };

        canvas.DrawText("Código de barras inválido", 300, 35, paint);
        canvas.DrawText(codigoBarras, 300, 55, paint);

        using var image = SKImage.FromBitmap(bitmap);
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        return data.ToArray();
    }

    private BoletoResponseDto MapToResponseDto(Boleto boleto, ParcelaAcordo? parcelaAcordo = null)
    {
        return new BoletoResponseDto
        {
            Id = boleto.Id,
            AcordoId = boleto.AcordoId,
            ParcelaAcordoId = boleto.ParcelaAcordoId,
            NumeroParcela = parcelaAcordo?.NumeroParcela,
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
