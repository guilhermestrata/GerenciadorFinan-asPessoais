





using System.Transactions;

namespace GerenciadorFinançasPessoais
{
    public class Program
    {
        static void Main(string[] args)
        {
            string opcao;

            do
            {
                Console.WriteLine(MostrarCabecalho());
                Console.WriteLine(DonoConta());
                Console.WriteLine(MostrarMenu());

                opcao = LerOpcaoMenu();
                ProcessarOpcaoMenu(opcao);
                PressionaTecla();
                Console.Clear();

            } while (opcao != "6");
            
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
            string cabecalho = "GERENCIADOR DE FINANÇAS PESSOAIS";
            return cabecalho;
        }

        static string LerOpcaoMenu()
        {
            string opcao;
            Console.WriteLine("Opção desejada: ");
            opcao = Console.ReadLine();
            return opcao;
        }

        private static void PressionaTecla()
        {
            Console.WriteLine("Pressione qualquer tecla para prosseguir.");
            Console.ReadKey();
        }
        static Transacao DonoConta()
        {
            Transacao transacao = new Transacao();
            Console.Write("Digite seu nome: ");
            string nome = Console.ReadLine();

            transacao.Nome = nome;
            return transacao;
        }

        public static void RealizarTransacaoDespesa()
        {
            Console.WriteLine("REALIZAR TRANSAÇÃO\n");
            Transacao transacao = new Transacao();

            transacao.Tipo = TipoTransacao.Despesa;
            Console.Write("Digite o valor da transação: ");
            try
            {
                transacao.Valor = double.Parse(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine("Ocorreu um problema {0}", e);
                PressionaTecla();
                return;
            }
            Console.WriteLine("Digite");

        }


        static void ProcessarOpcaoMenu(string opcao)
        {
            Transacao transacao = DonoConta();
            switch (opcao)
            {
                case "1":
                    Console.WriteLine($"Ainda trabalhando nessa opção {transacao.Nome}!");
                    break;
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                    Transacao donoConta = DonoConta(); 
                    Console.WriteLine($"Ainda trabalhando nessa opção {donoConta.Nome}!");
                    break;
                default:
                    Console.WriteLine("Opção de menu inválida!");
                    break;
            }
        }
    }
}