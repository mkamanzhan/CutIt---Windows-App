using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Canvas : Form
    {

        Point startPos,currentPos;
        bool drawing;

        public Canvas()
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.BackColor = Color.White;
            this.Opacity = 0.75;
            this.Cursor = Cursors.Cross;

            this.KeyDown += Canvas_KeyDown;
            this.MouseDown += Canvas_MouseDown;
            this.MouseMove += Canvas_MouseMove;
            this.MouseUp += Canvas_MouseUp;
            this.Paint += Canvas_Paint;
        }


        //-----------------------------Keys-Mouse-Events----------------------------------
   
        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
        }

        private void Canvas_MouseDown(object sender,MouseEventArgs e)
        {
            currentPos = startPos = e.Location;
            drawing = true;
        }

        private void Canvas_MouseMove(object sender,MouseEventArgs e)
        {
            currentPos = e.Location;
            if (drawing) this.Invalidate();
        }

        private void Canvas_MouseUp(object sender,MouseEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void Canvas_Paint(object sender,PaintEventArgs e)
        {
            if (drawing) e.Graphics.DrawRectangle(Pens.Red, getRectangle());
        }

        //-------------------------------Rectange------------------------------------

        public Rectangle getRectangle()
        {
            return new Rectangle(
                Math.Min(startPos.X,currentPos.X),
                Math.Min(startPos.Y,currentPos.Y),
                Math.Abs(startPos.X-currentPos.X),
                Math.Abs(startPos.Y-currentPos.Y));
        }
    }
}
