using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using static LoginandReg.Form1;

namespace LoginandReg
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.Refresh();

            radioButtonMale.Checked = false; 
            radioButtonFemale.Checked = false; 

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();  
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-2J98UKU\SQLEXPRESS;Initial Catalog=Student;Integrated Security=True");

            String gender ="";
            if(radioButtonMale.Checked)
            {
                gender = "Male";
            }
            else if (radioButtonFemale.Checked)
            {
                gender = "Female";
            }

            SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[RegistrationTable]
           ([RegNo]
           ,[FirstName]
           ,[LastName]
           ,[DateofBirth]
           ,[Gender]
           ,[Address]
           ,[Email]
           ,[MobilePhone]
           ,[HomePhone]
           ,[ParentName]
           ,[NIC]
           ,[ContactNumber])
     VALUES
           ('"+comboBox1.Text+ "','"+textBox1.Text+"','"+textBox2.Text+"','"+dateTimePicker1.Text+"', '"+gender+"','"+textBox3.Text+"','"+textBox4.Text+ "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox9.Text + "')",sqlcon);
            sqlcon.Open();
            cmd.ExecuteNonQuery();
            sqlcon.Close();
            MessageBox.Show("Registered Successfully");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //add the update query
            SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-2J98UKU\SQLEXPRESS;Initial Catalog=Student;Integrated Security=True");

            sqlcon.Open();

            string gender = "";
            if (radioButtonMale.Checked)
            {
                gender = "Male";
            }
            else if (radioButtonFemale.Checked)
            {
                gender = "Female";
            }

            string updateQuery = @"UPDATE [dbo].[RegistrationTable]
                               SET [FirstName] = @FirstName,
                                   [LastName] = @LastName,
                                   [DateofBirth] = @DateofBirth,
                                   [Gender] = @Gender,
                                   [Address] = @Address,
                                   [Email] = @Email,
                                   [MobilePhone] = @MobilePhone,
                                   [HomePhone] = @HomePhone,
                                   [ParentName] = @ParentName,
                                   [NIC] = @NIC,
                                   [ContactNumber] = @ContactNumber
                               WHERE [RegNo] = @RegNo";

            using (SqlCommand cmd = new SqlCommand(updateQuery, sqlcon))
            {
                cmd.Parameters.AddWithValue("@RegNo", comboBox1.Text);
                cmd.Parameters.AddWithValue("@FirstName", textBox1.Text);
                cmd.Parameters.AddWithValue("@LastName", textBox2.Text);
                cmd.Parameters.AddWithValue("@DateofBirth", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Address", textBox3.Text);
                cmd.Parameters.AddWithValue("@Email", textBox4.Text);
                cmd.Parameters.AddWithValue("@MobilePhone", textBox5.Text);
                cmd.Parameters.AddWithValue("@HomePhone", textBox6.Text);
                cmd.Parameters.AddWithValue("@ParentName", textBox7.Text);
                cmd.Parameters.AddWithValue("@NIC", textBox8.Text);
                cmd.Parameters.AddWithValue("@ContactNumber", textBox9.Text);

                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Updated Successfully");
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Do you want to exit", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                this.Show();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Delete command
            string connectionString = @"Data Source=DESKTOP-2J98UKU\SQLEXPRESS;Initial Catalog=Student;Integrated Security=True";

            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();

                    // Get the selected RegNo from the ComboBox
                    string selectedRegNo = comboBox1.Text;

                    // Construct the DELETE query
                    string deleteQuery = @"DELETE FROM [dbo].[RegistrationTable]
                               WHERE [RegNo] = @RegNo";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, sqlcon))
                    {
                        cmd.Parameters.AddWithValue("@RegNo", selectedRegNo);

                        // Execute the DELETE query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record deleted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("No records were deleted. Verify the RegNo.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
