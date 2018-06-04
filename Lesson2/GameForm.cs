using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lesson2
{
    public class GameForm : Form
    {
        private static Timer _timer;
        
        public GameForm()
        {
            // Настройка формы
            Width = 800;
            Height = 600;
            
            var size = new Size(800, 600);
            MinimumSize = size;
            MaximumSize = size;

            MaximizeBox = false;

            // Запуск таймера на Update + отрисовку
            // Примерно с фреймрейтом 60 в секунду
            Shown += Start;

            // Инициализация класса отрисовки
            // В целом бесполезен, но так красивее и правильнее
            Drawer.Init(this);
            Drawer.SetScene(new SplashScene());
            //Drawer.SetScene(new SpaceScene());
        }

        private void Start(object sender, EventArgs e)
        {
            _timer = new Timer();
            _timer.Tick += Tick;
            _timer.Interval = 1000 / 60;
            _timer.Start();
        }

        private void Tick(object sender, EventArgs e)
        {
            Drawer.Scene.Update();//В будущем можно будет убрать зависимость скорости от фпс
            Drawer.Draw();
        }
    }
}