using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using DonkeyKong.Classes;
using DonkeyKong.Objects;

namespace DonkeyKong
{
    public partial class MainWindow : Form
    {
        Jumpman jumpman = new Jumpman();

        public MainWindow()
        {
            InitializeComponent();
            Application.Idle += HandleApplicationIdle;
        }

        void HandleApplicationIdle(object sender, EventArgs e)
        {
            Update();
        }

        new void Update()
        {
            jumpman.ApplyGravity();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Controls.Add(jumpman);
        }
    }
}
