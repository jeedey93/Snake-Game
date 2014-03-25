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

        public int score = 0;
        public bool gameOver = false;
        List<SnakePart> snake = new List<SnakePart>();
        SnakePart food;
        int direction = 0;
        Timer gameLoop= new Timer();
        Timer snakeLoop = new Timer();

        public Snake()
        {
            InitializeComponent();
            gameLoop.Tick += new EventHandler(Update);
            gameLoop.Tick += new EventHandler(UpdateSnake);
            gameLoop.Interval = 1000 / 60;
            snakeLoop.Interval = 1000 / 10;
            gameLoop.Start();
            snakeLoop.Start();
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
            score = 0;
            SnakePart head = new SnakePart(10,8);
            snake.Add(head);
            GenerateFood();
        }

        private void GameOver() 
        { 

        }

        private void Update(object sender, EventArgs e) 
        {
            if (gameOver)
            { }
            else 
            {
                if (Input.Press(Keys.Left))
                {
                    if (snake.Count < 2 || snake[0].X == snake[1].X)
                        direction = 1;
                }
                else if (Input.Press(Keys.Right))
                {
                    if (snake.Count < 2 || snake[0].X == snake[1].X)
                        direction = 2;
                }
                else if (Input.Press(Keys.Up))
                {
                    if (snake.Count < 2 || snake[0].Y == snake[1].Y)
                        direction = 3;
                }
                else if (Input.Press(Keys.Down))
                {
                    if (snake.Count < 2 || snake[0].X == snake[1].X)
                        direction = 0;
                }
            }
                 
        }

        private void UpdateSnake(object sender, EventArgs e) 
        {
            if (!gameOver)
            {

            }
        }

        private void GenerateFood() 
        {
            Random random = new Random();
            food = new SnakePart(random.Next(0, 20), random.Next(0, 15));
        }

        private void Draw(Graphics canvas) 
        {
            Font font = this.Font;
            if (gameOver) 
            {
                SizeF message = canvas.MeasureString("GameOver", font);
                canvas.DrawString("GameOver", font, Brushes.White, new PointF(160 - message.Width / 2, 120));
                message = canvas.MeasureString("Final Score" + score.ToString(), font);
                canvas.DrawString("Final Score " + score.ToString(), font, Brushes.White, new PointF(160 - message.Width / 2, 140));
                message = canvas.MeasureString("Press Enter to start a new Game", font);
                canvas.DrawString("Press enter to start a new game", font, Brushes.White, new PointF(160 - message.Width / 2, 160));
            }
            else 
            {
                canvas.DrawString("Score " + score.ToString(), font, Brushes.Black, new Point(4, 4));
                for(int i=0; i<snake.Count; i++)
                {
                    Color snake_color = i == 0 ? Color.Red : Color.Black;
                    SnakePart currentpart= snake[i];
                    canvas.FillRectangle(new SolidBrush(snake_color), new Rectangle(currentpart.X=16, currentpart.Y=16,16,16));
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {




        }
    }
}
