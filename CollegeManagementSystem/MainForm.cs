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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        DataBase Con = new DataBase();

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Student Std = new Student();
            Std.Show();
            this.Hide();
        }

        private void guna2CircleButton4_Click(object sender, EventArgs e)
        {
            Fees payment = new Fees();
            payment.Show();
            this.Hide();
        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {
            Department dep = new Department();
            dep.Show();
            this.Hide();
        }

        private void guna2CircleButton5_Click(object sender, EventArgs e)
        {
            UserForm user = new UserForm();
            user.Show();
            this.Hide();
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            Teacher th = new Teacher();
            th.Show();
            this.Hide();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            Con.con.Open();
            //StudentTbl
            SqlDataAdapter sda1 = new SqlDataAdapter("select count(*) from StudentTbl", Con.con);
            DataTable dt = new DataTable();
            sda1.Fill(dt);
            Std.Text = dt.Rows[0][0].ToString();
            //FeesTbl
            SqlDataAdapter sda2 = new SqlDataAdapter("select count(*) from FeesTbl", Con.con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            Fees1.Text = dt2.Rows[0][0].ToString();
            //TeacherTbl
            SqlDataAdapter sda3 = new SqlDataAdapter("select count(*) from TeacherTbl", Con.con);
            DataTable dt3 = new DataTable();
            sda3.Fill(dt3);
            Teac.Text = dt3.Rows[0][0].ToString();
            //DepartmentTbl
            SqlDataAdapter sda4 = new SqlDataAdapter("select count(*) from DepartmentTbl", Con.con);
            DataTable dt4 = new DataTable();
            sda4.Fill(dt4);
            Dep.Text = dt4.Rows[0][0].ToString();
            //UserTbl
            SqlDataAdapter sda5 = new SqlDataAdapter("select count(*) from UserTbl", Con.con);
            DataTable dt5 = new DataTable();
            sda5.Fill(dt5);
            user.Text = dt5.Rows[0][0].ToString();
            Con.con.Close();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }
    }
}
