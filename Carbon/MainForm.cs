using CefSharp;
using CefSharp.WinForms;
using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Carbon
{
    public partial class MainForm : Form
    {
        // MainForm.cs
        private TerminalForm terminalForm;
        private string currentFilePath;
        private ChromiumWebBrowser chromiumBrowser;
        private FastColoredTextBoxNS.FastColoredTextBox htmlTextBox;
        private Timer updateTimer;
        public MainForm()
        {
            InitializeComponent();
            InitializeChromiumBrowser();
            InitializeHtmlTextBox();
            InitializeTerminalForm();
            InitializeToolbar(); // New method to initialize the toolbar
            KeyPreview = true;
            /*
            Font customFont = LoadCustomFont("YourNamespace.font.OTF");
            Font = new Font(customFont.FontFamily, 18f, FontStyle.Bold); // Adjust size and style as needed */
        }
        private void InitializeTerminalForm()
        {
            terminalForm = new TerminalForm();
            terminalForm.FormClosing += TerminalForm_FormClosing;
            terminalForm.Hide(); // Hide the terminal form initially
        }
        private void TerminalForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true; // Cancel the form closing
            Hide(); // Hide the terminal form instead of closing it
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Check if the '~' key is pressed
            if (keyData == Keys.Oemtilde)
            {
                terminalForm.ToggleVisibility();
                return true; // Signal that the key press is handled
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void UpdateHtmlFile()
        {
            if (!string.IsNullOrEmpty(currentFilePath))
            {
                File.WriteAllText(currentFilePath, htmlTextBox.Text);
            }
        }


        private void UpdateBrowser()
        {
            if (File.Exists(currentFilePath))
            {
                chromiumBrowser.Load(currentFilePath);
            }
            else
            {
                // Handle the case when the file doesn't exist
                chromiumBrowser.Load("about:blank");
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Load a new temporary HTML file by default
            LoadTemporaryHtmlFile();
        }


        private void InitializeChromiumBrowser()
        {
            chromiumBrowser = new ChromiumWebBrowser("about:blank");
            chromiumBrowser.Dock = DockStyle.Fill;
            Controls.Add(chromiumBrowser);
            chromiumBrowser.Load("www.github.com/agastyahukoo/Carbon");
        }

        private void InitializeHtmlTextBox()
        {
            htmlTextBox = new FastColoredTextBoxNS.FastColoredTextBox();
            htmlTextBox.Multiline = true;
        //    htmlTextBox.ScrollBars = ScrollBars.Both;
            htmlTextBox.Dock = DockStyle.Left;
            htmlTextBox.Width = Width / 2; // Default width, adjust as needed
            htmlTextBox.TextChanged += HtmlTextBox_TextChanged; // Handle TextChanged event
            Controls.Add(htmlTextBox);
            htmlTextBox.Language = FastColoredTextBoxNS.Language.HTML;
            htmlTextBox.LineNumberColor = Color.Black;
        }

        private void HtmlTextBox_TextChanged(object sender, EventArgs e)
        {
            // Update both the browser and file content immediately when text changes
            UpdateHtmlFile();
            UpdateBrowser();
        }


        private void LoadTemporaryHtmlFile()
        {
            string tempFolderPath = Path.Combine(Application.StartupPath, "Temp");
            string tempHtmlFileName = $"{DateTime.Now:yyyyMMddHHmmss}.html";
            currentFilePath = Path.Combine(tempFolderPath, tempHtmlFileName);

            if (!Directory.Exists(tempFolderPath))
                Directory.CreateDirectory(tempFolderPath);

            File.WriteAllText(currentFilePath, "<html><body><h1>Hello, World!</h1></body></html>");

            LoadHtmlFileIntoTextBox(currentFilePath);
            LoadHtmlFileIntoBrowser(currentFilePath);
        }

        private void LoadHtmlFileIntoTextBox(string filePath)
        {
            htmlTextBox.Text = File.ReadAllText(filePath);
        }

        private void LoadHtmlFileIntoBrowser(string filePath)
        {
            if (File.Exists(filePath))
            {
                chromiumBrowser.Load(filePath);
            }
            else
            {
                // Handle the case when the file doesn't exist
                chromiumBrowser.Load("about:blank");
            }
        }


        // Handle resizing to adjust the width of the TextBox
        private void MainForm_Resize(object sender, EventArgs e)
        {
            htmlTextBox.Width = Width / 2;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void InitializeToolbar()
        {
            // Create ToolStrip and ToolStripButtons
            ToolStrip toolStrip = new ToolStrip();
            ToolStripButton saveButton = new ToolStripButton("Save", null, SaveButtonClick);
            ToolStripButton openButton = new ToolStripButton("Open", null, OpenButtonClick);
            ToolStripDropDownButton moreOptionsButton = new ToolStripDropDownButton("More Options");

            // Set font and size for ToolStrip
            Font customFont = LoadCustomFont("Carbon.font.OTF");
            toolStrip.Font = new Font(customFont.FontFamily, 12f);

            // Set font for ToolStripButtons
            saveButton.Font = new Font(customFont.FontFamily, 12f);
            openButton.Font = new Font(customFont.FontFamily, 12f);

            // Add ToolStripButtons to ToolStrip
            toolStrip.Items.Add(saveButton);
            toolStrip.Items.Add(openButton);

            // Add More Options button with custom font
            moreOptionsButton.Font = new Font(customFont.FontFamily, 12f);
            moreOptionsButton.DropDownItems.Add("Option 1", null, Option1Click);
            moreOptionsButton.DropDownItems.Add("Option 2", null, Option2Click);

            // Add More Options button to ToolStrip
            toolStrip.Items.Add(moreOptionsButton);

            // Dock ToolStrip at the top
            toolStrip.Dock = DockStyle.Top;

            // Add ToolStrip to MainForm
            Controls.Add(toolStrip);
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            SaveHtmlFile();
        }

        private void SaveHtmlFile()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "HTML Files (*.html)|*.html|All Files (*.*)|*.*";
                saveFileDialog.DefaultExt = "html";
                saveFileDialog.AddExtension = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Save the contents of the TextBox to the selected file
                    File.WriteAllText(saveFileDialog.FileName, htmlTextBox.Text);

                    // Update the current file path
                    currentFilePath = saveFileDialog.FileName;

                    // Optionally, update the title bar with the new file name
                    UpdateFormTitle();
                }
            }
        }

        private void UpdateFormTitle()
        {
            // Check if a file is currently open
            if (!string.IsNullOrEmpty(currentFilePath))
            {
                // Display the file name in the title bar
                string fileName = Path.GetFileName(currentFilePath);
                Text = $"Carbon - {fileName}";
            }
            else
            {
                // No file is open, display the default title
                Text = "Carbon";
            }
        }



        private void OpenButtonClick(object sender, EventArgs e)
        {
            OpenHtmlFile();
        }

        private void OpenHtmlFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "HTML Files (*.html)|*.html|All Files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Load the contents of the selected file into the TextBox
                    LoadHtmlFileIntoTextBox(openFileDialog.FileName);

                    // Update the current file path
                    currentFilePath = openFileDialog.FileName;

                    // Optionally, update the title bar with the new file name
                    UpdateFormTitle();

                    // Load the HTML file into the browser
                    LoadHtmlFileIntoBrowser(currentFilePath);
                }
            }
        }

        private void Option1Click(object sender, EventArgs e)
        {
            // Implement option 1 logic
            MessageBox.Show("Option 1 clicked.");
        }

        private void Option2Click(object sender, EventArgs e)
        {
            // Implement option 2 logic
            MessageBox.Show("Option 2 clicked.");
        }
        private Font LoadCustomFont(string resourceName)
        {
            using (Stream fontStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (fontStream != null)
                {
                    byte[] fontData = new byte[fontStream.Length];
                    fontStream.Read(fontData, 0, (int)fontStream.Length);

                    IntPtr data = Marshal.AllocCoTaskMem((int)fontStream.Length);
                    Marshal.Copy(fontData, 0, data, (int)fontStream.Length);

                    PrivateFontCollection pfc = new PrivateFontCollection();
                    pfc.AddMemoryFont(data, (int)fontStream.Length);

                    Marshal.FreeCoTaskMem(data);

                    return new Font(pfc.Families[0], 12f);
                }
                else
                {
                    // Handle the case when the font resource is not found
                    return SystemFonts.DefaultFont;
                }
            }
        }

    }
}
