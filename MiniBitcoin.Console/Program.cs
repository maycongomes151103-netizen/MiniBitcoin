// MiniBitcoin.Console/Program.cs
using MiniBitcoin.Console.Models;
using MiniBitcoin.Console.Services;

var carteira = new Carteira(10000.0m, 0.0m); // Inicia com R$ 10.000,00 e 0 BTC
var mercado = new MercadoService(cotacaoBtc: 350000.0m); // Cotação fixa inicial de exemplo

int opcao = 0;
do
{
    Console.Clear();
    Console.WriteLine("=== MINIBITCOIN CONSOLE ===");
    Console.WriteLine($"Cotação Atual BTC: R$ {mercado.CotacaoBtc:N2}");
    Console.WriteLine($"Saldo BRL: R$ {carteira.SaldoBrl:N2}");
    Console.WriteLine($"Saldo BTC: {carteira.SaldoBtc:N8} BTC");
    Console.WriteLine($"Patrimônio Total: R$ {mercado.CalcularPatrimonioTotal(carteira):N2}");
    Console.WriteLine("----------------------------------");
    Console.WriteLine("1. Consultar / Atualizar Cotação");
    Console.WriteLine("2. Ver Saldos e Patrimônio");
    Console.WriteLine("3. Comprar Bitcoin");
    Console.WriteLine("4. Vender Bitcoin");
    Console.WriteLine("5. Exibir Extrato de Transações");
    Console.WriteLine("0. Sair");
    Console.Write("Escolha uma opção: ");

    if (int.TryParse(Console.ReadLine(), out opcao))
    {
        try
        {
            switch (opcao)
            {
                case 1:
                    Console.Write($"Cotação atual é R$ {mercado.CotacaoBtc:N2}. Deseja alterar? Digite o novo valor (ou 0 para manter): ");
                    if (decimal.TryParse(Console.ReadLine(), out var novaCotacao))
                    {
                        if (novaCotacao == 0)
                        {
                            Console.WriteLine("Cotação mantida.");
                        }
                        else
                        {
                            mercado.AtualizarCotacao(novaCotacao);
                            Console.WriteLine("Cotação atualizada com sucesso!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Valor inválido. Informe um número decimal.");
                    }
                    break;
                case 2:
                    Console.WriteLine($"\nSaldo BRL: R$ {carteira.SaldoBrl:N2}");
                    Console.WriteLine($"Saldo BTC: {carteira.SaldoBtc} BTC");
                    Console.WriteLine($"Patrimônio Consolidado: R$ {mercado.CalcularPatrimonioTotal(carteira):N2}");
                    break;
                case 3:
                    Console.Write("Quanto em BTC deseja comprar? ");
                    if (decimal.TryParse(Console.ReadLine(), out var qtdCompra))
                    {
                        mercado.ComprarBtc(carteira, qtdCompra);
                        Console.WriteLine("Compra realizada com sucesso!");
                    }
                    break;
                case 4:
                    Console.Write("Quanto em BTC deseja vender? ");
                    if (decimal.TryParse(Console.ReadLine(), out var qtdVenda))
                    {
                        mercado.VenderBtc(carteira, qtdVenda);
                        Console.WriteLine("Venda realizada com sucesso!");
                    }
                    break;
                case 5:
                    Console.WriteLine("\n--- EXTRATO DE TRANSAÇÕES ---");
                    if (mercado.Historico.Count == 0)
                    {
                        Console.WriteLine("Nenhuma transação realizada ainda.");
                    }
                    else
                    {
                        foreach (var item in mercado.Historico)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;
                case 0:
                    Console.WriteLine("Saindo...");
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERRO]: {ex.Message}");
        }
    }

    if (opcao != 0)
    {
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

} while (opcao != 0);
