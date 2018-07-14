using Lesson2.Events;
using Lesson2.Loggers;

namespace Lesson2
{
    /// <summary>
    /// Класс игрока. 
    /// </summary>
    public class Player
    {
        private int _hp;
        private int _score;
        private int _wave;

        public int Hp => _hp;
        public int Score => _score;
        public int Wave => _wave;

        public Player()
        {
            _hp = 100;
            _score = 0;
            
            EventManager<ChangeScoreEvent>.AddEventListener(GameEvents.CHANGE_SCORE, OnChangeScore);
            EventManager<ChangeScoreEvent>.AddEventListener(GameEvents.CHANGE_ENERGY, OnChangeEvent);
        }

        private void OnChangeEvent(ChangeScoreEvent args)
        {
            _hp += args.Score;
            Logger.Print("HP: {0}", Hp);
            if (_hp <= 0)
            {
                EventManager<GameEndEventArgs>.DispatchEvent(GameEvents.GAME_END, new GameEndEventArgs(_score, _wave));
            }
        }

        private void OnChangeScore(ChangeScoreEvent args)
        {
            _score += args.Score;
            Logger.Print("Score: {0}", Score);
        }
    }
}