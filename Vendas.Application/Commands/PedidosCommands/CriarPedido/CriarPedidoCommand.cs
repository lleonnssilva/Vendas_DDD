using Vendas.Application.Mediator.Interfaces;

namespace Vendas.Application.Commands.PedidosCommands.CriarPedido
{
    public sealed class CriarPedidoCommand : IRequest<CriarPedidoResultDto>
    {
        public Guid ClienteId { get; }
        public string Cep { get;  }
        public string Logradouro { get;  }
        public string Complemento { get; }
        public string Bairro { get;  }
        public string Estado { get;  }
        public string Cidade { get;  }
        public string Pais { get;  }
        public string Numero { get;  }
        public CriarPedidoCommand(
            Guid clienteId, string cep, string logradouro, string complemento, string bairro, string estado, string cidade, string pais, string numero)
        {
            ClienteId = clienteId;
            Cep = cep;
            Logradouro = logradouro;
            Complemento = complemento;
            Bairro = bairro;
            Estado = estado;
            Cidade = cidade;
            Pais = pais;
            Numero = numero;
        }
    }
}
