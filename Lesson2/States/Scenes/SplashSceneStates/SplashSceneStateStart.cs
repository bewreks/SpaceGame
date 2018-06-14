using Lesson2.Loggers;
using Lesson2.Scenes;

namespace Lesson2.States.Scenes.SplashSceneStates
{
    public class SplashSceneStateStart : SplashSceneState
    {
        private const float FadeOutTime = 2;

        public SplashSceneStateStart(Scene scene) : base(scene)
        {
        }

        public override SplashSceneState Update(float delta, ref float alpha)
        {
            SplashSceneState nextState = this;
            _wait += delta;
            alpha = 255 * (1 / FadeOutTime) * (FadeOutTime - _wait);
            if (_wait >= FadeOutTime)
            {
                var scene = new SpaceScene();
                scene.Load();
                nextState = new SplashSceneStateLoad(scene);
                Logger.Print("Waiting load main scene");
            }

            return nextState;
        }
    }
}