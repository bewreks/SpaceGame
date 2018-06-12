using System;
using System.Drawing;
using Lesson2.Drawables.BaseObjects;
using Lesson2.Loggers;
using Lesson2.Scenes;

namespace Lesson2.States.Scenes
{
    public class SceneStateLoaded : SceneState
    {
        public SceneStateLoaded(Scene scene) : base(scene)
        {
            
        }

        public override void Update(float delta)
        {
            
            _scene.ToUpdate.RemoveAll(DeleteIfDead);
            try
            {
                _scene.ToUpdate.ForEach(updatable => updatable.Update(delta));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
            }
        }

        public override void Draw(Graphics graphics)
        {
            _scene.ToDraw.RemoveAll(DeleteIfDead);
            try
            {
                _scene.ToDraw.ForEach(drawable => drawable.Draw(graphics));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
            }
        }

        public override void Load()
        {
            
        }

        private bool DeleteIfDead(IDrawable drawable)
        {
            return drawable is IKillable && (drawable as IKillable).IsDead;
        }

        private bool DeleteIfDead(IUpdatable updatable)
        {
            return updatable is IKillable && (updatable as IKillable).IsDead;
        }
    }
}