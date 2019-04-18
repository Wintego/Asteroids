using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class Asteroid: BaseObject, IDisposable
    {
        public int Power { get; set; }
        public Asteroid(Point pos, Point dir, Size size): base(pos,dir,size)
        {
            Power = 1;
        }
        Image asteroid = Image.FromFile(@"Resources/asteroid5.png");
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(asteroid, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public void Dispose()
        {
            asteroid.Dispose();
        }
    }
}
