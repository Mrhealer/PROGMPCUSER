using Management.FormState;
using Newtonsoft.Json;
using ProGMClient.Business;
using ProGMClient.View.Chat;
using ProGMClient.View.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace ProGMClient
{
    public partial class Main : DevExpress.XtraEditors.XtraForm
    {
        FormState frmMax;
        //private frmChat _frmChat;
        public Main()
        {
            InitializeComponent();
            frmMax = new FormState();
            frmMax.Maximize(this);
            SocketBussiness.tcpClient = new TcpClient();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SocketBussiness.tcpClient.Connect("127.0.0.1", 8888);

            backgroundWorker1.RunWorkerAsync();
            frmDangNhap frmDangNhap = new frmDangNhap(frmMax, this);
            frmDangNhap.KeyPreview = true;
            frmDangNhap.ShowDialog();
        }

        #region config socket
        TcpClient clientSocket = new TcpClient();
        #endregion

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (SocketBussiness.tcpClient.Connected)
            {
                try
                {
                    Application.DoEvents();
                    NetworkStream ns = SocketBussiness.tcpClient.GetStream();
                    string DuLieu = "";
                    while (ns.DataAvailable && SocketBussiness.tcpClient.Connected)
                    {
                        byte[] arrByte = new byte[SocketBussiness.tcpClient.ReceiveBufferSize];
                        ns.Read(arrByte, 0, arrByte.Length);
                        DuLieu = Encoding.UTF8.GetString(arrByte);
                        DuLieu = DuLieu.Substring(0, DuLieu.LastIndexOf("$")).Trim();
                        var dataT = JsonConvert.DeserializeObject<dataSend>(DuLieu);
                        var _frmChat = (frmChat)Application.OpenForms["frmChat"];
                        if (_frmChat == null)
                        {
                            _frmChat = new frmChat();
                        }
                        Invoke(new Action(() =>
                        {
                            _frmChat.TopMost = true;
                            _frmChat.UpdateHistory(dataT.name.ToUpper() + " Say: " + dataT.msg);
                            _frmChat.Show();
                        }));
                       

                    }
                }
                catch (Exception ex)
                {

                }
            }


        }
    }
}
