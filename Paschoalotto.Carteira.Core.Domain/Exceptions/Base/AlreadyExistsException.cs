namespace Paschoalotto.Carteira.Core.Domain.Exceptions.Base;

public class AlreadyExistsException : DomainException
{
    public AlreadyExistsException(string message) : base(message)
    {
    }
}
