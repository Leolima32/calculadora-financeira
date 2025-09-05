namespace CalculadoraFinanceira.Core.Services;

public class CalculosService
{
    public static (double Montante, double Juros) JurosSimples(double capital, double taxa, int tempo)
    {
        double juros = capital * taxa * tempo;
        double montante = capital + juros;
        return (montante, juros);
    }

    public static (double Montante, double Juros) JurosCompostos(double capital, double taxa, int tempo)
    {
        double montante = capital * Math.Pow(1 + taxa, tempo);
        double juros = montante - capital;
        return (montante, juros);
    }

    public static (double Parcela, double TotalPago, double TotalJuros) FinanciamentoPrice(double valor, double taxa, int parcelas)
    {
        double pmt = (valor * taxa) / (1 - Math.Pow(1 + taxa, -parcelas));
        double totalPago = pmt * parcelas;
        double totalJuros = totalPago - valor;
        return (pmt, totalPago, totalJuros);
    }

    public static List<(int Parcela, double ValorParcela, double Juros, double Amortizacao, double SaldoDevedor)> FinanciamentoSAC(double valor, double taxa, int parcelas)
    {
        var lista = new List<(int, double, double, double, double)>();
        double amortizacao = valor / parcelas;
        double saldo = valor;

        for (int k = 1; k <= parcelas; k++)
        {
            double juros = saldo * taxa;
            double parcela = amortizacao + juros;
            saldo -= amortizacao;
            lista.Add((k, parcela, juros, amortizacao, saldo));
        }

        return lista;
    }

    public static double ValorFuturo(double capital, double aporte, double taxa, int tempo)
    {
        return capital * Math.Pow(1 + taxa, tempo) + aporte * ((Math.Pow(1 + taxa, tempo) - 1) / taxa);
    }
}

