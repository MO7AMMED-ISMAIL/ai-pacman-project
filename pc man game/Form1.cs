using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pc_man_game
{
    public partial class Form1 : Form
    {
        bool goup, godown, goright, goleft,isGameOver;
        int score, playerSpeed, redGhostSpeed, yollowGhostSpeed,pinkGhostX,pinkGhostY;


        public Form1()
        {
            InitializeComponent();
            resetGame();
        }

        private void pictureBox34_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void yollowGhost_Click(object sender, EventArgs e)
        {

        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                goup = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }

        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                goup = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                resetGame();
                pacman.Left = 30;
                pacman.Top = 30;
            }
        }

        private void maingametimer(object sender, EventArgs e)
        {
            texscore.Text = "score " + score;

            if (goleft == true)
            {
                pacman.Left -= playerSpeed;
                pacman.Image = Properties.Resources.left;
            }
            if (goright == true)
            {
                pacman.Left += playerSpeed;
                pacman.Image = Properties.Resources.right;
            }
            if (godown == true)
            {
                pacman.Top += playerSpeed;
                pacman.Image = Properties.Resources.down;
            }
            if (goup == true)
            {
                pacman.Top -= playerSpeed;
                pacman.Image = Properties.Resources.Up;
            }
            //hight
            if (pacman.Left < -10)
            {
                pacman.Left = 680;
            }
            if (pacman.Left > 680)
            {
                pacman.Left = -10;
            }
            //width
            if (pacman.Top <-10)
            {
                pacman.Top = 550; ;
            }
            if (pacman.Top >550)
            {
                pacman.Top = 0; ;
            }


            //coin 
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox)
                {
                    if ((string)x.Tag == "coin" && x.Visible == true)
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            score++;
                            x.Visible = false;
                        }
                    }
                }
                // لو خبط في العمود
                if ((string)x.Tag == "wall")
                {
                    if (pacman.Bounds.IntersectsWith(x.Bounds))
                    {
                        //run gameover
                        gameOver("You lose !");
                    }
                    if (pinkGhost.Bounds.IntersectsWith(x.Bounds))
                    {
                        pinkGhostX = -pinkGhostX;
                    }
                }
                // لو خبط في العفريت
                if ((string)x.Tag == "ghost" && pacman.Bounds.IntersectsWith(x.Bounds))
                {
                    //run gameover
                    gameOver("You lose !");
                }
            }


            //moving ghost
            //red
            redGhost.Left += redGhostSpeed;
            if(
                redGhost.Bounds.IntersectsWith(pictureBox1.Bounds)||
                redGhost.Bounds.IntersectsWith(pictureBox2.Bounds)
              )
            {
                redGhostSpeed = -redGhostSpeed;
            }

            //yollow
            yollowGhost.Left += yollowGhostSpeed;
            if (
                yollowGhost.Bounds.IntersectsWith(pictureBox3.Bounds) ||
                yollowGhost.Bounds.IntersectsWith(pictureBox4.Bounds)
              )
            {
                yollowGhostSpeed = -yollowGhostSpeed;
            }

            //pinkghost
            pinkGhost.Left -= pinkGhostX;
            pinkGhost.Top -= pinkGhostY;

            if (pinkGhost.Top < 0 || pinkGhost.Top > 520)
            {
                pinkGhostY = -pinkGhostY;
            }
            if (pinkGhost.Left < 0 || pinkGhost.Left > 620) {
                pinkGhostX = -pinkGhostX;
            }
            




            if (score == 60)
            {
                //run winner
                gameOver("you win !!");
            }


        }
        private void resetGame()
        {
            texscore.Text = "score: 0";
            score = 0;

            isGameOver = false;

            redGhostSpeed = 5;
            yollowGhostSpeed = 5;
            pinkGhostX = 5;
            pinkGhostY = 5;
            playerSpeed = 8;



            redGhost.Left = 237;
            redGhost.Top = 55;

            yollowGhost.Left = 370;
            yollowGhost.Top = 350;

            pinkGhost.Left = 460;
            pinkGhost.Top = 190;
            

            foreach(Control x in this.Controls)
            {
                x.Visible = true;
            }

            timerGame.Start();

        }
        private void gameOver(string massege)
        {

            isGameOver = true;
            timerGame.Stop();
            texscore.Text = "score : "+score+ Environment.NewLine + massege; 


        }
    }
}
