using Paschoalotto.Carteira.Core.Domain.Exceptions.Base;

namespace Paschoalotto.Carteira.Core.Domain.Exceptions.Boleto;

public class BoletoNotFoundException : NotFoundException
{
    public BoletoNotFoundException(int id) 
        : base($"Boleto com ID {id} não foi encontrado.")
    {
    }

    public BoletoNotFoundException(string nossoNumero) 
        : base($"Boleto com nosso número {nossoNumero} não foi encontrado.")
    {
    }
}
