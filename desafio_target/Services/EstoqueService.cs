using desafio_target.Models;
using desafio_target.Models.Enumerables;

namespace desafio_target.Services
{
    public class EstoqueService
    {
        private static IList<Movimentacao> movimentacoes = new List<Movimentacao>();
        public int MovimentarProduto(Produto produto, TipoMovimentacaoEnum tipo, int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            if (tipo == TipoMovimentacaoEnum.SAIDA && quantidade > produto.Estoque)
                throw new InvalidOperationException("Estoque insuficiente.");

            movimentacoes.Add(new Movimentacao(produto.Codigo, quantidade, tipo));

            return tipo switch
            {
                TipoMovimentacaoEnum.ENTRADA => produto.Estoque += quantidade,
                TipoMovimentacaoEnum.SAIDA => produto.Estoque -= quantidade,
                _ => throw new ArgumentOutOfRangeException(nameof(tipo), "Tipo de movimentação inválido.")
            };

        }

        public IEnumerable<Movimentacao> ListarMovimentacoes()
        {
            return movimentacoes;
        }
    }
}
