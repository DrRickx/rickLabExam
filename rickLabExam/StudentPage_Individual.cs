using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace rickLabExam
{
    public partial class StudentPage_Individual : Form
    {
        private string studentId; // Store the studentId passed from the previous form
        private MySqlConnection Con;
        private MySqlCommand CMD;

        public StudentPage_Individual(string studentId)
        {
            InitializeComponent();
            this.studentId = studentId;  // Initialize the studentId
            Con = new MySqlConnection("Server=localhost; Database=studentinfodb; Uid=root; Password=12345");
            CMD = new MySqlCommand();
            CMD.Connection = Con;
        }

        private void StudentPage_Individual_Load(object sender, EventArgs e)
        {
            LoadStudentDetails(studentId); // Load student details based on studentId
        }

        private void LoadStudentDetails(string studentId)
        {
            try
            {
                // Open the database connection
                if (Con.State == System.Data.ConnectionState.Closed)
                {
                    Con.Open();
                }

                // Query to get student details by studentId
                CMD.CommandType = CommandType.Text;
                CMD.CommandText = "SELECT * FROM studentrecordtb WHERE studentId = @studentId";
                CMD.Parameters.AddWithValue("@studentId", studentId);
                MySqlDataReader reader = CMD.ExecuteReader();

                // Check if the student record is found
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // Populate the controls with the student's data
                        lblStudentName.Text = reader["firstName"].ToString() + " " + reader["middleName"].ToString() + " " + reader["lastName"].ToString();
                        lblAddress.Text = reader["houseNo"].ToString() + " " + reader["brgyName"].ToString() + ", " + reader["municipality"].ToString() + ", " + reader["province"].ToString() + ", Region " + reader["region"].ToString() + ", " + reader["country"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Student details not found.");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading student details: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }
    }
}
