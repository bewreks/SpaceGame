using Lesson2.Loggers;
using Lesson2.Scenes;

namespace Lesson2.States.Scenes.SplashSceneStates
{
    public class SplashSceneStateLoad : SplashSceneState
    {
        private const float WaitingTime = 5;
        
        public SplashSceneStateLoad(SplashScene scene) : base(scene){}
        
        public override void Update(float delta)
        {
            _wait += delta;
            if (_wait >= WaitingTime && _scene.NextScene.Loaded)
            {
                _scene.State = new SplashSceneStateEnd(_scene);
                Logger.Print("Main scene loaded");
            }
        }
    }
}