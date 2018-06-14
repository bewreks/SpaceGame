namespace Lesson2.Drawables.BaseObjects
{
    /// <summary>
    /// Интерфейс убиваемого объекта
    /// </summary>
    public interface IKillable
    {
        /// <summary>
        /// Свойство для проверки жизни
        /// </summary>
        bool IsDead { get; }
    }
}