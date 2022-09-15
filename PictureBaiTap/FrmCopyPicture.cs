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
            //Show ảnh ra
            ImageView.Image = Image.FromFile(locFile[counter]);
            //show giá trị index trong list / tổng giá trị index đếm được trong folder
            lblCount.Text = Convert.ToInt32(counter + 1) + "/" + locFile.Count.ToString();
            txtSave.Clear();
        }
        private void btnInputPath_Click(object sender, EventArgs e)
        {
            try
            {
                fd.ShowDialog();
                //lọc chọn ra file ảnh từ folder đc chọn
                //locFile = filterFile.SelectMany(file => Directory.GetFiles(fd.SelectedPath, file)).ToList();
                locFile = Directory.GetFiles(fd.SelectedPath,"*.jpg").ToList();
                if (locFile.Count() == 0)
                {
                    MessageBox.Show("This Folder Don't Have A Picture!!!\nPlease, Pick Folder Another.", "Alert Title", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    HienAnh();
                    //sau khi hiện ảnh focus thằng ảnh vị trí txtSave
                    txtSave.Focus();
                    DirectoryInfo dir = new DirectoryInfo(fd.SelectedPath + "\\" + "result");
                    //kiểm tra có folder result trong folder dược chọn không, không có tạo, có rồi thì thôi 
                    if (dir.Exists) { } else { dir.Create(); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void HamInGiaTriIndex()
        {
            //câu này là search vào thằng ổ file result sau đó kiếm đếm số lượng file png jpg trong folder result
            FileResult = filterFile.SelectMany(file => Directory.GetFiles(fd.SelectedPath + "\\" + "result", file)).ToList();
            int counterResult = FileResult.Count();
            //so sánh nếu trong ổ đĩa index có giá tri = 0, thì sẽ bắt đầu in từ giá trị đầu tiên + 1
            if (counterResult == 0)
            {
                ImageView.Image.Save(fd.SelectedPath + "\\" + "result\\" + (counter + 1).ToString().PadLeft(6, '0') + "_" + txtSave.Text.Trim() + ".jpg");
            }
            //ngược lại thì sẽ tiếp tục giá trị index cao nhất sau đó + 1 thêm để bỏ vào giá trị index hiện tại. In ra giá trị index tiếp theo không bị nhảy về lại giá trị 0 hoặc 1 trong cùng 1 folder
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
                HamInGiaTriIndex();
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
        private void FrmCopyPicture_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel) != DialogResult.OK)
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
                                //Ở đây counter ở trên đã + 1 rồi nên khi ở hàm delete phải - 1 để tính lại cái ảnh trước khi đã chuyển đi
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