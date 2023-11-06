using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace autoClicker
{
    public partial class Form1 : Form
    {
        //These are DLLImports that are needed to force a MouseClick
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        //Variables that define what is used for the MouseClicks
        private const int LEFTUP = 0x0004;
        private const int LEFTDOWN = 0x0002;

        //Variable for the Intervals of the MouseClicks (5 being the default)
        public int intervals = 5;

        //bool that determines if the Program should click
        public new bool Click = false;
        public int parsedValue;


        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        //Code for the "Help" LinkLabel
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string messageHelp = "Use UpArrow to Start and DownArrow to Stop the AutoClicker. After a Setting has been changed, press the 'Set' Button to Validate it. To exit the App, end the Task in Task Manager.";
            string titleHelp = "How to do the Click";
            MessageBox.Show(messageHelp, titleHelp);
            return;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            //This adds "private void AutoClick" to the load void
            Thread AC = new Thread(AutoClick);

            //This will start the BackgroundWorker
            backgroundWorker1.RunWorkerAsync();
            
            //"AC" is the reference for the AutoClick Thread
            AC.Start();
        }

        private void AutoClick()
        {
            //while (true) means always true
            while (true)
            {
                //If the bool click is true then it will start Clicking
                if (Click == true)
                {
                    //This will create the MouseClick with LEFTUP then LEFTDOWN
                    mouse_event(dwFlags: LEFTUP, dx: 0, dy: 0, cButtons: 0, dwExtraInfo: 0);
                    
                    //The Sleep Method temporarily suspends the Thread for the specified Milliseconds
                    //inside of the ().
                    Thread.Sleep(1);
                    mouse_event(dwFlags: LEFTDOWN, dx: 0, dy: 0, cButtons: 0, dwExtraInfo: 0);
                    
                    //The same thing applies here, just that the milliseconds are specified in the Variable
                    //above, which is 5.
                    Thread.Sleep(intervals);
                }
                //This is here to Stop the CPU from Burning down.
                Thread.Sleep(2);
            }
        }

        //This Checks if any of the Hotkeys are Pressed
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //while (true) means always true
            while (true)
            {
                //If the checkBox "Enable" is checked then the Following Code runs
                if (checkBox1.Checked)
                {
                    //This Turns the AutoClicker off -> Keys.Down = Down Arrow
                    if (GetAsyncKeyState(Keys.Down)< 0)
                    {
                        Click = false;
                    }
                    //This Turns the AutoClicker on -> Keys.Up = Up Arrow
                    else if (GetAsyncKeyState(Keys.Up)< 0)
                    {
                        Click = true;
                    }
                    //Slows down the Program to stop the CPU from burning
                    Thread.Sleep(1);
                }
                //Same thing applies to here
                Thread.Sleep(1);
            }
            
        }

        //Code for the "Set"-Button
        private void button1_Click(object sender, EventArgs e)
        {
            //this checks if the entered Value in the Textbox is a Number or not.
            if (!int.TryParse(textBox1.Text, out parsedValue))
            {
                MessageBox.Show("Please Enter a Number.");
                return;
            }
            else
            {
                intervals = int.Parse(textBox1.Text);
            }
        }
    }
}
