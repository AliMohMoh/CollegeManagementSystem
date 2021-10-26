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
    public partial class Teacher : Form
    {
        public Teacher()
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
            dt.Columns.Add("DepName",typeof(string));
            dt.Load(rdr);
            DepCb.ValueMember = "DepName";
            DepCb.DataSource = dt;
            Con.con.Close();
        }

        //A function of taking the names of columns with rows 
        private void populate()
        {
            Con.con.Open();
            string query = "select * from TeacherTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con.con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            //UserDGV
            TeacherDGV.DataSource = ds.Tables[0];
            Con.con.Close();
        }

        private void Teacher_Load(object sender, EventArgs e)
        {
            fillDepartment();
            populate();
        }


        //////////////////////Add//////////////////
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //check if empty or not
                if (TIdTb.Text == "" || TNameTb.Text == "" || TPhoneTb.Text == "" || Address.Text=="")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {

                    Con.con.Open();
                    SqlCommand cmd = new SqlCommand("insert into TeacherTbl values(@v1,@v2,@v3,@v4,@v5,@v6,@v7)", Con.con);
                    cmd.Parameters.AddWithValue("@v1",TIdTb.Text );
                    cmd.Parameters.AddWithValue("@v2", TNameTb.Text);
                    cmd.Parameters.AddWithValue("@v3", GenderCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@v4", Date.Text);
                    cmd.Parameters.AddWithValue("@v5", TPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@v6", DepCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@v7", Address.Text);
                    
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher Successfully Added.");
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

        private void TeacherDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //When pressed it moves to textBox
             TIdTb.Text = TeacherDGV.SelectedRows[0].Cells[0].Value.ToString();
             TNameTb.Text = TeacherDGV.SelectedRows[0].Cells[1].Value.ToString();
             GenderCb.SelectedItem = TeacherDGV.SelectedRows[0].Cells[2].Value.ToString();
             TPhoneTb.Text = TeacherDGV.SelectedRows[0].Cells[4].Value.ToString();
             Address.Text = TeacherDGV.SelectedRows[0].Cells[6].Value.ToString();
        }

        /////////////////////////Delete////////////////////
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //cheek if id enter in textBox
                if (TIdTb.Text == "")
                {
                    MessageBox.Show("Enter The Teacher's Id.");
                }
                else
                {
                    Con.con.Open();
                    string query = "delete from TeacherTbl where TeacherId=" +TIdTb.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con.con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher Deleted Successfully.");
                    Con.con.Close();
                    populate();
                }//end if

            }//end try
            catch
            {
                MessageBox.Show("Oops :(...Teacher Not Deleted!!!");
            }
        }

        ////////////////////Edit///////////////////////
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (TIdTb.Text == "" || TNameTb.Text == "" || TPhoneTb.Text == "" || Address.Text == "")
                {
                    MessageBox.Show("Missing Data");
                }
                else
                {
                    Con.con.Open();
                    string qurey = "update TeacherTbl set TeacherName='" + TNameTb.Text + "',TeacherGender='" + GenderCb.SelectedItem.ToString() + "',TeacherDOB='" + Date.Text + "',TeacherPhone='" + TPhoneTb.Text + "',TeacherDep='" + DepCb.SelectedValue.ToString() + "',TeacherAdd='" + Address.Text + "' where TeacherId=" + TIdTb.Text + ";";
                    SqlCommand cmd = new SqlCommand(qurey, Con.con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher Updated Successfully.");
                    Con.con.Close();
                    populate();
                }//end if

            }//end try
            catch
            {
                MessageBox.Show("Oops :(...Teacher Not Updated!!!");
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
