namespace Paschoalotto.Carteira.Core.Domain.Exceptions.Documento;

public class CPFInvalidException : Base.InvalidOperationException
{
    public CPFInvalidException(string message) : base(message)
    {
    }
}
