namespace CalculadoraFinanceira.Core.Models.Outputs;

public record FinanciamentoSacOutput(int Parcela, double ValorParcela, double Juros, double Amortizacao, double SaldoDevedor);