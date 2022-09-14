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
using System.Threading;
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
                ImageView.Image.Save(fd.SelectedPath + "\\" + "result\\" + (counter + 1).ToString().PadLeft(6, '0') + "_" + txtSave.Text.Trim() + ".jpg");
            }
            else
            {
                int maxCount = (int)counterResult;
                ImageView.Image.Save(fd.SelectedPath + "\\" + "result\\" + (maxCount + 1).ToString().PadLeft(6, '0') + "_" + txtSave.Text.Trim() + ".jpg");
            }
        }

        private void txtSave_KeyDown(object sender, KeyEventArgs e)
        {

                if (e.KeyCode == Keys.Enter)
                {
                    CheckCount();
                    //ImageView.Image.Save(fd.SelectedPath + "\\" + "result\\" + (counter + 1).ToString().PadLeft(6, '0') + "_" + txtSave.Text.Trim() + ".jpg")
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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            string[] files = Directory.GetFiles(fd.SelectedPath);
            switch (keyData)
            {
                case Keys.Escape:
                    Application.Exit();
                    break;

                case Keys.Delete:
                    try
                    {
                        counter += 1;
                        if (counter == locFile.Count)
                        {

                            return true;
                        }
                        else
                        {
                            ImageView.Image.Dispose();
                            File.Delete(locFile[counter - 1]);
                            HienAnh();
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
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


//    if (keyData == (Keys.Escape))
//    {
//        Application.Exit();
//        return true;
//    }
//    if (keyData == (Keys.PageUp))
//    {
//        counter += 1;
//        if (counter == locFile.Count)
//        {
//            MessageBox.Show("Complete!");
//            this.Close();
//            return true;
//        }
//        else
//        {
//            HienAnh();
//        }
//        return true;
//    }
//    if (keyData == (Keys.PageDown))
//    {
//        try
//        {
//            counter -= 1;
//            if (counter != locFile.Count)
//            {
//                //MessageBox.Show("Complete!");
//                //this.Close();
//                HienAnh();
//                return true;
//            }
//            return true;
//        }
//        catch { MessageBox.Show("Đã tới bức ảnh cuối cùng!!!", "ALERT ALERT ALERT", MessageBoxButtons.OK, MessageBoxIcon.Error); }
//    }
//}
//case Keys.PageUp:
//    counter += 1;
//    if (counter == locFile.Count)
//    {
//        MessageBox.Show("Complete!");
//        this.Close();
//        return true;
//    }
//    else
//    {
//        HienAnh();
//    }
//    break;
//case Keys.PageDown:
//    try
//    {
//        counter -= 1;
//        if (counter != locFile.Count)
//        {
//            //MessageBox.Show("Complete!");
//            //this.Close();
//            HienAnh();
//            return true;
//        }
//        return true;
//    }
//    catch { MessageBox.Show("Đã tới bức ảnh đầu!!!", "ALERT ALERT ALERT", MessageBoxButtons.OK, MessageBoxIcon.Error); }
//    break;

//public void DeleteFile(string path)
//{
//    if (!File.Exists(path))
//    {
//        return;
//    }

//    bool isDeleted = false;
//    while (!isDeleted)
//    {
//        fd.Dispose();
//        try
//        {
//        Image img = Image.FromStream(new MemoryStream(File.ReadAllBytes(path)));
//            ImageView.Image = img;
//            File.Delete(path);
//            isDeleted = true;
//        }
//        catch (Exception ex)
//        {
//            MessageBox.Show(ex.Message);
//        }
//        Thread.Sleep(150);
//    }
//}
//public void UctrlImage(string StrFileName)
//{
//    InitializeComponent();

//    if (StrFileName != null)
//    {
//        Image img = Image.FromStream(new MemoryStream(File.ReadAllBytes(StrFileName)));
//        ImageView.Image = img;
//    }
//    else
//    {
//        ImageView.Image.Dispose();

//    }
//}