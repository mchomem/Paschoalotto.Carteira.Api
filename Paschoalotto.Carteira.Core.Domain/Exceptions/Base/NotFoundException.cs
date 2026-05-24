namespace Paschoalotto.Carteira.Core.Domain.Exceptions.Base;

public class NotFoundException : DomainException
{
    public NotFoundException(string message) : base(message)
    {
    }
}
