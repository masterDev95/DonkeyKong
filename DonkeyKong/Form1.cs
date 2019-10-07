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

        DateTime _lastCheckTime = DateTime.Now;
        long _frameCount = 0;

        public MainWindow()
        {
            InitializeComponent();

            jumpman = new Jumpman(64, Height - 192);

            var timer = new Timer();
            timer.Interval = 13;
            timer.Tick += new EventHandler(HandleApplicationIdle);
            timer.Start();
            //Application.Idle += HandleApplicationIdle;
        }

        void HandleApplicationIdle(object sender, EventArgs e)
        {
            Update();
        }

        new void Update()
        {
            if (jumpman.IsInAir)
            {
                jumpman.ApplyGravity();
            }

            CheckCollision();
            KeyboardEvents();
        }

        void CheckCollision()
        {
            bool colision = false;

            foreach (Control picturebox in Controls)
            {
                var physix = new Physics();
                int gravity = physix.Gravity;

                var bounds = new Rectangle(picturebox.Bounds.Location, picturebox.Size);
                bounds.Location = new Point(bounds.Location.X, bounds.Location.Y - gravity);

                if (jumpman.Bounds.IntersectsWith(bounds) && picturebox != jumpman)
                {
                    jumpman.IsInAir = false;

                    if (jumpman.Location.Y > bounds.Location.Y)
                    {
                        jumpman.Location = new Point(jumpman.Location.X, jumpman.Location.Y - 1);
                    }

                    colision = true;
                }

                label1.Text = Convert.ToString(jumpman.Location.Y) + " " + Convert.ToString(bounds.Location.Y);
            }

            if (!colision)
            {
                jumpman.IsInAir = true;
            }
        }

        void KeyboardEvents()
        {
            if (Keyboard.IsKeyDown(Key.Left))
            {
                jumpman.Move("left");
            }

            if (Keyboard.IsKeyDown(Key.Right))
            {
                jumpman.Move("right");
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
