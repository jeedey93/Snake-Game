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
        Timer gameLoop= new Timer();
        Timer snakeLoop = new Timer();

        public Snake()
        {
            InitializeComponent();
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

        }

        private void GameOver() 
        { 

        }

        private void Update() 
        { 

        }

        private void Draw(Graphics graphics) 
        { 

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

    }
}
