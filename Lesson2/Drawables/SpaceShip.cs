using System.Drawing;
using System.Drawing.Drawing2D;
using Lesson2.Drawables.BaseObjects;
using Lesson2.Events;

namespace Lesson2.Drawables
{
    /// <summary>
    /// Класс космического корабля пользователя
    /// </summary>
    public class SpaceShip : CollisionKillableGameObjects
    {
        private readonly GraphicsPath _graphicsPath;

        private MouseMoveGameEvent _prEventArgs;
        
        public override Rectangle Rect => new Rectangle(new Point((int) _position.X, (int) (_position.Y - _size.Height/2.0f)), _size);

        public SpaceShip(Point position, Point dir, Size size) : base(position, dir, size)
        {
            var scaleX = _size.Width / 6.0f;
            var scaleY = _size.Height / 4.0f;

            _graphicsPath = new GraphicsPath();
            _graphicsPath.AddLine(0, 2, 6, 0);
            _graphicsPath.AddLine(6, 0, 0, -2);

            var matrix = new Matrix();
            matrix.Translate(_position.X, _position.Y);
            matrix.Scale(scaleX, scaleY);
            _graphicsPath.Transform(matrix);

            // Оставлю возможность управления с клавиатуры
            EventManager.AddEventListener(GameEvents.UP, Up);
            EventManager.AddEventListener(GameEvents.DOWN, Down);
            EventManager<MouseMoveGameEvent>.AddEventListener(GameEvents.MOVE, Move);
        }

        /// <summary>
        /// Обработчик события движения мышью
        /// </summary>
        /// <param name="args"></param>
        private void Move(MouseMoveGameEvent args)
        {
            lock (_graphicsPath)
            {
                float i = args.Y - (_prEventArgs?.Y ?? 0);
                _prEventArgs = args;

                _position.Y = _position.Y + i;
                var matrix = new Matrix();
                matrix.Translate(_dir.X, i);

                _graphicsPath.Transform(matrix);
            }
        }

        /// <summary>
        /// Обработчик события нажатия кнопки вниз
        /// </summary>
        /// <param name="args"></param>
        private void Down(GameEventArgs args)
        {
            lock (_graphicsPath)
            {
                if (_position.Y >= Drawer.Height - _size.Height)
                {
                    return;
                }

                _position.Y = _position.Y + _dir.Y;
                var matrix = new Matrix();
                matrix.Translate(_dir.X, _dir.Y);

                _graphicsPath.Transform(matrix);
            }
        }

        /// <summary>
        /// Обработчик события нажатия кнопки вверх
        /// </summary>
        /// <param name="args"></param>
        private void Up(GameEventArgs args)
        {
            lock (_graphicsPath)
            {
                if (_position.Y <= 0)
                {
                    return;
                }

                _position.Y = _position.Y - _dir.Y;
                var matrix = new Matrix();
                matrix.Translate(_dir.X, -_dir.Y);

                _graphicsPath.Transform(matrix);
            }
        }

        public override void Draw(Graphics graphics)
        {
            lock (_graphicsPath)
            {
                graphics.FillPath(Brushes.Silver, _graphicsPath);
            }
        }

        public override void Update(float deltaTime)
        {
        }


        public override void OnCollision(ICollision obj)
        {
            switch (obj)
            {
                case Asteroid _:
                    EventManager<ChangeScoreEvent>.DispatchEvent(GameEvents.CHANGE_SCORE,
                        new ChangeScoreEvent(-((Asteroid) obj).Score));
                    EventManager<ChangeScoreEvent>.DispatchEvent(GameEvents.CHANGE_ENERGY,
                        new ChangeScoreEvent(-((Asteroid) obj).Energy));
                    break;

                case Medic _:
                    EventManager<ChangeScoreEvent>.DispatchEvent(GameEvents.CHANGE_SCORE,
                        new ChangeScoreEvent(((Medic) obj).Score));
                    EventManager<ChangeScoreEvent>.DispatchEvent(GameEvents.CHANGE_ENERGY,
                        new ChangeScoreEvent(((Medic) obj).Energy));
                    break;
            }
        }

        /// <summary>
        /// Расположение пушки на корабле
        /// </summary>
        /// <returns></returns>
        public PointF GetPoint()
        {
            return _graphicsPath.PathPoints[1];
        }
    }
}