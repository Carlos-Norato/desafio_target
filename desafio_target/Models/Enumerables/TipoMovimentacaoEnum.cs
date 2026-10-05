using System.ComponentModel;

namespace desafio_target.Models.Enumerables
{
    public enum TipoMovimentacaoEnum
    {
        [Description("Entrada")]
        ENTRADA = 1,
        [Description("Saída")]
        SAIDA = 2
    }
}
