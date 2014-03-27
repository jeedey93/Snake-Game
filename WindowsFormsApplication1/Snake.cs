using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Snake : Form
    {

        public int score;
        public bool gameOver = false;
        List<SnakePart> snake = new List<SnakePart>();
        SnakePart food;
        SnakePart obstacle;
        List<SnakePart> obstacles = new List<SnakePart>();
        int direction = 0; // Down = 0, Left = 1, Right = 2, Up = 3
        Timer gameLoop = new Timer();
        Timer snakeLoop = new Timer();

        public Snake()
        {
            InitializeComponent();
            gameLoop.Interval = 100;
            snakeLoop.Interval = 100;
            gameLoop.Tick += new EventHandler(Update);
            gameLoop.Tick += new EventHandler(UpdateSnake);
            gameLoop.Start();
            snakeLoop.Start();
            StartGame();
        }

        private void Snake_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void Snake_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
        }

        private void StartGame()
        {
            gameOver = false;
            snake.Clear();
            obstacles.Clear();
            score = 0;
            SnakePart head = new SnakePart(10, 8);
            snake.Add(head);
            GenerateFood();
            GenerateObstacle(snake.Count);
        }

        private void GameOver()
        {
            gameOver = true;
        }

        private void Update(object sender, EventArgs e)
        {
            if (gameOver)
            {
                if (Input.Press(Keys.Enter))
                    StartGame();
            }
            else
            {
                if (Input.Press(Keys.Left) && direction != 2)
                {
                    if (snake.Count < 20 || snake[0].X == snake[1].X)
                        direction = 1;
                }
                else if (Input.Press(Keys.Right) && direction != 1)
                {
                    if (snake.Count < 20 || snake[0].X == snake[1].X)
                        direction = 2;
                }
                else if (Input.Press(Keys.Up) && direction != 0)
                {
                    if (snake.Count < 20 || snake[0].Y == snake[1].Y)
                        direction = 3;
                }
                else if (Input.Press(Keys.Down) && direction != 3)
                {
                    if (snake.Count < 20 || snake[0].Y == snake[1].Y)
                        direction = 0;
                }
            }
            pbCanvas.Invalidate();
        }

        private void UpdateSnake(object sender, EventArgs e)
        {
            if (!gameOver)
            {
                for (int i = snake.Count - 1; i >= 0; i--)
                {
                    if (i == 0)
                    {
                        switch (direction)
                        {
                            case 0: // Down
                                snake[i].Y++;
                                break;
                            case 1: // Left
                                snake[i].X--;
                                break;
                            case 2: // Right
                                snake[i].X++;
                                break;
                            case 3: // Up
                                snake[i].Y--;
                                break;
                        }
                        SnakePart head = snake[0];

                        // Out of Bound
                        //if (head.X >= 20 || head.X < 0 || head.Y >= 15 || head.Y < 0)
                        //GameOver();
                        if (head.X >= 20)
                            head.X = 0;
                        if (head.X < 0)
                            head.X = 20;
                        if (head.Y >= 15)
                            head.Y = 0;
                        if (head.Y < 0)
                            head.Y = 15;

                        // Collision with itself
                        for (int j = 1; j < snake.Count; j++)
                        {
                            if (head.X == snake[j].X && head.Y == snake[j].Y)
                                GameOver();
                        }

                        //Collision with obstacle
                        foreach (var collision in obstacles)
                        {
                            if (head.X == collision.X && head.Y == collision.Y)
                                GameOver();
                        }

                        // Collision with food
                        if (head.X == food.X && head.Y == food.Y)
                        {
                            SnakePart part = new SnakePart(snake[snake.Count - 1].X, snake[snake.Count - 1].Y);
                            snake.Add(part);
                            score++;
                            GenerateFood();
                            GenerateObstacle(snake.Count);
                        }
                    }
                    else
                    {
                        snake[i].X = snake[i - 1].X;
                        snake[i].Y = snake[i - 1].Y;
                    }
                }
            }
        }

        private void GenerateObstacle(int obstacleNumber)
        {

            //var newObstacle = new SnakePart(obstacleNumber, obstacleNumber);
            Random randomObstacle = new Random();
            var newObstacle = new SnakePart(randomObstacle.Next(0, 10), randomObstacle.Next(0, 10));
            obstacles.Add(newObstacle);
        }
        private void GenerateFood()
        {
            Random randomFood = new Random();
            food = new SnakePart(randomFood.Next(0, 20), randomFood.Next(0, 15));
        }

        private void Draw(Graphics canvas)
        {
            Font font = this.Font;
            if (gameOver)
            {
                SizeF message = canvas.MeasureString("GameOver", font);
                canvas.DrawString("GameOver", font, Brushes.White, new PointF(160 - message.Width / 2, 100));
                message = canvas.MeasureString("Final Score" + score.ToString(), font);
                canvas.DrawString("Final Score " + score.ToString(), font, Brushes.White, new PointF(160 - message.Width / 2, 120));
                message = canvas.MeasureString("Press Enter to start a new Game", font);
                canvas.DrawString("Press enter to start a new game", font, Brushes.White, new PointF(160 - message.Width / 2, 140));
            }
            else
            {
                canvas.DrawString("Score " + score.ToString(), font, Brushes.White, new Point(4, 4));
                canvas.FillRectangle(new SolidBrush(Color.Orange), new Rectangle(food.X * 16, food.Y * 16, 16, 16));
                foreach (var single in obstacles)
                {
                    canvas.FillRectangle(new SolidBrush(Color.Red), new Rectangle(single.X * 16, single.Y * 16, 16, 16));
                }
                for (int i = 0; i < snake.Count; i++)
                {
                    Color snake_color = i == 0 ? Color.Green : Color.Black;
                    SnakePart currentpart = snake[i];
                    canvas.FillRectangle(new SolidBrush(snake_color), new Rectangle(currentpart.X * 16, currentpart.Y * 16, 16, 16));
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new About()).ShowDialog();
        }
    }
}
