using GameV1.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Security.Cryptography;

namespace GameV1
{
    public partial class Form1 : Form
    {

        private String[] mapArray = new String[10];
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\map");
        Boolean left = false, right = false, up = false, down = false;

        int movementSpeed = 3;
        int currentMap = 0;
        Boolean conversation = false;
        Boolean[] conversationsBoolean = { false, false };
        Boolean interaction = false;
        Boolean[] keys = { false, false};
        Boolean[] doors = { false, false };
        List<Monster> monsters = new List<Monster>();
        int doorValue = 0;
        double playerHp = 100;
        Boolean mouseClicked = false;
        System.Windows.Forms.Timer timer;
        System.Windows.Forms.Timer timer1;
        int walk = 0;
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public Form1()
        {
            InitializeComponent();
            mapArray = Directory.GetFiles(filePath, "*.*");
            timer = new System.Windows.Forms.Timer();
            timer1 = new System.Windows.Forms.Timer();

            timer.Tick += new EventHandler(timerEvent);
            timer.Interval = 1500;

            // Conversation on game start
            if (!conversationsBoolean[0] && currentMap == 0)
            {
                ConversationLabel.Visible = true;
                timer1.Tick += new EventHandler(conversation1);
                timer1.Interval = 1500;
                timer1.Start();
            }
        }

