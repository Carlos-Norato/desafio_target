using desafio_target.Models.Enumerables;

namespace desafio_target.Models
{
    public class Movimentacao
    {
        public Guid Codigo { get; private set; }
        public int CodigoProduto { get; private set; }
        public int Quantidade { get; private set; }
        public TipoMovimentacaoEnum TipoMovimentacao { get; private set; }

        public Movimentacao(int codigoProduto, int quantidade, TipoMovimentacaoEnum tipoMovimentacao)
        {
            Codigo = Guid.NewGuid();
            CodigoProduto = codigoProduto;
            Quantidade = quantidade;
            TipoMovimentacao = tipoMovimentacao;
        }
    }
}
