using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TournamentApp
{
    public partial class ErrorDialog : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        string msn = "";
        public ErrorDialog(string message)
        {
            msn = message;
            InitializeComponent();
        }

        private void ErrorDialog_Load(object sender, EventArgs e)
        {
            lblmessage.Text = msn;
            Barratitulo.BackColor = ColorTranslator.FromHtml("#d63447");
            this.BackColor = ColorTranslator.FromHtml("#f3f3f3");
        }

        private void lblmessage_Click(object sender, EventArgs e)
        {

        }

        private void Barratitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
