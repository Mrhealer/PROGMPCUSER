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
using Microsoft.Win32;
using System.Diagnostics;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ProGMClient.View.TinhTien;
using Management.FormState;

namespace ProGMClient.View.Login
{
    public partial class frmDangNhap : DevExpress.XtraEditors.XtraForm
    {
        FormState frmState;
        Main frmMain;
        public frmDangNhap(FormState frmState, Main frmMain)
        {
            ProcessModule objCurrentModule = Process.GetCurrentProcess().MainModule; //Get Current Module
            objKeyboardProcess = new LowLevelKeyboardProc(captureKey); //Assign callback function each time keyboard process
            ptrHook = SetWindowsHookEx(13, objKeyboardProcess, GetModuleHandle(objCurrentModule.ModuleName), 0); //Setting Hook of Keyboard Process for current module
            this.frmState = frmState;
            this.frmMain = frmMain;
            InitializeComponent();
        }
        // Structure contain information about low-level keyboard input event
        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public Keys key;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr extra;
        }

        //System level functions to be used for hook and unhook keyboard input
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int id, LowLevelKeyboardProc callback, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hook, int nCode, IntPtr wp, IntPtr lp);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string name);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern short GetAsyncKeyState(Keys key);


        //Declaring Global objects
        private IntPtr ptrHook;
        private LowLevelKeyboardProc objKeyboardProcess;

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text == "quoctv" || txtMatKhau.Text == "123456789")
            {
                this.Hide();
                frmState.Restore(frmMain);
                frmMain.Hide();
                ProcessModule objCurrentModule = Process.GetCurrentProcess().MainModule; //Get Current Module
                objKeyboardProcess = new LowLevelKeyboardProc(captureKey); //Assign callback function each time keyboard process
                UnhookWindowsHookEx(GetModuleHandle(objCurrentModule.ModuleName));
                frmState.Restore(frmMain);
                //frmMain.Hide();
                frmTinhTien frmNguoiChoi = new frmTinhTien();
                frmNguoiChoi.Show();
                frmNguoiChoi.TopMost = true;
               
               
            }
        }
        private IntPtr captureKey(int nCode, IntPtr wp, IntPtr lp)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lp, typeof(KBDLLHOOKSTRUCT));

                if (objKeyInfo.key == Keys.RWin || objKeyInfo.key == Keys.LWin) // Disabling Windows keys
                {
                    return (IntPtr)1;
                }
            }
            return CallNextHookEx(ptrHook, nCode, wp, lp);
        }
       
        private void frmDangNhap_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


    }
}