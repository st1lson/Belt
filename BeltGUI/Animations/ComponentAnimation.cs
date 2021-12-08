using System;
using System.Windows.Forms;

namespace BeltGUI.Animations
{
    internal sealed class ComponentAnimation
    {
        public Control Control { get; }
        private const int TicksCount = 200;
        private readonly Timer _timer;

        public ComponentAnimation(Control control)
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

            (double increaseX, double increaseY) = 
                CalculateWay(Control.Location.X, Control.Location.Y, endPointX, endPointY);

            _timer.Start();
        }

        private void TimeTick(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private static (double, double) CalculateWay(int startPointX, int startPointY, int endPointX, int endPointY)
        {
            double increaseX = (double)Math.Abs(startPointX - endPointX) / TicksCount;
            double increaseY = (double)Math.Abs(startPointY - endPointY) / TicksCount;

            return (increaseX, increaseY);
        }
    }
}
