using Mapster;
using Paschoalotto.Carteira.Core.Application.DTOs.Cliente;
using Paschoalotto.Carteira.Core.Application.Interfaces;
using Paschoalotto.Carteira.Core.Domain.Entities;
using Paschoalotto.Carteira.Core.Domain.Exceptions.Cliente;
using Paschoalotto.Carteira.Core.Domain.Interfaces;

namespace Paschoalotto.Carteira.Core.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<ClienteResponseDto> CreateAsync(ClienteRequestDto request)
    {
        // Validar se documento já existe
        if (await _clienteRepository.ExistsByDocumentoAsync(request.Documento))
            throw new ClienteAlreadyExistsException(request.Documento);

        var cliente = request.Adapt<Cliente>();
        cliente.DataCadastro = DateTime.Now;
        cliente.Ativo = true;

        var clienteCriado = await _clienteRepository.AddAsync(cliente);
        return MapToResponseDto(clienteCriado);
    }

    public async Task<ClienteResponseDto> UpdateAsync(int id, ClienteRequestDto request)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);
        if (cliente == null)
            throw new ClienteNotFoundException(id);

        // Validar se o novo documento já pertence a outro cliente
        var clienteComMesmoDocumento = await _clienteRepository.GetByDocumentoAsync(request.Documento);
        if (clienteComMesmoDocumento != null && clienteComMesmoDocumento.Id != id)
            throw new ClienteAlreadyExistsException(request.Documento);

        cliente.TipoPessoa = request.TipoPessoa;
        cliente.Nome = request.Nome;
        cliente.Documento = request.Documento;
        cliente.Email = request.Email;
        cliente.Telefone = request.Telefone;
        cliente.Endereco = request.Endereco;
        cliente.Cidade = request.Cidade;
        cliente.Estado = request.Estado;
        cliente.Cep = request.Cep;

        var clienteAtualizado = await _clienteRepository.UpdateAsync(cliente);
        return MapToResponseDto(clienteAtualizado);
    }

    public async Task<ClienteResponseDto?> GetByIdAsync(int id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);
        return cliente != null ? MapToResponseDto(cliente) : null;
    }

    public async Task<ClienteResponseDto?> GetByDocumentoAsync(string documento)
    {
        var cliente = await _clienteRepository.GetByDocumentoAsync(documento);
        return cliente != null ? MapToResponseDto(cliente) : null;
    }

    public async Task<IEnumerable<ClienteResponseDto>> GetAllAsync()
    {
        var clientes = await _clienteRepository.GetAllAsync();
        return clientes.Select(MapToResponseDto);
    }

    public async Task<IEnumerable<ClienteResponseDto>> SearchAsync(string searchTerm)
    {
        var clientes = await _clienteRepository.SearchAsync(searchTerm);
        return clientes.Select(MapToResponseDto);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existe = await _clienteRepository.ExistsAsync(id);
        if (!existe)
            throw new ClienteNotFoundException(id);

        return await _clienteRepository.DeleteAsync(id);
    }

    private ClienteResponseDto MapToResponseDto(Cliente cliente)
    {
        return new ClienteResponseDto
        {
            Id = cliente.Id,
            TipoPessoa = cliente.TipoPessoa,
            TipoPessoaDescricao = cliente.TipoPessoa.ToString(),
            Nome = cliente.Nome,
            Documento = cliente.Documento,
            Email = cliente.Email,
            Telefone = cliente.Telefone,
            Endereco = cliente.Endereco,
            Cidade = cliente.Cidade,
            Estado = cliente.Estado,
            Cep = cliente.Cep,
            DataCadastro = cliente.DataCadastro,
            Ativo = cliente.Ativo
        };
    }
}
