using GerenciadorFinancasPessoais.Models;

namespace GerenciadorFinancasTeste.Exceptions
{
    public class ExceptionsNomeTeste
    {
        [Fact]
        public void TestaNumeroCaracteresNomeProprietario()
        {
            var transacao = new Transacao();

            var exception = Assert.Throws<FormatException>(() => (transacao.Nome = "Gu"));

            Assert.Equal("O nome deve ser apresentado com 3 ou mais caracteres", exception.Message);
        }

        [Fact]
        
        public void TestaNumeroCaracteresNomeDestinatario()
        {
            var transacao = new Transacao();

            var exception = Assert.Throws<FormatException>(() => transacao.Destinatario = "Gu");

            Assert.Equal("O nome deve conter mais do que 3 caracteres", exception.Message);
        }
    }
}
