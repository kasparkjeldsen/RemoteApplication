namespace AppClient
{
    partial class AppClient
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
            this.procs = new System.Windows.Forms.ComboBox();
            this.open = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // procs
            // 
            this.procs.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.procs.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.procs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.procs.FormattingEnabled = true;
            this.procs.Location = new System.Drawing.Point(12, 12);
            this.procs.Name = "procs";
            this.procs.Size = new System.Drawing.Size(260, 21);
            this.procs.TabIndex = 0;
            // 
            // open
            // 
            this.open.Location = new System.Drawing.Point(12, 39);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(75, 23);
            this.open.TabIndex = 1;
            this.open.Text = "Open";
            this.open.UseVisualStyleBackColor = true;
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // AppClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.open);
            this.Controls.Add(this.procs);
            this.Name = "AppClient";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox procs;
        private System.Windows.Forms.Button open;
    }
}

