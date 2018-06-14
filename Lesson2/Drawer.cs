using System;
using System.Drawing;
using System.Windows.Forms;
using Lesson2.Loggers;
using Lesson2.Scenes;

namespace Lesson2
{
    
    /// <summary>
    /// Класс отрисовки
    /// </summary>
    public static class Drawer
    {
        /// <summary>
        /// Текущая сцена
        /// </summary>
        private static Scene _scene;

        private static BufferedGraphicsContext _context;
        private static BufferedGraphics _buffer;

        private static int _width;
        private static int _height;

        private static DateTime _dateTime;

        public static int Width
        {
            get => _width;
            set
            {
                if (value > 1000 || value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _width = value;
            }
        }

        public static int Height
        {
            get => _height;
            set
            {
                if (value > 1000 || value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _height = value;
            }
        }

        /// <summary>
        /// Метод установки сцены
        /// Запускает метод загрузки сцены Load
        /// Запускает метод сцены OnShown
        /// </summary>
        /// <param name="scene">Объект сцены</param>
        public static void SetScene(Scene scene)
        {
            scene.Load();
            scene.OnShown();
            _scene = scene;
        }

        /// <summary>
        /// Метод инициализации
        /// Создает контекст и буфер для рисования
        /// </summary>
        /// <param name="form">Форма, на которой будет происходить отрисовка</param>
        public static void Init(Form form)
        {
            var g = form.CreateGraphics();
            _context = BufferedGraphicsManager.Current;
            Width = form.Width;
            Height = form.Height;
            _buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            _dateTime = DateTime.Now;
        }

        /// <summary>
        /// Метод отрисовки кадра
        /// Очищаем предыдущий кадр, отрисовываем сцену, если она есть
        /// </summary>
        public static void Draw()
        {
            try
            {
                //Нужно блокировать Graphics для все объектов
                _buffer.Graphics.Clear(Color.Black);

                _scene?.Draw(_buffer.Graphics);

                _buffer.Render();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
            }
        }

        /// <summary>
        /// Метод обновления
        /// Вызывается перед отрисовкой
        /// Используется для обновления объектов
        /// </summary>
        public static void Update()
        {
            try
            {
                {
                    var dateTime = DateTime.Now;
                    _scene?.Update((float) (dateTime - _dateTime).TotalSeconds);
                    _dateTime = dateTime;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
            }
        }
    }
}