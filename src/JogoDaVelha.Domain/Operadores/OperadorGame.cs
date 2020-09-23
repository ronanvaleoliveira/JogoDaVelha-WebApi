using JogoDaVelha.CrossCutting.Lib.Enumerators;
using JogoDaVelha.CrossCutting.Lib.Extensions;
using JogoDaVelha.Domain.Comandos;
using JogoDaVelha.Domain.Comandos.Contratos;
using JogoDaVelha.Domain.Factory;
using JogoDaVelha.Domain.Lib;
using JogoDaVelha.Domain.Modelo;
using System;

namespace JogoDaVelha.Domain.Operadores
{
    public class OperadorGame : OperadorBase
    {

        public IComandoResposta Exec()
        {
            try
            {

                var game = GameFactory.StartNewGame();

                var result = new GameMessagemModel();

                //Prepara resultado
                result.Id = game.Id.ToString();
                result.FirstPlayer = game.PlayerTurn;
                result.Status = game.StatusGame.GetDescription();

                return new ComandoGenericoResposta(true, "Partida iniciada com sucesso.", result);
            }
            catch (Exception ex)
            {
                return new ComandoGenericoResposta(false, $@"Ocorreu um erro ao iniciar a partida. {ex.Message}", null);
            }
        }




        public IComandoResposta Exec(ComandoExecutarMovimento comando, Guid id)
        {
            var result = new MovementMessageModel();
            try
            {

                //Recupera o jogo
                var gameModel = FileLib.GetJsonFile(id);


                //Executa as validações do movimento
                ValidaMovimento(comando, gameModel, result);
                if (result.Invalid)
                    return new ComandoGenericoResposta(false, "Erro ao executar movimento.", result);


                //Executa o movimento
                GameFactory.ExecutarMovimento(gameModel, comando.Position.X.ToInt32(0), comando.Position.Y.ToInt32(0), comando.Player);


                //Verifica o Termino da partida
                GameFactory.VerificarStatusPartida(gameModel);

                //Atualiza o jsonFile
                FileLib.CreateJsonFile(gameModel);


                result.PlayerTurn = gameModel.PlayerTurn;
                result.Winner = gameModel.Winner;
                result.Status = gameModel.StatusGame.GetDescription();


                return new ComandoGenericoResposta(true, "Movimento executado com sucesso.", result);
            }
            catch (Exception ex)
            {
                result.AddNotification("", ex.Message);
                return new ComandoGenericoResposta(false, $@"Erro ao executar movimento.", result);

            }

        }

        private static void ValidaMovimento(ComandoExecutarMovimento comando, GameModel gameModel, MovementMessageModel result)
        {
            gameModel.Validate();
            if (gameModel.Invalid)
                result.AddNotifications(gameModel.Notifications);

            comando.Validate();
            if (comando.Invalid)
                result.AddNotifications(comando.Notifications);


            if (result.Valid)
            {
                //Verifica o turno do jogador
                if (!comando.Player.ToUpper().Equals(gameModel.PlayerTurn))
                    result.AddNotification("Player", $@"Não é o turno do jogador {comando.Player.ToUpper()}!");

                //Validar se a posição xy já foi utilizada
                if (!gameModel.Tabuleiro[comando.Position.X.ToInt32(0), comando.Position.Y.ToInt32(0)].Equals(string.Empty))
                    result.AddNotification("Tabuleiro", $@"Movimento inválido! A posição 'X'{comando.Position.X}'Y'{comando.Position.Y} já esta sendo utilizada.");
            }
        }
    }
}
