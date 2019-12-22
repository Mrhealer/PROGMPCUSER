using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProGMClient.View.TinhTien
{
    public partial class frmTinhTien : DevExpress.XtraEditors.XtraForm
    {
        public frmTinhTien()
        {
            InitializeComponent();

            this.Location = new Point(Screen.PrimaryScreen.Bounds.Right - this.Width, //should be (0,0)
                          Screen.PrimaryScreen.Bounds.Y);
            this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;
        }

        private void frmTinhTien_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl15_Click(object sender, EventArgs e)
        {

        }
    }
}