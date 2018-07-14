using Lesson2.Events;
using Lesson2.Loggers;

namespace Lesson2.States.Scenes.SpaceSceneStates
{
    /// <summary>
    /// Класс состояния ожидания создания новой волны
    /// </summary>
    public class WaitForNewWaveState : WaveState
    {
        protected override void OnUpdate()
        {
            
            if (_timer >= 5)
            {
                Logger.Print("Волна создается");
                EventManager.DispatchEvent(GameEvents.STAGE_GENERATE);
                _timer = 0;
            }
        }
    }
}