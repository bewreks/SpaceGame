using System;
using System.Drawing;
using Lesson2.Loggers;
using Lesson2.Scenes;

namespace Lesson2.States.Scenes
{
    public class SceneStateLoading : SceneState
    {
        public SceneStateLoading(Scene scene) : base(scene)
        {
        }

        public override void Update(float delta)
        {
            
        }

        public override void Draw(Graphics graphics)
        {
            
        }

        public override void Load()
        {
            var dateTime = DateTime.Now;

            _scene.ToDraw.Clear();
            _scene.ToUpdate.Clear();

            _scene.OnLoad();
            
            _scene.State = new SceneStateLoaded(_scene);

            Logger.Print("Scene loaded with {0:f3} seconds", (DateTime.Now - dateTime).TotalSeconds);
        }
    }
}