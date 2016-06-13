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
    public partial class main_menu : Form
    {

        private ScreenCapture screenCapture;

        public main_menu()
        {
            InitializeComponent();

            if (DB.checkConnection())
            {
                label2.Text = "Успешно";
                label2.ForeColor = Color.Green;

            } else
            {
                label2.Text = "Неудачно";
                label2.ForeColor = Color.Red;
                button1.Enabled = false;
            }

            screenCapture = new ScreenCapture();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            
            Item item = new Item(screenCapture.getScreenShot());
            DialogResult res = item.ShowDialog();
            if (res == DialogResult.OK)
            {
                label3.Text = "Файл успешно сохранен в сервере.Ссылка на файл:";
                label3.ForeColor = Color.Green;
            } else if(res == DialogResult.Abort)
            {
                label3.Text = "Ошибка сохранения";
                label3.ForeColor = Color.Red;
            }

            this.Show();
        }
    }
}
