using Lesson2.Scenes;

namespace Lesson2.States.Scenes.SplashSceneStates
{
    public class SplashSceneStateEnd : SplashSceneState
    {
        private const float FadeInTime = 2;
        
        public SplashSceneStateEnd(SplashScene scene) : base(scene){}

        public override void Update(float delta)
        {
            _wait += delta;
            _scene.Alpha = 255 * (1 / FadeInTime) * _wait;
            if (_wait >= FadeInTime)
            {
                Drawer.SetLoadedScene(_scene.NextScene);
            }
        }
    }
}