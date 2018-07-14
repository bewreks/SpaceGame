namespace Lesson2.Events
{
    /// <summary>
    /// Событие обновления счета
    /// </summary>
    public class ChangeScoreEvent : IEventArgs
    {
        public int Score { get; }

        public ChangeScoreEvent(int score)
        {
            Score = score;
        }
    }
}