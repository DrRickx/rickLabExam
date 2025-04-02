using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
namespace rickLabExam
{
    public partial class Student_Page : Form
    {
        // Database variables and settings
        private const string ServerName = "localhost";
        private const string DatabaseName = "studentinfodb";
        private const string UUID = "root";
        private const string Password = "12345";

        // Create a data table for information
        private DataTable DT = new DataTable();

        // Initialize the database parameters connections
        private MySqlConnection Con;
        private MySqlCommand CMD;
        private MySqlDataReader? Reader;

        public Student_Page()
        {
            InitializeComponent();

            // Create the database connection
            Con = new MySqlConnection($"Server={ServerName}; Database={DatabaseName}; Uid={UUID}; Password={Password}");
            // Create a new cmd command
            CMD = new MySqlCommand();
            // Execute the command
            CMD.Connection = this.Con;
            // Check for the connection
            if (!connect())
            {
                MessageBox.Show("Please configure your connection");
            }
        }

        // Method for creating connection
        public bool connect()
        {
            if (this.Con.State == ConnectionState.Closed || this.Con.State == ConnectionState.Broken)
            {
                try
                {
                    this.Con.Open();
                    MessageBox.Show("Connection successful");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection failed: " + ex.Message);
                    return false;
                }
            }
            return true;
        }

        // Method to disconnect
        public void disconnect()
        {
            if (this.Con.State == ConnectionState.Open)
            {
                this.Con.Close();
            }
        }

        private void studentPage_load(object sender, EventArgs e)
        {
            if (Con.State == ConnectionState.Open)
            {
                load_Students();
            }
            else
            {
                Application.Exit();
            }
        }

        private void load_Students()
        {
            try
            {
                CMD.CommandType = CommandType.Text;
                CMD.CommandText = "SELECT studentId, CONCAT(firstName, ' ', lastName, ' ', middleName) AS FullName FROM studentrecordtb";
                Reader = CMD.ExecuteReader();

                // Debugging: Check if the query returns rows
                if (Reader.HasRows)
                {
                    MessageBox.Show("Data retrieved from database");
                }
                else
                {
                    MessageBox.Show("No data found");
                }

                DT.Clear();
                DT.Load(Reader);
                dataGridView1.DataSource = DT;

                // Set the column headers and make FullName column visible
                dataGridView1.Columns["FullName"].HeaderText = "Full Name";  // Set a readable header for the FullName column
                dataGridView1.Columns["studentId"].HeaderText = "Student ID";  // Set the header for studentId column

                // Add a button column for the VIEW button if not already added
                if (!dataGridView1.Columns.Contains("ViewDetails"))
                {
                    DataGridViewButtonColumn viewColumn = new DataGridViewButtonColumn();
                    viewColumn.Name = "ViewDetails";
                    viewColumn.HeaderText = "View";
                    viewColumn.Text = "VIEW";
                    viewColumn.UseColumnTextForButtonValue = true;
                    dataGridView1.Columns.Add(viewColumn);
                }

                // Auto-size the columns based on the content
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                // Handle the Button Click event
                dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Student loading error: " + ex.Message);
            }
            finally
            {
                Reader?.Close();
            }
        }



        private void dataGridView1_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["ViewDetails"].Index && e.RowIndex >= 0)
            {
                // Get the studentId from the DataGridView
                string studentId = dataGridView1.Rows[e.RowIndex].Cells["studentId"].Value.ToString();

                // Create and show a new form with the studentId
                StudentPage_Individual detailsForm = new StudentPage_Individual(studentId);
                detailsForm.Show();
            }
        }
    }
}

