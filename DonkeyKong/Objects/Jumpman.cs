using DonkeyKong.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DonkeyKong.Objects
{
    class Jumpman : PictureBox
    {
        public Jumpman()
        {
            Anchor = AnchorStyles.Bottom;
            Image = Properties.Resources.Idle;
            Location = new Point(56, 584);
            Size = new Size(48, 48);
        }

        public void ApplyGravity()
        {
            Physics physix = new Physics();
            int gravityForce = physix.Gravity;

            Location = new Point(Location.X, Location.Y + gravityForce);
        }
    }
}
