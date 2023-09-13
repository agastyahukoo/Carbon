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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using CefSharp;
using CefSharp.WinForms;
using FastColoredTextBoxNS;

namespace CodeCraft
{

    public partial class MainForm : Form
    {
        private SettingsForm settingsForm = null;
        private Color customTextColor;
        private Color customAppColor;
        private Color customButtonColor;
        public bool DarkMode = false;
        FlowLayoutPanel buttonStrip = new FlowLayoutPanel();
        private CodeAutoComplete codeAutoComplete;
        private static bool isCefInitialized = false;
        private string[] musicFiles = { "background.mp3", "music2.mp3", "No music" };

        public MainForm()
        {
            InitializeComponent();
            codeAutoComplete = new CodeAutoComplete(fastColoredTextBox1, this);
            SetupLayout();
            ApplyDarkMode();
        }

        private void SetupLayout()
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            tableLayoutPanel.Controls.Add(fastColoredTextBox1, 0, 0);
            fastColoredTextBox1.Dock = DockStyle.Fill;
            Panel browserPanel = new Panel();
            tableLayoutPanel.Controls.Add(browserPanel, 1, 0);
            browserPanel.Dock = DockStyle.Fill;
            browserPanel.Controls.Add(chromiumWebBrowser1);
            chromiumWebBrowser1.Dock = DockStyle.Fill;
            Controls.Add(tableLayoutPanel);
            buttonStrip.Dock = DockStyle.Bottom;
            buttonStrip.Padding = new Padding(15); 
            buttonStrip.Controls.Add(settingsButton);
            buttonStrip.Controls.Add(button2);
            buttonStrip.Controls.Add(button3);
            buttonStrip.Controls.Add(button4);
            buttonStrip.Controls.Add(newProjectButton);
            Controls.Add(buttonStrip);
        }

        private void SetupUI()
        {
            settingsButton.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.BorderSize = 0;
            button4.FlatAppearance.BorderSize = 0;
            newProjectButton.FlatAppearance.BorderSize = 0;
            fastColoredTextBox1.TextChanged += fastColoredTextBox1_TextChanged;
            axWindowsMediaPlayer1.settings.setMode("loop", true);
            axWindowsMediaPlayer1.uiMode = "None";
            musicPlayer.URL = musicFiles[0]; 
            musicPlayer.settings.setMode("loop", true);
            CefSettings settings = new CefSettings();
        }

        private async void CompileAndDisplayHTML()
        {
            if (!isCefInitialized)
            {
                CefSettings settings = new CefSettings();
                Cef.Initialize(settings);
                isCefInitialized = true;
            }
            await Task.Delay(30);

            string htmlCode = fastColoredTextBox1.Text;

            string tempDirectory = Path.Combine(Path.GetTempPath(), "HTMLCompiler");
            Directory.CreateDirectory(tempDirectory);

            string tempFilePath = Path.Combine(tempDirectory, "index.html");
            File.WriteAllText(tempFilePath, htmlCode);

            chromiumWebBrowser1.Load(tempFilePath);
        }


