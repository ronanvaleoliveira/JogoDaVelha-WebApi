using Flunt.Notifications;
using Flunt.Validations;
using JogoDaVelha.Domain.Comandos.Contratos;
using JogoDaVelha.CrossCutting.Lib.Enumerators;
using JogoDaVelha.Domain.Lib;
using System;
using System.Collections.Generic;

namespace JogoDaVelha.Domain.Modelo
{
    public class GameModel : Notifiable, IComando
    {

        public Guid Id { get; set; }
        public string PlayerTurn { get; set; }
        public StatusGameEnum StatusGame { get; set; }
        public string[,] Tabuleiro { get; set; }
        public string Winner { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                .Requires()
                .IsFalse(StatusGame == StatusGameEnum.TemVencedor, "StatusGame", "Partida encerrada! Não será possível executar o movimento.")
                .IsFalse(StatusGame == StatusGameEnum.Empate, "StatusGame", "Partida encerrada! Não será possível executar o movimento.")
                );
        }

    }
}
