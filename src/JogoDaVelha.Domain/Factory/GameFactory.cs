using JogoDaVelha.CrossCutting.Lib.Enumerators;
using JogoDaVelha.Domain.Lib;
using JogoDaVelha.Domain.Modelo;
using System;

namespace JogoDaVelha.Domain.Factory
{
    public static class GameFactory
    {
        public static GameModel StartNewGame()
        {
            var gameModel = new GameModel()
            {
                Id = Guid.NewGuid(),
                StatusGame = StatusGameEnum.Iniciada
            };

            FileLib.CreateJsonFile(gameModel);

            return gameModel;
        }

    }
}
