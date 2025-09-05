using CalculadoraFinanceira.Core.Models.Inputs;
using CalculadoraFinanceira.Core.Services;

namespace CalculadoraFinanceira.Core.Tests
{
    public class CalculadoraServiceTests
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
            var resultado = CalculadoraService.JurosSimples(input);
            Assert.Equal(montanteEsperado, resultado.MontanteFinal, 2);
            Assert.Equal(jurosEsperado, resultado.ValorTotalDoJuros, 2);
        }

        public static IEnumerable<object[]> JurosCompostosData()
        {
            yield return new object[] { new CalcularJurosInput(1000, 0.02, 12), 1268.24, 268.24 };
            yield return new object[] { new CalcularJurosInput(500, 0.05, 6), 671.56, 171.56 };
            yield return new object[] { new CalcularJurosInput(2000, 0.015, 24), 2863.92, 863.92 };
        }

        [Theory]
        [MemberData(nameof(JurosCompostosData))]
        public void JurosCompostos_DeveCalcularCorretamente(CalcularJurosInput input, double montanteEsperado, double jurosEsperado)
        {
            var resultado = CalculadoraService.JurosCompostos(input);
            Assert.Equal(montanteEsperado, resultado.MontanteFinal, 2);
            Assert.Equal(jurosEsperado, resultado.ValorTotalDoJuros, 2);
        }

        public static IEnumerable<object[]> FinanciamentoPriceData()
        {
            yield return new object[] { new FinanciamentoInput(10000, 0.02, 12), 943, 11316, 1316 };
            yield return new object[] { new FinanciamentoInput(5000, 0.015, 24), 246, 5904, 904 };
            yield return new object[] { new FinanciamentoInput(20000, 0.01, 36), 664, 23904, 3904 };
        }

        [Theory]
        [MemberData(nameof(FinanciamentoPriceData))]
        public void FinanciamentoPrice_DeveCalcularCorretamente(FinanciamentoInput input, double parcelaEsperada, double totalPagoEsperado, double jurosEsperado)
        {
            var resultado = CalculadoraService.FinanciamentoPrice(input);
            Assert.Equal(parcelaEsperada, resultado.Parcela, 0);
            Assert.Equal(totalPagoEsperado, resultado.TotalPago, 0);
            Assert.Equal(jurosEsperado, resultado.TotalJuros, 0);
        }

        public static IEnumerable<object[]> FinanciamentoSACData()
        {
            // Valor, Taxa, Parcelas, PrimeiraParcela, UltimaParcela, TotalPago, TotalJuros
            yield return new object[] { new FinanciamentoInput(12000, 0.02, 12), 1200, 1020, 13320, 1320 };
            yield return new object[] { new FinanciamentoInput(6000, 0.015, 6), 1150, 1025, 6525, 525 };
            yield return new object[] { new FinanciamentoInput(20000, 0.01, 10), 3000, 2020, 22100, 2100 };
        }

        [Theory]
        [MemberData(nameof(FinanciamentoSACData))]
        public void FinanciamentoSAC_DeveCalcularCorretamente(FinanciamentoInput input, double primeiraParcela, double ultimaParcela, double totalPago, double totalJuros)
        {
            var lista = CalculadoraService.FinanciamentoSAC(input);
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
            yield return new object[] { new ValorFuturoInput(1000, 200, 0.01, 12), 3774.39 };
            yield return new object[] { new ValorFuturoInput(5000, 0, 0.005, 24), 5628.89 };
            yield return new object[] { new ValorFuturoInput(0, 500, 0.02, 10), 5494.56 };
        }

        [Theory]
        [MemberData(nameof(ValorFuturoData))]
        public void ValorFuturo_DeveCalcularCorretamente(ValorFuturoInput input, double valorEsperado)
        {
            var resultado = CalculadoraService.ValorFuturo(input);
            Assert.Equal(valorEsperado, resultado, 2);
        }
    }
}
