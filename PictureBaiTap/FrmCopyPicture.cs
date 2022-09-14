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

        Image Zoom(Image img, Size size)
        {
            Bitmap bmp = new Bitmap(img, img.Width + (img.Width * size.Width / 100), img.Height + (img.Height * size.Height / 100));
            Graphics g = Graphics.FromImage(bmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bmp;
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CheckCount()
        {
            //câu này là search vào thằng ổ file result sau đó kiếm đếm số lượng file png jpg trong folder result
            FileResult = filterFile.SelectMany(file => Directory.GetFiles(fd.SelectedPath + "\\" + "result", file)).ToList();
            int counterResult = FileResult.Count();
            //so sánh nếu trong ổ đĩa index có giá tri = 0, thì sẽ bắt đầu in từ giá trị đầu tiên + 1
            if (counterResult == 0)
            {
                ImageView.Image.Save(fd.SelectedPath + "\\" + "result\\" + (counter + 1).ToString().PadLeft(6, '0') + "_" + txtSave.Text.Trim() + ".jpg");
            }
            //ngược lại thì sẽ tiếp tục giá trị index cao nhất sau đó + 1 thêm để bỏ vào giá trị index hiện tại.
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
        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{

        //    return base.ProcessCmdKey(ref msg, keyData);
        //}
        private void FrmCopyPicture_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void FrmCopyPicture_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyData)
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

                                return;
                            }
                            else
                            {
                                ImageView.Image.Dispose();
                                File.Delete(locFile[counter - 1]);
                                HienAnh();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
    }
}