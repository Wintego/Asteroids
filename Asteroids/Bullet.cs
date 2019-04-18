using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Asteroids
{
    class Bullet: BaseObject
    {
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        /// <summary>
        /// отрисовываем пулю в заданных координатах
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.FillRectangle(Brushes.Red, Pos.X, Pos.Y, 20, 2);
            Game.Buffer.Graphics.FillRectangle(Brushes.Orange, Pos.X, Pos.Y + 2, 20, 2);
            Game.Buffer.Graphics.FillRectangle(Brushes.Green, Pos.X, Pos.Y + 4, 20, 2);
            Game.Buffer.Graphics.FillRectangle(Brushes.Blue, Pos.X, Pos.Y + 6, 20, 2);
        }
        /// <summary>
        /// обновляем позицию пули и обрабатываем координаты за пределом формы
        /// </summary>
        public override void Update()
        {
            Pos.X += 10;
            if (Pos.X > Game.Width)
            {
                
            }
        }
    }
}
