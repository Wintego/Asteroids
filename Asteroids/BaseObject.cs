using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Asteroids
{
    abstract class BaseObject: ICollision
    {
        protected Point Pos, Dir;
        protected Size Size;
        protected BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }
        public abstract void Draw();
        public virtual void Update()
        {
            Pos.X += Dir.X/2;
            Pos.Y += Dir.Y/2;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }

        public bool Collision(ICollision obj)
        {
            return obj.Rect.IntersectsWith(this.Rect);
        }

        public Rectangle Rect => new Rectangle(Pos,Size);
    }
}
