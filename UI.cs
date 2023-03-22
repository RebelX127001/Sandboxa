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
using static System.Net.WebRequestMethods;

namespace Sandboxa
{
    public partial class UI : Form
    {
        //private string currentUser = Environment.UserName;
        //private string userRootFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), Environment.UserName);
        private string filePath = System.IO.Path.Combine("C:\\", "Users", Environment.UserName);
        private string cmdDir = System.IO.Path.Combine("C:\\", "Users", Environment.UserName);
        private bool isFile = false; //its false for navigation
        private string currentlySelectedItemName;
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
            this.KeyPreview = true; //enable keypress events to be raised:
            filePathTextBox.Text = filePath;
            loadFileAndDirectories();

        }

        public void loadFileAndDirectories()
        {
            DirectoryInfo fileList;
            string tempFilePath = "";
            FileAttributes fileAttr;
            try
            {
                if (isFile)
                {
                    tempFilePath = filePath + "\\" + currentlySelectedItemName;
                    FileInfo fileDetails = new FileInfo(tempFilePath);
                    fileAttr = System.IO.File.GetAttributes(tempFilePath);
                    FileInfo file = new FileInfo(tempFilePath);
                    string fileExtension = file.Extension.ToLower();
                }
                else
                {
                    fileAttr = System.IO.File.GetAttributes(filePath);


                }
                if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    fileList = new DirectoryInfo(filePath);
                    FileInfo[] files = fileList.GetFiles(); //get all files
                    DirectoryInfo[] dirs = fileList.GetDirectories(); //get all directories
                    string fileExtension = "";
                    listView1.Items.Clear();
                    for (int i = 0; i < files.Length; i++)
                    {
                        fileExtension = files[i].Extension.ToLower();
                        switch (fileExtension)
                        {
                            case ".mp3":
                            case ".mp2":
                                listView1.Items.Add(files[i].Name, 2);
                                break;
                            case ".exe":
                            case ".com":
                                listView1.Items.Add(files[i].Name, 9);
                                break;
                            case ".mp4":
                            case ".avi":
                            case ".mkv":
                                listView1.Items.Add(files[i].Name, 6);
                                break;
                            case ".pdf":
                                listView1.Items.Add(files[i].Name, 3);
                                break;
                            case ".doc":
                            case ".docx":
                                listView1.Items.Add(files[i].Name, 0);
                                break;
                            case ".png":
                            case ".jpg":
                            case ".jpeg":
                                listView1.Items.Add(files[i].Name, 4);
                                break;
                            case ".txt":
                            case ".xml":
                            case ".ini":
                            case ".json":
                                listView1.Items.Add(files[i].Name, 10);
                                break;

                            default:
                                listView1.Items.Add(files[i].Name, 8);
                                break;
                        }

                    }
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        listView1.Items.Add(dirs[i].Name, 1);

                    }
                }
                else
                {
                    fileNameLabel.Text = this.currentlySelectedItemName;
                }
            }
            catch (Exception e)
            {
            }
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
