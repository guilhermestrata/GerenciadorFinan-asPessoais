using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorFinançasPessoais
{
    public class Transacao
    {
        private string _nome;
        private double _valor;
        private TipoTransacao _tipo;

        public double Valor {
            get {
                return Math.Round(_valor, 2);
            }
            set {
                if (value <= 0)
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
        public string Nome {
            get
            {
                return _nome;
            }
            set
            {
                if (value.Length < 3)
                {
                    throw new FormatException("O nome deve conter mais do que 3 caracteres");
                }

            }
        }
        public string Categoria { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public TipoTransacao Tipo { get; set; }

        public Transacao()
        {
            
        }
    }
}
