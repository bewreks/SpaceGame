namespace Lesson2.Events
{
    public class ChangeScoreEvent : IEventArgs
    {
        public int Score { get; }
        
        public ChangeScoreEvent(int score)
        {
            Score = score;
        }
    }
}