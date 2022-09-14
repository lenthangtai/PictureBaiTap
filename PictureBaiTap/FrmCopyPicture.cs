using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureBaiTap
{
    public partial class FrmCopyPicture : Form
    {
        public FrmCopyPicture()
        {
            InitializeComponent();
        }
        List<string> locFile;
        List<string> FileResult;
        FolderBrowserDialog fd = new FolderBrowserDialog();
        string[] filterFile = { "*.png", "*.jpg", "*.gif" };
        int counter = 0;

        public void HienAnh()
        {
            ImageView.Image = Image.FromFile(locFile[counter]);
            lblCount.Text = Convert.ToInt32(counter + 1) + "/" + locFile.Count.ToString();
            txtSave.Clear();
        }
        public static string DirectoryName = "Main Directory";
        private void btnInputPath_Click(object sender, EventArgs e)
        {
            try
            {
                fd.ShowDialog();
                locFile = filterFile.SelectMany(file => Directory.GetFiles(fd.SelectedPath, file)).ToList();
                if (locFile.Count() == 0)
                {
                    MessageBox.Show("This Folder Don't Have A Picture!!!\nPlease, Pick Folder Another.", "Alert Title", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    HienAnh();
                    txtSave.Focus();
                    DirectoryInfo dir = new DirectoryInfo(fd.SelectedPath + "\\" + "result");
                    if (dir.Exists) { } else { dir.Create(); }
                }
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn đường dẫn tới folder", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void CheckCount()
        {
            FileResult = filterFile.SelectMany(file => Directory.GetFiles(fd.SelectedPath + "\\" + "result", file)).ToList();
            int counterResult = FileResult.Count();
            if (counterResult == 0)
            {
                ImageView.Image.Save(fd.SelectedPath + "\\" + "result\\" + (counter+1).ToString().PadLeft(6, '0') + "_" + txtSave.Text.Trim() + ".jpg");
            }
            else
            {
                int maxCount = (int)counterResult;
                ImageView.Image.Save(fd.SelectedPath + "\\" + "result\\" + (maxCount+1).ToString().PadLeft(6, '0') + "_" + txtSave.Text.Trim() + ".jpg");
            }
        }
        private void txtSave_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    CheckCount();
                    //ImageView.Image.Save(fd.SelectedPath + "\\" + "result\\" + (counter + 1).ToString().PadLeft(6, '0') + "_" + txtSave.Text.Trim() + ".jpg");
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
                    }
                }
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn folder có ảnh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Escape))
            {
                Application.Exit();
                return true;
            }
            if (keyData == (Keys.Delete))
            {
                counter += 1;
                if (counter == locFile.Count)
                {
                    MessageBox.Show("Complete!");
                    this.Close();
                    return true;
                }
                else
                {
                    HienAnh();
                }
                return true;
            }
            if (keyData == (Keys.PageDown))
            {
                try
                {
                    counter -= 1;
                    if (counter != locFile.Count)
                    {
                        //MessageBox.Show("Complete!");
                        //this.Close();
                        HienAnh();
                        return true;
                    }
                    return true;
                }
                catch { MessageBox.Show("Đã tới bức ảnh cuối cùng!!!", "ALERT ALERT ALERT", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void FrmCopyPicture_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}
