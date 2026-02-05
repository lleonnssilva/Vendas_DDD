using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Domain.Common.Base;
using Vendas.Domain.Common.Exceptions;
using Vendas.Domain.Common.Validations;

namespace Vendas.Domain.Catalogo.ValueObjects
{

    public sealed class PrecoProduto : ValueObject
    {
        public string Valor { get; }
        public PrecoProduto(string valor)
        {
            
            Guard.Against<DomainException>(valor.Length <=0, "O preço do produto deve ser maior que zero.");


            Valor = valor.Trim();
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Valor;
        }
    }
}