        private void fastColoredTextBox1_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            CompileAndDisplayHTML();
        }

        private void ShowSettingsForm()
        {
            SettingsForm settingsForm = new SettingsForm(musicFiles, musicPlayer.URL);
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                musicPlayer.URL = settingsForm.SelectedMusicFile;
            }

        }

        private void settingsButton_Click_1(object sender, EventArgs e)
        {
            if (settingsForm == null || settingsForm.IsDisposed)
            {
                settingsForm = new SettingsForm(musicFiles, musicPlayer.URL);

                if (DarkMode == true)
                {
                    settingsForm.BackColor = Color.FromArgb(30, 30, 30);
                    foreach (Control control in settingsForm.Controls)
                    {
                        if (control is System.Windows.Forms.Button)
                        {
                            control.ForeColor = Color.White;
                            control.BackColor = Color.FromArgb(60, 60, 60);
                        }
                        else if (control is System.Windows.Forms.TextBox)
                        {
                            control.ForeColor = Color.White;
                            control.BackColor = Color.FromArgb(40, 40, 40);
                        }
                    }
                    settingsForm.UpdateDarkModeButtonText(DarkMode);
                }

                settingsForm.Show();
            }
            else
            {
                settingsForm.BringToFront(); // Bring the existing form to the front
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "HTML Files (*.html)|*.html|Text Files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string fileContents = File.ReadAllText(filePath);
                fastColoredTextBox1.Text = fileContents;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|HTML Files (*.html)|*.html";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                string fileExtension = Path.GetExtension(filePath);
                string fileContents = fastColoredTextBox1.Text;

                if (fileExtension.Equals(".txt", StringComparison.OrdinalIgnoreCase))
                {
                    File.WriteAllText(filePath, fileContents);
                }
                else if (fileExtension.Equals(".html", StringComparison.OrdinalIgnoreCase))
                {
                    File.WriteAllText(filePath, fileContents);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Text = string.Empty;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
        public void ApplyDarkMode()
        {
            this.BackColor = Color.FromArgb(20, 20, 20);

            foreach (Control control in this.Controls)
            {
                if (control is System.Windows.Forms.Button) // Fully qualified class name
                {
                    System.Windows.Forms.Button button = control as System.Windows.Forms.Button;
                    button.ForeColor = Color.White;
                    button.BackColor = Color.FromArgb(50, 50, 50);
                }

                else if (control is FastColoredTextBoxNS.FastColoredTextBox)
                {
                    var fastColoredTextBox = control as FastColoredTextBoxNS.FastColoredTextBox;
                    fastColoredTextBox.ForeColor = Color.White;
                    fastColoredTextBox.BackColor = Color.FromArgb(40, 40, 40);
                    fastColoredTextBox.LineNumberColor = Color.White;
                    fastColoredTextBox.IndentBackColor = Color.FromArgb(40, 40, 40);
                    fastColoredTextBox.ServiceLinesColor = Color.FromArgb(40, 40, 40);
                    // Handle other color changes for FastColoredTextBox as needed
                }

                fastColoredTextBox1.ForeColor = Color.White;
                fastColoredTextBox1.BackColor = Color.FromArgb(40, 40, 40);
                fastColoredTextBox1.LineNumberColor = Color.White;
                fastColoredTextBox1.IndentBackColor = Color.FromArgb(40, 40, 40);
                fastColoredTextBox1.ServiceLinesColor = Color.FromArgb(40, 40, 40);
                string darkModeFilePath = "DarkMode.txt";
                if (File.Exists(darkModeFilePath))
                {
                    string darkModeContent = File.ReadAllText(darkModeFilePath);
                    fastColoredTextBox1.Text = darkModeContent;
                }
            }

            foreach (Control control in buttonStrip.Controls)
            {
                if (control is System.Windows.Forms.Button)
                {
                    System.Windows.Forms.Button button = control as System.Windows.Forms.Button;
                    button.ForeColor = Color.White;
                    button.BackColor = Color.FromArgb(50, 50, 50);
                }
            }
            DarkMode = true;
        }
        public void ApplyLightMode()
        {
            this.BackColor = SystemColors.Control; // Use the default system control color

            foreach (Control control in this.Controls)
            {
                if (control is System.Windows.Forms.Button)
                {
                    System.Windows.Forms.Button button = control as System.Windows.Forms.Button;
                    button.ForeColor = SystemColors.ControlText; // Use the default system control text color
                    button.BackColor = SystemColors.ControlLight; // Use the default system control color
                }
                else if (control is FastColoredTextBoxNS.FastColoredTextBox)
                {
                    var fastColoredTextBox = control as FastColoredTextBoxNS.FastColoredTextBox;
                    fastColoredTextBox.ForeColor = SystemColors.WindowText; // Use the default system window text color
                    fastColoredTextBox.BackColor = SystemColors.Window; // Use the default system window color
                    fastColoredTextBox.LineNumberColor = SystemColors.WindowText; // Use the default system window text color
                    fastColoredTextBox.IndentBackColor = SystemColors.Window; // Use the default system window color
                    fastColoredTextBox.ServiceLinesColor = SystemColors.Window; // Use the default system window color
                                                                                // Handle other color changes for FastColoredTextBox as needed
                }
            }

            foreach (Control control in buttonStrip.Controls)
            {
                if (control is System.Windows.Forms.Button)
                {
                    System.Windows.Forms.Button button = control as System.Windows.Forms.Button;
                    button.ForeColor = SystemColors.ControlText; // Use the default system control text color
                    button.BackColor = SystemColors.Control; // Use the default system control color
                }
            }
            settingsButton.BackColor = SystemColors.ControlLight;
            button2.BackColor = SystemColors.ControlLight;
            button3.BackColor = SystemColors.ControlLight;
            button4.BackColor = SystemColors.ControlLight;
            newProjectButton.BackColor = SystemColors.ControlLight;
            DarkMode = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetupUI();
            CompileAndDisplayHTML();
        }

        private void newProjectButton_Click(object sender, EventArgs e)
        {
            NewProjectForm newProjectForm = new NewProjectForm();
            if (newProjectButton.Visible == true)
            {
                if (DarkMode == true)
                {
                    newProjectForm.BackColor = Color.FromArgb(30, 30, 30);
                    foreach (Control control in newProjectForm.Controls)
                    {
                        if (control is System.Windows.Forms.Button)
                        {
                            control.ForeColor = Color.White;
                            control.BackColor = Color.FromArgb(60, 60, 60);
                        }
                        else if (control is System.Windows.Forms.TextBox)
                        {
                            control.ForeColor = Color.White;
                            control.BackColor = Color.FromArgb(40, 40, 40);
                        }
                    }
                    if (newProjectForm.ShowDialog() == DialogResult.OK)
                    {
                        string projectName = newProjectForm.ProjectName;

                        if (string.IsNullOrEmpty(projectName))
                        {
                            MessageBox.Show("Project name is empty or null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        string projectFolder = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                            "CodeCraft",
                            newProjectForm.ProjectName
                        );

                        string indexHtmlPath = Path.GetFullPath(Path.Combine(projectFolder, "index.html"));

                        if (File.Exists(indexHtmlPath))
                        {
                            fastColoredTextBox1.Text = File.ReadAllText(indexHtmlPath);
                            CompileAndDisplayHTML();
                        }
                        else
                        {
                            MessageBox.Show("index.html not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {

                    if (newProjectForm.ShowDialog() == DialogResult.OK)
                    {
                        string projectName = newProjectForm.ProjectName;

                        if (string.IsNullOrEmpty(projectName))
                        {
                            MessageBox.Show("Project name is empty or null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        string projectFolder = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                            "CodeCraft",
                            newProjectForm.ProjectName
                        );

                        string indexHtmlPath = Path.GetFullPath(Path.Combine(projectFolder, "index.html"));

                        if (File.Exists(indexHtmlPath))
                        {
                            fastColoredTextBox1.Text = File.ReadAllText(indexHtmlPath);
                            CompileAndDisplayHTML();
                        }
                        else
                        {
                            MessageBox.Show("index.html not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}
