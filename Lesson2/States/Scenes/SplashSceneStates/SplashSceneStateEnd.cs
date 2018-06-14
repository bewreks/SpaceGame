using Lesson2.Scenes;

namespace Lesson2.States.Scenes.SplashSceneStates
{
    public class SplashSceneStateEnd : SplashSceneState
    {
        private const float FadeInTime = 2;

        public SplashSceneStateEnd(Scene scene) : base(scene)
        {
        }

        public override SplashSceneState Update(float delta, ref float alpha)
        {
            SplashSceneState nextState = this;
            _wait += delta;
            alpha = 255 * (1 / FadeInTime) * _wait;
            if (_wait >= FadeInTime)
            {
                Drawer.SetScene(_scene);
            }

            return nextState;
        }

    }
}