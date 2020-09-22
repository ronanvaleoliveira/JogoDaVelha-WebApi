using Flunt.Notifications;
using Flunt.Validations;
using JogoDaVelha.Domain.Comandos.Contratos;
using System;
using System.Security.Cryptography.X509Certificates;

namespace JogoDaVelha.Domain.Comandos
{
    public class ComandoExecutarMovimento : Notifiable, IComando
    {
        public string Player { get; set; }
        public PositionModel Position { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                .Requires()
                .IsNotNullOrEmpty(Player, "Player", "Favor informar o Player que esta realizando a jogada!")
                .IsFalse(Player.ToUpper() != "X" && Player.ToUpper() != "O", "Player", "Os valores aceitos para o campo Player são 'X' ou 'O'!")
                .IsFalse(Position == null, "Position", "Favor informar as posições do tabuleiro!")
                .IsFalse(Position != null && Position.X == null, "Position.X", "Favor informar a posição X do Tabuleiro!")
                .IsFalse(Position != null && Position.Y == null, "Position.Y", "Favor informar a posição Y do Tabuleiro!")

                .IsFalse(Position != null && Position.X != null && (Position.X < 0 || Position.X > 2), "Position.X", "Movimento inválido! A posição 'X' tem que estar entre 0 e 2.")
                .IsFalse(Position != null && Position.Y != null && (Position.Y < 0 || Position.Y > 2), "Position.Y", "Movimento inválido! A posição 'Y' tem que estar entre 0 e 2.")
                );
        }
    }

    public class PositionModel
    {
        public int? X { get; set; }
        public int? Y { get; set; }
    }
}
