using Lesson2.Scenes;

namespace Lesson2.States.Scenes.SplashSceneStates
{
    public abstract class SplashSceneState
    {
        protected float _wait;
        public Scene Scene { get; set; }

        public SplashSceneState()
        {
            _wait = 0;
        }

        public abstract SplashSceneState Update(float delta, ref float alpha);
    }
}