using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;


namespace ConnectedDemo
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;

        public Form1()
        {
           
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                List<Dept2> list = new List<Dept2>();
                string qry = "select * from Dept2";
                cmd = new SqlCommand(qry, con);
                con.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Dept2 dept2 = new Dept2();
                        dept2.Did = Convert.ToInt32(reader["did"]);
                        dept2.Dname = reader["dname"].ToString();
                        list.Add(dept2);
                    }
                }
                // display dname & on selection of dname we need did
                cmdDepartment.DataSource = list;
                cmdDepartment.DisplayMember = "Dname";
                cmdDepartment.ValueMember = "Did";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "insert into Employe2 values(@name,@email,@age,@salary,@did)";
                cmd = new SqlCommand(qry, con);
                // assign value to each parameter
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@age", Convert.ToInt32(txtAge.Text));
                cmd.Parameters.AddWithValue("@salary", Convert.ToDouble(txtSalary.Text));
                cmd.Parameters.AddWithValue("@did", Convert.ToInt32(cmdDepartment.SelectedValue));
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Record inserted");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                GetAllEmps();
                ClearFields();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select e.*, d.dname from Employe2 e inner join dept2 d on d.did = e.did where e.id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                con.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        txtName.Text = reader["name"].ToString();
                        txtEmail.Text = reader["email"].ToString();
                        txtAge.Text = reader["age"].ToString();
                        txtSalary.Text = reader["salary"].ToString();
                        cmdDepartment.Text = reader["dname"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
              
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "update Employe2 set name=@name,email=@email,age=@age,salary=@salary,did=@did where id=@id";
                cmd = new SqlCommand(qry, con);
                // assign value to each parameter
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@age", Convert.ToInt32(txtAge.Text));
                cmd.Parameters.AddWithValue("@salary", Convert.ToDouble(txtSalary.Text));
                cmd.Parameters.AddWithValue("@did", Convert.ToInt32(cmdDepartment.SelectedValue));
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Record updated");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                GetAllEmps();
                ClearFields();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "delete from Employe2 where id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Record deleted");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                GetAllEmps();
                ClearFields();
            }
        }

        private void GetAllEmps()
        {
            string qry = "select e.*, d.dname from Employe2 e inner join dept2 d on d.did = e.did";
            cmd = new SqlCommand(qry, con);
            con.Open();
            reader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            dataGridView1.DataSource = table;
            con.Close();
        }
        private void ClearFields()
        {
            txtId.Clear();
            txtName.Clear();
            txtEmail.Clear();
            txtAge.Clear();
            txtSalary.Clear();
            cmdDepartment.Refresh();
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try 
            {
                GetAllEmps();
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            
            
            }   


        }
    }
}





