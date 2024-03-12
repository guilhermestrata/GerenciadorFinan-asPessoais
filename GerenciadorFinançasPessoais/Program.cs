
using System.Drawing;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Transactions;
using System.Xml;
using GerenciadorFinancasPessoais.Models;

namespace GerenciadorFinancasPessoais
{
    public class Program
    {
        static void Main(string[] args)
         {
            List<Transacao> listaDeTransacoes = new List<Transacao>();

            Transacao transacao = new Transacao();
            Configuracoes configuracoes = new Configuracoes();  

            try
            {
                Console.Write("Digite o nome do proprietário: ");
                string nome = Console.ReadLine();
                transacao.Nome = nome;
            }
            catch (ArgumentException e)
            {
                Cor("vermelha");
                Console.WriteLine(e.Message);
                Cor("branca");

                Console.WriteLine("\nPressione qualquer tecla para prosseguir");
                Console.ReadKey();
                return;
            }

            Cor("azul");
            Console.WriteLine(BemVindo(transacao));
            Cor("branca");

            string opcao;

            do
            {
                Console.WriteLine(MostrarMenu());

                opcao = LerOpcaoMenu();
                ProcessarOpcaoMenu(opcao, transacao.Nome, listaDeTransacoes, configuracoes, transacao);
                PressionaTecla();
                Console.Clear();

            } while (opcao != "6");
            
        }

        static string BemVindo(Transacao transacao)
        {
            string bv = $"\nBem-vindo ao Gerenciador de Finanças {transacao.Nome}\n" +
                        $"Como posso ajudá-lo hoje?";

            return bv;
        }

        static string MostrarMenu()
        {
            Cor("azul");
            Console.WriteLine("\n-------------------------------------------------");
            Console.WriteLine($"      GERENCIADOR DE FINANÇAS PESSOAIS      ");
            Console.WriteLine("-------------------------------------------------\n");
            Cor("branca");

            string menu = "\nEscolha uma opção: \n\n" +
                            "[1] - Realizar uma transação \n" +
                            "[2] - Visualizar transações \n" +
                            "[3] - Mostrar saldo total \n" +
                            "[4] - Gerenciar categorias \n" +
                            "[5] - Configurações \n" +
                            "[6] - Sair \n";
            return menu;
        }
        

        static string LerOpcaoMenu()
        {
            string opcao;
            Console.Write("Opção desejada: ");
            opcao = Console.ReadLine();
            return opcao;
        }

        public static void Cor(string cor)
        {
            ConsoleColor corConsole;
            switch (cor.ToLower())
            {
                case "verde":
                    corConsole = ConsoleColor.Green;
                    break;
                case "branca":
                    corConsole = ConsoleColor.White;
                    break;
                case "vermelha":
                    corConsole = ConsoleColor.Red;
                    break;
                case "azul":
                    corConsole = ConsoleColor.Cyan;
                    break;
                default:
                    corConsole = ConsoleColor.White;
                    break;
            }
            Console.ForegroundColor = corConsole;
        }


        private static void PressionaTecla()
        {
            Cor("azul");
            Console.WriteLine("Pressione qualquer tecla para prosseguir.");
            Cor("branca");
            Console.ReadKey();
        }
        
        

