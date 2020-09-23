using JogoDaVelha.CrossCutting.Lib.Enumerators;
using JogoDaVelha.Domain.Lib;
using JogoDaVelha.Domain.Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace JogoDaVelha.Domain.Factory
{
    public static class GameFactory
    {
        public static GameModel StartNewGame()
        {
            var gameModel = new GameModel()
            {
                Id = Guid.NewGuid(),
                PlayerTurn = RandoPlayer(),
                StatusGame = StatusGameEnum.Iniciada,
                Tabuleiro = InicializaTabuleiro()
            };

            FileLib.CreateJsonFile(gameModel);

            return gameModel;
        }

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
}
