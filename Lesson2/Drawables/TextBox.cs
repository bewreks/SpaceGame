using System.Drawing;
using Lesson2.Drawables.BaseObjects;

namespace Lesson2.Drawables
{
    public class TextBox : GameObjects
    {
        private string _text;
        private Font _font;
        private Brush _brush;

        public TextBox(Point position, string text, Font font, Brush brush) : base(position, new Point(), new Size())
        {
            _text = text;
            _font = font;
            _brush = brush;
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawString(_text, _font, _brush, _position);
        }

        public override void Update(float totalSeconds)
        {
            
        }
    }
}