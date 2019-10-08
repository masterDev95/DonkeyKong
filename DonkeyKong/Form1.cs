using System;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Forms;
using DonkeyKong.Objects;
using DonkeyKong.Classes;

namespace DonkeyKong
{
    public partial class MainWindow : Form
    {
        Jumpman jumpman;

        public MainWindow()
        {
            InitializeComponent();

            jumpman = new Jumpman(64, Height - 192);

            Timer timer = new Timer();
            timer.Interval = 13;
            timer.Tick += new EventHandler(Update);
            timer.Start();
        }

        bool KeyJumpReleased { get; set; }

        void Update(object sender, EventArgs e)
        {
            CheckCollision();
            KeyboardEvents();
            jumpman.Update();
        }

        void CheckCollision()
        {
            bool colision = false;

            foreach (Control control in Controls)
            {
                if (control.Name == "Floor")
                {
                    var physix = new Physics();
                    int gravity = physix.Gravity;

                    // - Pixel Perfect colision bounds
                    var bounds = new Rectangle(control.Bounds.Location, control.Size);
                    bounds.Location = new Point(bounds.Location.X, bounds.Location.Y - gravity);

                    if (jumpman.Bounds.IntersectsWith(bounds))
                    {
                        jumpman.ResetJumpForce();
                        jumpman.IsInAir = false;
                        colision = true;

                        // - Raise jumpman if under the wall
                        if (bounds.Location.Y - jumpman.Location.Y < 45)
                        {
                            jumpman.Location = new Point(jumpman.Location.X, jumpman.Location.Y - 1);
                        }

                        label1.Text = Convert.ToString(bounds.Location.Y - jumpman.Location.Y);
                    }
                }
            }

            if (!colision)
            {
                jumpman.IsInAir = true;
            }
        }

        void KeyboardEvents()
        {
            if (Keyboard.IsKeyUp(Key.Space))
            {
                KeyJumpReleased = true;
            }

            if (Keyboard.IsKeyDown(Key.Left))
            {
                jumpman.Move("left");
            }

            if (Keyboard.IsKeyDown(Key.Right))
            {
                jumpman.Move("right");
            }

            if (Keyboard.IsKeyUp(Key.Left) && Keyboard.IsKeyUp(Key.Right))
            {
                jumpman.IsMoving = jumpman.IsInAir ? false : true;
            }

            if (Keyboard.IsKeyDown(Key.Space) && KeyJumpReleased)
            {
                jumpman.Jump();
                KeyJumpReleased = false;
            }

            if (Keyboard.IsKeyToggled(Key.F5))
            {
                Application.Restart();
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Controls.Add(jumpman);
        }
    }
}
