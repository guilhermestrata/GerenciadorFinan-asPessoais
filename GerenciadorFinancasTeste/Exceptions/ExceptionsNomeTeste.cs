using GerenciadorFinancasPessoais.Models;

namespace GerenciadorFinancasTeste.Exceptions
{
    public class ExceptionsNomeTeste
    {
        [Fact]
        public void TestaNumeroCaracteresNomeProprietario()
        {
            var transacao = new Transacao();

            var exception = Assert.Throws<ArgumentException>(() => (transacao.Nome = "Gu"));

            Assert.Equal("O nome deve conter pelo menos 3 caracteres.", exception.Message);
        }

        [Fact]
        
        public void TestaNumeroCaracteresNomeDestinatario()
        {
            var transacao = new Transacao();

            var exception = Assert.Throws<ArgumentException>(() => transacao.Destinatario = "Gu");

            Assert.Equal("O nome do destinatário deve conter pelo menos 3 caracteres.", exception.Message);
        }
    }
}
