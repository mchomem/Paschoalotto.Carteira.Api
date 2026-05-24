namespace Paschoalotto.Carteira.Core.Domain.Exceptions.Base;

public class InvalidOperationException : DomainException
{
    public InvalidOperationException(string message) : base(message)
    {
    }
}
