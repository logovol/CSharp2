using System;
using System.Windows.Forms;

namespace task_1
{
    class Program
    {
        static void Main(string[] args)
        {

            Form form = new Form
            {
                //Width = Screen.PrimaryScreen.Bounds.Width,
                //Height = Screen.PrimaryScreen.Bounds.Height
                Width = 1000,
                Height = 500
            };
            try
            {
                
                
                Game.Init(form);
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message); ;
            }
            form.Show();
            Game.Load();
            Game.Draw();
            Application.Run(form);
        }
    }
}
