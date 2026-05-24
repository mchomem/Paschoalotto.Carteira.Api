using Paschoalotto.Carteira.Core.Domain.Exceptions.Base;

namespace Paschoalotto.Carteira.Core.Domain.Exceptions.Boleto;

public class BoletoInvalidOperationException : Base.InvalidOperationException
{
    public BoletoInvalidOperationException(string message) : base(message)
    {
    }
}
