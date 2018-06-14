using System;
using System.Drawing;
using Lesson2.Drawables.BaseObjects;
using Lesson2.Loggers;
using Lesson2.Scenes;
using Lesson2.Threads;

namespace Lesson2.States.Scenes
{
    /// <summary>
    /// Класс состояния полной загрузки сцены
    /// </summary>
    public class SceneStateLoaded : SceneState
    {
        public override bool IsLoaded => true;

        public override void Update(float delta, ThreadList<IUpdatable> updateList, Action<float> onUpdate)
        {
            updateList.RemoveAll(DeleteIfDead);
            try
            {
                updateList.ForEach(updatable => updatable.Update(delta));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
            }

            onUpdate(delta);
        }

        public override void Draw(Graphics graphics, ThreadList<IDrawable> drawList, Action<Graphics> onDraw)
        {
            drawList.RemoveAll(DeleteIfDead);
            try
            {
                drawList.ForEach(drawable => drawable.Draw(graphics));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
            }

            onDraw(graphics);
        }

        public override SceneState Load(ThreadList<IDrawable> drawList, ThreadList<IUpdatable> updateList,
            Action onLoad)
        {
            return this;
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