using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CodeCraft
{
    public class CodeAutoComplete
    {
        private FastColoredTextBoxNS.FastColoredTextBox textBox;
        private ListBox listBox;
        private Form parentForm;
        private List<string> autoCompleteList;

        public CodeAutoComplete(FastColoredTextBoxNS.FastColoredTextBox textBox, Form parentForm)
        {
            this.textBox = textBox;
            this.parentForm = parentForm;

            // Create a ListBox for showing the suggestions
            listBox = new ListBox();
            listBox.Visible = false;
            listBox.Font = textBox.Font;
            listBox.BorderStyle = BorderStyle.FixedSingle;
            listBox.BackColor = Color.White;
            parentForm.Controls.Add(listBox);

            // Subscribe to the events
            textBox.KeyDown += TextBox_KeyDown;
            textBox.KeyUp += TextBox_KeyUp;
            listBox.SelectedIndexChanged += ListBox_SelectedIndexChanged;

            // Populate the list with sample suggestions (you can add more)
            autoCompleteList = new List<string>
            {
                "html", "head", "body", "div", "span", "p", "h1", "h2", "h3", "h4", "h5", "h6",
                "ul", "ol", "li", "a", "img", "table", "tr", "td", "th", "br", "input", "button",
                "style", "script", "link", "title", "meta", "class", "id", "src", "href", "onclick"
            };
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox.Visible)
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    InsertSelectedItem();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    listBox.Hide();
                }
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (Char.IsLetterOrDigit((char)e.KeyCode) || e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                ShowAutoComplete();
            }
        }

        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                textBox.Text = textBox.Text.Remove(textBox.SelectionStart - GetCurrentWord().Length, GetCurrentWord().Length);
                textBox.Text = textBox.Text.Insert(textBox.SelectionStart, listBox.SelectedItem.ToString());
                textBox.SelectionStart = textBox.Text.Length;
                listBox.Hide();
            }
        }

        private void ShowAutoComplete()
        {
            string currentWord = GetCurrentWord();
            if (string.IsNullOrWhiteSpace(currentWord))
            {
                listBox.Hide();
                return;
            }

            List<string> filteredList = autoCompleteList.FindAll(s => s.StartsWith(currentWord, StringComparison.OrdinalIgnoreCase));
            if (filteredList.Count == 0)
            {
                listBox.Hide();
                return;
            }

            listBox.Items.Clear();
            listBox.Items.AddRange(filteredList.ToArray());
            Point point = textBox.PointToScreen(new Point(textBox.Left, textBox.Bottom));
            listBox.Left = point.X;
            listBox.Top = point.Y;
            listBox.Width = textBox.Width;
            listBox.Height = 100;
            listBox.Visible = true;
        }

        private void InsertSelectedItem()
        {
            if (listBox.SelectedItem != null)
            {
                textBox.Text = textBox.Text.Remove(textBox.SelectionStart - GetCurrentWord().Length, GetCurrentWord().Length);
                textBox.Text = textBox.Text.Insert(textBox.SelectionStart, listBox.SelectedItem.ToString());
                textBox.SelectionStart = textBox.Text.Length;
            }
            listBox.Hide();
        }

        private string GetCurrentWord()
        {
            int position = textBox.SelectionStart - 1;
            while (position >= 0 && !char.IsWhiteSpace(textBox.Text[position]))
            {
                position--;
            }

            return textBox.Text.Substring(position + 1, textBox.SelectionStart - position - 1);
        }
    }
}
