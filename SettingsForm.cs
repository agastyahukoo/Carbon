using CodeCraft.Properties;
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
        private Color customTextColor;
        private Color customAppColor;
        private Color customControlBackColor;
        private bool isDragging = false;
        private Point dragStartPoint;
        private string[] musicFiles;
        public string SelectedMusicFile { get; private set; }
        private ColorDialog colorDialog = new ColorDialog();

        public SettingsForm(string[] musicFiles, string selectedMusicFile)
        {
            InitializeComponent();
            this.musicFiles = musicFiles;
            SelectedMusicFile = selectedMusicFile;
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
            textColorButton.FlatAppearance.BorderSize = 0;
            appColorButton.FlatAppearance.BorderSize= 0;
            buttonColorButton.FlatAppearance.BorderSize = 0;
            background.settings.mute = true;

        }



        private void btnDarkMode_Click(object sender, EventArgs e)
        {
            MainForm mainForm = Application.OpenForms["MainForm"] as MainForm;
            if (mainForm.DarkMode == false)
            { 
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
                    label1.ForeColor = Color.White;
                }
                if (mainForm != null)
                {
                    mainForm.ApplyDarkMode();
                    mainForm.DarkMode = true;
                    mainForm.fastColoredTextBox1.ForeColor = Color.White;
                    mainForm.fastColoredTextBox1.BackColor = Color.FromArgb(40, 40, 40);
                    mainForm.fastColoredTextBox1.LineNumberColor = Color.White;
                    mainForm.fastColoredTextBox1.IndentBackColor = Color.FromArgb(40, 40, 40);
                    mainForm.fastColoredTextBox1.ServiceLinesColor = Color.FromArgb(40, 40, 40);
                }
                Properties.Settings.Default.DarkMode = true;
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
                btnDarkMode.Text = "Light Mode";
            }
            else 
            {
                this.BackColor = SystemColors.Control;
                foreach (Control control in this.Controls)
                {
                    if (control is Button)
                    {
                        control.ForeColor = SystemColors.ControlText; 
                        control.BackColor = SystemColors.ControlLight; 
                    }
                    else if (control is TextBox)
                    {
                        control.ForeColor = SystemColors.WindowText; 
                        control.BackColor = SystemColors.Window; 
                    }
                    label1.ForeColor = Color.Black;
                }
                if (mainForm != null)
                {
                    mainForm.ApplyLightMode(); 
                    mainForm.DarkMode = false; 
                    mainForm.fastColoredTextBox1.ForeColor = SystemColors.WindowText; 
                    mainForm.fastColoredTextBox1.BackColor = SystemColors.Window; 
                    mainForm.fastColoredTextBox1.LineNumberColor = SystemColors.WindowText; 
                    mainForm.fastColoredTextBox1.IndentBackColor = SystemColors.Window; 
                    mainForm.fastColoredTextBox1.ServiceLinesColor = SystemColors.Window; 
                    foreach (Control control in mainForm.Controls)
                    {
                        if (control is Button)
                        {
                            Button button = control as Button;
                            button.ForeColor = SystemColors.ControlText; 
                            button.BackColor = SystemColors.ControlLight; 
                        }
                    }                                                                   
                }
                Properties.Settings.Default.DarkMode = false; 
                Properties.Settings.Default.Save();
                string darkModeFilePath = "LightMode.txt";
                if (File.Exists(darkModeFilePath))
                {
                    string darkModeContent = File.ReadAllText(darkModeFilePath);
                    if (mainForm != null)
                    {
                        mainForm.fastColoredTextBox1.Text = darkModeContent;
                    }
                }
                btnDarkMode.Text = "Dark Mode";
            }
        }

        private void musicComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            music();
        }

        private void music()
        {
            SelectedMusicFile = musicComboBox.SelectedItem.ToString();
            //   DialogResult = DialogResult.OK;
            MessageBox.Show("Hello World");
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            SelectedMusicFile = musicComboBox.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
        }
        public void UpdateDarkModeButtonText(bool darkMode)
        {
            btnDarkMode.Text = darkMode ? "Light Mode" : "Dark Mode";
        }

        private void SettingsForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = new Point(e.X, e.Y);
            }
        }

        private void SettingsForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point diff = new Point(e.X - dragStartPoint.X, e.Y - dragStartPoint.Y);
                Location = new Point(Location.X + diff.X, Location.Y + diff.Y);
            }
        }

        private void SettingsForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void textColorButton_Click(object sender, EventArgs e)
        {
 
        }

        private void appColorButton_Click(object sender, EventArgs e)
        {


        }

        private void buttonColorButton_Click(object sender, EventArgs e)
        {

        }
    }
}
