using Lesson2.Loggers;
using Lesson2.Scenes;

namespace Lesson2.States.Scenes.SplashSceneStates
{
    public class SplashSceneStateStart : SplashSceneState
    {
        private const float FadeOutTime = 2;

        public SplashSceneStateStart(SplashScene scene) : base(scene)
        {
        }

        public override void Update(float delta)
        {
            _wait += delta;
            _scene.Alpha = 255 * (1 / FadeOutTime) * (FadeOutTime - _wait);
            if (_wait >= FadeOutTime)
            {
                _scene.State = new SplashSceneStateLoad(_scene);
                _scene.NextScene = new SpaceScene();
                _scene.NextScene.Load();
                Logger.Print("Waiting load main scene");
            }
        }
    }
}