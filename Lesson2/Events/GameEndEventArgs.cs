namespace Lesson2.Events
{
    public class GameEndEventArgs : IEventArgs
    {
        public int Score { get; }
        public int Wave { get; }

        public GameEndEventArgs(int score, int wave)
        {
            Score = score;
            Wave = wave;
        }
    }
}