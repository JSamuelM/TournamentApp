﻿using System;
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
    public partial class frmequipo : UserControl
    {
        public frmequipo()
        {
            InitializeComponent();
        }
        private static frmequipo _instance;
        public static frmequipo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new frmequipo();
                }
                return _instance;
            }
        }
        private void frmequipo_Load(object sender, EventArgs e)
        {

        }
    }
}