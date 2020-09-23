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
        public GameModel()
        {
            PlayerTurn = RandoPlayer();
            Tabuleiro = InicializaTabuleiro();
        }

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

        private static string RandoPlayer()
        {
            var list = new List<string> { "X", "O" };
            Random r = new Random();
            return list[r.Next(list.Count)];
        }

        private static string[,] InicializaTabuleiro()
        {
            var tab = new string[3, 3];

            //Zerando o tabuleiro
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    tab[x, y] = string.Empty;
                }
            }
            return tab;
        }
    }


    public static class GameEngine
    {

        public static void ExecutarMovimento(GameModel gameModel, int x, int y, string player)
        {
            gameModel.Tabuleiro[x, y] = player.ToUpper();
            ChangePlayer(gameModel);
            gameModel.StatusGame = StatusGameEnum.EmAndamento;
        }

        public static void VerificarStatusPartida(GameModel gameModel)
        {
            //Verifica combinação nas linhas
            for (int row = 0; row < 3; row++)
            {
                if (string.IsNullOrEmpty(gameModel.Tabuleiro[row, 0]) ||
                    string.IsNullOrEmpty(gameModel.Tabuleiro[row, 1]) ||
                    string.IsNullOrEmpty(gameModel.Tabuleiro[row, 2]))
                    continue;

                if (gameModel.Tabuleiro[row, 0].Equals(gameModel.Tabuleiro[row, 1]) &&
                    gameModel.Tabuleiro[row, 1].Equals(gameModel.Tabuleiro[row, 2]))
                {
                    gameModel.StatusGame = StatusGameEnum.TemVencedor;
                    gameModel.Winner = gameModel.Tabuleiro[row, 0].ToString();
                    return;
                }
            }


            //Verifica combinação nas colunas
            for (int col = 0; col < 3; col++)
            {
                if (string.IsNullOrEmpty(gameModel.Tabuleiro[0, col]) ||
                    string.IsNullOrEmpty(gameModel.Tabuleiro[1, col]) ||
                    string.IsNullOrEmpty(gameModel.Tabuleiro[2, col]))
                    continue;

                if (gameModel.Tabuleiro[0, col].Equals(gameModel.Tabuleiro[1, col]) &&
                    gameModel.Tabuleiro[1, col].Equals(gameModel.Tabuleiro[2, col]))
                {
                    gameModel.StatusGame = StatusGameEnum.TemVencedor;
                    gameModel.Winner = gameModel.Tabuleiro[0, col].ToString();
                    return;
                }
            }


            //Verifica combinação nas diagonais
            if (!string.IsNullOrEmpty(gameModel.Tabuleiro[0, 0]) &&
                !string.IsNullOrEmpty(gameModel.Tabuleiro[1, 1]) &&
                !string.IsNullOrEmpty(gameModel.Tabuleiro[2, 2]) &&
                gameModel.Tabuleiro[0, 0].Equals(gameModel.Tabuleiro[1, 1]) &&
                gameModel.Tabuleiro[1, 1].Equals(gameModel.Tabuleiro[2, 2]))
            {
                gameModel.StatusGame = StatusGameEnum.TemVencedor;
                gameModel.Winner = gameModel.Tabuleiro[0, 0].ToString();
                return;
            }

            if (!string.IsNullOrEmpty(gameModel.Tabuleiro[0, 2]) &&
                !string.IsNullOrEmpty(gameModel.Tabuleiro[1, 1]) &&
                !string.IsNullOrEmpty(gameModel.Tabuleiro[2, 0]) &&
                gameModel.Tabuleiro[0, 2].Equals(gameModel.Tabuleiro[1, 1]) &&
                gameModel.Tabuleiro[1, 1].Equals(gameModel.Tabuleiro[2, 0]))
            {
                gameModel.StatusGame = StatusGameEnum.TemVencedor;
                gameModel.Winner = gameModel.Tabuleiro[0, 2].ToString();
                return;
            }



            //Verifica se possui jogadas pendentes
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (string.IsNullOrEmpty(gameModel.Tabuleiro[x, y]))
                    {
                        gameModel.StatusGame = StatusGameEnum.EmAndamento;
                        gameModel.Winner = string.Empty;
                        return;
                    }
                }
            }


            gameModel.StatusGame = StatusGameEnum.Empate;
            gameModel.Winner = "Draw";
        }

        private static void ChangePlayer(GameModel gameModel)
        {
            gameModel.PlayerTurn = gameModel.PlayerTurn.Equals("X") ? "O" : "X";
        }

    }

}
