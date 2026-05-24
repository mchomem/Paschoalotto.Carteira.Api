using Paschoalotto.Carteira.Core.Domain.Exceptions.Base;

namespace Paschoalotto.Carteira.Core.Domain.Exceptions.Acordo;

public class AcordoNotFoundException : NotFoundException
{
    public AcordoNotFoundException(int id) 
        : base($"Acordo com ID {id} não foi encontrado.")
    {
    }

    public AcordoNotFoundException(string numeroAcordo) 
        : base($"Acordo {numeroAcordo} não foi encontrado.")
    {
    }
}
