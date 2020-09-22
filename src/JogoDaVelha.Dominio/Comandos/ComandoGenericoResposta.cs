using JogoDaVelha.Dominio.Comandos.Contratos;

namespace JogoDaVelha.Dominio.Comandos
{
    public class ComandoGenericoResposta : IComandoResposta
    {
        public ComandoGenericoResposta()
        {

        }

        public ComandoGenericoResposta(bool sucesso, string mensagem, object data)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Data = data;
        }

        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Data { get; set; }
    }
}
