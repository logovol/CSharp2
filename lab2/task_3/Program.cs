using System;
using System.Windows.Forms;

namespace NewGame
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Form form = new Form
                {
                    //Width = Screen.PrimaryScreen.Bounds.Width,
                    //Height = Screen.PrimaryScreen.Bounds.Height
                    Width = 999,
                    Height = 999
                };
                Game.Init(form);
                form.Show();
                Game.Load();
                Game.Draw();
                Application.Run(form);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);                    ;
            }
            

        }
    }
}
