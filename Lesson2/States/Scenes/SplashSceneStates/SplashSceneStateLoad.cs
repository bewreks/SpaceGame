using Lesson2.Loggers;
using Lesson2.Scenes;

namespace Lesson2.States.Scenes.SplashSceneStates
{
    public class SplashSceneStateLoad : SplashSceneState
    {
        private const float WaitingTime = 5;
        
        public SplashSceneStateLoad(Scene scene) : base(scene)
        {
        }

        public override SplashSceneState Update(float delta, ref float alpha)
        {
            SplashSceneState nextState = this;
            _wait += delta;
            if (_wait >= WaitingTime && _scene.IsLoaded)
            {
                nextState = new SplashSceneStateEnd(_scene);
                Logger.Print("Main scene loaded");
            }

            return nextState;
        }

    }
}