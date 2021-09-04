using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        private Label labelscore;
        private int score = 0;
        private int rI, rJ;
        private PictureBox[] snake = new PictureBox[400];
        private PictureBox fruit;
        private int dirx, diry;
        private int _width = 900;
        private int _height = 800;
        private int _sizeofside = 40;
        public Form1()
        {

            InitializeComponent();
            this.Text = "Snake";
            this.Width = _width;
            this.Height = _height;
            labelscore = new Label();
            labelscore.Text = "Score 0";
            labelscore.Location = new Point(810, 10);
            this.Controls.Add(labelscore);
            dirx = 1;
            diry = 0;
            snake[0] = new PictureBox();
            snake[0].Location = new Point(200, 200);
            snake[0].Size = new Size(_sizeofside, _sizeofside);
            snake[0].BackColor = Color.Red;
            this.Controls.Add(snake[0]);
            this.KeyDown += new KeyEventHandler(OKP);
            GenerateMap();

            fruit = new PictureBox();
            fruit.BackColor = Color.Yellow;
            fruit.Size = new Size(_sizeofside, _sizeofside);
            Generatefruit();
            timer1.Tick += new EventHandler(Update);
            timer1.Interval = 50;
            timer1.Start();
        }
        private void Eatfruit()
        {
            if (snake[0].Location.X == rI * _sizeofside && snake[0].Location.Y == rJ * _sizeofside)
            {
                labelscore.Text = "Score" + ++score;
                snake[score] = new PictureBox();
                snake[score].Location = new Point(snake[score - 1].Location.X + 40 * dirx, snake[score - 1].Location.Y + 40 * diry);
                snake[score].Size = new Size(_sizeofside, _sizeofside);
                snake[score].BackColor = Color.Green;
                this.Controls.Add(snake[score]);
                Generatefruit();
            }

        }
        private void Outofborder()
        {
            if (snake[0].Location.X < 0 || snake[0].Location.X > 770 || snake[0].Location.Y < 0 || snake[0].Location.Y > 770)
            {
                timer1.Stop();
                MessageBox.Show("Game Over!!(out of border) score="+score);
                while (score != 0)
                {
                    this.Controls.Remove(snake[score]);
                    score--;
                }
                
                labelscore.Text = "Score " + score;
                snake[0].Location = new Point(200, 200);
                timer1.Start();
            }
        }
            private void Movesnake()
            {
            Outofborder();

                Eatitself();
                for (int i = score; i >= 1; i--)
                {
                    snake[i].Location = snake[i - 1].Location;
                }
                snake[0].Location = new Point(snake[0].Location.X + dirx * (_sizeofside), snake[0].Location.Y + diry * (_sizeofside));
            }
            private void Eatitself()
            {
                for (int i = 1; i < score; i++)
                {
                    if (snake[0].Location == snake[i].Location)
                    {
                        for (int j = i; j <= score; j++)
                            this.Controls.Remove(snake[j]);
                        score -= score - i + 1;
                        labelscore.Text = "Score " + score;
                    }
                }
            }
            private void Generatefruit()
            {
                Random r = new Random();
                rI = r.Next(0, 20);

                rJ = r.Next(0, 20);

                fruit.Location = new Point(rI * _sizeofside, rJ * _sizeofside);
                this.Controls.Add(fruit);
            }
            private void GenerateMap()
            {
                for (int i = 0; i < _width / _sizeofside; i++)
                {
                    PictureBox pic = new PictureBox();
                    pic.BackColor = Color.Black;
                    pic.Size = new Size(_width - 100, 1);
                    pic.Location = new Point(0, _sizeofside * i);
                    this.Controls.Add(pic);
                }

                for (int i = 0; i <= _height / _sizeofside; i++)
                {
                    PictureBox pic = new PictureBox();
                    pic.BackColor = Color.Black;
                    pic.Size = new Size(1, _width);
                    pic.Location = new Point(_sizeofside * i, 0);
                    this.Controls.Add(pic);
                }
            }
            private void Update(object sender, EventArgs e)
            {
                Eatfruit();
                Movesnake();
            }
            private void OKP(object sender, KeyEventArgs e)
            {
                switch (e.KeyCode.ToString())
                {
                    case "Right":
                        dirx = 1;
                        diry = 0;
                        break;
                    case "Left":
                        dirx = -1;
                        diry = 0;
                        break;
                    case "Up":
                        diry = -1;
                        dirx = 0;
                        break;
                    case "Down":
                        diry = 1;
                        dirx = 0;
                        break;
                }
            }


        }
    }

