using JogoDaVelha.CrossCutting.Lib.Enumerators;
using JogoDaVelha.Domain.Comandos;
using JogoDaVelha.Domain.Comandos.Contratos;
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

                var game = GameModel.GameModelFactory.StartNewGame();

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
            try
            {
                //Recupera o jogo
                var gameModel = FileLib.GetJsonFile(id);

                //TODO RVO Executar a validação...
                //Executa a validação dos objetos
                //result.ResultValidation = gameModel.ValidaGame(movementModel);

                //Executa o movimento
                GameModel.GameModelFactory.ExecutarMovimento(gameModel, comando.Position.X, comando.Position.Y, comando.Player);


                //Verifica o Termino da partida
                GameModel.GameModelFactory.VerificarStatusPartida(gameModel);

                //Atualiza o jsonFile
                FileLib.CreateJsonFile(gameModel);

                var result = new MovementMessageModel();

                result.PlayerTurn = gameModel.PlayerTurn;
                result.Winner = gameModel.Winner;
                result.Status = gameModel.StatusGame.GetDescription();



                return new ComandoGenericoResposta(true, "Movimento executado com sucesso.", result);
            }
            catch (Exception ex)
            {

                return new ComandoGenericoResposta(false, $@"Erro ao executar movimento. {ex.Message}", null);

            }

        }


    }
}
