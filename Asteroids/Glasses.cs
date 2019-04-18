using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Asteroids
{
    class Glasses : BaseObject
    {
        public Image glasses = Image.FromFile(@"Resources/glasses.png");
        public Glasses(Point pos, Point dir) : base(pos, dir, new Size(100,26)) { }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(glasses, Pos.X, Pos.Y, 100, 26);
        }

        /// <summary>
        /// провека для фраз догика
        /// </summary>
        private bool lastcheck = false;
        private string[] dogikSay = {"WOW!", "WOW", "such game", "not bad", "very good", "amaze", "such skill", "so pro", "good game", "nice"};
        public override void Update()
        {
            Pos.X = Game.Width / 2-50;
            if (Pos.Y < Game.Height / 2 - 22)
            {
                Pos.Y += 2;
            }
            else
            {
                Font dogikFont = new Font("Comic Sans MS", 25);
                Brush dogColorReply = Brushes.Fuchsia;
                Random r = new Random();
                Game.Buffer.Graphics.DrawString("doge with it", dogikFont, dogColorReply, 250, 190);
                if (lastcheck)
                {
                    Thread.Sleep(4000);
                    dogColorReply = Brushes.White;
                    Game.Buffer.Graphics.DrawString(dogikSay[r.Next(0,dogikSay.Length)], dogikFont, dogColorReply, r.Next(100, Game.Width - 150), r.Next(100, Game.Height - Game.Height / 3));
                    Game.Buffer.Graphics.DrawString(dogikSay[r.Next(0, dogikSay.Length)], dogikFont, dogColorReply, r.Next(100, Game.Width - 150), r.Next(100, Game.Height - Game.Height / 3));
                }
                lastcheck = true;
            }
        }
    }
}
