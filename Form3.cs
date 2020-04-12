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
    public partial class Form3 : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Barratitulo.BackColor = ColorTranslator.FromHtml("#009975");
            btnlogin.BackColor = ColorTranslator.FromHtml("#454d66");
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            string user_name = txtuser.Text;
            string pass = txtpassword.Text;
            bool bandera = true, bandera1 = true;
            if (user_name != "admin")
            {
                bandera = false;
            }

            if (pass != "12345")
            {
                bandera1 = false;
            }

            if (!bandera || !bandera1)
            {
                new ErrorDialog("Error de validación, por favor verifica las credenciasles \n ingresadas.").ShowDialog();
            }
            else
            {
                this.Hide();
                new frmmenu().Show();
            }
        }

        private void Barratitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
