namespace Driver
{
    partial class DataGridViewBinding
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lbUnindexedProperties = new System.Windows.Forms.ListBox();
            this.lbIndexedProperties = new System.Windows.Forms.ListBox();
            this.AddIndexButton = new System.Windows.Forms.Button();
            this.RemoveIndexButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.SearchPropertyComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(32, 23);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(638, 271);
            this.dataGridView1.TabIndex = 0;
            // 
            // lbUnindexedProperties
            // 
            this.lbUnindexedProperties.FormattingEnabled = true;
            this.lbUnindexedProperties.Location = new System.Drawing.Point(32, 342);
            this.lbUnindexedProperties.Name = "lbUnindexedProperties";
            this.lbUnindexedProperties.Size = new System.Drawing.Size(120, 95);
            this.lbUnindexedProperties.TabIndex = 1;
            // 
            // lbIndexedProperties
            // 
            this.lbIndexedProperties.FormattingEnabled = true;
            this.lbIndexedProperties.Location = new System.Drawing.Point(205, 342);
            this.lbIndexedProperties.Name = "lbIndexedProperties";
            this.lbIndexedProperties.Size = new System.Drawing.Size(120, 95);
            this.lbIndexedProperties.TabIndex = 2;
            // 
            // AddIndexButton
            // 
            this.AddIndexButton.Location = new System.Drawing.Point(158, 361);
            this.AddIndexButton.Name = "AddIndexButton";
            this.AddIndexButton.Size = new System.Drawing.Size(41, 23);
            this.AddIndexButton.TabIndex = 3;
            this.AddIndexButton.Text = ">>";
            this.AddIndexButton.UseVisualStyleBackColor = true;
            this.AddIndexButton.Click += new System.EventHandler(this.AddIndexButton_Click);
            // 
            // RemoveIndexButton
            // 
            this.RemoveIndexButton.Location = new System.Drawing.Point(158, 390);
            this.RemoveIndexButton.Name = "RemoveIndexButton";
            this.RemoveIndexButton.Size = new System.Drawing.Size(41, 23);
            this.RemoveIndexButton.TabIndex = 4;
            this.RemoveIndexButton.Text = "<<";
            this.RemoveIndexButton.UseVisualStyleBackColor = true;
            this.RemoveIndexButton.Click += new System.EventHandler(this.RemoveIndexButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 306);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Unindexed Properties";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 306);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Indexed Properties";
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(595, 390);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 7;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(413, 340);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Search property ";
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.Location = new System.Drawing.Point(416, 364);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(254, 20);
            this.SearchTextBox.TabIndex = 9;
            // 
            // SearchPropertyComboBox
            // 
            this.SearchPropertyComboBox.FormattingEnabled = true;
            this.SearchPropertyComboBox.Location = new System.Drawing.Point(504, 337);
            this.SearchPropertyComboBox.Name = "SearchPropertyComboBox";
            this.SearchPropertyComboBox.Size = new System.Drawing.Size(166, 21);
            this.SearchPropertyComboBox.TabIndex = 10;
            // 
            // DataGridViewBinding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 453);
            this.Controls.Add(this.SearchPropertyComboBox);
            this.Controls.Add(this.SearchTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RemoveIndexButton);
            this.Controls.Add(this.AddIndexButton);
            this.Controls.Add(this.lbIndexedProperties);
            this.Controls.Add(this.lbUnindexedProperties);
            this.Controls.Add(this.dataGridView1);
            this.Name = "DataGridViewBinding";
            this.Text = "DataGridViewBinding";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ListBox lbUnindexedProperties;
        private System.Windows.Forms.ListBox lbIndexedProperties;
        private System.Windows.Forms.Button AddIndexButton;
        private System.Windows.Forms.Button RemoveIndexButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox SearchTextBox;
        private System.Windows.Forms.ComboBox SearchPropertyComboBox;
    }
}