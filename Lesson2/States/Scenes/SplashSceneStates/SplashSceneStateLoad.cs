using Lesson2.Loggers;

namespace Lesson2.States.Scenes.SplashSceneStates
{
    public class SplashSceneStateLoad : SplashSceneState
    {
        private const float WaitingTime = 5;
        
        public override SplashSceneState Update(float delta, ref float alpha)
        {
            SplashSceneState nextState = this;
            _wait += delta;
            if (_wait >= WaitingTime && Scene.IsLoaded)
            {
                nextState = new SplashSceneStateEnd{Scene = Scene};
                Logger.Print("Main scene loaded");
            }

            return nextState;
        }
    }
}