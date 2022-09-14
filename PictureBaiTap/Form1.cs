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
                fd.ShowDialog();
                string[] filterFile = { "*.png","*.jpg", };
                locFile = filterFile.SelectMany(file => Directory.GetFiles(fd.SelectedPath, file)).ToList();
                if (locFile.Count() == 0)
                {
                    MessageBox.Show("This Folder Don't Have A Picture!!!\nPlease, Pick Folder Another.", "Alert Title", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    HienAnh();
                    DirectoryInfo dir = new DirectoryInfo(fd.SelectedPath + "\\" + "result");
                    if (dir.Exists) { } else { dir.Create(); }
                }

            }
            catch
            {
                MessageBox.Show("Vui lòng chọn đường dẫn tới folder", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private void txtSave_KeyDown(object sender, KeyEventArgs e)
        {
            try
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
            catch
            {
                MessageBox.Show("Vui lòng chọn folder có ảnh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
