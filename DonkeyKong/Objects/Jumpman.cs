using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DonkeyKong.Classes;

enum Direction
{
    Left, Right
}

enum State
{
    Idle1, Run1, Idle2, Run2, Jump
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
            // Booleans
            IsInAir = true;
            IsJumping = false;
            IsMoving = false;

            // Timers
            AnimTimer = 0;

            // Enums
            Direction = Direction.Right;
            State = State.Idle1;

            // Physics properties
            JumpHealth = JumpForce;
            Speed = 3;
        }

        // Constants
        const int AnimTimerLimit = 3;
        const int JumpForce = 12;

        // Public Booleans
        public bool IsInAir { get; set; }
        public bool IsJumping { get; set; }
        public bool IsMoving { get; set; }

        //Timers
        int AnimTimer { get; set; }

        // Enums
        Direction Direction { get; set; }
        State State { get; set; }

        // Physics Properties
        int JumpHealth { get; set; }
        int Speed { get; }

        // ---------------------------
        // -- Jumpman main loop update
        // ---------------------------
        public new void Update()
        {
            if (IsInAir)
            {
                ApplyGravity();
            }

            UpdateGraphics();
            UpdateJump();
            UpdateAirMotion();
        }

        // - Gravity loop
        void ApplyGravity()
        {
            Physics physix = new Physics();
            int gravityForce = physix.Gravity;

            Location = new Point(Location.X, Location.Y + gravityForce);
        }

        // - Graphics loop
        void UpdateGraphics()
        {
            // - Floor State
            if (!IsMoving && !IsInAir)
            {
                State = State.Idle1;
            }

            // - Running state
            if (IsMoving && !IsInAir && AnimTimer == 0)
            {
                switch (State)
                {
                    case State.Idle1:
                        State = State.Run1;
                        break;
                    case State.Run1:
                        State = State.Idle2;
                        break;
                    case State.Idle2:
                        State = State.Run2;
                        break;
                    case State.Run2:
                        State = State.Idle1;
                        break;
                    case State.Jump:
                        State = State.Run1;
                        break;
                }
            }

            // - In air state
            if (IsInAir)
            {
                State = State.Jump;
            }

            // - Update image according to the state
            if (Direction == Direction.Left)
            {
                switch (State)
                {
                    case State.Idle1:
                        Image = Properties.Resources.IdleLeft;
                        break;
                    case State.Run1:
                        Image = Properties.Resources.Run01Left;
                        break;
                    case State.Idle2:
                        Image = Properties.Resources.IdleLeft;
                        break;
                    case State.Run2:
                        Image = Properties.Resources.Run02Left;
                        break;
                    case State.Jump:
                        Image = Properties.Resources.JumpLeft;
                        break;
                }
            }
            else
            {
                switch (State)
                {
                    case State.Idle1:
                        Image = Properties.Resources.IdleRight;
                        break;
                    case State.Run1:
                        Image = Properties.Resources.Run01Right;
                        break;
                    case State.Idle2:
                        Image = Properties.Resources.IdleRight;
                        break;
                    case State.Run2:
                        Image = Properties.Resources.Run02Right;
                        break;
                    case State.Jump:
                        Image = Properties.Resources.JumpRight;
                        break;
                }
            }

            // - Timer to limit image update speed
            if (AnimTimer < AnimTimerLimit && IsMoving)
            {
                AnimTimer++;
            }
            else
            {
                AnimTimer = 0;
            }

            // - Update boundaries according to the image size
            Size = Image.Size;
        }

        // - Jump loop
        void UpdateJump()
        {
            if (IsJumping)
            {
                Location = new Point(Location.X, Location.Y - JumpHealth);
                JumpHealth--;
                IsJumping = JumpHealth <= 0 ? false : true;
            }
        }

        // - Air motion loop (if user move and jump)
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

        // --------------------------------------------------------------------------------
        // - Movements function (called by the keyboard event function from the form class)
        // --------------------------------------------------------------------------------
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

        // - Function to reset the jump health
        public void ResetJumpForce()
        {
            JumpHealth = JumpForce;
        }
    }
}
