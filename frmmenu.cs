using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TournamentApp
{
    public partial class frmmenu : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        public frmmenu()
        {
            InitializeComponent();
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnmaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnmaxi.Visible = true;
            btnmaximizar.Visible = false;
        }

        private void btnmaxi_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnmaximizar.Visible = true;
            btnmaxi.Visible = false;
        }

        private void btnocultarmenu_Click(object sender, EventArgs e)
        {
            if (Menuvertical.Width == 220)
            {
                Menuvertical.Width = 0;
            }
            else Menuvertical.Width = 220;
        }

        private void Barratitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnjugadores_Click(object sender, EventArgs e)
        {
            if (!panelcontenedor.Controls.Contains(frmjugador.Instance))
            {
                panelcontenedor.Controls.Add(frmjugador.Instance);
                frmjugador.Instance.Dock = DockStyle.Fill;
                frmjugador.Instance.BringToFront();
            }
            else
            {
                frmjugador.Instance.BringToFront();
            }
        }

        private void btnequipos_Click(object sender, EventArgs e)
        {
            if (!panelcontenedor.Controls.Contains(frmequipo.Instance))
            {
                panelcontenedor.Controls.Add(frmequipo.Instance);
                frmequipo.Instance.Dock = DockStyle.Fill;
                frmequipo.Instance.BringToFront();
            }
            else
            {
                frmequipo.Instance.BringToFront();
            }
        }

        private void btnnuevotorneo_Click(object sender, EventArgs e)
        {
            if (!panelcontenedor.Controls.Contains(frmtorneo.Instance))
            {
                panelcontenedor.Controls.Add(frmtorneo.Instance);
                frmtorneo.Instance.Dock = DockStyle.Fill;
                frmtorneo.Instance.BringToFront();
            }
            else
            {
                frmtorneo.Instance.BringToFront();
            }
        }

        private void frmmenu_Load(object sender, EventArgs e)
        {
            Menuvertical.BackColor = ColorTranslator.FromHtml("#58b368");
            Barratitulo.BackColor = ColorTranslator.FromHtml("#009975");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            new Form3().Show();
        }

        private void btnantiguotorneo_Click(object sender, EventArgs e)
        {

        }

        private void btnnuevotorneo_MouseEnter_1(object sender, EventArgs e)
        {

        }
    }
}
