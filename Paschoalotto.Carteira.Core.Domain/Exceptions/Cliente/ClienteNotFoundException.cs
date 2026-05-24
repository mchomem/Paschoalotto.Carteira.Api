using Paschoalotto.Carteira.Core.Domain.Exceptions.Base;

namespace Paschoalotto.Carteira.Core.Domain.Exceptions.Cliente;

public class ClienteNotFoundException : NotFoundException
{
    public ClienteNotFoundException(int id) 
        : base($"Cliente com ID {id} não foi encontrado.")
    {
    }

    public ClienteNotFoundException(string documento) 
        : base($"Cliente com documento {documento} não foi encontrado.")
    {
    }
}
