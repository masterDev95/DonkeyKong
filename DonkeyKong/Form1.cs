using System;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Forms;
using DonkeyKong.Objects;
using System.Threading;

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

            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 16;
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

            OnMapUpdated();

            //label1.Text = Convert.ToString(GetFps());
        }

        void CheckCollision()
        {
            foreach (Control picturebox in Controls)
            {
                if (jumpman.Bounds.IntersectsWith(picturebox.Bounds) && picturebox != jumpman)
                {
                    jumpman.IsInAir = false;
                    jumpman.Location = new Point(jumpman.Location.X, jumpman.Location.Y - 5);
                }
            }
        }

        void KeyboardEvents()
        {
            if (Keyboard.IsKeyDown(Key.Right))
            {
                jumpman.MoveRight();
            }

            if (Keyboard.IsKeyToggled(Key.F5))
            {
                Application.Restart();
            }
        }

        void OnMapUpdated()
        {
            Interlocked.Increment(ref _frameCount);
        }

        double GetFps()
        {
            double secondsElapsed = (DateTime.Now - _lastCheckTime).TotalSeconds;
            long count = Interlocked.Exchange(ref _frameCount, 0);
            double fps = count / secondsElapsed;
            _lastCheckTime = DateTime.Now;
            return fps;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Controls.Add(jumpman);
        }
    }
}
