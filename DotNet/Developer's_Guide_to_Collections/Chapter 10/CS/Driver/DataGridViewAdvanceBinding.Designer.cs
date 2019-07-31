namespace Driver
{
    partial class DataGridViewAdvanceBinding
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
            this.SearchPropertyComboBox = new System.Windows.Forms.ComboBox();
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.RemoveIndexButton = new System.Windows.Forms.Button();
            this.AddIndexButton = new System.Windows.Forms.Button();
            this.lbIndexedProperties = new System.Windows.Forms.ListBox();
            this.lbUnindexedProperties = new System.Windows.Forms.ListBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.FilterTextBox = new System.Windows.Forms.TextBox();
            this.FilterButton = new System.Windows.Forms.Button();
            this.SortTextBox = new System.Windows.Forms.TextBox();
            this.SortButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // SearchPropertyComboBox
            // 
            this.SearchPropertyComboBox.FormattingEnabled = true;
            this.SearchPropertyComboBox.Location = new System.Drawing.Point(500, 326);
            this.SearchPropertyComboBox.Name = "SearchPropertyComboBox";
            this.SearchPropertyComboBox.Size = new System.Drawing.Size(166, 21);
            this.SearchPropertyComboBox.TabIndex = 21;
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.Location = new System.Drawing.Point(412, 353);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(254, 20);
            this.SearchTextBox.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(409, 329);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Search property ";
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(591, 379);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 18;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 295);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Indexed Properties";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 295);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Unindexed Properties";
            // 
            // RemoveIndexButton
            // 
            this.RemoveIndexButton.Location = new System.Drawing.Point(154, 447);
            this.RemoveIndexButton.Name = "RemoveIndexButton";
            this.RemoveIndexButton.Size = new System.Drawing.Size(41, 23);
            this.RemoveIndexButton.TabIndex = 15;
            this.RemoveIndexButton.Text = "<<";
            this.RemoveIndexButton.UseVisualStyleBackColor = true;
            this.RemoveIndexButton.Click += new System.EventHandler(this.RemoveIndexButton_Click);
            // 
            // AddIndexButton
            // 
            this.AddIndexButton.Location = new System.Drawing.Point(154, 418);
            this.AddIndexButton.Name = "AddIndexButton";
            this.AddIndexButton.Size = new System.Drawing.Size(41, 23);
            this.AddIndexButton.TabIndex = 14;
            this.AddIndexButton.Text = ">>";
            this.AddIndexButton.UseVisualStyleBackColor = true;
            this.AddIndexButton.Click += new System.EventHandler(this.AddIndexButton_Click);
            // 
            // lbIndexedProperties
            // 
            this.lbIndexedProperties.FormattingEnabled = true;
            this.lbIndexedProperties.Location = new System.Drawing.Point(201, 331);
            this.lbIndexedProperties.Name = "lbIndexedProperties";
            this.lbIndexedProperties.Size = new System.Drawing.Size(120, 251);
            this.lbIndexedProperties.TabIndex = 13;
            // 
            // lbUnindexedProperties
            // 
            this.lbUnindexedProperties.FormattingEnabled = true;
            this.lbUnindexedProperties.Location = new System.Drawing.Point(28, 331);
            this.lbUnindexedProperties.Name = "lbUnindexedProperties";
            this.lbUnindexedProperties.Size = new System.Drawing.Size(120, 251);
            this.lbUnindexedProperties.TabIndex = 12;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(28, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(638, 271);
            this.dataGridView1.TabIndex = 11;
            // 
            // FilterTextBox
            // 
            this.FilterTextBox.Location = new System.Drawing.Point(412, 418);
            this.FilterTextBox.Multiline = true;
            this.FilterTextBox.Name = "FilterTextBox";
            this.FilterTextBox.Size = new System.Drawing.Size(254, 60);
            this.FilterTextBox.TabIndex = 23;
            // 
            // FilterButton
            // 
            this.FilterButton.Location = new System.Drawing.Point(591, 484);
            this.FilterButton.Name = "FilterButton";
            this.FilterButton.Size = new System.Drawing.Size(75, 23);
            this.FilterButton.TabIndex = 22;
            this.FilterButton.Text = "Filter";
            this.FilterButton.UseVisualStyleBackColor = true;
            this.FilterButton.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // SortTextBox
            // 
            this.SortTextBox.Location = new System.Drawing.Point(412, 522);
            this.SortTextBox.Multiline = true;
            this.SortTextBox.Name = "SortTextBox";
            this.SortTextBox.Size = new System.Drawing.Size(254, 60);
            this.SortTextBox.TabIndex = 25;
            // 
            // SortButton
            // 
            this.SortButton.Location = new System.Drawing.Point(591, 588);
            this.SortButton.Name = "SortButton";
            this.SortButton.Size = new System.Drawing.Size(75, 23);
            this.SortButton.TabIndex = 24;
            this.SortButton.Text = "Sort";
            this.SortButton.UseVisualStyleBackColor = true;
            this.SortButton.Click += new System.EventHandler(this.SortButton_Click);
            // 
            // DataGridViewAdvanceBinding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 616);
            this.Controls.Add(this.SortTextBox);
            this.Controls.Add(this.SortButton);
            this.Controls.Add(this.FilterTextBox);
            this.Controls.Add(this.FilterButton);
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
            this.Name = "DataGridViewAdvanceBinding";
            this.Text = "DataGridViewAdvanceBinding";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox SearchPropertyComboBox;
        private System.Windows.Forms.TextBox SearchTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RemoveIndexButton;
        private System.Windows.Forms.Button AddIndexButton;
        private System.Windows.Forms.ListBox lbIndexedProperties;
        private System.Windows.Forms.ListBox lbUnindexedProperties;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox FilterTextBox;
        private System.Windows.Forms.Button FilterButton;
        private System.Windows.Forms.TextBox SortTextBox;
        private System.Windows.Forms.Button SortButton;
    }
}