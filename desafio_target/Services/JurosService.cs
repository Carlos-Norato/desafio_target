namespace desafio_target.Services
{
    public class JurosService
    {
        private const decimal TAXA_DIARIA = 0.025m;

        public decimal CalcularJurosSimples(
            decimal valor,
            DateTime vencimento)
        {
            if (valor <= 0)
                return 0m;

            var diasEmAtraso = (DateTime.Today - vencimento.Date).Days;

            if (diasEmAtraso <= 0)
                return 0m;

            return Math.Round(valor * TAXA_DIARIA * diasEmAtraso, 2, MidpointRounding.AwayFromZero);
        }

        public decimal CalcularJurosComposto(
            decimal valor,
            DateTime vencimento)
        {
            if (valor <= 0)
                return 0m;

            var diasEmAtraso = (DateTime.Today - vencimento.Date).Days;

            if (diasEmAtraso <= 0)
                return 0m;

            var fator = 1m;
            for (var i = 0; i < diasEmAtraso; i++)
                fator *= 1 + TAXA_DIARIA;

            var valorFinal = valor * fator;

            return Math.Round(valorFinal - valor, 2, MidpointRounding.AwayFromZero);
        }
    }
}
