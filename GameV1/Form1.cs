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
        Boolean conversation = false;
        int movementSpeed = 5;
        int currentMap = 0;
        int[] conversations = { 0, 0 };
        Boolean[] conversationsBoolean = { false, false };
        Boolean interaction = false;
        Boolean[] keys = { false, false};
        Boolean[] doors = { false, false };
        List<Monster> monsters = new List<Monster>();
        int doorValue = 0;
        int monstersNr = 0;
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

            timer1.Tick += new EventHandler(conversation1);
            timer1.Interval = 2000;
            timer.Tick += new EventHandler(timerEvent);
            timer.Interval = 1500;
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
                timer.Stop();
            }
            if (mouseClicked)
            {
                mouseClicked = !mouseClicked;
            }
            gameTimer.Interval = 10;
        }

        int p = 0;
        void conversation1(object source, EventArgs e)
        {
            switch (p++)
            {
                case 0: ConversationLabel.Text = "Hmm, where am i?";
                    break;
                case 1: ConversationLabel.Text = "Why am I here?!";
                    break;
                case 2: ConversationLabel.Text = "Feels like I've been here before.";
                    break;
                case 3:
                    {
                        conversationsBoolean[0] = true;
                        ConversationLabel.Visible = false;
                        timer1.Stop();
                    }
                    break;

            }
        }

        void scrollTimer(object source, EventArgs e)
        {
            letterLabel.Text = letterLabel.Text.Substring(1, letterLabel.Text.Length - 1) + letterLabel.Text.Substring(0, 1);
        }

        public void gameTimerEvent(object sender, EventArgs e)
        {

            for (int g = 0; g < doors.Length; g++)
            {
                if (doors[g] && doorValue == 0 && currentMap == g)
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

            Bitmap bm = new Bitmap(mapArray[currentMap]);

            if (right)
            {
                if (bm.GetPixel(playerBox.Location.X + 70, playerBox.Location.Y + 64) != Color.FromArgb(255, 204, 153, 102)
                || bm.GetPixel(playerBox.Location.X + 70, playerBox.Location.Y) != Color.FromArgb(255, 204, 153, 102))
                {
                    playerBox.Location = new Point(playerBox.Location.X - movementSpeed, playerBox.Location.Y);
                }
                else
                {
                    if(walk == 0) {
                        playerBox.Image = Properties.Resources.walkRight2;
                        walk = 1;
                    }
                    else
                    {
                        playerBox.Image = Properties.Resources.walkRight1;
                        walk = 0;
                    }
                    playerBox.Left += movementSpeed;
                }
            }
            if (left)
            {
                if (playerBox.Location.X - 40 < 0 && currentMap >= 1)
                {
                    this.BackgroundImage = Image.FromFile(mapArray[--currentMap]);
                    Console.WriteLine(currentMap);
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
                    if (walk == 0)
                    {
                        playerBox.Image = Properties.Resources.walkLeft2;
                        walk = 1;
                    }
                    else
                    {
                        playerBox.Image = Properties.Resources.walkLeft1;
                        walk = 0;
                    }
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
            bm.Dispose();
           
            
            int k = 0;
            foreach (Control x in this.Controls)
            {
                if ((x is PictureBox) && x.Tag == "Zombie")
                {
                    if (k == 0)
                    {
                        if (monsters[k].getWalk())
                        {
                            ((PictureBox)x).Left -= 7;
                        }
                        else if (!monsters[k].getWalk())
                        {
                            ((PictureBox)x).Left += 7;
                        }
                        if (((PictureBox)x).Left < 160)
                        {
                            monsters[k].setWalk();
                        }
                        else if (((PictureBox)x).Left > 480)
                        {
                            monsters[k].setWalk();
                        }
                    }
                }

                if ((x is PictureBox) && x.Tag == "bullet")
                {
                    if (((PictureBox)x).Left <= 0 || ((PictureBox)x).Left >= 650)
                    {
                        this.Controls.Remove(((PictureBox)x));
                        ((PictureBox)x).Dispose();
                    }
                    ((PictureBox)x).Left += 6;
                }
                int index = 0;

                if ((x is PictureBox) && x.Tag == "zombie1")
                {
                    if (((PictureBox)x).Left > playerBox.Left)
                    {
                        ((PictureBox)x).Left -= 1;
                        ((PictureBox)x).Image = Properties.Resources.zombieL;
                    }

                    if (((PictureBox)x).Top > playerBox.Top)
                    {
                        ((PictureBox)x).Top -= 1;
                    }

                    if (((PictureBox)x).Left < playerBox.Left)
                    {
                        ((PictureBox)x).Left += 1;
                        ((PictureBox)x).Image = Properties.Resources.zombieR;
                    }

                    if (((PictureBox)x).Top < playerBox.Top)
                    {
                        ((PictureBox)x).Top += 1;
                    }
                }

                foreach (Control j in this.Controls)
                {
                    if (((x is PictureBox) && x.Tag == "Zombie" || (x is PictureBox) && x.Tag == "zombie1") && (j is PictureBox) && j.Tag == "bullet")
                    {
                        if (((PictureBox)x).Bounds.IntersectsWith(((PictureBox)j).Bounds))
                        {
                            this.Controls.Remove(j);
                            j.Dispose();

                            monsters[index].setHealth(50);
                            Console.WriteLine(monsters[index].getHealth());
                            if (monsters[index].getHealth() == 0)
                            {
                                monsters.RemoveAt(index);
                                if (x.Tag == "zombie1")
                                {
                                    PictureBox pb1 = new PictureBox();
                                    pb1.Image = Properties.Resources.letter;
                                    pb1.BackColor = Color.Transparent;
                                    pb1.Location = new Point(300, 140);
                                    pb1.Tag = "letter";
                                    this.Controls.Add(pb1);
                                }
                                this.Controls.Remove(x);
                                x.Dispose();
                            }
                        }
                    }

                    if ((x is PictureBox) && x.Tag == "letter" && (j is PictureBox) && j.Tag == "player")
                    {
                        if (((PictureBox)x).Bounds.IntersectsWith(((PictureBox)j).Bounds)){

                            letterLabel.Visible = true;

                            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                            timer.Tick += new EventHandler(scrollTimer);
                            timer.Interval = 150;
                            timer.Enabled = true;

                            this.Controls.Remove(x);
                            x.Dispose();
                            doors[1] = true;
                        }
                    }

                    if ((x is PictureBox) && x.Tag == "Door" && (j is PictureBox) && j.Tag == "player")
                    {
                        if (((PictureBox)x).Bounds.IntersectsWith(((PictureBox)j).Bounds))
                        {
                            doorValue = 0;
                            this.BackgroundImage = Image.FromFile(mapArray[++currentMap]);
                            Console.WriteLine(currentMap);
                            playerBox.Location = new Point(80, playerBox.Location.Y);
                            this.Controls.RemoveAt(this.Controls.Count-1);
                        }
                    }

                    if(((x is PictureBox) && x.Tag == "Zombie" || (x is PictureBox) && x.Tag == "zombie1") && (j is PictureBox) && j.Tag == "player")
                    {
                        if (((PictureBox)x).Bounds.IntersectsWith(((PictureBox)j).Bounds)){
                            playerHp -= 5;
                            if (playerHp <= 0)
                            {
                                //Application.Restart();
                            }
                            else
                            {
                                playerHealth.Value = Convert.ToInt32(playerHp);

                            }
                            playerBox.BackColor = Color.Red;
                        }else
                        {
                            playerBox.BackColor = Color.Transparent;
                        }
                    }

                    if ((x is PictureBox) && x.Tag == "star" && (j is PictureBox) && j.Tag == "player")
                    {
                        if (((PictureBox)x).Bounds.IntersectsWith(((PictureBox)j).Bounds))
                        {
                            playerHp -= 5;
                            if (playerHp <= 0)
                            {
                                //Application.Restart(); // gör om till äkta restart
                            }
                            playerHealth.Value = Convert.ToInt32(playerHp);

                            playerBox.BackColor = Color.Red;
                        }
                        else
                        {
                            playerBox.BackColor = Color.Transparent;
                        }
                    }


                }

            }
            if (!conversationsBoolean[0] && currentMap == 0)
            {
                ConversationLabel.Visible = true;
                timer1.Start();
            }

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

            if ((Math.Sqrt(Math.Pow((30 - playerBox.Location.X), 2) + Math.Pow((120 - playerBox.Location.Y), 2)) < 50) &&  currentMap == 1)
            {
                gameTimer.Interval = 2000;
                ConversationLabel.Visible = true;
                switch (conversations[1]++)
                {
                    case 0:
                        ConversationLabel.Text = "I need to get the fuck out of here!";
                        break;
                    case 1:
                            ConversationLabel.Text = "I need to get fucking out of here";
                        break;
                    case 2:
                        ConversationLabel.Text = "Yuumi iam cooming for u waifu. oWo.";
                        break;
                    case 3:
                        {
                            {
                                Monster m = new Monster();
                                m.Tag = "zombie1";
                                m.Image = Properties.Resources.zombieL;
                                m.Location = new Point(500,50);
                                m.BackColor = Color.Transparent;
                                monsters.Add(m);
                                this.Controls.Add(m);
                            }
                            ConversationLabel.Text = "What the hell is happening in here!!!";
                        }
                        
                        break;
                }
                if (conversations[1] == 4)
                {
                    ConversationLabel.Visible = false;
                    conversations[1] = 0;
                    gameTimer.Interval = 10;
                    playerBox.Location = new Point(playerBox.Location.X+50, playerBox.Location.Y);
                }
            }
        }
    }
}