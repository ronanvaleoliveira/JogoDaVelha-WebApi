using JogoDaVelha.Domain.Comandos;
using JogoDaVelha.Domain.Operadores;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JogoDaVelha.Api.Controllers
{
    /// <summary>
    /// Documentação da controller
    /// </summary>
    [ApiController]
    [Route("v1/game")]
    public class GameController : ControllerBase
    {

        /// <summary>
        /// Cria uma nova partida e sorteia o primeiro jogador.
        /// </summary>
        /// <param name="operador"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public ComandoGenericoResposta Post([FromServices] OperadorGame operador)
        {
            return (ComandoGenericoResposta)operador.Exec();
        }

        /// <summary>
        /// Executa o movimento de cada jogador na partida.
        /// </summary>
        /// <remarks>
        /// RN1 - O campo "Player" deverá conter o valor de cada jogador ("X" ou "O").<br/>
        /// RN2 - O campo "Position" deverá conter as coordenadas [X, Y] de onde será efetuada o movimento.<br/>
        /// <br/>
        /// Posições do Tabuleiro:  
        /// [0,0]   [1,0]	[2,0]<br/>
        /// [0,1]	[1,1]	[2,1]<br/>
        /// [0,2]	[1,2]	[2,2]<br/>
        /// <br/>
        /// </remarks>
        /// <param name="comando">Objeto que contem as intruções para a execução do movimento</param>
        /// <param name="operador"></param>
        /// <param name="id">Identificador da partida</param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}/movement")]
        public ComandoGenericoResposta Post([FromBody] ComandoExecutarMovimento comando, [FromServices] OperadorGame operador, Guid id)
        {
            return (ComandoGenericoResposta)operador.Exec(comando, id);
        }
    }
}