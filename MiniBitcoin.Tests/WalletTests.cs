// MiniBitcoin.Tests/WalletTests.cs
using Xunit;
using MiniBitcoin.Console.Models;

namespace MiniBitcoin.Tests;

public class WalletTests
{
    [Fact]
    public void Carteira_Deve_Inicializar_Com_Saldos_Validos()
    {
        var carteira = new Carteira(1000.0m, 0.5m);
        Assert.Equal(1000.0m, carteira.SaldoBrl);
        Assert.Equal(0.5m, carteira.SaldoBtc);
    }

    [Fact]
    public void Carteira_Nao_Deve_Permitir_Saldo_Negativo_Inicial()
    {
        Assert.Throws<ArgumentException>(() => new Carteira(-100.0m, 0));
        Assert.Throws<ArgumentException>(() => new Carteira(100, -0.1m));
    }
}