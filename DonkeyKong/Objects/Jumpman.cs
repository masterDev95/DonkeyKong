using System.Drawing;
using System.Windows.Forms;
using DonkeyKong.Classes;

namespace DonkeyKong.Objects
{
    class Jumpman : PictureBox
    {
        public Jumpman(int xSpawn, int ySpawn)
        {
            //  PictureBox Properties
            Anchor = AnchorStyles.Bottom;
            Image = Properties.Resources.Idle;
            Location = new Point(xSpawn, ySpawn);
            Size = new Size(48, 48);

            //  Jumpman's Properties
            IsInAir = true;
            Speed = 2;
        }

        public bool IsInAir { get; set; }
        public int Speed { get; }

        public void ApplyGravity()
        {
            Physics physix = new Physics();
            int gravityForce = physix.Gravity;

            Location = new Point(Location.X, Location.Y + gravityForce);
        }

        public new void Move(string dir)
        {
            if (!IsInAir)
            {
                switch (dir)
                {
                    case "left":
                        Location = new Point(Location.X - Speed, Location.Y);
                        break;
                    case "right":
                        Location = new Point(Location.X + Speed, Location.Y);
                        break;
                }
            }
        }
    }
}
