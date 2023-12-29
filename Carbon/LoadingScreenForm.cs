using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Carbon
{
    public partial class LoadingScreenForm : Form
    {
        private FastColoredTextBox htmlTextBox;
        public LoadingScreenForm()
        {
            InitializeComponent();
        }

        private void LoadingScreenForm_Load(object sender, EventArgs e)
        {
            // Start a new thread to load the main form and other forms in the background
            Thread thread = new Thread(new ThreadStart(LoadMainForm));
            thread.Start();
        }
        private void LoadMainForm()
        {
            // Simulate loading time (replace with your actual loading logic)
            Thread.Sleep(3000);

            // Run the code on the main UI thread
            this.Invoke((MethodInvoker)delegate
            {
                InitializeHtmlTextBox();
                Hide(); // Hide the loading screen
                MainForm mainForm = new MainForm();
                mainForm.Show();
               // mainForm.ShowDialog();
            });
        }

        private void InitializeHtmlTextBox()
        {
            htmlTextBox = new FastColoredTextBox();
            htmlTextBox.Multiline = true;
            htmlTextBox.Dock = DockStyle.Left;
            htmlTextBox.Width = Width / 2;
            Controls.Add(htmlTextBox);
        }
    }
    }