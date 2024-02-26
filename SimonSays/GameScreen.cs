using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Drawing.Drawing2D;
using System.Threading;

namespace SimonSays
{
    public partial class GameScreen : UserControl
    {
        private int userGuessIndex = 0;

        public GameScreen()
        {
            InitializeComponent();
        }

        
        //checks if the selected colour matches the desired colour
        private bool isInputCorrect(int colour, int currIndex, List<int> colourList) {
            return (colour == colourList.ElementAt(currIndex)) ? true : false;
        }

        private void HandleInput(bool isInputCorrect) {
            if (!isInputCorrect) {
                GameOver();
            }

            userGuessIndex++;
            //if the current guess was the last item of the guessing list, re-run computer turn
            if (userGuessIndex >= (Form1.colourList.Count()))
            {
                ComputerTurn();
                Form1.score++;
            }
        }

        //highlights any given button
        //unused in this build as I'm not sure about sytnax for capturing / modifying the colour of a generic button, but its a better idea.
        private void BetterHighlightButton(Button button)
        {
            if (button == null) { //semi-unneccessary protection
                return;
            } else
            {
                //button.ForeColor += Color.FromArgb(); //add some rgb values to the current button to make it lighter
                Thread.Sleep(200);
                //button.ForeColor -= Color.FromArgb(); //de-highlight the button
            }
        }

        private void HighlightButton(int colour)
        {
            const int delayTime = 100;
            switch (colour)
                {
                    case 0: 
                        redButton.BackColor = Color.Red;
                        SoundPlayer rButtonSound = new SoundPlayer(Properties.Resources.red);
                        rButtonSound.Play();

                        Refresh();
                        Thread.Sleep(delayTime);
                        redButton.BackColor = Color.DarkRed;
                        break;
                    case 1: 
                        greenButton.BackColor = Color.Lime;
                        SoundPlayer gButtonSound = new SoundPlayer(Properties.Resources.green);
                        gButtonSound.Play();

                        Refresh();
                        Thread.Sleep(delayTime);
                        greenButton.BackColor = Color.ForestGreen;
                        break;
                    case 2: 
                        blueButton.BackColor = Color.LightBlue;
                        SoundPlayer bButtonSound = new SoundPlayer(Properties.Resources.blue);
                        bButtonSound.Play();

                        Refresh();
                        Thread.Sleep(delayTime);
                        blueButton.BackColor = Color.DarkBlue;
                        break;
                    case 3: 
                        yellowButton.BackColor = Color.Gold;
                        SoundPlayer yButtonSound = new SoundPlayer(Properties.Resources.yellow);
                        yButtonSound.Play();
                    
                        Refresh();
                        Thread.Sleep(delayTime);
                        yellowButton.BackColor = Color.DarkGoldenrod;
                        break;
                }
            
            Thread.Sleep(delayTime);
            Refresh();
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {
            Form1.colourList.Clear();
            Refresh();
            Thread.Sleep(1000); //I'm aware this is bad practice, but I'm rushing and don't care to find the syntax for better implementation.
                                //Will change later if I have time
            ComputerTurn();
        }

        private void ComputerTurn() {
            
            //graphic indicator that the computer turn has started
            redButton.BackColor = Color.White;
            greenButton.BackColor = Color.White;
            blueButton.BackColor = Color.White;
            yellowButton.BackColor = Color.White;

            Refresh();
            Thread.Sleep(200);

            redButton.BackColor = Color.DarkRed;
            greenButton.BackColor = Color.ForestGreen;
            blueButton.BackColor = Color.DarkBlue;
            yellowButton.BackColor = Color.DarkGoldenrod;

            Refresh();
            Thread.Sleep(200);


            Random rand = new Random();

            Form1.colourList.Add(rand.Next(0, 4));

            foreach (int currColour in Form1.colourList) {

                HighlightButton(currColour);
                Thread.Sleep(200);
            }

            userGuessIndex = 0;
        }

        public void GameOver() {
            SoundPlayer gameOverSound = new SoundPlayer(Properties.Resources.mistake);
            gameOverSound.Play();

            Form1.colourList.Clear();

            Form1.ChangeScreen(this, new GameOverScreen());

        }

        //TODO: create one of these event methods for each button
        private void greenButton_Click(object sender, EventArgs e)
        {
            HighlightButton(1);
            HandleInput(isInputCorrect(1, userGuessIndex, Form1.colourList));

        }
        private void redButton_Click(object sender, EventArgs e)
        {
            HighlightButton(0);
            HandleInput(isInputCorrect(0, userGuessIndex, Form1.colourList));
        }
        private void yellowButton_Click(object sender, EventArgs e)
        {
            HighlightButton(3);
            HandleInput(isInputCorrect(3, userGuessIndex, Form1.colourList));
        }
        private void blueButton_Click(object sender, EventArgs e)
        {
            HighlightButton(2);
            HandleInput(isInputCorrect(2, userGuessIndex, Form1.colourList));
        }
    }
}
