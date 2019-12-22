using Management.FormState;
using ProGMClient.View.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProGMClient
{
    public partial class Main : DevExpress.XtraEditors.XtraForm
    {
        FormState frmMax;
        public Main()
        {
            InitializeComponent();
            frmMax = new FormState();
            frmMax.Maximize(this);
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            frmDangNhap frmDangNhap = new frmDangNhap(frmMax, this);
            frmDangNhap.ShowDialog();
        }
    }
}
