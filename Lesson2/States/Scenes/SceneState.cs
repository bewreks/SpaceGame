using System.Drawing;
using Lesson2.Scenes;

namespace Lesson2.States.Scenes
{
    public abstract class SceneState
    {
        protected readonly Scene _scene;
        
        public bool Loaded => false;

        protected SceneState(Scene scene)
        {
            _scene = scene;
        }

        public abstract void Update(float delta);
        public abstract void Draw(Graphics graphics);
        public abstract void Load();
    }
}