using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Carbon
{
    public partial class TerminalForm : Form
    {
        private TextBox inputTextBox;
        private RichTextBox outputRichTextBox;
        public TerminalForm()
        {
            InitializeComponent();
            InitializeComponents();
            inputTextBox.KeyPress += InputTextBox_KeyPress;
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
                ToggleVisibility();
                return true; 
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void ToggleVisibility()
        {
            if (Visible)
            {
                Hide();
            }
            else
            {
                Show();
                Focus();
            }
        }

        private void InitializeComponents()
        {
            inputTextBox = new TextBox();
            inputTextBox.Dock = DockStyle.Bottom;

            outputRichTextBox = new RichTextBox();
            outputRichTextBox.Dock = DockStyle.Fill;
            outputRichTextBox.ReadOnly = true;

            Controls.Add(outputRichTextBox);
            Controls.Add(inputTextBox);
        }

        private void InputTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

                string command = inputTextBox.Text.Trim();
                DisplayCommand(command);
                HandleCommand(command);

                inputTextBox.Clear();
            }
        }

        private void DisplayCommand(string command)
        {
            outputRichTextBox.AppendText(command + Environment.NewLine);
        }

        private void HandleCommand(string command)
        {
            if (command.Equals("test", StringComparison.OrdinalIgnoreCase))
            {
                TestForm form1 = new TestForm();
                form1.Show();
            }
            else if (command.Equals("openForm2", StringComparison.OrdinalIgnoreCase))
            {
                NewProjectForm form2 = new NewProjectForm();
                form2.Show();
            }
            else if (command.Equals("darkmode", StringComparison.OrdinalIgnoreCase))
            {
                // Toggle dark mode
                ToggleDarkMode();
            }
            // Add more commands as needed
        }

        private void ToggleDarkMode()
        {
            // Implement your dark mode logic here

            // Example: Change the color scheme of all forms and controls
            foreach (Form form in Application.OpenForms)
            {
                ApplyDarkModeToForm(form);
            }
        }

        private void ApplyDarkModeToForm(Form form)
        {
            // Set background color, text color, and other properties for dark mode
            form.BackColor = Color.FromArgb(30, 30, 30);
            form.ForeColor = Color.White;

            // Recursive call to apply dark mode to controls within the form
            ApplyDarkModeToControls(form.Controls);
        }

        private void ApplyDarkModeToControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                // Set control-specific properties for dark mode
                // Example: For TextBoxes
                if (control is TextBox textBox)
                {
                    textBox.BackColor = Color.FromArgb(40, 40, 40);
                    textBox.ForeColor = Color.White;
                }
                // Add more control-specific properties as needed

                // Recursive call to apply dark mode to nested controls
                ApplyDarkModeToControls(control.Controls);
            }
        }
    }
}
