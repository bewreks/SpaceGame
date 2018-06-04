using System;
using System.Drawing;
using Lesson2.Drawables;
using Lesson2.Scenes;

namespace Lesson2
{
    public class SplashScene : Scene
    {
        private byte alpha;
        private int wait;
        private Action _action;

        public override void Load()
        {
            alpha = 255;
            wait = 0;
            
            _toDraw.Add(new TextBox(new Point(230, 200), "Space Game", new Font("Arial", 40), Brushes.White));
            _toDraw.Add(new TextBox(new Point(570, 540), "Соколовский Дмитрий", new Font("Arial", 14), Brushes.White));

            _action = FadeOut;
        }

        // Отрисуем сцену, а поверх нарисуем еще квадрат с альфой для симуляции эффекта Fade
        public override void Draw(Graphics graphics)
        {
            base.Draw(graphics);
            
            graphics.FillRectangle(new SolidBrush(Color.FromArgb(alpha, Color.Black)), 0, 0, Drawer.Width, Drawer.Height);
        }

        public override void Update()
        {
            // На каждый апдейт вызывается FadeIn, FadeOut или Wait
            // при достижении определенных параметров эти методы сами меняются на нужные
            _action?.Invoke();
        }
        
        private void FadeIn()
        {
            alpha += 5;
            if (alpha >= 255)
            {
                _action = null;
                Drawer.SetScene(new SpaceScene());
            }
        }

        private void FadeOut()
        {
            alpha -= 5;
            if (alpha <= 0)
            {
                _action = Wait;
            }
        }

        private void Wait()
        {
            wait += 1;
            if (wait >= 40)
            {
                _action = FadeIn;
            }
        }
    }
}