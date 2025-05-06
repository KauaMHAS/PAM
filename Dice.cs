using System;

namespace DiceRoller_Kaua_Julia
{
    public class Dice
    {
        private int ladoSorteado;
        private static readonly Random random = new Random();

        public int LadoSorteado => ladoSorteado;

        public int Roll()
        {
            ladoSorteado = random.Next(1, 7);
            return ladoSorteado;
        }
    }
}