        public static void RealizarTransacaoAgora(string nome, List<Transacao> transacoes)
        {
            double valor;
            string finalizar;

            double saldo = transacoes.Any() ? transacoes.Last().Saldo : Transacao.SaldoInicial;

            Console.Clear();
            Console.WriteLine("=== REALIZAR TRANSAÇÃO ===\n");

            Cor("azul");
            Console.WriteLine("Tipo de Transação: Despesas\n");
            Cor("branca");

            Transacao transacao = new Transacao(); 

            transacao.Tipo = TipoTransacao.Agora;

            Console.Write("Qual valor a ser transferido {0}? : ", nome);

            try
            {
                valor = double.Parse(Console.ReadLine());

                if (valor > saldo)
                {
                    Cor("vermelha");
                    Console.WriteLine("Transação excede o saldo disponível. O valor foi ajustado para {0:C}.\n", saldo);
                    Cor("branca");
                    valor = saldo;
                }

                transacao.Valor = valor; 
                transacao.Saldo = saldo - valor; 

                Console.Write("Descrição da transação: ");
                transacao.Descricao = Console.ReadLine();

                transacao.Data = DateTime.Now;

                transacoes.Add(transacao); 
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);

                Console.WriteLine("Pressione qualquer tecla para prosseguir");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nConfirmar Transação:");
            Console.WriteLine("1 - [Confirmar]");
            Console.WriteLine("2 - [Cancelar]");

            finalizar = Console.ReadLine();

            switch (finalizar)
            {
                case "1":
                    Console.WriteLine("Transação realizada com sucesso!");
                    break;
                case "2":
                    transacoes.Remove(transacao);
                    transacao.Saldo += valor;
                    Console.WriteLine("Transação cancelada!");
                    break;
                default:
                    Console.WriteLine("Opção inválida. Transação cancelada.");
                    transacoes.Remove(transacao);
                    transacao.Saldo += valor;
                    break;
            }
            Console.WriteLine("Pressione qualquer tecla para prosseguir");
            Console.ReadKey();
        }


        public static void RealizarTransacaoAgendada(string nome, List<Transacao> transacoes)
        {
            double valor;
            string finalizar;

            double saldo = transacoes.Any() ? transacoes.Last().Saldo : Transacao.SaldoInicial;

            Console.Clear();
            Console.WriteLine("=== REALIZAR TRANSAÇÃO ===\n");
            Cor("azul");
            Console.WriteLine("Tipo de Transação: Agendada\n");
            Cor("branca");

            Transacao transacao = new Transacao();

            transacao.Tipo = TipoTransacao.Agendar;

            Console.Write($"Qual valor da transação agendada {nome}? ");

            try
            {
                valor = double.Parse(Console.ReadLine());

                if (valor > saldo)
                {
                    Console.WriteLine("Transação agendada excede o saldo disponível.\n O valor foi ajustado para {0:C}.", saldo);
                    valor = saldo;
                }

                transacao.Valor = valor; 
                transacao.Saldo = saldo - valor; 

                Console.Write("Para quando deseja agendar a transação (DD/MM/AAAA HH:mm)? ");
                string dataHoraString = Console.ReadLine();

                DateTime dataHora;
                if (!DateTime.TryParseExact(dataHoraString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataHora))
                {
                    throw new FormatException("Formato de data/hora inválido. Use o formato DD/MM/AAAA HH:mm.");
                }
                while (DateTime.Now < dataHora)
                {
                    Thread.Sleep(1000);
                }

                transacao.Data = dataHora; 
                transacoes.Add(transacao);

                Cor("verde");
                Console.WriteLine("Transação agendada realizada com sucesso!");
                Cor("branca");
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Pressione qualquer tecla para prosseguir");
                Console.ReadKey();
                return;
            }
        }

        public static void MostrarSaldoConta(List<Transacao> transacoes)
        {
            Console.Clear();
            Console.WriteLine(":::::::::::: SALDO TOTAL DA CONTA ::::::::::::");

            double saldo = Transacao.SaldoInicial;

            foreach (Transacao transacao in transacoes)
            {
                saldo -= transacao.Valor;
            }

            Console.WriteLine("\n-------------------------------------------------");
            Console.WriteLine($"Saldo total da conta: {saldo.ToString("C")}");
            Console.WriteLine("-------------------------------------------------\n");

            Cor("azul");
            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu.");
            Cor("branca");
            Console.ReadKey();
        }



        static void ProcessarOpcaoMenu(string opcao, string nome, List<Transacao> transacoes, Configuracoes configuracoes, Transacao transacao)
        {

            switch (opcao)
            {
                case "1":
                    transacao.RealizarTransacao(nome, transacoes);
                    break;
                case "2":
                    transacao.MostrarTransacoes(transacoes);
                    break;
                case "3":
                    MostrarSaldoConta(transacoes);
                    break;
                case "4":
                case "5":
                    configuracoes.Config();
                    break;
                case "6":
                    Console.WriteLine($"Ainda trabalhando nessa opção {nome}!");
                    break;
                default:
                    Console.WriteLine("Opção de menu inválida!");
                    break;
            }
        }
    }
}