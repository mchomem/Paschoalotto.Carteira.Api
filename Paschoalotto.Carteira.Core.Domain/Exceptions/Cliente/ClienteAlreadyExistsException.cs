using Paschoalotto.Carteira.Core.Domain.Exceptions.Base;

namespace Paschoalotto.Carteira.Core.Domain.Exceptions.Cliente;

public class ClienteAlreadyExistsException : AlreadyExistsException
{
    public ClienteAlreadyExistsException(string documento) 
        : base($"Cliente com documento {documento} já existe no sistema.")
    {
    }
}
