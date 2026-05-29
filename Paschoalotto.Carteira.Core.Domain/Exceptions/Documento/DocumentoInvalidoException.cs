namespace Paschoalotto.Carteira.Core.Domain.Exceptions.Documento;

public class DocumentoInvalidoException : Base.InvalidOperationException
{
    public DocumentoInvalidoException(string message) : base(message)
    {
    }
}
