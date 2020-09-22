using System.ComponentModel;

namespace JogoDaVelha.CrossCutting.Lib.Enumerators
{
    public enum StatusGameEnum
    {
        [Description("Partida iniciada")]
        Iniciada = 0,
        [Description("Partida em andamento")]
        EmAndamento = 1,
        [Description("Partida possui vencedor")]
        TemVencedor = 2,
        [Description("Partida empatada")]
        Empate = 3
    }
}
