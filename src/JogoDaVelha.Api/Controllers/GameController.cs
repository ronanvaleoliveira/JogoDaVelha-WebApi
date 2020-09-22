using JogoDaVelha.Domain.Comandos;
using JogoDaVelha.Domain.Operadores;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JogoDaVelha.Api.Controllers
{
    [ApiController]
    [Route("v1/game")]
    public class GameController : ControllerBase
    {


        [HttpPost]
        [Route("")]
        public ComandoGenericoResposta Post([FromServices] OperadorGame operador)
        {
            return (ComandoGenericoResposta)operador.Exec();
        }

        [HttpPost]
        [Route("{id}/movement")]
        public ComandoGenericoResposta Post([FromBody] ComandoExecutarMovimento comando, [FromServices] OperadorGame operador, Guid id)
        {
            return (ComandoGenericoResposta)operador.Exec(comando, id);
        }
    }
}