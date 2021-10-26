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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        DataBase Con = new DataBase();

        private void Dashboard_Load(object sender, EventArgs e)
        {
            Con.con.Open();
            SqlDataAdapter sda1 = new SqlDataAdapter("select count(*) from StudentTbl",Con.con);
            DataTable dt = new DataTable();
            sda1.Fill(dt);
            Std.Text = dt.Rows[0][0].ToString();
            SqlDataAdapter sda2 = new SqlDataAdapter("select count(*) from FeesTbl", Con.con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            Fees.Text = dt2.Rows[0][0].ToString();
            SqlDataAdapter sda3 = new SqlDataAdapter("select count(*) from TeacherTbl", Con.con);
            DataTable dt3 = new DataTable();
            sda3.Fill(dt3);
            Teac.Text = dt3.Rows[0][0].ToString();
            SqlDataAdapter sda4 = new SqlDataAdapter("select count(*) from DepartmentTbl", Con.con);
            DataTable dt4 = new DataTable();
            sda4.Fill(dt4);
            Dep.Text = dt4.Rows[0][0].ToString();
            Con.con.Close();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainForm home = new MainForm();
            home.Show();
            this.Hide();
        }
    }
}
