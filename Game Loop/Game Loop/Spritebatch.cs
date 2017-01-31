
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Game_Loop
{
    public class Spritebatch
    {
        private BufferedGraphics buffed;
        private Graphics graphics;
        private BufferedGraphicsContext buffContext = BufferedGraphicsManager.Current;

        public Spritebatch(Size  screensize , Graphics gfx)
        {
            buffContext.MaximumBuffer = new Size(screensize.Width + 1, screensize.Height + 1);
            buffed = buffContext.Allocate(gfx, new Rectangle(Point.Empty, screensize));
            graphics = gfx;
            gfx.SmoothingMode = SmoothingMode.None;
        }

        public void DrawRectangle(Rectangle rec , Pen p)
        {
            buffed.Graphics.DrawRectangle(p, rec);
        }

        public void DrawArc(Pen p , Rectangle rec , float startAngle , float sweepAngle )
        {
            buffed.Graphics.DrawArc(p,rec,startAngle,sweepAngle);
        }

        public void DrawLines(Pen p, PointF[] points)
        {
            buffed.Graphics.DrawLines(p, points);
        }

        public void DrawString(string s , Point p)
        {
            buffed.Graphics.DrawString(s,new Font("Arial",14),Brushes.Green, p);
        }
        public void DrawLine ( Pen p , Point start , Point end)
        {
            buffed.Graphics.DrawLine(p, start, end);
        }

        public void FillElipse(Rectangle rec)
        {
            buffed.Graphics.FillEllipse(Brushes.Orange, rec);
        }

        public void DrawImage(Bitmap b , Rectangle rec)
        {
            buffed.Graphics.DrawImageUnscaled(b, rec);
        }

        public void Begin()
        {
            buffed.Graphics.Clear(Color.Black);
        }
        public void End()
        {
            buffed.Render(graphics);
        }


    }
}
