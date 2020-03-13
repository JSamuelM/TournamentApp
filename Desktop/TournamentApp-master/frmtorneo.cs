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
    public partial class frmtorneo : UserControl
    {
        public frmtorneo()
        {
            InitializeComponent();
        }
        private static frmtorneo _instance;
        public static frmtorneo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new frmtorneo();
                }
                return _instance;
            }
        }
        private void frmtorneo_Load(object sender, EventArgs e)
        {

        }
    }
}
