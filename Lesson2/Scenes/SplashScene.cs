using System.Drawing;
using Lesson2.Drawables;
using Lesson2.Loggers;
using Lesson2.States.Scenes.SplashSceneStates;

namespace Lesson2.Scenes
{
    public class SplashScene : Scene
    {
        /// <summary>
        /// Альфа канал для Fade эффекта
        /// </summary>
        private float _alpha;

        private SplashSceneState _state;

        public override void OnShown()
        {
            
        }

        protected override void OnLoad()
        {
            Logger.Print("Splash scene start load");

            _alpha = 255;

            AddDrawable(new TextBox(new Point(230, 200), "Space Game", new Font("Arial", 40), Brushes.White));
            AddDrawable(new TextBox(new Point(570, 540), "Соколовский Дмитрий", new Font("Arial", 14), Brushes.White));

            _state = new SplashSceneStateStart(null);
        }

        protected override void OnDraw(Graphics graphics)
        {
            graphics.FillRectangle(new SolidBrush(Color.FromArgb((int) _alpha, Color.Black)), 0, 0, Drawer.Width,
                Drawer.Height);
        }

        protected override void OnUpdate(float delta)
        {
            _state = _state.Update(delta, ref _alpha);
        }
    }
}