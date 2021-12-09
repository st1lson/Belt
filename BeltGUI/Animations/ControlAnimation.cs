using System;
using System.Drawing;
using System.Windows.Forms;

namespace BeltGUI.Animations
{
    internal sealed class ControlAnimation
    {
        public Control Control { get; }
        private const int TicksCount = 100;
        private readonly Timer _timer;
        private int _increaseX;
        private int _increaseY;
        private int _endPointX;
        private int _endPointY;
        private int _count;

        public ControlAnimation(Control control)
        {
            Control = control;
            _timer = new Timer();
            _timer.Tick += TimeTick;
        }

        public void Animate(int endPointX, int endPointY)
        {
            if (Control is null)
            {
                return;
            }

            _endPointX = endPointX;
            _endPointY = endPointY;
            (_increaseX, _increaseY) = 
                CalculateWay(Control.Location.X, Control.Location.Y, endPointX, endPointY);

            _timer.Interval = 10;
            _timer.Enabled = true;
        }

        private void TimeTick(object sender, EventArgs e)
        {
            if (_count == TicksCount || _endPointX == Control.Location.X && _endPointY == Control.Location.Y)
            {
                _timer.Enabled = false;
            }

            Control.Location = new Point(Control.Location.X + _increaseX, Control.Location.Y + _increaseY);
            _count++;
        }

        private static (int, int) CalculateWay(int startPointX, int startPointY, int endPointX, int endPointY)
        {
            int increaseX = Math.Abs(startPointX - endPointX) / TicksCount;
            int increaseY = Math.Abs(startPointY - endPointY) / TicksCount;

            return (increaseX, increaseY);
        }
    }
}
