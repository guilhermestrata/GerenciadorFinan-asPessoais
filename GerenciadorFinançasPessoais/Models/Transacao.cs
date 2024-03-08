using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorFinancasPessoais.Models
{
    public class Transacao
    {
        private string _nome;
        private double _valor;
        private TipoTransacao _tipo;
        private string _destinatario;
        private string _codigo;

        public double Valor
        {
            get
            {
                return Math.Round(_valor, 2);
            }
            set
            {
                if (value < 0.01)
                {
                    throw new FormatException("A quantia deve ser no mínimo de 0,01 centavos");
                }
                if (value > 1000000)
                {
                    throw new FormatException("O sistema não permite transações com um valor acima de 1 milhão");
                }

                _valor = value;
            }
        }

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
        public Transacao()
        {
        }

        public Transacao(string nome)
        {
            Nome = nome;
        }

    }
}
