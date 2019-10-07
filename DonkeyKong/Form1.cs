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
            timer.Tick += new EventHandler(HandleApplicationIdle);
            timer.Start();
        }

        void HandleApplicationIdle(object sender, EventArgs e)
        {
            Update();
        }

        new void Update()
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
                if (control is PictureBox)
                {
                    PictureBox pictureBox = (PictureBox)control;
                    Bitmap floorTexture = Properties.Resources.Floor;

                    if (pictureBox.Name == "Floor")
                    {
                        var physix = new Physics();
                        int gravity = physix.Gravity;

                        var bounds = new Rectangle(pictureBox.Bounds.Location, pictureBox.Size);
                        bounds.Location = new Point(bounds.Location.X, bounds.Location.Y - gravity);

                        if (jumpman.Bounds.IntersectsWith(bounds))
                        {
                            jumpman.IsInAir = false;
                            colision = true;

                            if (bounds.Location.Y - jumpman.Location.Y < 45)
                            {
                                jumpman.Location = new Point(jumpman.Location.X, jumpman.Location.Y - 1);
                            }

                            label1.Text = Convert.ToString(bounds.Location.Y - jumpman.Location.Y);
                        }
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
