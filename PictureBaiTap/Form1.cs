using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureBaiTap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<string> locFile;
        FolderBrowserDialog fd = new FolderBrowserDialog();
        int counter = 0;

        public void HienAnh()
        {
            ImageView.Image = Image.FromFile(locFile[counter]);
            lblCount.Text = Convert.ToInt32(counter + 1) + "/" + locFile.Count.ToString();
        }
        private void btnInputPath_Click(object sender, EventArgs e)
        {
            try
            {
                string[] filterFile = { "*.png","*.jpg", };
                fd.ShowDialog();
                //locFile = Directory.GetFiles(fd.SelectedPath, "*").Where(file => file.ToLower().EndsWith("jpg") || file.ToLower().EndsWith("png")).ToList();
                //locFile = Directory.GetFiles(fd.SelectedPath, "*.jpg").Concat(Directory.GetFiles(fd.SelectedPath, "*.png")).ToList();
                locFile = filterFile.SelectMany(file => Directory.GetFiles(fd.SelectedPath, file)).ToList();
                if (locFile.Count() == 0)
                {
                    MessageBox.Show("This Folder Don't Have A Picture!!!\nPlease, Pick Folder Another.", "Alert Title", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    HienAnh();
                    //Tạo folder Result
                    DirectoryInfo dir = new DirectoryInfo(fd.SelectedPath + "\\" + "result");
                    if (dir.Exists) { } else { dir.Create(); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ImageView.Image.Save(fd.SelectedPath + "\\" + "result\\" + counter.ToString().PadLeft(6, '0') + "_" + txtSave.Text.Trim() + ".jpg");
                counter += 1;
                if (counter == locFile.Count)
                {
                    MessageBox.Show("Complete!");
                    this.Close();
                    return;
                }
                else
                {
                    HienAnh();
                    txtSave.Clear();
                }
            }
        }


    }
}
