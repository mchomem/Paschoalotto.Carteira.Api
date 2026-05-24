using Paschoalotto.Carteira.Core.Domain.Exceptions.Base;

namespace Paschoalotto.Carteira.Core.Domain.Exceptions.Contrato;

public class ContratoNotFoundException : NotFoundException
{
    public ContratoNotFoundException(int id) 
        : base($"Contrato com ID {id} não foi encontrado.")
    {
    }

    public ContratoNotFoundException(string numeroContrato) 
        : base($"Contrato {numeroContrato} não foi encontrado.")
    {
    }
}
