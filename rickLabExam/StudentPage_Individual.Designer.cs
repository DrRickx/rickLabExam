namespace rickLabExam
{
    partial class StudentPage_Individual
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblStudentName = new Label();
            lblStdName = new Label();
            lblAddress = new Label();
            SuspendLayout();
            // 
            // lblStudentName
            // 
            lblStudentName.AutoSize = true;
            lblStudentName.Location = new Point(109, 26);
            lblStudentName.Name = "lblStudentName";
            lblStudentName.Size = new Size(128, 20);
            lblStudentName.TabIndex = 0;
            lblStudentName.Text = "**Student Name**";
            // 
            // lblStdName
            // 
            lblStdName.AutoSize = true;
            lblStdName.Location = new Point(20, 26);
            lblStdName.Name = "lblStdName";
            lblStdName.Size = new Size(79, 20);
            lblStdName.TabIndex = 1;
            lblStdName.Text = "Full Name:";
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(109, 55);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(86, 20);
            lblAddress.TabIndex = 2;
            lblAddress.Text = "**Address**";
            // 
            // StudentPage_Individual
            // 
            AutoSize = true;  // This allows the form to resize based on its controls.
            AutoSizeMode = AutoSizeMode.GrowAndShrink;  // Ensures the form resizes based on the content of its controls.
            ClientSize = new Size(400, 300); // Initial size, but it will resize when content changes.
            Padding = new Padding(10);
            Controls.Add(lblAddress);
            Controls.Add(lblStdName);
            Controls.Add(lblStudentName);
            Name = "StudentPage_Individual";
            Text = "Student Details";
            Load += StudentPage_Individual_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblStdName;
        private Label lblStudentName;
        private Label lblAddress;
    }
}
