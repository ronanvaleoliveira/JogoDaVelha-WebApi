using Flunt.Notifications;
using JogoDaVelha.Domain.Comandos.Contratos;

namespace JogoDaVelha.Domain.Modelo
{
    public class MovementMessageModel : Notifiable, IComando
    {
        public string PlayerTurn { get; set; }
        public string Winner { get; set; }
        public string Status { get; set; }

        public void Validate()
        {

        }
    }
}
