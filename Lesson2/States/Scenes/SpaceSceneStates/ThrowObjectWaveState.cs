using Lesson2.Events;
using Lesson2.Loggers;

namespace Lesson2.States.Scenes.SpaceSceneStates
{
    /// <summary>
    /// Класс состояния вывода нового объекта на сцену
    /// </summary>
    public class ThrowObjectWaveState : WaveState
    {
        
        protected override void OnUpdate()
        {
            if (_objects.Count == 0)
            {
                Logger.Print("Волна окончена");
                EventManager.DispatchEvent(GameEvents.STAGE_COMPLETE);

                return;
            }
            
            if (_timer >= 1)
            {
                Logger.Print("Новый объект");
                EventManager<ThrowObjectWaveEventArgs>.DispatchEvent(GameEvents.WAVE_NEXT_OBJECT, new ThrowObjectWaveEventArgs(_objects.Dequeue()));
                _timer = 0;
            }
        }
    }
}