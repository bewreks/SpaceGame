using Lesson2.Events;
using Lesson2.Loggers;

namespace Lesson2
{
    public class Player
    {
        private int _hp;
        private int _score;

        public int Hp => _hp;
        public int Score => _score;

        public Player()
        {
            _hp = 100;
            _score = 0;
            
            EventManager.AddEventListener(EventManager.Events.ChangeScoreEvent, OnChangeScore);
            EventManager.AddEventListener(EventManager.Events.ChangeEnergyEvent, OnChangeEvent);
        }

        private void OnChangeEvent(IEventArgs args)
        {
            _hp += (args as ChangeScoreEvent)?.Score??0;
            Logger.Print("HP: {0}", Hp);
        }

        private void OnChangeScore(IEventArgs args)
        {
            _score += (args as ChangeScoreEvent)?.Score??0;
            Logger.Print("Score: {0}", Score);
        }
    }
}