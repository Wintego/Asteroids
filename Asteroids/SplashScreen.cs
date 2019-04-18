using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Asteroids
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide(); // скрываем splash screen
            Form form = new Form();
            form.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);
            form.Width = 800;
            form.Height = 600;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
            form.MaximumSize = new System.Drawing.Size(form.Width, form.Height);
            form.MinimumSize = new System.Drawing.Size(form.Width, form.Height);
            Game.Init(form, soundCheckBox);
            //Application.Run(form);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            Game.timer.Stop();
            Game.Buffer.Dispose();
            this.Close();
            Game.l.Invoke($"Игра успешно закрыта. Патронов:{Game.Ammo} Жизней: {Game.LifeCount} Счёт: {Game.Scores.Count}");
        }
    }
}
