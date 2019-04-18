using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Asteroids
{
    class Health: BaseObject
    {
        public Health(Point pos, Point dir, Size size) : base(pos, dir, size) { }
        //static Image image;
        
        public static Image Image { get; set; }
        public Image toScore;
        /// <summary>
        /// определяем внешний вид объекта Health
        /// </summary>
        public override void Draw()
        {
            if (Image!= null) Game.Buffer.Graphics.DrawImage(Image, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        /// <summary>
        /// получаем иконку для Health
        /// </summary>
        /// <returns></returns>
        public static Image GetIcon()
        {
            if (Image != null)
            {
                return null;
            }
            else
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(@"Resources/food/");
                FileInfo[] files = directoryInfo.GetFiles(@"food*.png");
                return Image.FromFile(@"Resources/food/" + files[new Random().Next(0, files.Length)].ToString());
            }
        }

        /// <summary>
        /// обноновляем позицию, если Health улетел, отображаем новый
        /// </summary>
        public override void Update()
        {
            Random r = new Random();

            if (Pos.X < -Size.Width)
            {
                Pos.X = Game.Width;
                Pos.Y = r.Next(100, Game.Height - Size.Width);
                Image = GetIcon();
                toScore = Image;
            }                
            Pos.X -= 2;
        }
    }
}
