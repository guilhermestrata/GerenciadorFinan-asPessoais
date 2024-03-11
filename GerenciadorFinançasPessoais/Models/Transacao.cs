using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorFinancasPessoais.Models
{
    public class Transacao : Program
    {
        private string _nome;
        private double _valor;
        private TipoTransacao _tipo;
        private string _destinatario;
        private string _codigo;
        private double _saldo;
        private static double _saldoInicial = 10000;

        public static double SaldoInicial
        {
            get 
            { 
                return _saldoInicial;
            }
            set 
            { 
                _saldoInicial = value; 
            }
        }

        public double Saldo
        {
            get
            {
                return Math.Round(_saldo, 2);
            }
            set
            {
                if (value < 0)
                {
                    throw new FormatException("Sem saldo disponível");
                }
                else
                {
                    _saldo = value;
                }
            }
        }

        public double Valor { get; set; }

        public string Nome
        {
            get
            {
                return _nome;
            }
            set
            {
                if (value.Length < 3 || value.Length == 0)
                {
                    throw new FormatException("O nome deve ser apresentado com 3 ou mais caracteres");
                }

                _nome = value;
            }
        }
        public string Categoria { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public TipoTransacao Tipo { get; set; }
        public string Comprovante { get; set; }
        public string Codigo { get; set; }
        public string Destinatario
        {
            get
            {
                return _destinatario;
            }
            set
            {
                if (value.Length < 3)
                {
                    throw new FormatException("O nome deve conter mais do que 3 caracteres");
                }

            }
        }

        public void RealizarTransacao(string nome, List<Transacao> transacoes)
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
                    RealizarTransacaoAgendada(nome, transacoes);
                    break;
                default:
                    Console.WriteLine("Opção de transação inválida!");
                    break;
            }
        }

        public void RealizarTransacaoAgora(string nome, List<Transacao> transacoes)
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

            Console.Write("Qual valor a ser transferido {0}? : R$", nome);

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

        public void MostrarTransacoes(List<Transacao> transacoes)
        {
            Console.Clear();
            Console.WriteLine("::::::::::::::::: LISTA DE TRANSAÇÕES :::::::::::::::::");

            foreach (var transacao in transacoes)
            {
                Cor("azul");
                Console.WriteLine($"\nTipo: {transacao.Tipo}");
                Console.WriteLine("Valor: {0:C}", transacao.Valor);
                Console.WriteLine($"Descrição: {transacao.Descricao}");
                Console.WriteLine($"Data: {transacao.Data}\n");
                Cor("branca");
                Console.WriteLine("---------------------------------------");
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

        public Transacao()
        {
        }

        public Transacao(string nome)
        {
            Nome = nome;
        }

    }
}
