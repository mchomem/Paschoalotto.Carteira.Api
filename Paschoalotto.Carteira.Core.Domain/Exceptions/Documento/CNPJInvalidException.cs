namespace Paschoalotto.Carteira.Core.Domain.Exceptions.Documento;

public class CNPJInvalidException : Base.InvalidOperationException
{
    public CNPJInvalidException(string message) : base(message)
    {
    }
}
