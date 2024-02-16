
using System.Runtime.CompilerServices;
using System.Transactions;
using System.Xml;

namespace GerenciadorFinancasPessoais
{
    public class Program
    {
        static void Main(string[] args)
        {
            Transacao transacao = new Transacao();

            try
            {
                Console.Write("Digite o nome do proprietário: ");
                string nome = Console.ReadLine();
                transacao.Nome = nome;
            }
            catch (FormatException e )
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
                Console.WriteLine(MostrarCabecalho());
                Console.WriteLine(MostrarMenu());

                opcao = LerOpcaoMenu();
                ProcessarOpcaoMenu(opcao, transacao.Nome);
                PressionaTecla();
                Console.Clear();

            } while (opcao != "6");
            
        }

        static string BemVindo(Transacao transacao)
        {
            string bv = $"BOAS VINDAS {transacao.Nome}!\n";
            return bv;
        }

        static string MostrarMenu()
        {
            string menu = "\nEscolha uma opção: \n" +
                            "1 - Realizar uma transação \n" +
                            "2 - Visualizar transações \n" +
                            "3 - Gerar relatórios \n" +
                            "4 - Gerenciar categorias \n" +
                            "5 - Configurações \n" +
                            "6 - Sair \n";
            return menu;
        }
        
        static string MostrarCabecalho()
        {
            string cabecalho = "\nGERENCIADOR DE FINANÇAS PESSOAIS";
            return cabecalho;
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
        

        public static void RealizarTransacao(string nome)
        {
            Console.Clear();
            Console.WriteLine("REALIZAR TRANSAÇÃO\n");
            Console.WriteLine("Tipo de transação:\n" +
                                "1 - DESPESAS\n" +
                                "2 - RECEITA");

            string tipo = Console.ReadLine();

            switch (tipo) 
            {
                case "1":
                    RealizarTransacaoDespesas(nome);
                    break;
                case "2":
                    RealizarTransacaoReceita();
                    break;
                default:
                    Console.WriteLine("Opção de transação inválida!");
                    break;
            }
        }
        public static void RealizarTransacaoDespesas(string nome)
        {
            double valor;
            string finalizar;

            Console.Clear();
            Console.WriteLine("=== REALIZAR TRANSAÇÃO ===\n");
            Console.WriteLine("Tipo de Transação: Despesas\n");

            Transacao despesas = new Transacao();
            despesas.Tipo = TipoTransacao.Despesa;

            Console.Write("Qual valor a ser transferido {0}? : ", nome);
            try
            {
            valor = double.Parse(Console.ReadLine());
            despesas.Valor = valor;
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

            Console.WriteLine("1 - [CONFIRMAR]\n" +
                            "2 - [CANCELAR]");
            finalizar = Console.ReadLine();

            switch (finalizar)
            {
                case "1":
                    Console.WriteLine("Transação realizada com sucesso!");
                    break;
                case "2":
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }

            Console.WriteLine("Pressione qualquer tecla para prosseguir");
            Console.ReadKey();

        }
        public static void RealizarTransacaoReceita()
        {
            
        }

        static void ProcessarOpcaoMenu(string opcao, string nome)
        {
            switch (opcao)
            {
                case "1":
                    RealizarTransacao(nome);
                    break;
                case "2":
                case "3":
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