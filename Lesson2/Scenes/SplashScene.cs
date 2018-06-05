using System;
using System.Drawing;
using Lesson2.Drawables;
using Lesson2.Scenes;

namespace Lesson2
{
    public class SplashScene : Scene
    {
        private const float FadeInTime = 2;
        private const float FadeOutTime = 2;
        private const float WaitingTime = 5;
        
        private float alpha;
        private float wait;
        private Function _action;

        private delegate void Function(float totalSeconds);

        protected override void OnLoad()
        {
            alpha = 255;
            wait = 0;
            
            AddDrawable(new TextBox(new Point(230, 200), "Space Game", new Font("Arial", 40), Brushes.White));
            AddDrawable(new TextBox(new Point(570, 540), "Соколовский Дмитрий", new Font("Arial", 14), Brushes.White));
 
            _action = FadeOut;
        }

        // Отрисуем сцену, а поверх нарисуем еще квадрат с альфой для симуляции эффекта Fade
        public override void Draw(Graphics graphics)
        {
            base.Draw(graphics);
            
            graphics.FillRectangle(new SolidBrush(Color.FromArgb((int) alpha, Color.Black)), 0, 0, Drawer.Width, Drawer.Height);
        }

        public override void Update(float totalSeconds)
        {
            // На каждый апдейт вызывается FadeIn, FadeOut или Wait
            // при достижении определенных параметров эти методы сами меняются на нужные
            _action?.Invoke(totalSeconds);
        }

        private void FadeIn(float totalSeconds)
        {
            wait += totalSeconds;
            alpha = 255 * (1/FadeInTime) * wait;
            if (wait >= FadeInTime)
            {
                wait = 0;
                _action = null;
                Drawer.SetScene(new SpaceScene());
            }
        }

        private void FadeOut(float totalSeconds)
        {
            wait += totalSeconds;
            alpha = 255 * (1/FadeOutTime) * (FadeOutTime - wait);
            if (wait >= FadeOutTime)
            {
                wait = 0;
                _action = Wait;
            }
        }

        private void Wait(float totalSeconds)
        {
            wait += totalSeconds;
            if (wait >= WaitingTime)
            {
                wait = 0;
                _action = FadeIn;
            }
        }
    }
}