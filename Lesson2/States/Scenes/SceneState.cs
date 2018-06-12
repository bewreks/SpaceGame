using System;
using System.Drawing;
using Lesson2.Drawables.BaseObjects;
using Lesson2.Scenes;
using Lesson2.Threads;

namespace Lesson2.States.Scenes
{
    public abstract class SceneState
    {
        public virtual bool IsLoaded => false;

        public abstract void Update(float delta, ThreadList<IUpdatable> updateList);
        public abstract void Draw(Graphics graphics, ThreadList<IDrawable> drawList);

        public abstract SceneState Load(ThreadList<IDrawable> drawList, ThreadList<IUpdatable> updateList,
            Action onLoad);
    }
}