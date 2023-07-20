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
    public partial class Form1 : Form
    {
        private static bool isCefInitialized = false;
        private string[] musicFiles = { "background.mp3", "music2.mp3", "No music" }; // Add more music files as needed
        public Form1()
        {
            InitializeComponent();
        }

 

        private void Form1_Load(object sender, EventArgs e)
        {
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

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void fastColoredTextBox1_Load(object sender, EventArgs e)
        {

        }

        private void musicPlayer_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|HTML Files (*.html)|*.html";

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

    }
}
