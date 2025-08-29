# 📊 Calculadora Financeira - API em .NET

Esta é uma **API REST** construída em **.NET** que disponibiliza cálculos financeiros comuns, como juros simples, compostos, simulações de financiamento (Price e SAC) e cálculo de valor futuro com aportes.

A aplicação é **stateless**, não usa banco de dados e pode ser executada em **um único container Docker**.

---

## 🚀 Funcionalidades

A API expõe endpoints para os seguintes cálculos financeiros:

1. **Juros Simples**
   - Fórmula: `J = C * i * t`
   - Retorna montante final e valor de juros.

2. **Juros Compostos**
   - Fórmula: `M = C * (1+i)^t`
   - Retorna montante final e valor de juros.

3. **Financiamento - Sistema PRICE (Parcelas Fixas)**
   - Fórmula: `PMT = (PV * i) / (1 - (1+i)^-n)`
   - Retorna valor da parcela fixa, total pago e total de juros.

4. **Financiamento - Sistema SAC (Amortização Constante)**
   - Amortização fixa: `A = PV / n`
   - Juros variáveis sobre saldo devedor.
   - Retorna lista das parcelas, saldo devedor, total pago e juros.

5. **Valor Futuro com Aportes**
   - Fórmula: `FV = C * (1+i)^n + PMT * ((1+i)^n - 1) / i`
   - Retorna o valor futuro de um investimento com aportes mensais.

---

## 📦 Como rodar localmente

### ✅ Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/)

### ▶️ Rodar sem Docker
Clone o repositório e rode a aplicação:
```bash
git clone https://github.com/Leolima32/calculadora-financeira.git
cd calculadora-financeira
dotnet run
```
A API ficará disponível em:
```
http://localhost:5000
```

### 🐳 Rodar com Docker
1. Build da imagem:
   ```
   docker build -t calculadora-financeira .
   ```
2. Executar o container:
   ```
   docker run -d -p 5000:5000 calculadora-financeira
   ```
A API ficará disponível em:
```
http://localhost:5000
```

### 📖 Exemplos de Uso
1. Juros Simples

> Requisição:
   ```
   GET /juros-simples?capital=1000&taxa=0.02&tempo=12
   ```
> Resposta:
   ``` json
   {
      "montante": 1240,
      "juros": 240
   }
   ```

2. Juros Compostos

> Requisição:
   ```
   GET /juros-compostos?capital=1000&taxa=0.02&tempo=12
   ```
> Resposta:
   ``` json
   {
    "montante": 1268.24,
    "juros": 268.24
   }
   ```

3. Financiamento Price

> Requisição:
   ```
   GET /financiamento/price?valor=10000&taxa=0.02&parcelas=12
   ```
> Resposta:
   ``` json
   {
    "parcela": 943.00,
    "totalPago": 11316.00,
    "totalJuros": 1316.00
   }
   ```
4. Financiamento SAC
   
> Requisição:
   ```
   GET /financiamento/sac?valor=12000&taxa=0.02&parcelas=12
   ```
> Resposta:
   ``` json
   [
    { "parcela": 1, "valorParcela": 1200, "juros": 200, "amortizacao": 1000, "saldoDevedor": 11000 },
    { "parcela": 2, "valorParcela": 1180, "juros": 180, "amortizacao":   1000, "saldoDevedor": 10000 }
   ]
   ```

5. Valor Futuro com Aportes
   
> Requisição:
   ```
   GET /valor-futuro?capital=1000&aporte=200&taxa=0.01&tempo=12
   ```
> Resposta:
   ``` json
   {
  "valorFuturo": 3774.39
   }
   ```
