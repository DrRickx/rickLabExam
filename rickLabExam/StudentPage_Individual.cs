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
                CMD.CommandText = "SELECT " +
                    "sr.studentId, " +
                    "CONCAT(sr.firstName, ' ', sr.middleName, ' ', sr.lastName) AS fullName, " +
                    "sr.houseNo," +
                    "sr.brgyName," +
                    "sr.municipality," +
                    "sr.province," +
                    "sr.region," +
                    "sr.country," +
                    "sr.birthdate," +
                    "sr.age," +
                    "sr.studContactNo," +
                    "sr.emailAddress," +
                    "CONCAT(sr.guardianFirstName, ' ', sr.guardianLastName) as guardianName," +
                    "sr.hobbies," +
                    "sr.nickname," +
                    "c.courseName," +
                    "y.yearLvl " +
                    "FROM studentrecordtb sr " +
                    "INNER JOIN coursetb c ON sr.courseId = c.courseId " +
                    "INNER JOIN yeartb y ON sr.yearId = y.yearId " +
                    "WHERE studentId = @studentId";
                CMD.Parameters.AddWithValue("@studentId", studentId);
                MySqlDataReader reader = CMD.ExecuteReader();

                // Check if the student record is found
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // Populate the controls with the student's data
                        lblStudentId.Text = reader["studentId"].ToString();
                        lblStudentName.Text = reader["fullName"].ToString();
                        lblAddress.Text = reader["houseNo"].ToString() + " " + reader["brgyName"].ToString() + ", " + reader["municipality"].ToString() + ", " + reader["province"].ToString() + ", Region " + reader["region"].ToString() + ", " + reader["country"].ToString();
                        lblBirthDate.Text = reader["birthdate"].ToString();
                        lblAge.Text = reader["age"].ToString();
                        lblStudentNumber.Text = reader["studContactNo"].ToString();
                        lblEmailAddress.Text = reader["emailAddress"].ToString();
                        lblGuardianName.Text = reader["guardianName"].ToString();
                        lblHobbies.Text = reader["hobbies"].ToString();
                        lblNickname.Text = reader["nickname"].ToString();
                        lblCourse.Text = reader["courseName"].ToString();
                        lblYear.Text = reader["yearLvl"].ToString();
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
