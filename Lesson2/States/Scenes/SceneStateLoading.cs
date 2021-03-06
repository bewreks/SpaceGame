﻿using System;
using System.Drawing;
using Lesson2.Drawables.BaseObjects;
using Lesson2.Loggers;
using Lesson2.Threads;

namespace Lesson2.States.Scenes
{
    /// <summary>
    /// Класс состояния загрузки сцены
    /// </summary>
    public class SceneStateLoading : SceneState
    {
        public override void Update(float delta, ThreadList<IUpdatable> updateList, Action<float> onUpdate)
        {
        }

        public override void Draw(Graphics graphics, ThreadList<IDrawable> drawList, Action<Graphics> onDraw)
        {
        }

        public override SceneState Load(ThreadList<IDrawable> drawList, ThreadList<IUpdatable> updateList,
            Action onLoad)
        {
            var dateTime = DateTime.Now;

            drawList.Clear();
            updateList.Clear();

            onLoad();

            Logger.Print("Scene loaded with {0:f3} seconds", (DateTime.Now - dateTime).TotalSeconds);

            return new SceneStateLoaded();
        }
    }
}