        private void mouseClick(object sender, MouseEventArgs e)
        {
            mouseClicked = true;
            timer.Start();
        }
        private void keyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                up = false;
            }
            else if (e.KeyCode == Keys.A)
            {
                left = false;
            }
            else if (e.KeyCode == Keys.S)
            {
                down = false;
            }
            else if (e.KeyCode == Keys.D)
            {
                right = false;
            }
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                up = true;

            } else if (e.KeyCode == Keys.A)
            {
                left = true;
            } else if (e.KeyCode == Keys.S)
            {
                down = true;
            } else if (e.KeyCode == Keys.D)
            {
                right = true;
            }
        }

        int k = 0;
        void timerEvent(object source, EventArgs e)
        {
            k++;
            if (ConversationLabel.Visible == true)
            {
                ConversationLabel.Visible = false;
            }
            if(k == 2)
            {
                k = 0;
                interaction = false;
                conversation = false;
                timer.Stop();
            }
            if (mouseClicked)
            {
                mouseClicked = !mouseClicked;
            }
        }

        int conversation1N = 0;
        void conversation1(object source, EventArgs e)
        {
            switch (conversation1N++)
            {
                case 0: { 
                        conversation = true;
                        Console.WriteLine("ffgfgergergaergeagr");
                        ConversationLabel.Text = "Hmm, where am i?";
                    }
                    break;
                case 1: ConversationLabel.Text = "Why am I here?!";
                    break;
                case 2: ConversationLabel.Text = "Feels like I've been here before.";
                    break;
                case 3:
                    {
                        conversationsBoolean[0] = true;

                        ConversationLabel.Visible = false;
                        conversation = false;
                        timer1.Stop();
                    }
                    break;

            }
        }

        int conversation2N = 0;
        void conversation2(object source, EventArgs e)
        {
            ConversationLabel.Visible = true;

            Console.WriteLine("GEGLAEGLFDGDFG");
            switch (conversation2N++)
            {
                case 0:
                    {
                        conversation = true; 
                        ConversationLabel.Text = "I need to find a way out of here.";
                    }
                    break;
                case 1: ConversationLabel.Text = "eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee";
                    break;
                case 2:
                    {
                        conversationsBoolean[0] = true;
                        ConversationLabel.Visible = false;
                        conversation = false;
                        Console.WriteLine("här");
                        timer1.Stop();
                    }
                    break;
            }   
        }
        void scrollTimer(object source, EventArgs e)
        {
            if (letterLabel.Text.Length == 0)
            {
                Console.WriteLine("fffffffsadsdfasdfasdf");
                timer1.Stop();
            }
            else
            {
                letterLabel.Text = letterLabel.Text.Substring(1, letterLabel.Text.Length - 1);
            }
        }

        void openDoor(int currentMap)
        {
            // Show doors on each level
            if (doors[currentMap] && doorValue == 0)
            {
                Console.WriteLine(" OpenDoorFunction " + currentMap);
                PictureBox pb = new PictureBox();
                pb.Tag = "Door";
                pb.Image = Properties.Resources.doorROpen;
                pb.Location = new Point(580, 216);
                pb.BackColor = Color.Transparent;
                pb.Size = new System.Drawing.Size(32, 64);
                this.Controls.Add(pb);
                doorValue = 1;
            }
        }
        void walkAnimationRight()
        {
            if (walk == 0)
            {
                playerBox.Image = Properties.Resources.walkRight2;
                System.Threading.Thread.Sleep(300);
                walk = 1;
            }
            else
            {
                playerBox.Image = Properties.Resources.walkRight1;
                System.Threading.Thread.Sleep(300);
                walk = 0;
            }
        }
        void walkAnimationLeft()
        {
            if (walk == 0)
            {
                playerBox.Image = Properties.Resources.walkLeft2;
                System.Threading.Thread.Sleep(300);
                walk = 1;
            }
            else
            {
                playerBox.Image = Properties.Resources.walkLeft1;
                System.Threading.Thread.Sleep(300);
                walk = 0;
            }
        }

        public void gameTimerEvent(object sender, EventArgs e)
        {

            Bitmap bm = new Bitmap(mapArray[currentMap]);
            if (!conversation) {
                if (right)
                {
                    if (bm.GetPixel(playerBox.Location.X + 32 + movementSpeed, playerBox.Location.Y + 62) != Color.FromArgb(255, 204, 153, 102)
                    || bm.GetPixel(playerBox.Location.X + 32 + movementSpeed, playerBox.Location.Y) != Color.FromArgb(255, 204, 153, 102))
                    {
                        playerBox.Location = new Point(playerBox.Location.X - movementSpeed, playerBox.Location.Y);
                    }
                    else
                    {

                        Thread thread = new Thread(new ThreadStart(walkAnimationRight));
                        thread.Start();
                        playerBox.Left += movementSpeed;
                    }
                }
                if (left)
                {
                    if (playerBox.Location.X - 40 < 0 && currentMap >= 1)
                    {
                        this.BackgroundImage = Image.FromFile(mapArray[--currentMap]);
                        playerBox.Location = new Point(460, playerBox.Location.Y);
                    }
                    else if (playerBox.Location.X - 10 <= 0 && currentMap == 0)
                    {
                        playerBox.Left += movementSpeed;
                    }

                    if (bm.GetPixel(playerBox.Location.X - movementSpeed, playerBox.Location.Y + 62) != Color.FromArgb(255, 204, 153, 102)
                        || bm.GetPixel(playerBox.Location.X - movementSpeed, playerBox.Location.Y) != Color.FromArgb(255, 204, 153, 102))
                    {
                        playerBox.Location = new Point(playerBox.Location.X + movementSpeed, playerBox.Location.Y);
                        return;
                    }
                    else
                    {
                        Thread thread = new Thread(new ThreadStart(walkAnimationLeft));
                        thread.Start();
                        playerBox.Left -= movementSpeed;
                    }
                }
                if (up)
                {
                    if (bm.GetPixel(playerBox.Location.X + 32, playerBox.Location.Y - movementSpeed) != Color.FromArgb(255, 204, 153, 102))
                    {
                        playerBox.Location = new Point(playerBox.Location.X, playerBox.Location.Y + movementSpeed);
                    }
                    else
                    {
                        playerBox.Top -= movementSpeed;
                    }
                }
                if (down)
                {
                    if (bm.GetPixel(playerBox.Location.X + 32, playerBox.Location.Y + 62 + movementSpeed) != Color.FromArgb(255, 204, 153, 102))
                    {
                        playerBox.Location = new Point(playerBox.Location.X, playerBox.Location.Y - movementSpeed);
                    }
                    else
                    {
                        playerBox.Top += movementSpeed;

                    }
                }
            }
            
            bm.Dispose();

            foreach (Control x in this.Controls)
            {
                foreach (Control j in this.Controls)
                {
                    if ((x is PictureBox) && x.Tag == "Door" && (j is PictureBox) && j.Tag == "player")
                    {
                        if (((PictureBox)x).Bounds.IntersectsWith(((PictureBox)j).Bounds))
                        {
                            PictureBox pb = new PictureBox();
                            pb.Image = Properties.Resources.letter;
                            pb.Location = new Point(270, 338);
                            pb.BackColor = Color.Transparent;
                            this.Controls.Add(pb);
                            pb.Tag = "letter";

                            doorValue = 0;
                            this.BackgroundImage = Image.FromFile(mapArray[++currentMap]);
                            playerBox.Location = new Point(80, playerBox.Location.Y);
                            
                            //this.Controls.RemoveAt(this.Controls.Count-1);
                            this.Controls.Remove(x);
                            x.Dispose();
                            openDoor(currentMap);
                        }
                    }
                    if(x.Tag == "letter" && j.Tag == "player")
                    {
                        if ((((PictureBox)x).Bounds.IntersectsWith(((PictureBox)j).Bounds)))
                        {
                            letterLabel.Visible = true;
                            letterLabel.Text = "..TEST1TEST2TEST3test4";
                            timer1.Interval = 500;
                            timer1.Tick += new EventHandler(scrollTimer);
                            timer1.Start();
                            x.Dispose();
                            this.Controls.Remove(x);
                        }
                    }
                }
            }
            // Level 1
            if(currentMap == 0)
            {
                if ((Math.Sqrt(Math.Pow((252 - playerBox.Location.X), 2) + Math.Pow((129 - playerBox.Location.Y), 2)) < 50) && currentMap == 0 && !interaction)
                {
                    ConversationLabel.Visible = true;
                    ConversationLabel.Text = "Strange. It's a picture of me and my daughter.";
                    interaction = true;

                    timer.Start();
                }
                else if ((Math.Sqrt(Math.Pow((595 - playerBox.Location.X), 2) + Math.Pow((250 - playerBox.Location.Y), 2)) <= 100) && currentMap == 0 && !interaction)
                {
                    if (keys[0])
                    {
                        doors[0] = true;
                        openDoor(currentMap);
                    }
                    else
                    {
                        ConversationLabel.Visible = true;
                        ConversationLabel.Text = "The door is locked";
                    }

                    interaction = true;
                    timer.Start();
                }
                else if ((Math.Sqrt(Math.Pow((550 - playerBox.Location.X), 2) + Math.Pow((425 - playerBox.Location.Y), 2)) <= 80) && currentMap == 0 && !interaction)
                {
                    ConversationLabel.Visible = true;
                    ConversationLabel.Text = "fuck empty!";
                    interaction = true;

                    timer.Start();
                }

                if ((Math.Sqrt(Math.Pow((530 - playerBox.Location.X), 2) + Math.Pow((60 - playerBox.Location.Y), 2)) <= 80) && currentMap == 0 && mouseClicked)
                {
                    Rectangle rec2 = new Rectangle(470, 63, 130, 25);
                    Rectangle rec = new Rectangle(470, 35, 130, 25);
                    if (rec.Contains(this.PointToClient(Cursor.Position)))
                    {
                        ConversationLabel.Text = "This drawer looks empty, maybe the one below.";
                        ConversationLabel.Visible = true;
                    }
                    else if (!keys[0] && rec2.Contains(this.PointToClient(Cursor.Position)))
                    {
                        ConversationLabel.Text = "A key!, might work on the door";
                        keys[0] = true;
                        ConversationLabel.Visible = true;
                    }
                    timer.Start();
                }
            }
            else if(currentMap == 1)
            {
                if (!conversationsBoolean[1] && currentMap == 1)
                {
                    timer1.Tick += new EventHandler(conversation2);
                    timer1.Interval = 2500;
                    timer1.Start();
                    conversationsBoolean[1] = true;
                }
            }
            
        }
    }
}