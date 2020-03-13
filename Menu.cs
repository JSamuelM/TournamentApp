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

namespace torneo
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void pictureBox5_Click(object sender, EventArgs e)  //cerrar aplicacion
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

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void Barratitulo_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox3_Click(object sender, EventArgs e) //hacer el despliegue del menu.
        {
            if (Menuvertical.Width == 220)
            {
                Menuvertical.Width = 72;
            }
            else Menuvertical.Width = 220;
        }



        private void panelcontenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //nuevo torneo
        {
            if (!panelcontenedor.Controls.Contains(fmrtorneo.Instance))
            {
                panelcontenedor.Controls.Add(fmrtorneo.Instance);
                fmrtorneo.Instance.Dock = DockStyle.Fill;
                fmrtorneo.Instance.BringToFront();
            }
            else
            {
                fmrtorneo.Instance.BringToFront();
            }
        }

        private void btnantiguotorneo_Click(object sender, EventArgs e)
        {
           
        }

        private void btnequipos_Click(object sender, EventArgs e)
        {
            if (!panelcontenedor.Controls.Contains(fmrEquipo.Instance))
                {
                    panelcontenedor.Controls.Add(fmrEquipo.Instance);
                fmrEquipo.Instance.Dock = DockStyle.Fill;
                fmrEquipo.Instance.BringToFront();
                }
                else
                {
                fmrEquipo.Instance.BringToFront();
                }

         
        }

        private void btnjugadores_Click(object sender, EventArgs e)
        {
            if (!panelcontenedor.Controls.Contains(fmrjugador.Instance))
            {
                panelcontenedor.Controls.Add(fmrjugador.Instance);
                fmrjugador.Instance.Dock = DockStyle.Fill;
                fmrjugador.Instance.BringToFront();
            }
            else
            {
                fmrjugador.Instance.BringToFront();
            }
        }
    }
}
