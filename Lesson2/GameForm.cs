using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Lesson2.Loggers;
using Lesson2.Scenes;
using Timer = System.Windows.Forms.Timer;

namespace Lesson2
{
    public class GameForm : Form
    {
        private Thread _updateThread;
        private Thread _drawThread;

        public GameForm()
        {
            Logger.Init(new ConsoleLogger());
            
            Logger.Print("Start form");
            // Настройка формы
            Width = 800;
            Height = 600;
            
            var size = new Size(Width, Height);
            MinimumSize = size;
            MaximumSize = size;

            MaximizeBox = false;
            Logger.Print("Form created");

            // Запуск потоков на Update + отрисовку
            _updateThread = new Thread(Drawer.Update);
            _drawThread = new Thread(Drawer.Draw);
            
            Shown += Start;
            Closed += End;

            // Инициализация класса отрисовки
            // В целом бесполезен, но так красивее и правильнее
            Drawer.Init(this);

        }

        private void End(object sender, EventArgs e)
        {
            Logger.Print("Form closed");
            _drawThread.Abort();
            _updateThread.Abort();
        }

        private void Start(object sender, EventArgs e)
        {
            Logger.Print("Form shown");
//            Drawer.SetScene(new SplashScene());
            Drawer.SetScene(new SpaceScene());
            _updateThread.Start();
            _drawThread.Start();
        }
    }
}