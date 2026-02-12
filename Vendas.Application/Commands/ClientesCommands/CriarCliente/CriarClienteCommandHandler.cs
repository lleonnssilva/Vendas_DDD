using Vendas.Application.Abstractions.Persistence;
using Vendas.Application.Commands.CatalogoCommands.CategoriaCommands.CriarCategoria;
using Vendas.Application.Mediator.Interfaces;
using Vendas.Domain.Clientes;
using Vendas.Domain.Clientes.Enums;
using Vendas.Domain.Clientes.ValueObjects;

namespace Vendas.Application.Commands.ClientesCommands.CriarCliente
{

    public sealed class CriarClienteCommandHandler : IRequestHandler<CriarClienteCommand, CriarClienteResultDto>
    {
        private readonly IClienteRepository _clienteRepository;

        public CriarClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<CriarClienteResultDto> HandleAsync(CriarClienteCommand command, CancellationToken cancellationToken)
        {
            //var categoria = await _categoriaRepository.ObterPorIdAsync(command.CategoriaId, cancellationToken) ??
            //    throw new DomainException("Categoria não localizada.");
            //Guard.Against<DomainException>(!categoria.Ativa, "Não é possível criar um produto em uma categoria inativa.");

            var nome = new NomeCompleto(command.Nome);
            var cpf = new Cpf(command.Cpf);
            var email = new Email(command.Email);
            var telefone = new Telefone(command.Telefone);
            var endereco = new Endereco(command.Cep, command.Logradouro, command.Numero, command.Bairro, command.Cidade, command.Estado, command.Pais, command.Complemento);


            var cliente = new Cliente(
                nome,
                cpf,
                email,
                telefone,
                endereco,
                Sexo.NaoInformado,
                EstadoCivil.NaoInformado
                
                );

            await _clienteRepository.AdicionarAsync(cliente, cancellationToken);
            return new CriarClienteResultDto
            {

                Nome = cliente.Nome.NomeCompletoFormatado,
            };
        }
    }
}
