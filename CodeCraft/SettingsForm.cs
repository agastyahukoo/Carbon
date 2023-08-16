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
    public partial class SettingsForm : Form
    {
        private string[] musicFiles;
        public string SelectedMusicFile { get; private set; }

        public SettingsForm(string[] musicFiles, string selectedMusicFile)
        {
            InitializeComponent();
            this.musicFiles = musicFiles;
            SelectedMusicFile = selectedMusicFile;
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            SelectedMusicFile = musicComboBox.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            foreach (string musicFile in musicFiles)
            {
                musicComboBox.Items.Add(musicFile);
            }
            musicComboBox.SelectedItem = SelectedMusicFile;
            btnDarkMode.FlatAppearance.BorderSize = 0;
            saveButton.FlatAppearance.BorderSize = 0;
        }

        private void btnDarkMode_Click(object sender, EventArgs e)
        {
            // Change colors for SettingsForm
            this.BackColor = Color.FromArgb(30, 30, 30);
            foreach (Control control in this.Controls)
            {
                if (control is Button)
                {
                    control.ForeColor = Color.White;
                    control.BackColor = Color.FromArgb(60, 60, 60);
                }
                else if (control is TextBox)
                {
                    control.ForeColor = Color.White;
                    control.BackColor = Color.FromArgb(40, 40, 40);
                }
            }

            // Change colors for MainForm
            MainForm mainForm = Application.OpenForms["MainForm"] as MainForm;
            if (mainForm != null)
            {
                mainForm.ApplyDarkMode();

                // Additional change for FastColoredTextBox in MainForm
                mainForm.fastColoredTextBox1.ForeColor = Color.White;
                mainForm.fastColoredTextBox1.BackColor = Color.FromArgb(40, 40, 40);
                mainForm.fastColoredTextBox1.LineNumberColor = Color.White;
                mainForm.fastColoredTextBox1.IndentBackColor = Color.FromArgb(40, 40, 40);
                mainForm.fastColoredTextBox1.ServiceLinesColor = Color.FromArgb(40, 40, 40);
                // Handle other color changes for FastColoredTextBox in MainForm as needed
            }

            // Save Dark Mode preference to settings
            Properties.Settings.Default.DarkMode = true; // Enable Dark Mode
            Properties.Settings.Default.Save();

            string darkModeFilePath = "DarkMode.txt";
            if (File.Exists(darkModeFilePath))
            {
                string darkModeContent = File.ReadAllText(darkModeFilePath);
                if (mainForm != null)
                {
                    mainForm.fastColoredTextBox1.Text = darkModeContent;
                }
            }
        }
    }
}
