using Lesson2.Loggers;
using Lesson2.Scenes;

namespace Lesson2.States.Scenes.SplashSceneStates
{
    public class SplashSceneStateStart : SplashSceneState
    {
        private const float FadeOutTime = 2;

        public override SplashSceneState Update(float delta, ref float alpha)
        {
            SplashSceneState nextState = this;
            _wait += delta;
            alpha = 255 * (1 / FadeOutTime) * (FadeOutTime - _wait);
            if (_wait >= FadeOutTime)
            {
                Scene = new SpaceScene();
                Scene.Load();
                nextState = new SplashSceneStateLoad {Scene = Scene};
                Logger.Print("Waiting load main scene");
            }

            return nextState;
        }
    }
}