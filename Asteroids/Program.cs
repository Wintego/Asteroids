using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids
{
    public delegate void Log(string str);
    class Program
    {
        static void Main(string[] args)
        {
            //Form form = new Form();
            //form.Width = 800;
            //form.Height = 600;
            //form.StartPosition = FormStartPosition.CenterScreen;
            //form.Show();
            //Game.Init(form);
            //Application.Run(form);
            Game.l("Запуск SplashScreen");
            Form splash = new SplashScreen();
            
            Application.Run(splash);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        
    }
}
