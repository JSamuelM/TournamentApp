using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TournamentApp
{
    public partial class frmjugador : UserControl
    {

        private static frmjugador _instance;
        public static frmjugador Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new frmjugador();
                }
                return _instance;
            }
        }

        public frmjugador()
        {
            InitializeComponent();
        }

        private void btnnuevo_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
