using System;
using Lesson2.Drawables;
using Lesson2.Drawables.BaseObjects;
using Lesson2.Events;
using Lesson2.Loggers;

namespace Lesson2.States.Scenes.SpaceSceneStates
{
    /// <summary>
    /// Класс состояния создания новой волны
    /// </summary>
    public class GenerateWaveState : WaveState
    {

        private const int BaseObjectsCount = 10;
        private int _wave = 0;
        
        protected override void OnUpdate()
        {
            var random = new Random();
            for (int i = 0; i < BaseObjectsCount + _wave; i++)
            {
                GameObjects obj;
                var next = random.Next(100);
                if (next % 10 == 0)
                {
                    obj = GameObjectsFactory.CreateMedic();
                }
                else
                {
                    obj = GameObjectsFactory.CreateAsteroid();
                }

                _objects.Enqueue(obj);
            }
            _wave++;
            Logger.Print("Волна создана {0}", _wave);
            EventManager.DispatchEvent(GameEvents.STAGE_GENERATED);
        }
    }
}