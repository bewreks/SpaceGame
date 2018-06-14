using System.Drawing;

namespace Lesson2.Drawables.BaseObjects
{
    /// <summary>
    /// Интерфейс объекта коллизии
    /// </summary>
    public interface ICollision
    {
        /// <summary>
        /// Метод проверки на коллизию
        /// </summary>
        /// <param name="obj">Другой объект ICollosion</param>
        /// <returns>Произошла ли коллизия</returns>
        bool Collision(ICollision obj);
        
        /// <summary>
        /// Метод, вызываемый при положительном результате на коллизию
        /// </summary>
        /// <param name="obj"></param>
        void OnCollision(ICollision obj);
        
        /// <summary>
        /// Прямоугольник, проверяемый на коллизию
        /// </summary>
        Rectangle Rect { get; }
    }
}