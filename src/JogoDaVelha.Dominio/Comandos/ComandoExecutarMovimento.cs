using System;

namespace JogoDaVelha.Dominio.Comandos
{
    public class ComandoExecutarMovimento
    {
        public string Player { get; set; }
        public PositionModel Position { get; set; }
    }

    public class PositionModel
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
