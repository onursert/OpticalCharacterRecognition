using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpticalCharacterRecognition
{
    public partial class OpticalCharacterRecognition : Form
    {
        public OpticalCharacterRecognition()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("English");
            comboBox1.Items.Add("German");
            comboBox1.Items.Add("Turkish");
            comboBox1.SelectedIndex = 0;
        }

        private void pickConvertButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Image|*.jpg; *.png; *.bmp; *.jpeg; *.PNG;";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName.ToString());
            }

            try
            {
                MODI.Document doc = new MODI.Document();
                doc.Create(openFileDialog1.FileName.ToString());

                if(comboBox1.SelectedIndex == 0)
                {
                    doc.OCR(MODI.MiLANGUAGES.miLANG_ENGLISH, true, true);
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    doc.OCR(MODI.MiLANGUAGES.miLANG_GERMAN, true, true);
                }
                else
                {
                    doc.OCR(MODI.MiLANGUAGES.miLANG_TURKISH, true, true);
                }

                foreach (MODI.Image p in doc.Images)
                {
                    MODI.Layout txt = p.Layout;
                    richTextBox1.Text = txt.Text;
                }
                doc.Close();
            }
            catch (Exception errorMes)
            {
                MessageBox.Show(errorMes.Message.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
