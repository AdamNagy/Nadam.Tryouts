namespace Driver
{
    partial class MainForm
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
            this.ListBoxButton = new System.Windows.Forms.Button();
            this.DataGridViewButton = new System.Windows.Forms.Button();
            this.ComboBoxButton = new System.Windows.Forms.Button();
            this.DataGridAdvanceButton = new System.Windows.Forms.Button();
            this.BindingSourceButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ListBoxButton
            // 
            this.ListBoxButton.Location = new System.Drawing.Point(80, 35);
            this.ListBoxButton.Name = "ListBoxButton";
            this.ListBoxButton.Size = new System.Drawing.Size(185, 24);
            this.ListBoxButton.TabIndex = 0;
            this.ListBoxButton.Text = "ListBox Example";
            this.ListBoxButton.UseVisualStyleBackColor = true;
            this.ListBoxButton.Click += new System.EventHandler(this.ListBoxButton_Click);
            // 
            // DataGridViewButton
            // 
            this.DataGridViewButton.Location = new System.Drawing.Point(80, 95);
            this.DataGridViewButton.Name = "DataGridViewButton";
            this.DataGridViewButton.Size = new System.Drawing.Size(185, 24);
            this.DataGridViewButton.TabIndex = 2;
            this.DataGridViewButton.Text = "DataGridView Example";
            this.DataGridViewButton.UseVisualStyleBackColor = true;
            this.DataGridViewButton.Click += new System.EventHandler(this.DataGridViewButton_Click);
            // 
            // ComboBoxButton
            // 
            this.ComboBoxButton.Location = new System.Drawing.Point(80, 65);
            this.ComboBoxButton.Name = "ComboBoxButton";
            this.ComboBoxButton.Size = new System.Drawing.Size(185, 24);
            this.ComboBoxButton.TabIndex = 3;
            this.ComboBoxButton.Text = "ComboBox Example";
            this.ComboBoxButton.UseVisualStyleBackColor = true;
            this.ComboBoxButton.Click += new System.EventHandler(this.ComboBoxButton_Click);
            // 
            // DataGridAdvanceButton
            // 
            this.DataGridAdvanceButton.Location = new System.Drawing.Point(80, 125);
            this.DataGridAdvanceButton.Name = "DataGridAdvanceButton";
            this.DataGridAdvanceButton.Size = new System.Drawing.Size(185, 24);
            this.DataGridAdvanceButton.TabIndex = 4;
            this.DataGridAdvanceButton.Text = "DataGridView Advance Example";
            this.DataGridAdvanceButton.UseVisualStyleBackColor = true;
            this.DataGridAdvanceButton.Click += new System.EventHandler(this.DataGridAdvanceButton_Click);
            // 
            // BindingSourceButton
            // 
            this.BindingSourceButton.Location = new System.Drawing.Point(80, 155);
            this.BindingSourceButton.Name = "BindingSourceButton";
            this.BindingSourceButton.Size = new System.Drawing.Size(185, 24);
            this.BindingSourceButton.TabIndex = 5;
            this.BindingSourceButton.Text = "BindingSource Example";
            this.BindingSourceButton.UseVisualStyleBackColor = true;
            this.BindingSourceButton.Click += new System.EventHandler(this.BindingSourceButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 205);
            this.Controls.Add(this.BindingSourceButton);
            this.Controls.Add(this.DataGridAdvanceButton);
            this.Controls.Add(this.ComboBoxButton);
            this.Controls.Add(this.DataGridViewButton);
            this.Controls.Add(this.ListBoxButton);
            this.Name = "MainForm";
            this.Text = "Window Forms Binding Examples";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ListBoxButton;
        private System.Windows.Forms.Button DataGridViewButton;
        private System.Windows.Forms.Button ComboBoxButton;
        private System.Windows.Forms.Button DataGridAdvanceButton;
        private System.Windows.Forms.Button BindingSourceButton;


    }
}

