using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    class ScreenCapture
    {

        private Rectangle canvasBounds = Screen.GetBounds(Point.Empty); 

        public ScreenCapture()
        {
            
        }

        public void setCanvas()
        {
            Canvas canvas = new Canvas();

            if(canvas.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.canvasBounds = canvas.getRectangle();
            }
        }

        public Bitmap getScreenShot()
        {

            setCanvas();

            Image image = new Bitmap(canvasBounds.Width,canvasBounds.Height);

            Graphics graphics = Graphics.FromImage(image as Image);

            graphics.CopyFromScreen(new Point(canvasBounds.Left, canvasBounds.Top), Point.Empty, canvasBounds.Size);

            return image as Bitmap;
        }
    }



    
}
