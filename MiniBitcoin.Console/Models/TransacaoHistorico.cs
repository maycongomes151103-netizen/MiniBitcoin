// MiniBitcoin.Console/Models/TransacaoHistorico.cs
namespace MiniBitcoin.Console.Models;

public class TransacaoHistorico
{
    public string Tipo { get; set; } = string.Empty; // "COMPRA" ou "VENDA"
    public decimal QuantidadeBtc { get; set; }
    public decimal PrecoCotacao { get; set; }
    public decimal ValorTotalBrl { get; set; }
    public DateTime DataHora { get; set; } = DateTime.Now;

    public override string ToString()
    {
        return $"[{DataHora:dd/MM/yyyy HH:mm}] {Tipo} | Qtd: {QuantidadeBtc} BTC | Preço: R$ {PrecoCotacao:N2} | Total: R$ {ValorTotalBrl:N2}";
    }
}