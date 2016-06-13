using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;


namespace WindowsFormsApplication2
{
    public partial class Item : Form
    {

        private DB db;
        private Bitmap img;

        public Item(Bitmap screenShot)
        {
            InitializeComponent();

            db = new DB();

            img = screenShot;

            pictureBox1.Image = img;

            comboBox1.DataSource = db.getClasses();

            comboBox4.DataSource = new string[] {"1", "2", "3"};
            comboBox5.DataSource = new string[] { "A", "B", "C", "D", "E"};
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                savePDF();
                db.save(
                "name",
                "link",
                comboBox1.SelectedItem.ToString(),
                comboBox2.SelectedItem.ToString(),
                comboBox3.SelectedItem.ToString(),
                comboBox4.SelectedItem.ToString(),
                comboBox5.SelectedItem.ToString());

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            } catch
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Abort;
                
            }
            
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.DataSource = db.getSubjects(comboBox1.SelectedItem.ToString());
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.DataSource = db.getTopics(comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString());
        }

        private void savePDF()
        {

            Document doc = new Document(new iTextSharp.text.Rectangle(img.Width,img.Height),0f,0f,0f,0f);

            PdfWriter.GetInstance(doc, new FileStream("1.pdf", FileMode.Create));

            doc.Open();
            iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Bmp);
            doc.Add(pdfImage);
            doc.Close();
        }
        

    }
}
