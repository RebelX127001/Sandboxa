using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Sandboxa
{
    public partial class UI : Form
    {
        public UI()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFIle = new OpenFileDialog();

            if (openFIle.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFIle.FileName;
                Console.WriteLine(filePath); // or use any other method to print the file path
            }
        }

        private void exePath_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program program = new Program();
            string pathToUntrusted = exePath.Text;
            
            string untrustedAssembly = exeName.Text;
            if (pathToUntrusted == "" || untrustedAssembly == "")
            {
                Console.WriteLine("Field cannot be empty");
                MessageBox.Show("Field cannot be empty");
            }
            else
            {
                foreach (var item in permList.CheckedItems)
                {
                    program.selectedItems.Add(item.ToString());
                }

                program.UiAppDomain(pathToUntrusted, untrustedAssembly, null, null);
            }
        }


        private void UI_Load(object sender, EventArgs e)
        {

        }

        private void permList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < permList.Items.Count; i++)
            {
                permList.SetItemChecked(i, true);

            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            for (int i = 0; i < permList.Items.Count; i++)
            {
                permList.SetItemChecked(i, false);

            }

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fileName = ofd.FileName;
                Console.WriteLine(fileName);

                // Extract the app name
                string appName = Path.GetFileName(fileName);

                // Extract the file path without the app name
                string filePathWithoutApp = Path.GetDirectoryName(fileName);

                // Extract the app name without the ".exe" extension
                string appNameWithoutExtension = Path.GetFileNameWithoutExtension(appName);


                exePath.Text = filePathWithoutApp;
                exeName.Text = appNameWithoutExtension;
            }

        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
