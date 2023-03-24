using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace treasure
{
    public partial class Form1 : Form
    {
        private bool goUp, goDown, goRight, goLeft, gameOver;
        private int Speed = Chest.Speed;
        private int points1 = Chest.points1;
        private int points2 = Chest.points2;
        private int timeleft = Chest.timeleft;
        private int score = Chest.score;
        private int indchesttime = Chest.indchesttime;
        private int chestcount = Chest.chestcount;
        private int chesttime = Chest.chesttime;
        private int chestOffSetY = Chest.chestOffSetY;
        private int chestOffSetX = Chest.chestOffSetX;

        Random random = new Random();
        List<PictureBox> chestlist = new List<PictureBox>();

        public Form1()
        {

            InitializeComponent();
            Init();
            MainTimer.Start();
            GameTimer.Start();
            ChestTimer.Start();
        }

        public void Init()
        {
            this.Width = 500;
            this.Height = 500;
            SpawnChest();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (goLeft == true && mc.Left > 0 && gameOver == false)
            {
                mc.Left -= Speed;
            }
            if (goRight == true && mc.Left + mc.Width < this.ClientSize.Width && gameOver == false)
            {
                mc.Left += Speed;
            }
            if (goUp == true && mc.Top > 0 && gameOver == false)
            {
                mc.Top -= Speed;
            }
            if (goDown == true && mc.Top + mc.Height < this.ClientSize.Height && gameOver == false)
            {
                mc.Top += Speed;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "Wall")
                    {
                        if (goLeft == true && mc.Bounds.IntersectsWith(x.Bounds))
                        {
                            mc.Left = x.Left + mc.Width;
                        }
                        if (goRight == true && mc.Bounds.IntersectsWith(x.Bounds))
                        {
                            mc.Left = x.Left - mc.Width;
                        }
                        if (goUp == true && mc.Bounds.IntersectsWith(x.Bounds))
                        {
                            mc.Top = x.Top + mc.Height;
                        }
                        if (goDown == true && mc.Bounds.IntersectsWith(x.Bounds))
                        {
                            mc.Top = x.Top - mc.Height;
                        }
                    }

                    if ((string)x.Tag == "Chest1" || (string)x.Tag == "Chest2")
                    {
                        if (mc.Bounds.IntersectsWith(x.Bounds) && gameOver == false)
                        {
                            this.Controls.Remove(x);
                            ((PictureBox)x).Dispose();
                            if ((string) x.Tag == "Chest1")
                                score += points1;
                            else
                                score += points2;
                            Points.Text = score.ToString();
                            SpawnChest();
                        }  
                    }
                }
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (timeleft > 0)
            {
                timeleft -= 1;
                Timer.Text = timeleft + " seconds";
                indchesttime += 1;
                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox)
                    {
                        if ((string)x.Tag == "Chest1" || (string)x.Tag == "Chest2")
                        {
                            if (indchesttime % 15 == 0)
                            {
                                this.Controls.Remove(x);
                                ((PictureBox)x).Dispose();
                            }
                                
                        }
                    }
                }
            }
            else
            {
                gameOver = true;
                Over.Text = "TIME'S UP!";
                PressToReset.Text = "press SPACE to restart";
                GameTimer.Stop();
                ChestTimer.Stop();
            }
        }


        private void ChestTimer_Tick(object sender, EventArgs e)
        {
            chesttime += 1;
            if (chesttime % 5 == 0)
                SpawnChest();
        }

        private void NotPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
        }

        private void OnPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Space)
            {
                ResetGame();
            }
        }

        
        private void SpawnChest()
        {
            int chestX = random.Next(1, Data.MapSize - 2);
            int chestY = random.Next(1, Data.MapSize - 2);
            bool isempty = Chest.IsEmpty(chestX, chestY);
            
            if (isempty == true)
            {
                PictureBox chest = new PictureBox();
                if (chestcount % 5 != 0)
                {
                    chest.Image = Properties.Resources.chest_1;
                    chest.Tag = "Chest1";
                }
                else
                {
                    chest.Image = Properties.Resources.chest_2;
                    chest.Tag = "Chest2";
                }
                chest.Width = Chest.Width;
                chest.Height = Chest.Height;
                chest.BackColor = Color.Transparent;
                chest.Left = chestOffSetY + ((chestY) * Data.Cell);
                chest.Top = chestOffSetX + ((chestX) * Data.Cell);
                chestlist.Add(chest);
                this.Controls.Add(chest);
                mc.BringToFront();
                chestcount += 1;

                Timer IndChestTimer = new Timer();
                IndChestTimer.Interval = 1000;
                IndChestTimer.Start();
                indchesttime += 1;
            }
            else 
            {
                SpawnChest();
            }

        }  
        
        private void ResetGame()
        {
            foreach (PictureBox i in chestlist)
            {
                this.Controls.Remove(i);
            }
            chestlist.Clear();
            Over.Text = " ";
            PressToReset.Text = " ";
            SpawnChest();
            mc.Left = 73;
            mc.Top = 53;
            goUp = false;
            goDown = false;
            goLeft = false;
            goRight = false;
            gameOver = false;

            timeleft = Chest.timeleft;
            score = Chest.score;
            Points.Text = score.ToString();
            GameTimer.Start();
        }
    }
}
