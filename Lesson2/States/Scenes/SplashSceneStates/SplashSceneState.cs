using Lesson2.Scenes;

namespace Lesson2.States.Scenes.SplashSceneStates
{
    public abstract class SplashSceneState
    {
        protected readonly SplashScene _scene;
        protected float _wait;

        public SplashSceneState(SplashScene scene)
        {
            _wait = 0;
            _scene = scene;
        }

        public abstract void Update(float delta);
    }
}