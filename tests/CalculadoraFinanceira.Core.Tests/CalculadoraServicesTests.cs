using CalculadoraFinanceira.Core.Models.Inputs;
using CalculadoraFinanceira.Core.Services;

namespace CalculadoraFinanceira.Core.Tests
{
    public class CalculadoraServicessTests
    {
        public static IEnumerable<object[]> JurosSimplesData()
        {
            yield return new object[] { new CalcularJurosInput(1000, 0.02, 12), 1240, 240 };
            yield return new object[] { new CalcularJurosInput(500, 0.05, 6), 650, 150 };
            yield return new object[] { new CalcularJurosInput(2000, 0.015, 24), 2720, 720 };
        }

        [Theory]
        [MemberData(nameof(JurosSimplesData))]
        public void JurosSimples_DeveCalcularCorretamente(CalcularJurosInput input, double montanteEsperado, double jurosEsperado)
        {
            var resultado = CalculadoraServices.JurosSimples(input);
            Assert.Equal(montanteEsperado, resultado.MontanteFinal, 2);
            Assert.Equal(jurosEsperado, resultado.ValorTotalDoJuros, 2);
        }

        public static IEnumerable<object[]> JurosCompostosData()
        {
            yield return new object[] { new CalcularJurosInput(1000, 0.02, 12), 1268.24, 268.24 };
            yield return new object[] { new CalcularJurosInput(500, 0.05, 6), 670.05, 170.05 };
            yield return new object[] { new CalcularJurosInput(2000, 0.015, 24), 2859.01, 859.01 };
        }

        [Theory]
        [MemberData(nameof(JurosCompostosData))]
        public void JurosCompostos_DeveCalcularCorretamente(CalcularJurosInput input, double montanteEsperado, double jurosEsperado)
        {
            var resultado = CalculadoraServices.JurosCompostos(input);
            Assert.Equal(montanteEsperado, resultado.MontanteFinal, 2);
            Assert.Equal(jurosEsperado, resultado.ValorTotalDoJuros, 2);
        }

        public static IEnumerable<object[]> FinanciamentoPriceData()
        {
            yield return new object[] { new FinanciamentoInput(10000, 0.02, 12), 945.60, 11347.20, 1347.20 };
            yield return new object[] { new FinanciamentoInput(5000, 0.015, 24), 249.62, 5990.88, 990.88 };
            yield return new object[] { new FinanciamentoInput(20000, 0.01, 36), 664.29, 23914.44, 3914.44 };
        }

        [Theory]
        [MemberData(nameof(FinanciamentoPriceData))]
        public void FinanciamentoPrice_DeveCalcularCorretamente(FinanciamentoInput input, double parcelaEsperada, double totalPagoEsperado, double jurosEsperado)
        {
            var resultado = CalculadoraServices.FinanciamentoPrice(input);
            Assert.Equal(parcelaEsperada, resultado.Parcela, 2);
            Assert.Equal(totalPagoEsperado, resultado.TotalPago, 2);
            Assert.Equal(jurosEsperado, resultado.TotalJuros, 2);
        }

        public static IEnumerable<object[]> FinanciamentoSACData()
        {
            // Valor, Taxa, Parcelas, PrimeiraParcela, UltimaParcela, TotalPago, TotalJuros
            yield return new object[] { new FinanciamentoInput(12000, 0.02, 12), 1240, 1020, 13560, 1560 };
            yield return new object[] { new FinanciamentoInput(6000, 0.015, 6), 1090, 1015, 6315, 315 };
            yield return new object[] { new FinanciamentoInput(20000, 0.01, 10), 2200, 2020, 21100, 1100 };
        }

        [Theory]
        [MemberData(nameof(FinanciamentoSACData))]
        public void FinanciamentoSAC_DeveCalcularCorretamente(FinanciamentoInput input, double primeiraParcela, double ultimaParcela, double totalPago, double totalJuros)
        {
            var lista = CalculadoraServices.FinanciamentoSAC(input);
            Assert.Equal(primeiraParcela, lista[0].ValorParcela, 0);
            Assert.Equal(ultimaParcela, lista[^1].ValorParcela, 0);
            double somaParcelas = 0;
            double somaJuros = 0;
            foreach (var item in lista)
            {
                somaParcelas += item.ValorParcela;
                somaJuros += item.Juros;
            }
            Assert.Equal(totalPago, somaParcelas, 0);
            Assert.Equal(totalJuros, somaJuros, 0);
        }

        public static IEnumerable<object[]> ValorFuturoData()
        {
            yield return new object[] { new ValorFuturoInput(1000, 200, 0.01, 12), 3663.33 };
            yield return new object[] { new ValorFuturoInput(5000, 0, 0.005, 24), 5635.80 };
            yield return new object[] { new ValorFuturoInput(0, 500, 0.02, 10), 5474.86 };
        }

        [Theory]
        [MemberData(nameof(ValorFuturoData))]
        public void ValorFuturo_DeveCalcularCorretamente(ValorFuturoInput input, double valorEsperado)
        {
            var resultado = CalculadoraServices.ValorFuturo(input);
            Assert.Equal(valorEsperado, resultado, 2);
        }
    }
}
