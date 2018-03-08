using System;
using System.IO;
using System.Windows.Forms;

namespace asterTake2
{
    public partial class EnterScoreWindow : Form
    {
        public EnterScoreWindow(int points)
        {
            InitializeComponent();
            label3.Text = points.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var trimmedName = textBox1.Text.Trim();
            if (trimmedName == "")
            {
                Console.WriteLine("comon, give me some name!");                
            }
            else
            {
                File.WriteAllLines("scores.txt", new []{$"{trimmedName}:{label3.Text}"});
                Close();
            }
        }
    }
}
