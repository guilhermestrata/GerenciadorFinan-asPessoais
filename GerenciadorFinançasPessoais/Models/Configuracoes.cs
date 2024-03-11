using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorFinancasPessoais.Models
{
    public class Configuracoes : Program
    {
        public Configuracoes()
        {
        }

        

        static string MostrarMenuConfig()
        {
            Cor("azul");
            Console.WriteLine("\n-------------------------------------------------");
            Console.WriteLine($"         CONFIGURAÇÕES DO SOFTWARE         ");
            Console.WriteLine("-------------------------------------------------\n");
            Cor("branca");

            string menu = "\nEscolha uma opção: \n\n" +
                           "[1] - Configurações de Aparência \n" +
                           "[2] - Configurações de Padronização \n" +
                           "[3] - Voltar ao Menu Principal\n";

            return menu;
        }

        static string LerOpcaoConfig()
        {
            string opcao;
            Console.Write("Opção desejada: ");
            opcao = Console.ReadLine();
            return opcao;
        }

        public void Config() 
        {
            Transacao transacao = new Transacao();
            Program program = new Program();
            Console.Clear();

            string opcao;
            do
            {

                Console.WriteLine(MostrarMenuConfig());
                opcao = LerOpcaoConfig();

                switch (opcao)
                {
                    case "1":
                        ConfigAparencia(transacao.Nome);
                        break;
                    case "2":
                        ConfigPadronizacao(transacao.Nome);
                        break;
                    case "3":
                        break;
                    default:
                        Console.WriteLine("Opção de menu inválida!");
                        break;
                }
                Cor("azul");
                Console.WriteLine("Pressione qualquer tecla para prosseguir.");
                Cor("branca");
                Console.ReadKey();

                Console.Clear();
            } while (opcao != "3");

        }
        public void ConfigAparencia(string nome)
        {
            Console.WriteLine($"Ainda trabalhando nesta opção {nome}");
        }
        public void ConfigPadronizacao(string nome)
        {
            Console.WriteLine($"Ainda trabalhando nesta opção {nome}");
        }

    }
}
