using CalculadoraFinanceira.Core.Models.Inputs;
using CalculadoraFinanceira.Core.Models.Outputs;

namespace CalculadoraFinanceira.Core.Services;

public class CalculadoraService
{
    public static CalcularJurosOutput JurosSimples(CalcularJurosInput input)
    {
        double juros = input.Capital * input.Taxa * input.Tempo;
        double montante = input.Capital + juros;
        return new CalcularJurosOutput(montante, juros);
    }

    public static CalcularJurosOutput JurosCompostos(CalcularJurosInput input)
    {
        double montante = input.Capital * Math.Pow(1 + input.Taxa, input.Tempo);
        double juros = montante - input.Capital;
        return new CalcularJurosOutput(montante, juros);
    }

    public static FinanciamentoPriceOutput FinanciamentoPrice(FinanciamentoInput input)
    {
        double valorParcela = (input.Valor * input.Taxa) / (1 - Math.Pow(1 + input.Taxa, -input.Parcelas));
        double totalPago = valorParcela * input.Parcelas;
        double totalJuros = totalPago - input.Valor;
        return new FinanciamentoPriceOutput(valorParcela, totalPago, totalJuros);
    }

    public static List<FinanciamentoSacOutput> FinanciamentoSAC(FinanciamentoInput input)
    {
        var lista = new List<FinanciamentoSacOutput>();
        double amortizacao = input.Valor / input.Parcelas;
        double saldo = input.Valor;

        for (int k = 1; k <= input.Parcelas; k++)
        {
            double juros = saldo * input.Taxa;
            double parcela = amortizacao + juros;
            saldo -= amortizacao;
            lista.Add(new FinanciamentoSacOutput(k, parcela, juros, amortizacao, saldo));
        }

        return lista;
    }

    public static double ValorFuturo(ValorFuturoInput input)
    {
        return input.Capital * Math.Pow(1 + input.Taxa, input.Tempo) + input.Aporte * ((Math.Pow(1 + input.Taxa, input.Tempo) - 1) / input.Taxa);
    }
}

