using System.Drawing;
using System.Windows.Forms;

namespace asterTake2
{
    class Canvas : Panel
    {
        public Canvas()
        {
            DoubleBuffered = true;
            BackColor = Color.Black;
            Dock = DockStyle.Fill;
        }
    }
}