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
            InitializeToolbar(); 
            KeyPreview = true;
            /*
            Font customFont = LoadCustomFont("YourNamespace.font.OTF");
            Font = new Font(customFont.FontFamily, 18f, FontStyle.Bold); // Adjust size and style as needed */
        }
        private void InitializeTerminalForm()
        {
            terminalForm = new TerminalForm();
            terminalForm.FormClosing += TerminalForm_FormClosing;
            terminalForm.Hide(); 
        }
        private void TerminalForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide(); 
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (keyData == Keys.Oemtilde)
            {
                terminalForm.ToggleVisibility();
                return true; 
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
         
                chromiumBrowser.Load("about:blank");
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        
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
            htmlTextBox.Width = Width / 2; // Default width
            htmlTextBox.TextChanged += HtmlTextBox_TextChanged;
            Controls.Add(htmlTextBox);
            htmlTextBox.Language = FastColoredTextBoxNS.Language.HTML;
            htmlTextBox.LineNumberColor = Color.Black;
        }

        private void HtmlTextBox_TextChanged(object sender, EventArgs e)
        {
      
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
   
                chromiumBrowser.Load("about:blank");
            }
        }


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

            ToolStrip toolStrip = new ToolStrip();
            ToolStripButton saveButton = new ToolStripButton("Save", null, SaveButtonClick);
            ToolStripButton openButton = new ToolStripButton("Open", null, OpenButtonClick);
            ToolStripDropDownButton moreOptionsButton = new ToolStripDropDownButton("More Options");


            Font customFont = LoadCustomFont("Carbon.font.OTF");
            toolStrip.Font = new Font(customFont.FontFamily, 12f);

          
            saveButton.Font = new Font(customFont.FontFamily, 12f);
            openButton.Font = new Font(customFont.FontFamily, 12f);


            toolStrip.Items.Add(saveButton);
            toolStrip.Items.Add(openButton);

            moreOptionsButton.Font = new Font(customFont.FontFamily, 12f);
            moreOptionsButton.DropDownItems.Add("Option 1", null, Option1Click);
            moreOptionsButton.DropDownItems.Add("Option 2", null, Option2Click);
            toolStrip.Items.Add(moreOptionsButton);
            toolStrip.Dock = DockStyle.Top;

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
                    File.WriteAllText(saveFileDialog.FileName, htmlTextBox.Text);

                    currentFilePath = saveFileDialog.FileName;

                    UpdateFormTitle();
                }
            }
        }

        private void UpdateFormTitle()
        {
            if (!string.IsNullOrEmpty(currentFilePath))
            {
                string fileName = Path.GetFileName(currentFilePath);
                Text = $"Carbon - {fileName}";
            }
            else
            {
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
                    LoadHtmlFileIntoTextBox(openFileDialog.FileName);

                    currentFilePath = openFileDialog.FileName;

                    UpdateFormTitle();

                    LoadHtmlFileIntoBrowser(currentFilePath);
                }
            }
        }

        private void Option1Click(object sender, EventArgs e)
        {
            MessageBox.Show("Option 1 clicked.");
        }

        private void Option2Click(object sender, EventArgs e)
        {
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
                    return SystemFonts.DefaultFont;
                }
            }
        }

    }
}
