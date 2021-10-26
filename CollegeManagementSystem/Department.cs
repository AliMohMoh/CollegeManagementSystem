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
    public partial class Department : Form
    {
        public Department()
        {
            InitializeComponent();
        }

        DataBase Con = new DataBase();

        //A function of taking the names of columns with rows 
        private void populate()
        {
            Con.con.Open();
            string query = "select * from DepartmentTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con.con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            //DepDGV
            DepDGV.DataSource = ds.Tables[0];
            Con.con.Close();
        }

        //////////////////////Add//////////////////
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //check if empty or not
                if (DepNameTb.Text== "" || DepDescTb.Text == "" || DepDurationTb.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {

                    Con.con.Open();
                    SqlCommand cmd = new SqlCommand("insert into DepartmentTbl values('" + DepNameTb.Text + "','" + DepDescTb.Text + "','" + DepDurationTb.Text + "')", Con.con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Department Successfully Added.");
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

        private void Department_Load(object sender, EventArgs e)
        {
            populate();
        }

        /////////////////////////Delete////////////////////
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //cheek if id enter in textBox
                if (DepNameTb.Text == "")
                {
                    MessageBox.Show("Enter The Department Name.");
                }
                else
                {
                    Con.con.Open();
                    string query = "delete from DepartmentTbl where DepName='" + DepNameTb.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con.con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Department Deleted Successfully.");
                    Con.con.Close();
                    populate();
                }//end if

            }//end try
            catch
            {
                MessageBox.Show("Oops :(...User Not Deleted!!!");
            }
        }

        private void DepDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //When pressed it moves to textBox
            DepNameTb.Text= DepDGV.SelectedRows[0].Cells[0].Value.ToString();
            DepDescTb.Text = DepDGV.SelectedRows[0].Cells[1].Value.ToString();
            DepDurationTb.Text = DepDGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        ////////////////////Edit///////////////////////
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if(DepNameTb.Text== "" || DepDescTb.Text == "" || DepDurationTb.Text == "")
                {
                    MessageBox.Show("Missing Data");
                }
                else
                {
                    Con.con.Open();
                    string qurey = "update DepartmentTbl set DepDesc='" + DepDescTb.Text + "',DepDuration=" + DepDurationTb.Text + " where DepName='" + DepNameTb.Text + "';";
                    SqlCommand cmd = new SqlCommand(qurey, Con.con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Department Updated Successfully.");
                    Con.con.Close();
                    populate();
                }//end if

            }//end try
            catch
            {
                MessageBox.Show("Oops :(...Department Not Updated!!!");
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
    }
}
