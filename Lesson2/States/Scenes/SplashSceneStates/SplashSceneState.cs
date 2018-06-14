using Lesson2.Scenes;

namespace Lesson2.States.Scenes.SplashSceneStates
{
    /// <summary>
    /// Класс базового состояния сцены SplashScreen
    /// </summary>
    public abstract class SplashSceneState
    {
        protected float _wait;

        /// <summary>
        /// Сцена, подготавливаемая под показ
        /// </summary>
        protected readonly Scene _scene;

        protected SplashSceneState(Scene scene)
        {
            _wait = 0;
            _scene = scene;
        }

        /// <summary>
        /// Метод обновления
        /// </summary>
        /// <param name="delta">Дельта времени между кадрами</param>
        /// <param name="alpha">Альфа для затемнения</param>
        /// <returns>Следующее состояние</returns>
        public abstract SplashSceneState Update(float delta, ref float alpha);
    }
}