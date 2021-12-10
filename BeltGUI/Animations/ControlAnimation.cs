using System;
using System.Drawing;
using System.Windows.Forms;

namespace BeltGUI.Animations
{
    internal sealed class ControlAnimation
    {
        public Control Control { get; internal set; }
        private const int TicksCount = 20;
        private readonly Timer _timer;
        private int _currentPointX;
        private int _currentPointY;
        private int _increaseX;
        private int _increaseY;
        private int _count;

        public ControlAnimation()
        {
            _timer = new Timer()
            {
                Interval = 10
            };
            _timer.Tick += TimeTick;
        }

        public void Animate(int endPointX, int endPointY)
        {
            if (Control is null)
            {
                return;
            }

            _count = 0;
            _currentPointX = Control.Location.X;
            _currentPointY = Control.Location.Y;
            (_increaseX, _increaseY) =
                CalculateWay(Control.Location.X, Control.Location.Y, endPointX, endPointY);

            _timer.Interval = 10;
            _timer.Enabled = true;
        }

        private void TimeTick(object sender, EventArgs e)
        {
            if (_count == TicksCount)
            {
                _timer.Enabled = false;
            }

            _currentPointX += _increaseX;
            _currentPointY += _increaseY;
            Control.Location = new Point(_currentPointX, _currentPointY);
            _count++;
        }

        private static (int, int) CalculateWay(int startPointX, int startPointY, int endPointX, int endPointY)
        {
            int increaseX = (endPointX - startPointX) / TicksCount;
            int increaseY = (endPointY - startPointY) / TicksCount;

            return (increaseX, increaseY);
        }
    }
}
