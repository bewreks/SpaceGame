namespace Lesson2.Drawables.BaseObjects
{
    /// <summary>
    /// Интерфейс обновляемого объекта
    /// </summary>
    public interface IUpdatable
    {
        /// <summary>
        /// Метод обновления
        /// </summary>
        /// <param name="deltaTime">Дельта времени между кадрами</param>
        void Update(float deltaTime);
    }
}