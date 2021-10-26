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
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }

        DataBase Con = new DataBase();

        //A function of taking the names of columns with rows 
        private void populate()
        {
            Con.con.Open();
            string query = "select * from UserTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con.con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            //UserDGV
            UserDGV.DataSource = ds.Tables[0];
            Con.con.Close();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        //////////////////////Add//////////////////
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //check if empty or not
                if (UidTb.Text == "" || UnameTb.Text == "" || UpassTb.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {

                    Con.con.Open();
                    SqlCommand cmd = new SqlCommand("insert into UserTbl values("+UidTb.Text+",'"+UnameTb.Text+"','"+UpassTb.Text+"')",Con.con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Successfully Added.");
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

        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //When pressed it moves to textBox
            UidTb.Text = UserDGV.SelectedRows[0].Cells[0].Value.ToString();
            UnameTb.Text = UserDGV.SelectedRows[0].Cells[1].Value.ToString();
            UpassTb.Text = UserDGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /////////////////////////Delete////////////////////
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //cheek if id enter in textBox
                if (UidTb.Text == "")
                {
                    MessageBox.Show("Enter The User Id.");
                }
                else
                {
                    Con.con.Open();
                    string query = "delete from UserTbl where UserId="+UidTb.Text+";";
                    SqlCommand cmd = new SqlCommand(query,Con.con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Deleted Successfully.");
                    Con.con.Close();
                    populate();
                }//end if
                
            }//end try
            catch {
                MessageBox.Show("Oops :(...User Not Deleted!!!");
            }
        }

        ////////////////////Edit///////////////////////
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (UidTb.Text == "" || UnameTb.Text == "" || UpassTb.Text == "")
                {
                    MessageBox.Show("Missing Data");
                }
                else
                {
                    Con.con.Open();
                    string qurey = "update UserTbl set UserName='"+UnameTb.Text+"',password='"+UpassTb.Text+"' where UserId="+UidTb.Text+";";
                    SqlCommand cmd = new SqlCommand(qurey,Con.con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Updated Successfully.");
                    Con.con.Close();
                    populate();
                }//end if

            }//end try
            catch {
                MessageBox.Show("Oops :(...User Not Updated!!!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainForm home = new MainForm();
            home.Show();
            this.Hide();
        }
    }
}
