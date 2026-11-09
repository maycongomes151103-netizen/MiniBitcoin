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
        var transacao = Assert.Single(servico.Historico);
        Assert.Equal("COMPRA", transacao.Tipo);
        Assert.Equal(50000m, transacao.ValorTotalBrl);
    }

    [Fact]
    public void Nao_Deve_Comprar_Bitcoin_Sem_Saldo_Brl()
    {
        var carteira = new Carteira(1000m, 0m);
        var servico = new MercadoService(cotacaoBtc: 50000m);

        Assert.Throws<InvalidOperationException>(() => servico.ComprarBtc(carteira, 1.0m));
        Assert.Empty(servico.Historico);
    }

    [Fact]
    public void Deve_Vender_Bitcoin_Com_Sucesso()
    {
        var carteira = new Carteira(0m, 1.5m);
        var servico = new MercadoService(cotacaoBtc: 50000m);

        servico.VenderBtc(carteira, 1.0m);

        Assert.Equal(50000m, carteira.SaldoBrl);
        Assert.Equal(0.5m, carteira.SaldoBtc);
        var transacao = Assert.Single(servico.Historico);
        Assert.Equal("VENDA", transacao.Tipo);
        Assert.Equal(50000m, transacao.ValorTotalBrl);
    }

    [Fact]
    public void Nao_Deve_Vender_Bitcoin_Sem_Saldo_Btc()
    {
        var carteira = new Carteira(0m, 0.5m);
        var servico = new MercadoService(cotacaoBtc: 50000m);

        Assert.Throws<InvalidOperationException>(() => servico.VenderBtc(carteira, 1.0m));
        Assert.Empty(servico.Historico);
    }

    [Fact]
    public void Deve_Atualizar_Cotacao_Para_Valor_Positivo()
    {
        var carteira = new Carteira(100000m, 0m);
        var servico = new MercadoService(cotacaoBtc: 50000m);

        servico.AtualizarCotacao(60000m);
        servico.ComprarBtc(carteira, 1m);

        Assert.Equal(60000m, servico.CotacaoBtc);
        Assert.Equal(40000m, carteira.SaldoBrl);
        Assert.Equal(60000m, Assert.Single(servico.Historico).PrecoCotacao);
    }

    [Fact]
    public void Nao_Deve_Atualizar_Cotacao_Com_Valor_Nao_Positivo()
    {
        var servico = new MercadoService(cotacaoBtc: 50000m);

        Assert.Throws<ArgumentException>(() => servico.AtualizarCotacao(0m));
        Assert.Throws<ArgumentException>(() => servico.AtualizarCotacao(-1m));
        Assert.Equal(50000m, servico.CotacaoBtc);
    }
}