namespace CodeCraft
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.musicComboBox = new System.Windows.Forms.ComboBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDarkMode = new System.Windows.Forms.Button();
            this.textColorButton = new System.Windows.Forms.Button();
            this.appColorButton = new System.Windows.Forms.Button();
            this.buttonColorButton = new System.Windows.Forms.Button();
            this.background = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.background)).BeginInit();
            this.SuspendLayout();
            // 
            // musicComboBox
            // 
            this.musicComboBox.FormattingEnabled = true;
            this.musicComboBox.Location = new System.Drawing.Point(23, 244);
            this.musicComboBox.Name = "musicComboBox";
            this.musicComboBox.Size = new System.Drawing.Size(169, 21);
            this.musicComboBox.TabIndex = 0;
            this.musicComboBox.SelectedValueChanged += new System.EventHandler(this.musicComboBox_SelectedValueChanged);
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.Location = new System.Drawing.Point(366, 422);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(101, 37);
            this.saveButton.TabIndex = 7;
            this.saveButton.Text = "Select";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Visible = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(23, 228);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Music";
            // 
            // btnDarkMode
            // 
            this.btnDarkMode.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnDarkMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDarkMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDarkMode.Location = new System.Drawing.Point(23, 285);
            this.btnDarkMode.Name = "btnDarkMode";
            this.btnDarkMode.Size = new System.Drawing.Size(101, 37);
            this.btnDarkMode.TabIndex = 10;
            this.btnDarkMode.Text = "Dark Mode";
            this.btnDarkMode.UseVisualStyleBackColor = false;
            this.btnDarkMode.Click += new System.EventHandler(this.btnDarkMode_Click);
            // 
            // textColorButton
            // 
            this.textColorButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.textColorButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textColorButton.Location = new System.Drawing.Point(130, 285);
            this.textColorButton.Name = "textColorButton";
            this.textColorButton.Size = new System.Drawing.Size(101, 37);
            this.textColorButton.TabIndex = 12;
            this.textColorButton.Text = "Text Color";
            this.textColorButton.UseVisualStyleBackColor = false;
            this.textColorButton.Click += new System.EventHandler(this.textColorButton_Click);
            // 
            // appColorButton
            // 
            this.appColorButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.appColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.appColorButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appColorButton.Location = new System.Drawing.Point(130, 328);
            this.appColorButton.Name = "appColorButton";
            this.appColorButton.Size = new System.Drawing.Size(101, 37);
            this.appColorButton.TabIndex = 13;
            this.appColorButton.Text = "App Color";
            this.appColorButton.UseVisualStyleBackColor = false;
            this.appColorButton.Click += new System.EventHandler(this.appColorButton_Click);
            // 
            // buttonColorButton
            // 
            this.buttonColorButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonColorButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonColorButton.Location = new System.Drawing.Point(130, 371);
            this.buttonColorButton.Name = "buttonColorButton";
            this.buttonColorButton.Size = new System.Drawing.Size(101, 37);
            this.buttonColorButton.TabIndex = 14;
            this.buttonColorButton.Text = "Secondary Color";
            this.buttonColorButton.UseVisualStyleBackColor = false;
            this.buttonColorButton.Click += new System.EventHandler(this.buttonColorButton_Click);
            // 
            // background
            // 
            this.background.Enabled = true;
            this.background.Location = new System.Drawing.Point(0, 0);
            this.background.Name = "background";
            this.background.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("background.OcxState")));
            this.background.Size = new System.Drawing.Size(480, 201);
            this.background.TabIndex = 11;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 471);
            this.Controls.Add(this.buttonColorButton);
            this.Controls.Add(this.appColorButton);
            this.Controls.Add(this.textColorButton);
            this.Controls.Add(this.background);
            this.Controls.Add(this.btnDarkMode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.musicComboBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SettingsForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SettingsForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SettingsForm_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.background)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox musicComboBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label1;
        private AxWMPLib.AxWindowsMediaPlayer background;
        private System.Windows.Forms.Button textColorButton;
        private System.Windows.Forms.Button appColorButton;
        private System.Windows.Forms.Button buttonColorButton;
        public System.Windows.Forms.Button btnDarkMode;
    }
}