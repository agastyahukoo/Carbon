using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            // Populate drop-down list with music files
            foreach (string musicFile in musicFiles)
            {
                musicComboBox.Items.Add(musicFile);
            }

            // Set selected music file in the drop-down list
            musicComboBox.SelectedItem = SelectedMusicFile;
        }
    }
}
