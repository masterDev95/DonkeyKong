using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace DonkeyKong
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string projectDir = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string stageImg = projectDir + @"\Assets\Sprites\Stage.png";

            BackgroundImage = new Bitmap(stageImg);
            BackgroundImageLayout = ImageLayout.Stretch;

            MaximizeBox = false;
        }
    }
}
