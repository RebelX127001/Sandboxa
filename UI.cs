using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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



            foreach (var item in permList.CheckedItems)
            {
                program.selectedItems.Add(item.ToString());
            }

            program.UiAppDomain(pathToUntrusted, untrustedAssembly, null, null);
            //sandboxa.ExecuteUntrustedCode(untrustedAssembly, null, null);
;            //Console.WriteLine (path + namechat
        }

        private void UI_Load(object sender, EventArgs e)
        {

        }

        private void permList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
