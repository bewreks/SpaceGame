namespace Lesson2.Events
{
    public static class GameEvents
    {
        /// <summary>
        /// Событие окончания игры
        /// </summary>
        public static string GAME_END = "GameEnd";

        /// <summary>
        /// Событие изменения счета
        /// </summary>
        public static string CHANGE_SCORE = "GameChangeScore";
        
        /// <summary>
        /// Событие обновления энергии
        /// </summary>
        public static string CHANGE_ENERGY = "GameChangeEnergy";
        
        /// <summary>
        /// Событие завершения волны
        /// </summary>
        public static string STAGE_COMPLETE = "StageComplete";

        /// <summary>
        /// Событие выстрела
        /// </summary>
        public static string SHOOT = "Shoot";

        /// <summary>
        /// Событие появления нового объекта волны
        /// </summary>
        public static string WAVE_NEXT_OBJECT = "WaveNextObject";

        /// <summary>
        /// Событие успешной генерации новой волны 
        /// </summary>
        public static string STAGE_GENERATED = "StageGenerated";

        /// <summary>
        /// Событие начала генерации новой волны 
        /// </summary>
        public static string STAGE_GENERATE = "StageGenerate";

        /// <summary>
        /// Событие нажатия кнопки вверх
        /// </summary>
        public static string UP = "Up";

        /// <summary>
        /// Событие нажатия кнопки вниз
        /// </summary>
        public static string DOWN = "Down";

        /// <summary>
        /// Событие движения мыши
        /// Требуется MouseMoveGameEvent
        /// </summary>
        public static string MOVE = "Move";
    }
}