using MiniBitcoin.Console.Models;

namespace MiniBitcoin.Console.Services;

public class MercadoService
{
    private readonly decimal _cotacaoBtc;

    public MercadoService(decimal cotacaoBtc)
    {
        if (cotacaoBtc <= 0)
        {
            throw new ArgumentException("A cotação do Bitcoin deve ser maior que zero.", nameof(cotacaoBtc));
        }

        _cotacaoBtc = cotacaoBtc;
    }

    public void ComprarBtc(Carteira carteira, decimal quantidadeBtc)
    {
        ArgumentNullException.ThrowIfNull(carteira);

        if (quantidadeBtc <= 0)
        {
            throw new ArgumentException("A quantidade de Bitcoin deve ser maior que zero.", nameof(quantidadeBtc));
        }

        var custoBrl = _cotacaoBtc * quantidadeBtc;
        if (quantidadeBtc > decimal.MaxValue - carteira.SaldoBtc)
        {
            throw new ArgumentOutOfRangeException(nameof(quantidadeBtc), "A quantidade excede o saldo máximo de Bitcoin.");
        }

        carteira.DebitarBrl(custoBrl);
        carteira.AdicionarBtc(quantidadeBtc);
    }
}
