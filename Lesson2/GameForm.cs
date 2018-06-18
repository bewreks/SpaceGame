using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Lesson2.Events;
using Lesson2.Loggers;
using Lesson2.Scenes;

namespace Lesson2
{
    /// <summary>
    /// Главная форма игры
    /// Организовываем первичную настройку формы
    /// Инициируем основные классы
    /// Подписываемся на необходимые события 
    /// </summary>
    public class GameForm : Form
    {
        /// <summary>
        /// Основной поток игры
        /// </summary>
        private readonly Thread _gameThread;

        public GameForm()
        {
            Logger.AddLogger(new ConsoleLogger());
//            Logger.AddLogger(new FileLogger());

            Logger.Print("Start form");

            Width = 800;
            Height = 600;

            var size = new Size(Width, Height);
            MinimumSize = size;
            MaximumSize = size;

            MaximizeBox = false;
            Logger.Print("Form created");

            _gameThread = new Thread(Game) {IsBackground = true};

            Shown += Start;
            Closed += End;
            KeyDown += OnKeyDown;
            MouseMove += OnMouseMove;
            MouseDown += OnMouseClick;
            
            EventManager.AddEventListener(EventManager.Events.GameEndEvent, args =>
            {
                Drawer.SetScene(new SplashScene());
                Logger.Print("Игра окончена со счетом {0} на {1} волне", (args as GameEndEventArgs).Score, (args as GameEndEventArgs).Wave);
            });

            Drawer.Init(this);
        }

        /// <summary>
        /// Метод для потока
        /// </summary>
        private void Game()
        {
            while (true)
            {
                Drawer.Update();
                Drawer.Draw();
            }
        }

        #region EventRegion
        
        /// <summary>
        /// Метод обработки клика
        /// Запускает событие выстрела
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            EventManager.DispatchEvent(EventManager.Events.ShootEvent);
        }

        /// <summary>
        /// Метод обработки движения мыши
        /// Запускает событие движения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            EventManager.DispatchEvent(EventManager.Events.MoveEvent, new MouseMoveGameEvent(e.X, e.Y));
        }

        /// <summary>
        /// Метод обработки нажатий кнопок на клавиатуре
        /// Запускает события движения и выстрела
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    EventManager.DispatchEvent(EventManager.Events.UpEvent);
                    break;
                case Keys.Down:
                    EventManager.DispatchEvent(EventManager.Events.DownEvent);
                    break;
                case Keys.ControlKey:
                    EventManager.DispatchEvent(EventManager.Events.ShootEvent);
                    break;
            }
        }

        /// <summary>
        /// Метод обработки закрытия формы
        /// Останавливает поток игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void End(object sender, EventArgs e)
        {
            Logger.Print("Form closed");
            _gameThread.Suspend();
        }

        /// <summary>
        /// Метод обработки отображения формы
        /// Устанавливает стартовую сцену и запускает основной поток игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start(object sender, EventArgs e)
        {
            Logger.Print("Form shown");
            Drawer.SetScene(new SplashScene());
//            Drawer.SetScene(new SpaceScene());

            _gameThread.Start();
        }
        #endregion

    }
}