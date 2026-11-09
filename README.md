# MiniBitcoin - Gestão e Qualidade de Software

Aplicação de console desenvolvida em C# (.NET) aplicando conceitos de **Test-Driven Development (TDD)**, testes unitários com **xUnit** e arquitetura limpa em camadas para simulação de carteira de criptomoedas (BRL e Bitcoin) e operações de mercado.

---

## 🚀 Funcionalidades da Aplicação

1. **Gestão de Carteira (Wallet):** Gerenciamento de saldo em moeda fiduciária (BRL) e Satoshis/Bitcoins, com validações rigorosas para evitar saldos negativos em operações ou inicializações inválidas.
2. **Cotação e Simulação de Mercado:** Manutenção e consulta em tempo real do preço atual do Bitcoin.
3. **Compra e Venda de Bitcoin:** Execução de ordens de compra e venda utilizando saldo em BRL, validando fundos disponíveis e atualizando os saldos correspondentes de forma atômica.
4. **Extrato de Transações:** Histórico completo de operações realizadas (compras e vendas) e cálculo do patrimônio total consolidado.

---

## 🛠️ Tecnologias e Ferramentas Utilizadas

* **Linguagem:** C# (.NET 8+)
* **Testes Unitários:** xUnit
* **Controle de Versão:** Git e GitHub (seguindo o padrão *Conventional Commits*)

---

## 📂 Estrutura do Projeto

```text
MiniBitcoin/
│
├── MiniBitcoin.sln                     # Arquivo de solução do .NET
├── MiniBitcoin.Console/                # Projeto principal (Aplicação de Console)
│   ├── Models/                         # Entidades de Domínio (Carteira, TransacaoHistorico)
│   ├── Services/                       # Regras de Negócio (MercadoService)
│   └── Program.cs                      # Interface de Menu Interativo via CLI
│
└── MiniBitcoin.Tests/                  # Projeto de Testes Unitários
    ├── WalletTests.cs                  # Testes de validação da Carteira
    └── TransacaoTests.cs               # Testes de operações de Compra/Venda e Extrato