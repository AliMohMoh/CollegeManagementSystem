using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CollegeManagementSystem
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        DataBase Con = new DataBase();

        private void button1_Click(object sender, EventArgs e)
        {
            /*MainForm Mform = new MainForm();
            Mform.Show();
            this.Hide();*/

            MainForm Home = new MainForm();
            Con.con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from UserTbl where UserName='"+UserName.Text+"'and password='"+pass.Text+"'",Con.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            //check if user and password in table or not 
            if (dt.Rows[0][0].ToString() == "1")
            {
                Home.Show();
                this.Hide();
                Con.con.Close();
            }
            else
            {
                MessageBox.Show("Wrong UserName or Password.");
            }
            //end if
            Con.con.Close();

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox1.Checked==true)
            {
                pass.isPassword = false;
            }
            else
            {
                pass.isPassword = true;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            UserName.Text = "";
            pass.Text = "";
        }
    }
}
