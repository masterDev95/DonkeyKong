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
            IsJumping = false;
            JumpHealth = JumpForce;
            IsInAir = true;
            IsMoving = false;
            Speed = 2;
        }

        const int JumpForce = 15;

        public bool IsMoving { get; set; }
        public bool IsInAir { get; set; }

        Direction Direction { get; set; }
        bool IsJumping { get; set; }
        int JumpHealth { get; set; }
        int Speed { get; }


        public new void Update()
        {
            if (IsInAir)
            {
                ApplyGravity();
            }

            UpdateSize();
            UpdateGraphics();
            UpdateJump();
            UpdateAirMotion();
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

        void UpdateJump()
        {
            if (IsJumping)
            {
                Location = new Point(Location.X, Location.Y - JumpHealth);
                JumpHealth--;
                IsJumping = JumpHealth <= 0 ? false : true;
            }
        }

        void UpdateAirMotion()
        {
            if (IsInAir && IsMoving)
            {
                switch (Direction)
                {
                    case Direction.Left:
                        Location = new Point(Location.X - Speed, Location.Y);
                        break;
                    case Direction.Right:
                        Location = new Point(Location.X + Speed, Location.Y);
                        break;
                }
            }
        }

        public new void Move(string dir)
        {
            if (!IsInAir)
            {
                IsMoving = true;

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

        public void Jump()
        {
            if (!IsInAir)
            {
                IsJumping = true;
            }
        }

        public void ResetJumpForce()
        {
            JumpHealth = JumpForce;
        }
    }
}
