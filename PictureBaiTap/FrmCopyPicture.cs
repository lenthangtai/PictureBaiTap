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
            FileResult = Directory.GetFiles(fd.SelectedPath + "\\" + "result", "*.jpg", SearchOption.TopDirectoryOnly).ToList();
            int counterResult = FileResult.Count();
            if (counterResult != 0)
            {
                int countern = FileResult.Count();
                ImageView.Image = Image.FromFile(locFile[countern]);
                //show giá trị index trong list / tổng giá trị index đếm được trong folder
                lblCount.Text = Convert.ToInt32(counterResult + 1) + "/" + locFile.Count.ToString();
                txtSave.Clear();
            }
            else if (counterResult == 0)
            {
                ImageView.Image = Image.FromFile(locFile[counter]);
                //show giá trị index trong list / tổng giá trị index đếm được trong folder
                lblCount.Text = Convert.ToInt32(counter + 1) + "/" + locFile.Count.ToString();
                txtSave.Clear();
            }
        }
        private void btnInputPath_Click(object sender, EventArgs e)
        {
            try
            {
                fd.ShowDialog();
                //lọc chọn ra file ảnh từ folder đc chọn
                locFile = Directory.GetFiles(fd.SelectedPath, "*.jpg", SearchOption.TopDirectoryOnly).ToList();
                if (locFile.Count() == 0)
                {
                    MessageBox.Show("This Folder Don't Have A Picture!!!\nPlease, Pick Folder Another.", "Alert Title", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //
                    //sau khi hiện ảnh focus thằng ảnh vị trí txtSave
                    txtSave.Focus();
                    DirectoryInfo dir = new DirectoryInfo(fd.SelectedPath + "\\" + "result");
                    //kiểm tra có folder result trong folder dược chọn không, không có tạo, có rồi thì thôi 
                    if (!dir.Exists)
                    {
                        dir.Create();
                    }
                    else
                    {
                        HienAnh();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSave_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //khi focus ở ô txtsave và ấn enter, sẽ save bức ảnh được show ra ở trong list được lưu, in ra filename = "index_txtSave.Text".
                if (e.KeyCode == Keys.Enter)
                {
                    int counterResult = FileResult.Count();
                    if (counterResult == 0)
                    {
                        ImageView.Image.Save(fd.SelectedPath + "\\" + "result\\" + (counter + 1).ToString().PadLeft(6, '0') + "_" + txtSave.Text.Trim() + ".jpg");
                    }
                    else
                    {
                        ImageView.Image.Save(fd.SelectedPath + "\\" + "result\\" + (counterResult + 1).ToString().PadLeft(6, '0') + "_" + txtSave.Text.Trim() + ".jpg");
                    }
                    //ImageView.Image.Save(fd.SelectedPath + "\\" + "result\\" + (counter + 1).ToString().PadLeft(6, '0') + "_" + txtSave.Text.Trim() + ".jpg");
                    counter += 1; counterResult += 1;
                    //nếu số lương file chạy đã chạy bằng tổng giá trị file đếm được thì hiển thị mesagebox.show ghi hoàn thành và đóng lại form
                    if (counter == locFile.Count || counterResult == locFile.Count)
                    {
                        MessageBox.Show("Complete!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        return;
                    }
                    else
                    {
                        HienAnh();
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void FrmCopyPicture_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyData)
                {
                    case Keys.F1:
                        FrmThongTin frm = new FrmThongTin();
                        this.Hide();
                        frm.ShowDialog();
                        this.Show();
                        break;

                    case Keys.Escape:
                        DialogResult dg = MessageBox.Show("Bạn muốn thoát hệ thống.!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if(dg == DialogResult.OK)
                        {
                            this.Close();
                        }
                        break;
                    case Keys.Delete:
                        counter += 1;
                        if (counter == locFile.Count)
                        {
                            return;
                        }
                        else
                        {
                            //Ở đây counter ở trên đã + 1 khi ấn nút delete là đã chuyển ảnh rồi nên khi ở hàm delete ta muốn xóa cái ảnh vừa chuyển thì phải - 1 để tính lại cái ảnh trước khi đã chuyển đi
                            ImageView.Image.Dispose();
                            File.Delete(locFile[counter - 1]);
                            HienAnh();
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