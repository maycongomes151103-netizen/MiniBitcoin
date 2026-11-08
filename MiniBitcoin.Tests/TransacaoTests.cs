// MiniBitcoin.Tests/TransacaoTests.cs
using Xunit;
using MiniBitcoin.Console.Models;
using MiniBitcoin.Console.Services;

namespace MiniBitcoin.Tests;

public class TransacaoTests
{
    [Fact]
    public void Deve_Comprar_Bitcoin_Com_Sucesso()
    {
        var carteira = new Carteira(100000m, 0m);
        var servico = new MercadoService(cotacaoBtc: 50000m);

        servico.ComprarBtc(carteira, 1.0m); // Custa 50.000

        Assert.Equal(50000m, carteira.SaldoBrl);
        Assert.Equal(1.0m, carteira.SaldoBtc);
    }

    [Fact]
    public void Nao_Deve_Comprar_Bitcoin_Sem_Saldo_Brl()
    {
        var carteira = new Carteira(1000m, 0m);
        var servico = new MercadoService(cotacaoBtc: 50000m);

        Assert.Throws<InvalidOperationException>(() => servico.ComprarBtc(carteira, 1.0m));
    }
}