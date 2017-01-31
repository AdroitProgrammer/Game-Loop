using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace Game_Loop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private float TargetFps = 60;
        private int FPS;
        private int fpsCounter;
        private long fpsTime;
        private int updateCounter;
        private int UPS;
        private long updateTime;
        private long lastTime;
        private long deltaTime;

        private Spritebatch spriteBatch;
        private Stopwatch Gametime = new Stopwatch();

        private Point mousePosition;
        private List<Keys> keysPressed = new List<Keys>();
        private List<Keys> keysHeld = new List<Keys>();
        private bool leftClick;
        private bool rightClick;



        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            TargetFps += TargetFps / 10;
            spriteBatch = new Spritebatch(this.ClientSize, this.CreateGraphics());
            Thread Game = new Thread(GameLoop);
            Game.Start();
        }

        private void GameLoop()
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
            Gametime.Start();
            while (this.Created)
            {
                deltaTime = Gametime.ElapsedMilliseconds - lastTime;
                lastTime = Gametime.ElapsedMilliseconds;
                bool CanUpdate = CheckFps();
                Input();
                if (CanUpdate)
                    Logic();
                Render();
            }
        }

        private bool CheckFps()
        {
            if (Gametime.ElapsedMilliseconds - fpsTime > 1000)
            {
                fpsTime = Gametime.ElapsedMilliseconds;
                FPS = fpsCounter;
                UPS = updateCounter;
                fpsCounter = 0;
                updateCounter = 0;
            }
            else
            {
                fpsCounter++;
            }

            if (Gametime.ElapsedMilliseconds - updateTime > 1000 / TargetFps)
            {
                updateTime = Gametime.ElapsedMilliseconds;
                updateCounter++;
                return true;
            }
            else
                return false;

        }


        private void Input()
        {
            this.Invoke(new MethodInvoker(
                delegate {
                    mousePosition = this.PointToClient(Cursor.Position);
                    this.Text = " Updates:" + UPS + " Frames:" + FPS;
                }));

           

        }

        private void Logic()
        {
            // write logic code for the game here

            rightClick = false;
            leftClick = false;
            keysHeld.Clear();
            keysPressed.Clear();
        }

        private void Render()
        {
            spriteBatch.Begin();
            // add all drawing code inbetween the begin and end
            spriteBatch.End();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            keysPressed.Add((Keys)e.KeyChar.ToString().ToUpper().ToCharArray()[0]);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            string button = e.Button.ToString();
            if (button == "Left")
            {
                leftClick = true;
            }
            else
            {
                rightClick = true;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            keysHeld.Add(e.KeyCode);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Environment.Exit(0);
        }

    }
}
