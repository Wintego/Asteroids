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
    class Ship: BaseObject
    {
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        private static double currentFrame;
        private Image[] vars = GetFramesFromAnimatedGIF(new Bitmap(@"Resources/bullet.gif"));
        /// <summary>
        /// отрисовываем кота в заданных координатах
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(vars[(int)currentFrame], Pos.X, Pos.Y, 90, 40);
            currentFrame = currentFrame < vars.Length-1 ? currentFrame+0.3 : 0;
        }
        /// <summary>
        /// обновляем позицию кота и обрабатываем координаты за пределом формы
        /// </summary>
        public override void Update()
        {
            if (Pos.X > Game.Width) Pos.X = Game.Height/2;
            if (Pos.Y > Game.Height) Pos.Y = 10;
        }
        /// <summary>
        /// получаем кадры из гифки в массив изображений
        /// </summary>
        /// <returns></returns>
        public static Image[] GetFramesFromAnimatedGIF(Image IMG)
        {
            List<Image> IMGs = new List<Image>();
            int Length = IMG.GetFrameCount(FrameDimension.Time);

            for (int i = 0; i < Length; i++)
            {
                IMG.SelectActiveFrame(FrameDimension.Time, i);
                IMGs.Add(new Bitmap(IMG));
            }
            return IMGs.ToArray();
        }

        public void Up()
        {
            if (Pos.Y > 80) Pos.Y = Pos.Y -=10;
        }
        public void Down()
        {
            if (Pos.Y < Game.Height - 50) Pos.Y += 10;
        }

        public void Die()
        {

        }
    }
}
