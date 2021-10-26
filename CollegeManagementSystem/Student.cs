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
    public partial class Student : Form
    {
        public Student()
        {
            InitializeComponent();
        }

        DataBase Con = new DataBase();

        //Function of fill Department ComboBox From the DepartmentTbl table 
        private void fillDepartment()
        {
            Con.con.Open();
            SqlCommand cmd = new SqlCommand("select DepName from DepartmentTbl", Con.con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("DepName", typeof(string));
            dt.Load(rdr);
            DepCb.ValueMember = "DepName";
            DepCb.DataSource = dt;
            Con.con.Close();
        }

        //A function of taking the names of columns with rows 
        private void populate()
        {
            Con.con.Open();
            string query = "select * from StudentTbl ";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con.con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            //StudentDGV
            StudentDGV.DataSource = ds.Tables[0];
            Con.con.Close();
        }

        //////////////////////Add//////////////////
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //check if empty or not
                if (StdId.Text== "" || StdName.Text == "" || StdPhone.Text == "" ||FeesTb.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {

                    Con.con.Open();
                    SqlCommand cmd = new SqlCommand("insert into StudentTbl values(@v1,@v2,@v3,@v4,@v5,@v6,@v7)", Con.con);
                    cmd.Parameters.AddWithValue("@v1", StdId.Text);
                    cmd.Parameters.AddWithValue("@v2", StdName.Text);
                    cmd.Parameters.AddWithValue("@v3", GenderCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@v4", Date.Text);
                    cmd.Parameters.AddWithValue("@v5", StdPhone.Text);
                    cmd.Parameters.AddWithValue("@v6", DepCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@v7", FeesTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Successfully Added.");
                    Con.con.Close();
                    //After the function works
                    populate();
                } //end if

            }//end try

            catch
            {
                MessageBox.Show("Wrong!!! :(");
            }
        }

        private void Student_Load(object sender, EventArgs e)
        {
            fillDepartment();
            populate();
        }

        private void StudentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //When pressed it moves to textBox
            StdId.Text = StudentDGV.SelectedRows[0].Cells[0].Value.ToString();
            StdName.Text = StudentDGV.SelectedRows[0].Cells[1].Value.ToString();
            GenderCb.SelectedItem = StudentDGV.SelectedRows[0].Cells[2].Value.ToString();
            StdPhone.Text = StudentDGV.SelectedRows[0].Cells[4].Value.ToString();
            FeesTb.Text = StudentDGV.SelectedRows[0].Cells[6].Value.ToString();
        }

        /////////////////////////Delete////////////////////
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //cheek if id enter in textBox
                if (StdId.Text == "")
                {
                    MessageBox.Show("Enter The Student Id.");
                }
                else
                {
                    Con.con.Open();
                    string query = "delete from StudentTbl where StdId=" + StdId.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con.con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Deleted Successfully.");
                    Con.con.Close();
                    populate();
                }//end if

            }//end try
            catch
            {
                MessageBox.Show("Oops :(...Student Not Deleted!!!");
            }
        }

        ////////////////////Edit///////////////////////
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (StdId.Text== "" || StdName.Text == "" || StdPhone.Text == "" ||FeesTb.Text == "")
                {
                    MessageBox.Show("Missing Data");
                }
                else
                {
                    Con.con.Open();
                    string qurey = "update StudentTbl set StdName='" + StdName.Text + "',StdGender='" + GenderCb.SelectedItem.ToString() + "',StdDOB='" + Date.Text + "',StdPhone='" +StdPhone.Text+ "',StdDep='" + DepCb.SelectedValue.ToString() + "',StdFees='" + FeesTb.Text + "' where StdId=" + StdId.Text + ";";
                    SqlCommand cmd = new SqlCommand(qurey, Con.con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Updated Successfully.");
                    Con.con.Close();
                    populate();
                }//end if

            }//end try
            catch
            {
                MessageBox.Show("Oops :(...Student Not Updated!!!");
            }
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

        private void button6_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void noduelist()
        {
            Con.con.Open();
            string query = "select * from StudentTbl where StdFees > "+0+"";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con.con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            //StudentDGV
            StudentDGV.DataSource = ds.Tables[0];
            Con.con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            noduelist();
        }
        
    }
}
