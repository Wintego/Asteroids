using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class Star: BaseObject
    {
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        /// <summary>
        /// определяем внешний вид объекта star
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(new Pen(Color.White, 5), Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(new Pen(Color.White, 5), Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
        }
        /// <summary>
        /// изменяем позицию объекта star
        /// </summary>
        public override void Update()
        {
            Pos.X -= Dir.X/4;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
    }
}