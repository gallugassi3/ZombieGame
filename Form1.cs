﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using WMPLib;

namespace ZombieApp
{
    public partial class Form1 : Form
    {
        WindowsMediaPlayer player1 = new WindowsMediaPlayer();
        SoundPlayer player2;
        SpeedyZombie character;
        SpeedyZombie fighter;
        Zombie zombie;
        bool goLeft, goRight, goUp, goDown, gameOver;
        string facing = "right";
        int playerHealth = 100;
        int speed = 10;
        int Ammo = 10;
        int Score;
        int currentLevel = 1; // משתנה למעקב אחרי השלב הנוכחי
        int zombieHealth = 100; // כמות החיים ההתחלתית של זומבי
        Random randNum = new Random();

        List<PictureBox> zombiesList = new List<PictureBox>();
        Dictionary<PictureBox, int> zombieHealthDict = new Dictionary<PictureBox, int>(); // מיפוי חיים לכל זומבי

        public Form1()
        {
            InitializeComponent();
            player1.URL = "Game Of Thrones.mp3";
            player2 = new SoundPlayer("sound-shot.wav");
            RestartGame();
            character = new SpeedyZombie();
            fighter = new SpeedyZombie();

            healthBar.Minimum = 0;
            healthBar.Maximum = 100;
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            CollectAmmo();

            if (character.Health >= healthBar.Minimum && character.Health <= healthBar.Maximum)
            {
                healthBar.Value = character.Health;
            }
            else if (character.Health < healthBar.Minimum)
            {
                healthBar.Value = healthBar.Minimum;
            }
            else if (character.Health > healthBar.Maximum)
            {
                healthBar.Value = healthBar.Maximum;
            }

            if (character.Health > 1)
            {
                healthBar.Value = character.Health;
            }
            else
            {
                gameOver = true;
                player.Image = Properties.Resources.dead;
                GameTimer.Stop();
                ShowGameOverDialog();
            }

            txtAmmo.Text = "Ammo: " + Ammo;
            txtScore.Text = "Kills: " + Score;

            if (goLeft && player.Left > 0)
            {
                player.Left -= character.Speed;
            }
            if (goRight && player.Left + player.Width < ClientSize.Width)
            {
                player.Left += speed;
            }
            if (goUp && player.Top > 45)
            {
                player.Top -= speed;
            }
            if (goDown && player.Top + player.Height < ClientSize.Height)
            {
                player.Top += speed;
            }

            foreach (PictureBox zombie in zombiesList)
            {
                if (player.Bounds.IntersectsWith(zombie.Bounds))
                {
                    character.Health -= 1;
                }

                zombie.Left -= character.Speed;

                if (zombie.Left < 1)
                {
                    zombie.Left = ClientSize.Width + randNum.Next(100, 800);
                    zombie.Top = randNum.Next(60, ClientSize.Height - zombie.Height - 60);
                }
            }

            if (character.Health < 1)
            {
                gameOver = true;
                GameTimer.Stop();
                ShowGameOverDialog();
            }

            if (Score >= 6)
            {
                GameTimer.Stop();
                currentLevel++; // העלאת השלב

                if (currentLevel > 5) // אם הגענו לשלב 6, סיימנו את המשחק
                {
                    MessageBox.Show("You have defeated all the zombies and completed the game!", "Game Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RestartGame();
                }
                else
                {
                    MessageBox.Show($"Congratulations! You completed level {currentLevel - 1}. Moving to level {currentLevel}!", "Level Up", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RestartGame();
                }
            }
        }

        private void ShowGameOverDialog()
        {
            DialogResult result = MessageBox.Show("You died. Do you want to restart the game from level 1?", "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                currentLevel = 1; // כאשר מתים, חוזרים תמיד לשלב 1
                RestartGame();
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (gameOver)
            {
                return;
            }

            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
                facing = "left";
                player.Image = Properties.Resources.left;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
                facing = "right";
                player.Image = Properties.Resources.right;
            }

            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
                facing = "up";
                player.Image = Properties.Resources.up;
            }

            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
                facing = "down";
                player.Image = Properties.Resources.down;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
            if (e.KeyCode == Keys.Space && Ammo > 0 && !gameOver)
            {
                player2.Play();
                Ammo--;
                ShootBullet(facing);
                if (Ammo < 1)
                {
                    DropAmmo();
                }
            }
        }

        private void ShootBullet(string direction)
        {
            PictureBox bullet = new PictureBox();
            bullet.SizeMode = PictureBoxSizeMode.AutoSize;
            bullet.BackColor = Color.Gold;
            bullet.Size = new Size(10, 3);
            bullet.Tag = "bullet";
            bullet.Left = player.Left + (player.Width / 2);
            bullet.Top = player.Top + (player.Height / 2);
            Controls.Add(bullet);
            bullet.BringToFront();

            switch (direction)
            {
                case "left":
                    bullet.Left -= 20;
                    break;
                case "right":
                    bullet.Left += 20;
                    break;
                case "up":
                    bullet.Top -= 20;
                    break;
                case "down":
                    bullet.Top += 20;
                    break;
            }

            Timer bulletTimer = new Timer();
            bulletTimer.Interval = 20;
            bulletTimer.Tick += (sender, e) =>
            {
                if (direction == "left" && bullet.Left > 0)
                {
                    bullet.Left -= 20;
                }
                else if (direction == "right" && bullet.Left + bullet.Width < ClientSize.Width)
                {
                    bullet.Left += 20;
                }
                else if (direction == "up" && bullet.Top > 0)
                {
                    bullet.Top -= 20;
                }
                else if (direction == "down" && bullet.Top + bullet.Height < ClientSize.Height)
                {
                    bullet.Top += 20;
                }
                else
                {
                    Controls.Remove(bullet);
                    bulletTimer.Stop();
                }

                foreach (PictureBox zombie in zombiesList)
                {
                    if (bullet.Bounds.IntersectsWith(zombie.Bounds))
                    {
                        zombieHealthDict[zombie] -= 20;
                        Controls.Remove(bullet);
                        bulletTimer.Stop();

                        if (zombieHealthDict[zombie] <= 0)
                        {
                            Controls.Remove(zombie);
                            zombiesList.Remove(zombie);
                            Score++;
                            break;
                        }
                    }
                }
            };

            bulletTimer.Start();

            if (Ammo < 1)
            {
                DropAmmo();
            }
        }

        private void MakeZombies()
        {
            PictureBox zombiePictureBox = new PictureBox();
            zombiePictureBox.Tag = "zombie";
            zombiePictureBox.Image = Properties.Resources.zleft;
            zombiePictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            zombiePictureBox.Left = ClientSize.Width + randNum.Next(100, 700);
            zombiePictureBox.Top = randNum.Next(20, ClientSize.Height - zombiePictureBox.Height);
            zombiesList.Add(zombiePictureBox);
            zombieHealthDict[zombiePictureBox] = zombieHealth;
            Controls.Add(zombiePictureBox);
            zombiePictureBox.BringToFront();
        }

        private void DropAmmo()
        {
            PictureBox ammo = new PictureBox();
            ammo.Image = Properties.Resources.ammo;
            ammo.SizeMode = PictureBoxSizeMode.AutoSize;
            ammo.Left = randNum.Next(10, ClientSize.Width - ammo.Width);
            ammo.Top = randNum.Next(60, ClientSize.Height - ammo.Height);
            ammo.Tag = "ammo";
            Controls.Add(ammo);
            ammo.BringToFront();
            player.BringToFront();
        }

        private void CollectAmmo()
        {
            foreach (Control item in Controls)
            {
                if (item is PictureBox && item.Tag == "ammo")
                {
                    if (player.Bounds.IntersectsWith(item.Bounds))
                    {
                        Controls.Remove(item);
                        ((PictureBox)item).Dispose();
                        Ammo += 5;
                    }
                }
            }
        }

        private void RestartGame()
        {
            character = new SpeedyZombie();
            zombieHealth = currentLevel * 50;
            player.Image = Properties.Resources.right;
            foreach (PictureBox zombie in zombiesList)
            {
                Controls.Remove(zombie);
            }
            zombiesList.Clear();
            zombieHealthDict.Clear();

            for (int i = 0; i < currentLevel * 6; i++)
            {
                MakeZombies();
            }

            goLeft = false;
            goRight = false;
            goUp = false;
            goDown = false;
            gameOver = false;

            playerHealth = 100;
            character.Health = 100;
            speed = 10;
            Ammo = 10;
            Score = 0;

            player.Left = 100;
            player.Top = 100;

            healthBar.Value = playerHealth;
            txtAmmo.Text = "Ammo: " + Ammo;
            txtScore.Text = "Kills: " + Score;

            GameTimer.Start();
        }
    }
}
