using System.Drawing;
using Lesson2.Drawables;
using Lesson2.Loggers;
using Lesson2.States.Scenes.SplashSceneStates;

namespace Lesson2.Scenes
{
    public class SplashScene : Scene
    {
        private float _alpha;

        private Scene _nextScene;

        private SplashSceneState _state;

        protected override void OnLoad()
        {
            Logger.Print("Splash scene start load");

            _alpha = 255;

            AddDrawable(new TextBox(new Point(230, 200), "Space Game", new Font("Arial", 40), Brushes.White));
            AddDrawable(new TextBox(new Point(570, 540), "Соколовский Дмитрий", new Font("Arial", 14), Brushes.White));

            _state = new SplashSceneStateStart();
        }

        // Отрисуем сцену, а поверх нарисуем еще квадрат с альфой для симуляции эффекта Fade
        public override void Draw(Graphics graphics)
        {
            base.Draw(graphics);

            graphics.FillRectangle(new SolidBrush(Color.FromArgb((int) _alpha, Color.Black)), 0, 0, Drawer.Width,
                Drawer.Height);
        }

        public override void Update(float delta)
        {
            _state = _state.Update(delta, ref _alpha);
        }
    }
}