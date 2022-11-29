using GameV1.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;

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
        {}
        public Form1()
        {
            InitializeComponent();
            mapArray = Directory.GetFiles(filePath, "*.*");
            timer = new System.Windows.Forms.Timer();
            timer1 = new System.Windows.Forms.Timer();
        }

        private void mouseClick(object sender, MouseEventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(mouseCliked));
            thread.Start();
        }

        private void mouseCliked()
        {
            mouseClicked = true;
            System.Threading.Thread.Sleep(100);
            mouseClicked = false;
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

        void openDoor(int currentMap)
        {
            // Show doors on each level
            if (doors[currentMap] && doorValue == 0)
            {
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
                    if (bm.GetPixel(playerBox.Location.X + 70, playerBox.Location.Y + 64) != Color.FromArgb(255, 204, 153, 102)
                    || bm.GetPixel(playerBox.Location.X + 70, playerBox.Location.Y) != Color.FromArgb(255, 204, 153, 102))
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

                    if (bm.GetPixel(playerBox.Location.X - 3, playerBox.Location.Y + 64) != Color.FromArgb(255, 204, 153, 102)
                        || bm.GetPixel(playerBox.Location.X - 3, playerBox.Location.Y) != Color.FromArgb(255, 204, 153, 102))
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
                    if (bm.GetPixel(playerBox.Location.X + 32, playerBox.Location.Y - 1) != Color.FromArgb(255, 204, 153, 102))
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
                    if (bm.GetPixel(playerBox.Location.X + 34, playerBox.Location.Y + 70) != Color.FromArgb(255, 204, 153, 102))
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
                            Thread thread = new Thread(new ThreadStart(scrollTimer));
                            thread.Start();
                            x.Dispose();
                            this.Controls.Remove(x);
                        }
                    }
                }
            }
            // Level 1
            if(currentMap == 0)
            {
                // Conversation on game start
                if (!conversationsBoolean[0] && currentMap == 0)
                {
                    conversationsBoolean[0] = true;
                    ConversationLabel.Visible = true;
                    Thread thread = new Thread(new ThreadStart(conversation1));
                    thread.Start();
                }

                if ((Math.Sqrt(Math.Pow((252 - playerBox.Location.X), 2) + Math.Pow((129 - playerBox.Location.Y), 2)) < 50) && currentMap == 0 && !interaction)
                {
                    ConversationLabel.Visible = true;
                    ConversationLabel.Text = "Strange. It's a picture of me and my daughter.";
                    Thread thread = new Thread(new ThreadStart(timerEvent));
                    thread.Start();
                    interaction = true;
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
                        Thread thread = new Thread(new ThreadStart(timerEvent));
                        thread.Start();
                    }

                    interaction = true;
                }
                else if ((Math.Sqrt(Math.Pow((550 - playerBox.Location.X), 2) + Math.Pow((425 - playerBox.Location.Y), 2)) <= 80) && currentMap == 0 && !interaction)
                {
                    interaction = true;
                    ConversationLabel.Visible = true;
                    ConversationLabel.Text = "fuck empty!";

                    Thread thread = new Thread(new ThreadStart(timerEvent));
                    thread.Start();
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

                    Thread thread = new Thread(new ThreadStart(timerEvent));
                    thread.Start();
                }
            }
            else if(currentMap == 1)
            {
                if (!conversationsBoolean[1] && currentMap == 1)
                {
                    conversationsBoolean[1] = true;
                    ConversationLabel.Visible = true;
                    Thread thread = new Thread(new ThreadStart(conversation2));
                    thread.Start();
                }
                if ((Math.Sqrt(Math.Pow((330 - playerBox.Location.X), 2) + Math.Pow((155 - playerBox.Location.Y), 2)) <= 80) && currentMap == 1 && mouseClicked)
                {
                    Rectangle rec = new Rectangle(315, 115, 44, 42);
                    if (rec.Contains(this.PointToClient(Cursor.Position)) && !keys[1])
                    {
                        ConversationLabel.Visible = true;
                        Thread thread = new Thread(new ThreadStart(lockedDrawer));
                        thread.Start();

                    }
                }
            }
        }
        void timerEvent()
        {
            int k1 = 0;

            while (k1++ < 10)
            {
                if (k1 == 9)
                {
                    if (ConversationLabel.Visible == true)
                    {
                        ConversationLabel.Invoke((MethodInvoker)(() =>
                        {
                            ConversationLabel.Visible = false;
                        }));
                    }
                    k1 = 0;
                    interaction = false;
                    conversation = false;
                    return;
                }
                System.Threading.Thread.Sleep(100);

            }
        }

        void conversation1()
        {
            int conversation1N = 0;

            while (conversation1N < 4)
            {
                switch (conversation1N++)
                {
                    case 0:
                        {
                            conversation = true;
                            ConversationLabel.Invoke((MethodInvoker)(() =>
                            {
                                ConversationLabel.Text = "Hmm, where am I?";
                            }));
                        }
                        break;
                    case 1:
                        {
                            ConversationLabel.Invoke((MethodInvoker)(() =>
                            {
                                ConversationLabel.Text = "Why am I here?!";
                            }));
                        }
                        break;
                    case 2:
                        {
                            ConversationLabel.Invoke((MethodInvoker)(() =>
                            {
                                ConversationLabel.Text = "Feels like I've been here before.";
                            }));
                        }
                        break;
                    case 3:
                        {
                            ConversationLabel.Invoke((MethodInvoker)(() =>
                            {
                                ConversationLabel.Visible = false;
                            }));
                            conversation = false;
                        }
                        break;
                }
                System.Threading.Thread.Sleep(2000);
            }
        }

        void conversation2()
        {
            int conversation2N = 0;

            while (conversation2N < 4)
            {
                switch (conversation2N++)
                {
                    case 0:
                        {
                            conversation = true;
                            ConversationLabel.Invoke((MethodInvoker)(() =>
                            {
                                ConversationLabel.Text = "Fuck me I need to get out of here";
                            }));
                        }
                        break;
                    case 1:
                        {
                            ConversationLabel.Invoke((MethodInvoker)(() =>
                            {
                                ConversationLabel.Text = "Feels like I've been here before.";
                            }));

                        }
                        break;
                    case 2:
                        {
                            ConversationLabel.Invoke((MethodInvoker)(() =>
                            {
                                ConversationLabel.Text = "Isn't this my kithchen?";
                            }));

                        }
                        break;
                    case 3:
                        {
                            ConversationLabel.Invoke((MethodInvoker)(() =>
                            {
                                ConversationLabel.Visible = false;
                            }));
                            conversation = false;
                        }
                        break;
                }
                System.Threading.Thread.Sleep(2000);
            }
        }

        void scrollTimer()
        {
            int n = 0;
            int length = 0;

            letterLabel.Invoke((MethodInvoker)(() =>
            {
                length = letterLabel.Text.Length;
            }));

            while (n++ < length)
            {
                letterLabel.Invoke((MethodInvoker)(() =>
                {
                    letterLabel.Text = letterLabel.Text.Substring(1, letterLabel.Text.Length - 1);
                }));
                System.Threading.Thread.Sleep(250);
            }
        }

        private void lockedDrawer()
        {
            int k = 0;
            while (k < 4)
            {
                switch (k++)
                {
                    case 0:
                        {
                            ConversationLabel.Invoke((MethodInvoker)(() =>
                            {
                                ConversationLabel.Text = "hmm It's locked.";
                            }));
                        }
                        break;
                    case 1:
                        {
                            ConversationLabel.Invoke((MethodInvoker)(() =>
                            {
                                ConversationLabel.Text = "hmm It's locked1.";
                            }));
                        }                      
                        break;
                    case 2:
                        {
                            ConversationLabel.Invoke((MethodInvoker)(() =>
                            {
                                ConversationLabel.Text = "hmm It's locked2.";
                            }));
                        }
                        break;
                    case 3:
                        {
                            ConversationLabel.Invoke((MethodInvoker)(() =>
                            {
                                ConversationLabel.Visible = false;
                            }));
                        }
                        break;
                }
                System.Threading.Thread.Sleep(2000);
            }
        }
    }
}