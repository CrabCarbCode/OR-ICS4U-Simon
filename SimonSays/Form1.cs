using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Media;
using System.Drawing.Drawing2D;

namespace SimonSays
{
    public partial class Form1 : Form
    {
        
        //TODO: create a List to store the pattern. Must be accessable on other screens
        public static List<int> colourList = new List<int>();
        public static int score = 0;

        public Form1()
        {
            InitializeComponent();
        }

        public static void ChangeScreen(object sender, UserControl nextScreen) { 
            Form f; // will either be the sender or parent of sender 

            if (sender is Form) {
                f = (Form)sender;
            } else {
                UserControl current = (UserControl)sender;
                f = current.FindForm();
                f.Controls.Remove(current);
            }

            nextScreen.Location = new Point((f.ClientSize.Width - nextScreen.Width) / 2, (f.ClientSize.Height - nextScreen.Height) / 2);

            f.Controls.Add(nextScreen);
            nextScreen.Focus();
        }

    private void Form1_Load(object sender, EventArgs e)
        {
            ChangeScreen(this, new MenuScreen());
        }
    }
}
