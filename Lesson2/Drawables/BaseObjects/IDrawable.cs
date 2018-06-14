using System.Drawing;

namespace Lesson2.Drawables.BaseObjects
{
    /// <summary>
    /// Интерфейс рисуемого объекта
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// Метод для отрисовки объекта
        /// </summary>
        /// <param name="graphics"></param>
        void Draw(Graphics graphics);
    }
}