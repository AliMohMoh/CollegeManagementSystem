using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CollegeManagementSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Variable for looked timer1
        int startpos = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            //a plus the ProgressBar =2
            startpos += 2;
            ProgressBar.Value = startpos;

            //If it the end 100, it returns to 0 and stops
            //and go the form Login
            if (ProgressBar.Value == 100)
            {
                ProgressBar.Value = 0;
                timer1.Stop();

                Login log = new Login();
                log.Show();
                this.Hide();
            }
            //end if
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
