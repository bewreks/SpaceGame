using System.Collections.Generic;
using System.Drawing;
using Lesson2.Drawables;
using Lesson2.Drawables.BaseObjects;

namespace Lesson2.Scenes
{
    public abstract class Scene
    {
        // Отдельные списки для рисовки и обновления
        // Ведь нет смысла обновлять то, что не должно обновляться
        protected List<IDrawable> _toDraw;
        protected List<IUpdatable> _toUpdate;

        protected Scene()
        {
            _toDraw = new List<IDrawable>();
            _toUpdate = new List<IUpdatable>();
        }

        // Перебераем весь список, но оставляем пользователю возможность
        // переопределить метод и обновлять данные как ему надо
        public virtual void Update()
        {
            foreach (var updatable in _toUpdate)
            {
                updatable.Update();
            }
        }

        // Перебераем весь список, но оставляем пользователю возможность
        // переопределить метод и рисовать как ему надо
        public virtual void Draw(Graphics graphics)
        {
            foreach (var drawable in _toDraw)
            {
                drawable.Draw(graphics);
            }
        }

        // Метод создания объектов сцены
        public abstract void Load();
    }
}