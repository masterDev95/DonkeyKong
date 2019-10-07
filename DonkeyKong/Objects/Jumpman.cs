using System.Drawing;
using System.Windows.Forms;
using DonkeyKong.Classes;

enum Direction
{
    Left, Right
}

namespace DonkeyKong.Objects
{
    class Jumpman : PictureBox
    {
        public Jumpman(int xSpawn, int ySpawn)
        {
            // - PictureBox Properties
            Anchor = AnchorStyles.Bottom;
            Image = Properties.Resources.IdleRight;
            Location = new Point(xSpawn, ySpawn);
            Size = Image.Size;

            // - Jumpman's Properties
            Direction = Direction.Right;
            IsInAir = true;
            Speed = 2;
        }

        Direction Direction { get; set; }
        public bool IsInAir { get; set; }
        public int Speed { get; }

        public new void Update()
        {
            if (IsInAir)
            {
                ApplyGravity();
            }

            UpdateSize();
            UpdateGraphics();
        }

        void ApplyGravity()
        {
            Physics physix = new Physics();
            int gravityForce = physix.Gravity;

            Location = new Point(Location.X, Location.Y + gravityForce);
        }

        void UpdateSize()
        {
            Size = Image.Size;
        }

        void UpdateGraphics()
        {
            switch (Direction)
            {
                case Direction.Left:
                    Image = Properties.Resources.IdleLeft;
                    break;
                case Direction.Right:
                    Image = Properties.Resources.IdleRight;
                    break;
            }
        }

        public new void Move(string dir)
        {
            if (!IsInAir)
            {
                switch (dir)
                {
                    case "left":
                        Location = new Point(Location.X - Speed, Location.Y);
                        Direction = Direction.Left;
                        break;
                    case "right":
                        Location = new Point(Location.X + Speed, Location.Y);
                        Direction = Direction.Right;
                        break;
                }
            }
        }
    }
}
