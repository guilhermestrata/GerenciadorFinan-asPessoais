
using System.Drawing;
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

            try
            {
                Console.Write("Digite o nome do proprietário: ");
                string nome = Console.ReadLine();
                transacao.Nome = nome;
            }
            catch (FormatException e)
            {
                Cor("vermelha");
                Console.WriteLine(e.Message);

                Cor("branca");

                Console.WriteLine("\nPressione qualquer tecla para prosseguir");
                Console.ReadKey();
                return;
            }

            Cor("verde");
            Console.WriteLine(BemVindo(transacao));
            Cor("branca");

            string opcao;

            do
            {
                Console.WriteLine(MostrarMenu());

                opcao = LerOpcaoMenu();
                ProcessarOpcaoMenu(opcao, transacao.Nome, listaDeTransacoes);
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
            string menu = "\nEscolha uma opção: \n" +
                            "1 - Realizar uma transação \n" +
                            "2 - Visualizar transações \n" +
                            "3 - Mostrar saldo total \n" +
                            "4 - Gerenciar categorias \n" +
                            "5 - Configurações \n" +
                            "6 - Sair \n";
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
            Console.WriteLine("Pressione qualquer tecla para prosseguir.");
            Console.ReadKey();
        }
        

        public static void RealizarTransacao(string nome, List<Transacao> transacoes)
        {
            Console.Clear();
            Console.WriteLine("REALIZAR TRANSAÇÃO\n");
            Console.WriteLine("[ 1 - AGORA ]\n" + "[ 2 - AGENDAR]\n \n");
            Console.Write("Tipo de transação: ");

            string tipo = Console.ReadLine();

            switch (tipo) 
            {
                case "1":
                    RealizarTransacaoAgora(nome, transacoes);
                    break;
                case "2":
                    RealizarTransacaoAgendada();
                    break;
                default:
                    Console.WriteLine("Opção de transação inválida!");
                    break;
            }
        }


        public static void MostrarTransacoes(List<Transacao> transacoes)
        {
            Console.Clear();
            Console.WriteLine("::::::::::::::::: LISTA DE TRANSAÇÕES :::::::::::::::::");

            foreach (var transacao in transacoes)
            {
                Cor("azul");
                Console.WriteLine($"\nTipo: {transacao.Tipo}");
                Console.WriteLine($"Valor: {transacao.Valor}");
                Console.WriteLine($"Descrição: {transacao.Descricao}");
                Console.WriteLine($"Data: {transacao.Data}\n");
                Console.WriteLine("---------------------------------------");
                Cor("branca");
            }
            



            if (transacoes.Count() == 0) 
            {
                Cor("vermelha");
                Console.WriteLine("\nNENHUMA TRANSAÇÃO REGISTRADA\n");
                Cor("branca");
            }


            Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();
        }

        public static void RealizarTransacaoAgora(string nome, List<Transacao> transacoes)
        {
            double valor;
            string finalizar;

            Transacao transacao = new Transacao();
            transacao.Saldo = 10000; 

            Console.Clear();
            Console.WriteLine("=== REALIZAR TRANSAÇÃO ===\n");

            Cor("azul");
            Console.WriteLine("Tipo de Transação: Despesas\n");
            Cor("branca");

            Transacao despesas = new Transacao();
            despesas.Tipo = TipoTransacao.Despesa;

            Console.Write("Qual valor a ser transferido {0}? : ", nome);

            try
            {
                valor = double.Parse(Console.ReadLine());
                if (valor > transacao.Saldo)
                {
                    throw new FormatException("Sem saldo disponível");
                }
                despesas.Valor = valor;
                transacao.Saldo -= valor; 
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Pressione qualquer tecla para prosseguir");
                Console.ReadKey();
                return;
            }

            Console.Write("Descrição da transação: ");
            despesas.Descricao = Console.ReadLine();

            despesas.Data = DateTime.Now;

            transacoes.Add(despesas);

            Console.WriteLine("\nConfirmar Transação:");
            Console.WriteLine("1 - [Confirmar]");
            Console.WriteLine("2 -  [Cancelar]");

            finalizar = Console.ReadLine();

            switch (finalizar)
            {
                case "1":
                    Console.WriteLine("Transação realizada com sucesso!");
                    break;
                case "2":
                    transacoes.Remove(despesas); 
                    transacao.Saldo += valor; 
                    Console.WriteLine("Transação cancelada!");
                    break;
                default:
                    Console.WriteLine("Opção inválida. Transação cancelada.");
                    transacoes.Remove(despesas); 
                    transacao.Saldo += valor; 
                    break;
            }

            Console.WriteLine("Pressione qualquer tecla para prosseguir");
            Console.ReadKey();
        }

        public static void RealizarTransacaoAgendada()
        {
            
        }

        public static void MostrarSaldoConta(List<Transacao> transacoes)
        {
            Console.Clear();
            Console.WriteLine(":::::::::::: SALDO TOTAL DA CONTA ::::::::::::");

            double saldo = 10000;

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


        static void ProcessarOpcaoMenu(string opcao, string nome, List<Transacao> transacoes)
        {
            switch (opcao)
            {
                case "1":
                    RealizarTransacao(nome, transacoes);
                    break;
                case "2":
                    MostrarTransacoes(transacoes);
                    break;
                case "3":
                    MostrarSaldoConta(transacoes);
                    break;
                case "4":
                case "5":
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