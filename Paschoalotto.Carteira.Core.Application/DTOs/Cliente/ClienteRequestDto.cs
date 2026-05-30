namespace Paschoalotto.Carteira.Core.Application.DTOs.Cliente;

public class ClienteRequestDto
{
    public TipoPessoa TipoPessoa { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Documento { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string? Endereco { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public string? Cep { get; set; }
}
