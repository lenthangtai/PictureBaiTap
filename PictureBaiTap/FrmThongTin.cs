using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureBaiTap
{
    public partial class FrmThongTin : Form
    {
        public FrmThongTin()
        {
            InitializeComponent();
        }

        private void FrmThongTin_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Escape:
                    this.Close();
                    break;
            }
        }
    }
}
