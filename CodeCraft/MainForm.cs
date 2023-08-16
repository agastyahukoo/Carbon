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

namespace CodeCraft
{

    public partial class MainForm : Form
    {
        FlowLayoutPanel buttonStrip = new FlowLayoutPanel();
        private CodeAutoComplete codeAutoComplete;
        private static bool isCefInitialized = false;
        private string[] musicFiles = { "background.mp3", "music2.mp3", "No music" }; // Add more music files as needed
        public MainForm()
        {
            InitializeComponent();
            codeAutoComplete = new CodeAutoComplete(fastColoredTextBox1, this);

            // Create TableLayoutPanel
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30)); // Left side
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70)); // Right side

            // Add FastColoredTextBox with padding (left cell)
            tableLayoutPanel.Controls.Add(fastColoredTextBox1, 0, 0);
            fastColoredTextBox1.Dock = DockStyle.Fill;

            // Add Panel for ChromiumWebBrowser (right cell)
            Panel browserPanel = new Panel();
            tableLayoutPanel.Controls.Add(browserPanel, 1, 0);
            browserPanel.Dock = DockStyle.Fill;

            // Add ChromiumWebBrowser (or panel) to the browserPanel
            browserPanel.Controls.Add(chromiumWebBrowser1);
            chromiumWebBrowser1.Dock = DockStyle.Fill;

            // Add TableLayoutPanel to the MainForm
            Controls.Add(tableLayoutPanel);

            // Create a strip for buttons with padding
            buttonStrip.Dock = DockStyle.Bottom;
            buttonStrip.Padding = new Padding(15); // Add padding

            // Add buttons to the strip
            buttonStrip.Controls.Add(settingsButton);
            buttonStrip.Controls.Add(button2);
            buttonStrip.Controls.Add(button3);
            buttonStrip.Controls.Add(button4);

            // Add the button strip to the MainForm
            Controls.Add(buttonStrip);
        }



        private void Form1_Load(object sender, EventArgs e)
        {

            settingsButton.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.BorderSize = 0;
            button4.FlatAppearance.BorderSize = 0;
            //
            fastColoredTextBox1.TextChanged += fastColoredTextBox1_TextChanged;
            CompileAndDisplayHTML();
            axWindowsMediaPlayer1.URL = "background.mp4"; // Replace with the actual path and filename of your video file
            axWindowsMediaPlayer1.settings.setMode("loop", true);
            axWindowsMediaPlayer1.uiMode = "None";

            // Play default music file
            musicPlayer.URL = musicFiles[0]; // Change index to play different default music file
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
            // Delay for 500 milliseconds (adjustable)
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
            ShowSettingsForm();
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



        }




    }
}
