using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;



namespace GradingSystem
{
    public partial class AddStudent : Form
    {
        public AddStudent()
        {
            InitializeComponent();
        }
        connectionDB opencon = new connectionDB();
        public string file_name;
        public string filepath;
        public string tempPath;
        private bool newrec;

        private void btnAddExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private static bool IsValid(string email)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }

        
        public bool checkID(string idnum)
        {

            bool result = false;
            opencon.dbconnect();
            if (opencon.OpenConnection() == true)
            {
                string sql = "SELECT STUDID FROM student WHERE(STUDID=" + "'" + txtStudID.Text + "') LIMIT 1";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, opencon.connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {

                    result = false;
                }
                else
                {

                    result = true;
                }
            }

            return result;
        }
        private void locktextbox() {
            txtStudID.Enabled = false;
            txtFname.Enabled = false;
            txtMname.Enabled = false;
            txtLname.Enabled = false;
            cboCourse.Enabled = false;
            txtYear.Enabled = false;
            cboGender.Enabled = false;
            txtAdd.Enabled = false;
            txtEmail.Enabled = false;
        }

        private void unlocktextbox() {
            txtStudID.Enabled = true;
            txtFname.Enabled = true;
            txtMname.Enabled = true;
            txtLname.Enabled = true;
            cboCourse.Enabled = true;
            txtYear.Enabled = true;
            cboGender.Enabled = true;
            txtAdd.Enabled = true;
            txtEmail.Enabled = true;
        }
        private void cleartext() {
            txtStudID.Text = "";
            txtFname.Text = "";
            txtMname.Text = "";
            txtLname.Text = "";
            cboCourse.Text = "";
            txtYear.Text = "";
            cboGender.Text = "";
            txtAdd.Text = "";
            txtEmail.Text = "";
            txtPath.Text = "";
        }
        

        
        private void btnUpload_Click(object sender, EventArgs e)
        {

         OpenFileDialog photo = new OpenFileDialog();
            photo.InitialDirectory = "C:\\Users\\Mark Vincent Cantuba\\Desktop\\GradingSystem\\photo";
            photo.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            photo.ShowDialog();
            file_name = System.IO.Path.GetFileName(photo.FileName);

            filepath = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 37));
            //MessageBox.Show(filepath);
            idPicture.ImageLocation = filepath + "\\photo" + "\\" + file_name;
            txtPath.Text = file_name;
            tempPath = "C: Users Mark Vincent Cantuba Desktop GradingSystem photo " + file_name;
           // MessageBox.Show(txtPath.Text);
          //  MessageBox.Show(tempPath);
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
         if (txtStudID.Text == "")
            {
                MessageBox.Show("ID Number is required field, enter ID No.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStudID.BackColor = Color.Cyan;
                txtStudID.Focus();
                

            }
            else if (txtFname.Text == "")
            {
                MessageBox.Show("First Name is required field, enter First Name.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFname.BackColor = Color.Cyan;
                txtFname.Focus();
                
            }
            else if (txtMname.Text == "")
            {
                MessageBox.Show("Middle Name is required field, enter Middle Name.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMname.BackColor = Color.Cyan;
                txtMname.Focus();
                
            }
            else if (txtLname.Text == "")
            {
                MessageBox.Show("Last Name is required field, enter Last Name.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLname.BackColor = Color.Cyan;
                txtLname.Focus();
                
            }
            else
            {

                try
                {
                    opencon.dbconnect();
                    if (opencon.OpenConnection() == true)
                    {

                     
                        if (newrec == true)
                        {
                            if (checkID(Convert.ToString(txtStudID.Text)) == false)
                            {
                                string query = "INSERT INTO student(STUDID, FIRSTNAME, MIDDLENAME, LASTNAME, COURSE, YEAR ,GENDER, ADDRESS,EMAIL,IMAGELOCATION) VALUES " +
                                               "('" + Convert.ToString(txtStudID.Text) + "','" + Convert.ToString(txtFname.Text.ToUpper()) + "','"
                                               + Convert.ToString(txtMname.Text.ToUpper()) + "', '" + Convert.ToString(txtLname.Text.ToUpper()) + "', '"
                                               + Convert.ToString(cboCourse.Text) + "', '" + Convert.ToString(txtYear.Text) + "', '" + Convert.ToString(cboGender.Text) + "', '" + Convert.ToString(txtAdd.Text) + "', '" + Convert.ToString(txtEmail.Text) + "', '" + Convert.ToString(tempPath) + "')";

                                MySqlCommand addUser = new MySqlCommand(query, opencon.connection);
                                addUser.ExecuteNonQuery();

                                // Display maxnum after the insertion
                                MessageBox.Show("Newecord has been added");
                                loadGrid();
                                opencon.CloseConnection();
                                btnAdd.Enabled = true;
                                btnEdit.Enabled = true;
                                btnSave.Enabled = false;
                                locktextbox();
                                dataGridView1.Enabled = false;
                                cleartext();
                                idPicture.ImageLocation = @"C:\Users\Mark Vincent Cantuba\Desktop\GradingSystem\photo\def.png";
                            }
                            else {
                                MessageBox.Show("Duplicate ID Number.", "Error!", MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                                txtStudID.Text = "";
                                txtStudID.Focus();


                            }


                        }
                        else
                        {
                            string query = "UPDATE student SET FIRSTNAME='" + Convert.ToString(txtFname.Text.ToUpper()) + "', MIDDLENAME='" +
                               Convert.ToString(txtMname.Text.ToUpper()) + "', LASTNAME='" + Convert.ToString(txtLname.Text.ToUpper()) + "', COURSE='" + Convert.ToString(cboCourse.Text.ToUpper()) + "' ,YEAR='" +
                             Convert.ToString(txtYear.Text.ToUpper()) + "', GENDER='" + Convert.ToString(cboGender.Text.ToUpper()) + "',ADDRESS='" + Convert.ToString(txtAdd.Text.ToUpper()) + "' , EMAIL='" + Convert.ToString(txtEmail.Text.ToUpper()) + "' , IMAGELOCATION='" + Convert.ToString(txtPath.Text.ToUpper()) + "' WHERE STUDID='" + Convert.ToString(txtStudID.Text) + "'";
                       MySqlCommand addUser = new MySqlCommand(query, opencon.connection);
                         addUser.ExecuteNonQuery();
                            MessageBox.Show("Record has been updated.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                           opencon.CloseConnection();
                            loadGrid();
                            btnAdd.Enabled = true;
                            btnEdit.Enabled = true;
                            locktextbox();
                            cleartext();
                            btnSave.Enabled = false;
                            dataGridView1.Enabled = false;
                            idPicture.ImageLocation = @"C:\Users\Mark Vincent Cantuba\Desktop\GradingSystem\photo\def.png";
                        }
                    }//end of opencon
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);

                }

            } //end of else

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            newrec = true;
            btnSave.Enabled = true;
            btnEdit.Enabled = false;
            btnUpload.Enabled = true;
            btnAdd.Enabled = false;
            unlocktextbox();
        }
        private void loadGrid()
        {
            try
            {
                opencon.dbconnect();
                if (opencon.OpenConnection() == true)
                {


                    dataGridView1.Refresh();
                    string query = ("SELECT STUDID, FIRSTNAME, MIDDLENAME, LASTNAME,COURSE,YEAR, GENDER,ADDRESS,EMAIL FROM student LIMIT 10");
                    MySqlCommand command = new MySqlCommand(query, opencon.connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    dataGridView1.DataSource = data;
                    opencon.CloseConnection();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);

            }
        }


        private void AddStudent_Load(object sender, EventArgs e)
        {
            idPicture.ImageLocation = @"C:\Users\Mark Vincent Cantuba\Desktop\GradingSystem\photo\def.png";
            loadGrid();
            dataGridView1.Enabled = false;
            btnSave.Enabled = false;
            btnUpload.Enabled = false;
            locktextbox();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
            newrec = false;
            btnSave.Enabled = true;
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            unlocktextbox();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];//
                    txtStudID.Text = row.Cells["STUDID"].Value.ToString();
                    txtFname.Text = row.Cells["FIRSTNAME"].Value.ToString();
                    txtMname.Text = row.Cells["MIDDLENAME"].Value.ToString();
                    txtLname.Text = row.Cells["LASTNAME"].Value.ToString();
                    cboCourse.Text = row.Cells["COURSE"].Value.ToString();
                    txtYear.Text = row.Cells["YEAR"].Value.ToString();
                    cboGender.Text = row.Cells["GENDER"].Value.ToString();
                    txtAdd.Text = row.Cells["ADDRESS"].Value.ToString();
                    txtEmail.Text = row.Cells["EMAIL"].Value.ToString();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtSearchLname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                opencon.dbconnect();
                if (opencon.OpenConnection() == true)
                {

                    string query = ("SELECT STUDID, FIRSTNAME, MIDDLENAME, LASTNAME, COURSE , YEAR, GENDER, ADDRESS,EMAIL  FROM student WHERE LASTNAME LIKE '" + txtSearchLname.Text + "%'");

                    MySqlCommand command = new MySqlCommand(query, opencon.connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    dataGridView1.DataSource = data;
                    opencon.CloseConnection();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            txtPath.Text = txtPath.Text.Replace("\\", "; ");
        }
    }
}
