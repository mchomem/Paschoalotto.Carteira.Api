namespace Paschoalotto.Carteira.Core.Domain.Exceptions.Acordo;

public class AcordoInvalidOperationException : Base.InvalidOperationException
{
    public AcordoInvalidOperationException(string message) : base(message)
    {
    }
}
