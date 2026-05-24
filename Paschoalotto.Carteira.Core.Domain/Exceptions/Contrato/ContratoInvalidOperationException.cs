using Paschoalotto.Carteira.Core.Domain.Exceptions.Base;

namespace Paschoalotto.Carteira.Core.Domain.Exceptions.Contrato;

public class ContratoInvalidOperationException : Base.InvalidOperationException
{
    public ContratoInvalidOperationException(string message) : base(message)
    {
    }
}
