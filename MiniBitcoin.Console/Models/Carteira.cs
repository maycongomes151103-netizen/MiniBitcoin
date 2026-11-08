// MiniBitcoin.Console/Models/Carteira.cs
namespace MiniBitcoin.Console.Models;

public class Carteira
{
    public decimal SaldoBrl { get; private set; }
    public decimal SaldoBtc { get; private set; }

    public Carteira(decimal saldoBrlInicial, decimal saldoBtcInicial)
    {
        if (saldoBrlInicial < 0) throw new ArgumentException("O saldo em BRL não pode ser negativo.");
        if (saldoBtcInicial < 0) throw new ArgumentException("O saldo em Bitcoin não pode ser negativo.");

        SaldoBrl = saldoBrlInicial;
        SaldoBtc = saldoBtcInicial;
    }

    public void AdicionarBrl(decimal valor)
    {
        if (valor <= 0) throw new ArgumentException("O valor deve ser maior que zero.");
        SaldoBrl += valor;
    }

    public void DebitarBrl(decimal valor)
    {
        if (valor <= 0) throw new ArgumentException("O valor deve ser maior que zero.");
        if (valor > SaldoBrl) throw new InvalidOperationException("Saldo em BRL insuficiente.");
        SaldoBrl -= valor;
    }

    public void AdicionarBtc(decimal valor)
    {
        if (valor <= 0) throw new ArgumentException("O valor deve ser maior que zero.");
        SaldoBtc += valor;
    }

    public void DebitarBtc(decimal valor)
    {
        if (valor <= 0) throw new ArgumentException("O valor deve ser maior que zero.");
        if (valor > SaldoBtc) throw new InvalidOperationException("Saldo em Bitcoin insuficiente.");
        SaldoBtc -= valor;
    }
}