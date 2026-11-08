using MiniBitcoin.Console.Models;

namespace MiniBitcoin.Console.Services;

public class MercadoService
{
    private readonly List<TransacaoHistorico> _historico = [];

    public decimal CotacaoBtc { get; }
    public IReadOnlyList<TransacaoHistorico> Historico => _historico.AsReadOnly();

    public MercadoService(decimal cotacaoBtc)
    {
        if (cotacaoBtc <= 0)
        {
            throw new ArgumentException("A cotação do Bitcoin deve ser maior que zero.", nameof(cotacaoBtc));
        }

        CotacaoBtc = cotacaoBtc;
    }

    public void ComprarBtc(Carteira carteira, decimal quantidadeBtc)
    {
        ArgumentNullException.ThrowIfNull(carteira);
        ValidarQuantidade(quantidadeBtc);

        var valorTotalBrl = quantidadeBtc * CotacaoBtc;
        if (quantidadeBtc > decimal.MaxValue - carteira.SaldoBtc)
        {
            throw new ArgumentOutOfRangeException(nameof(quantidadeBtc), "A quantidade excede o saldo máximo de Bitcoin.");
        }

        carteira.DebitarBrl(valorTotalBrl);
        carteira.AdicionarBtc(quantidadeBtc);
        _historico.Add(CriarTransacao("COMPRA", quantidadeBtc, valorTotalBrl));
    }

    public void VenderBtc(Carteira carteira, decimal quantidadeBtc)
    {
        ArgumentNullException.ThrowIfNull(carteira);
        ValidarQuantidade(quantidadeBtc);

        var valorTotalBrl = quantidadeBtc * CotacaoBtc;
        if (valorTotalBrl > decimal.MaxValue - carteira.SaldoBrl)
        {
            throw new ArgumentOutOfRangeException(nameof(quantidadeBtc), "A venda excede o saldo máximo em BRL.");
        }

        carteira.DebitarBtc(quantidadeBtc);
        carteira.AdicionarBrl(valorTotalBrl);
        _historico.Add(CriarTransacao("VENDA", quantidadeBtc, valorTotalBrl));
    }

    public decimal CalcularPatrimonioTotal(Carteira carteira)
    {
        ArgumentNullException.ThrowIfNull(carteira);
        return carteira.SaldoBrl + carteira.SaldoBtc * CotacaoBtc;
    }

    private static void ValidarQuantidade(decimal quantidadeBtc)
    {
        if (quantidadeBtc <= 0)
        {
            throw new ArgumentException("A quantidade de Bitcoin deve ser maior que zero.", nameof(quantidadeBtc));
        }
    }

    private TransacaoHistorico CriarTransacao(string tipo, decimal quantidadeBtc, decimal valorTotalBrl)
    {
        return new TransacaoHistorico
        {
            Tipo = tipo,
            QuantidadeBtc = quantidadeBtc,
            PrecoCotacao = CotacaoBtc,
            ValorTotalBrl = valorTotalBrl
        };
    }
}
