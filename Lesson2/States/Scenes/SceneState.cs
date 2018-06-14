using System;
using System.Drawing;
using Lesson2.Drawables.BaseObjects;
using Lesson2.Scenes;
using Lesson2.Threads;

namespace Lesson2.States.Scenes
{
    
    /// <summary>
    /// Класс базового состояния сцены
    /// </summary>
    public abstract class SceneState
    {
        /// <summary>
        /// Проверка на полную загрузку сцены
        /// </summary>
        public virtual bool IsLoaded => false;

        /// <summary>
        /// Метод обновления списка
        /// </summary>
        /// <param name="delta">Дельта времени между кадрами</param>
        /// <param name="updateList">Список для обновления</param>
        /// <param name="onUpdate">Пользовательский делегат обновления</param>
        public abstract void Update(float delta, ThreadList<IUpdatable> updateList, Action<float> onUpdate);

        /// <summary>
        /// Метод отрисовки списка
        /// </summary>
        /// <param name="graphics">Объект, куда происходит рисование</param>
        /// <param name="drawList">Список для рисования</param>
        /// <param name="onDraw">Пользовательский делегат отрисовки</param>
        public abstract void Draw(Graphics graphics, ThreadList<IDrawable> drawList, Action<Graphics> onDraw);

        /// <summary>
        /// Метод загрузки сцены
        /// </summary>
        /// <param name="drawList">Список рисования</param>
        /// <param name="updateList">Список обновления</param>
        /// <param name="onLoad">Делегат, вызываемый после окончания загрузки</param>
        /// <returns></returns>
        public abstract SceneState Load(ThreadList<IDrawable> drawList, ThreadList<IUpdatable> updateList,
            Action onLoad);
    }
}