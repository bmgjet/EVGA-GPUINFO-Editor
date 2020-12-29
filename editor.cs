using System;
using System.IO;
using System.Windows.Forms;

namespace EVGA_GpuInfo
{
    public partial class editor : Form
    {

        public static string currentDirectory = Directory.GetCurrentDirectory();
        public string _pathBin = currentDirectory + "\\GpuInfo.bin";

        public editor()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //closes program if doesnt decode file.
            if (!this.encryptFile(this._pathBin))
            {
                MessageBox.Show("Run exe as admin from same location as GpuUnfo.bin (EVGA Folder)");
                this.Close();
            }
        }

        //decode file
        private bool encryptFile(string sourceFile)
        {
            if (!File.Exists(sourceFile))
            {
                return false;
            }
            try
            {
                byte[] bytes = File.ReadAllBytes(sourceFile);
                Utility.Encrypt(ref bytes, "GpuInfo");
                textBox1.Text = Utility.widebytearray2string(bytes);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        //re-encode file and save.
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytes2 = Utility.narrowstring2widearray(textBox1.Text);
                Utility.Encrypt(ref bytes2, "GpuInfo");
                File.WriteAllBytes(_pathBin, bytes2);
                MessageBox.Show("File Saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
