using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeCraft
{
    public partial class NewProjectForm : Form
    {
        public string ProjectName { get; private set; }
        public NewProjectForm()
        {
            InitializeComponent();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            string projectName = projectNameTextBox.Text;

            if (string.IsNullOrWhiteSpace(projectName))
            {
                MessageBox.Show("Please enter a valid project name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string projectFolderPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "CodeCraft",
                projectName
            );

            try
            {
                // Create the "CodeCraft" folder if it doesn't exist
                string codeCraftFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CodeCraft");
                if (!Directory.Exists(codeCraftFolder))
                {
                    Directory.CreateDirectory(codeCraftFolder);
                }

                // Create the project folder
                Directory.CreateDirectory(projectFolderPath);

                // Create index.html file inside the project folder
                string indexPath = Path.Combine(projectFolderPath, "index.html");
                if (!File.Exists(indexPath))
                {
                    File.WriteAllText(indexPath, "<!DOCTYPE html>\n<html>\n<head>\n<title></title>\n</head>\n<body>\n\n</body>\n</html>");
                }

                MessageBox.Show("Project created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ProjectName = projectName; // Set the ProjectName property

            DialogResult = DialogResult.OK;
        }

        private void NewProjectForm_Load(object sender, EventArgs e)
        {
            createButton.FlatAppearance.BorderSize = 0;
            axWindowsMediaPlayer1.settings.mute = true;
        }
    }
}
