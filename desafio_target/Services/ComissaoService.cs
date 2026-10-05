global using Vendedor = System.String;
global using ValorComissoes = System.Decimal;

using desafio_target.Models;

namespace desafio_target.Services
{
    public class ComissaoService
    {

        public IDictionary<Vendedor, ValorComissoes> CalcularComissoes(IEnumerable<Venda> vendas)
        {
            Dictionary<Vendedor, ValorComissoes> comissoesPorVendedor = new ();

            foreach (var venda in vendas)
            {
                decimal valorComissao = CalcularValorComissao(venda.Valor);
                comissoesPorVendedor[venda.Vendedor] = comissoesPorVendedor.GetValueOrDefault(venda.Vendedor) + valorComissao;
            }

            return comissoesPorVendedor;
        }

        public decimal CalcularValorComissao(decimal valorVenda) => valorVenda switch
        {
            < 100m => 0m,
            < 500m => valorVenda * 0.01m,
            >= 500m => valorVenda * 0.05m
        };
    }
}
