namespace DiceRoller_Kaua_Julia
{
    public class Game
    {
        public int PlayerPoint { get; private set; }
        public int Streak { get; private set; }

        public bool CheckWinner(int escolhido, int sorteado)
        {
            if (escolhido == sorteado)
            {
                PlayerPoint++;
                Streak++;
                return true;
            }
            else
            {
                Streak = 0;
                return false;
            }
        }
    }
}