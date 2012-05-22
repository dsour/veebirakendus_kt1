namespace ScheduleClientForms
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.startStationBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.endStationBox = new System.Windows.Forms.ComboBox();
            this.labelResults = new System.Windows.Forms.Label();
            this.searchButton = new System.Windows.Forms.Button();
            this.resultsListBox = new System.Windows.Forms.ListBox();
            this.returnButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vali lähtejaam";
            // 
            // startStationBox
            // 
            this.startStationBox.FormattingEnabled = true;
            this.startStationBox.Location = new System.Drawing.Point(16, 45);
            this.startStationBox.Name = "startStationBox";
            this.startStationBox.Size = new System.Drawing.Size(121, 21);
            this.startStationBox.TabIndex = 1;
            this.startStationBox.SelectedIndexChanged += new System.EventHandler(this.startStationBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(165, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Vali sihtkoht";
            // 
            // endStationBox
            // 
            this.endStationBox.FormattingEnabled = true;
            this.endStationBox.Location = new System.Drawing.Point(168, 44);
            this.endStationBox.Name = "endStationBox";
            this.endStationBox.Size = new System.Drawing.Size(121, 21);
            this.endStationBox.TabIndex = 3;
            // 
            // labelResults
            // 
            this.labelResults.AutoSize = true;
            this.labelResults.Location = new System.Drawing.Point(13, 87);
            this.labelResults.Name = "labelResults";
            this.labelResults.Size = new System.Drawing.Size(178, 13);
            this.labelResults.TabIndex = 4;
            this.labelResults.Text = "Järgmised väljumis- ja saabumisajad:";
            this.labelResults.Visible = false;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(306, 42);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 8;
            this.searchButton.Text = "Otsi väljumisi";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // resultsListBox
            // 
            this.resultsListBox.FormattingEnabled = true;
            this.resultsListBox.Location = new System.Drawing.Point(16, 114);
            this.resultsListBox.Name = "resultsListBox";
            this.resultsListBox.Size = new System.Drawing.Size(175, 69);
            this.resultsListBox.TabIndex = 9;
            this.resultsListBox.Visible = false;
            // 
            // returnButton
            // 
            this.returnButton.Location = new System.Drawing.Point(16, 190);
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(107, 23);
            this.returnButton.TabIndex = 10;
            this.returnButton.Text = "Otsi tagasireise";
            this.returnButton.UseVisualStyleBackColor = true;
            this.returnButton.Visible = false;
            this.returnButton.Click += new System.EventHandler(this.returnButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 264);
            this.Controls.Add(this.returnButton);
            this.Controls.Add(this.resultsListBox);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.labelResults);
            this.Controls.Add(this.endStationBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.startStationBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Unusual Suspects presents: Incredible Timetable for Trains";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox startStationBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox endStationBox;
        private System.Windows.Forms.Label labelResults;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.ListBox resultsListBox;
        private System.Windows.Forms.Button returnButton;
    }
}

