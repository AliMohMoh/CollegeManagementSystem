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
    public partial class Fees : Form
    {
        public Fees()
        {
            InitializeComponent();
        }

        DataBase Con = new DataBase();

        //Function of fill Department ComboBox From the DepartmentTbl table 
        private void fillStudent()
        {
            Con.con.Open();
            SqlCommand cmd = new SqlCommand("select StdId from StudentTbl", Con.con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("StdId", typeof(int));
            dt.Load(rdr);
            StdIdCb.ValueMember = "StdId";
            StdIdCb.DataSource = dt;
            Con.con.Close();
        }

        //A function of taking the names of columns with rows 
        private void populate()
        {
            Con.con.Open();
            string query = "select * from FeesTbl ";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con.con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            //FeesDGV
            FeesDGV.DataSource = ds.Tables[0];
            Con.con.Close();
        }

        //دالة عندما يدخل كم دفع يغير في جدول الطلاب كم قد دفع
        private void updateStd()
        {
            Con.con.Open();
            string qurey = "update StudentTbl set StdFees='" + AmountTb.Text + "' where StdId=" + StdIdCb.SelectedValue.ToString() + ";";
            SqlCommand cmd = new SqlCommand(qurey, Con.con);
            cmd.ExecuteNonQuery();
            //MessageBox.Show("User Updated Successfully.");
            Con.con.Close();
        }

        private void Fees_Load(object sender, EventArgs e)
        {
            fillStudent();
            populate();
        }

        private void StdIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //لما اختار رقم من الطلاب يظهر اسمه في نص الاسم
            Con.con.Open();
            string query = "select * from StudentTbl where StdId=" + StdIdCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query,Con.con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                StdName.Text = dr["StdName"].ToString();
            }
            Con.con.Close();
        }

        /////////////////////Pay//////////////////
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //check if empty or not
                if (Num.Text == "" || StdName.Text == "" || AmountTb.Text == "" )
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    //show just Year
                    string date = PeriodDate.Value.Year.ToString();

                    Con.con.Open();
                    SqlDataAdapter da=new SqlDataAdapter("select count(*) from FeesTbl where StdId="+StdIdCb.SelectedValue.ToString()+"and Period='"+date+"'",Con.con);
                    DataTable dt=new DataTable();
                    da.Fill(dt);
                    //يتاكد اذا ما كان في تشابة في التاريخ
                    if(dt.Rows[0][0].ToString()=="1")
                    {
                        MessageBox.Show("No Dues For The Selected Period!!!");
                        Con.con.Close();
                    }
                    else
                    {
                       // Con.con.Open();
                    SqlCommand cmd = new SqlCommand("insert into FeesTbl values(@v1,@v2,@v3,@v4,@v5)", Con.con);
                    cmd.Parameters.AddWithValue("@v1", Num.Text);
                    cmd.Parameters.AddWithValue("@v2", StdIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@v3", StdName.Text);
                    cmd.Parameters.AddWithValue("@v4", date);
                    cmd.Parameters.AddWithValue("@v5", AmountTb.Text);
                   

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Fees Successfully Added.");
                    Con.con.Close();
                    //After the function works
                    populate();
                    updateStd();
                    }//end if2
                } //end if1

            }//end try

            catch
            {
                MessageBox.Show("Wrong!!! :(");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainForm home = new MainForm();
            home.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        ////////////////////Edit///////////////////////
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Num.Text == "" || StdName.Text == "" || AmountTb.Text == "")
                {
                    MessageBox.Show("Missing Data");
                }
                else
                {
                    string date = PeriodDate.Value.Year.ToString();
                    Con.con.Open();
                    string qurey = "update FeesTbl set StdId='" + StdIdCb.SelectedValue.ToString() + "',StdName='" + StdName.Text + "',Period='" + date + "',Amount='" + AmountTb.Text + "' where FeesNum=" +Num.Text + ";";
                    SqlCommand cmd = new SqlCommand(qurey, Con.con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Fees Updated Successfully.");
                    Con.con.Close();
                    populate();
                    updateStd();
                }//end if

            }//end try
            catch
            {
                MessageBox.Show("Oops :(...Fees Not Updated!!!");
            }
        }

        private void FeesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //When pressed it moves to textBox
            Num.Text = FeesDGV.SelectedRows[0].Cells[0].Value.ToString();
            StdIdCb.SelectedValue= FeesDGV.SelectedRows[0].Cells[1].Value.ToString();
            StdName.Text = FeesDGV.SelectedRows[0].Cells[2].Value.ToString();
            AmountTb.Text = FeesDGV.SelectedRows[0].Cells[4].Value.ToString();
           
        }

       
    }
}
