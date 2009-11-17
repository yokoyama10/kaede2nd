namespace kaede2nd
{
    partial class GassanForm
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
            this.dataGridView1 = new kaede2nd.MyDataGridView();
            this.button_reload = new System.Windows.Forms.Button();
            this.button_csv = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 44);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(410, 336);
            this.dataGridView1.TabIndex = 7;
            // 
            // button_reload
            // 
            this.button_reload.Location = new System.Drawing.Point(15, 10);
            this.button_reload.Name = "button_reload";
            this.button_reload.Size = new System.Drawing.Size(60, 23);
            this.button_reload.TabIndex = 8;
            this.button_reload.Text = "更新";
            this.button_reload.UseVisualStyleBackColor = true;
            this.button_reload.Click += new System.EventHandler(this.button_reload_Click);
            // 
            // button_csv
            // 
            this.button_csv.Location = new System.Drawing.Point(102, 10);
            this.button_csv.Name = "button_csv";
            this.button_csv.Size = new System.Drawing.Size(60, 23);
            this.button_csv.TabIndex = 9;
            this.button_csv.Text = "csv";
            this.button_csv.UseVisualStyleBackColor = true;
            this.button_csv.Click += new System.EventHandler(this.button_csv_Click);
            // 
            // GassanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 380);
            this.Controls.Add(this.button_csv);
            this.Controls.Add(this.button_reload);
            this.Controls.Add(this.dataGridView1);
            this.Name = "GassanForm";
            this.Text = "返金額を合算";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MyDataGridView dataGridView1;
        private System.Windows.Forms.Button button_reload;
        private System.Windows.Forms.Button button_csv;

    }
}