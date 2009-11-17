namespace kaede2nd
{
    partial class HenkinForm
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
            this.button_refresh = new System.Windows.Forms.Button();
            this.dataGridView1 = new kaede2nd.MyDataGridView();
            this.button_return_print = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button_csv = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button_meisai_print = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_refresh
            // 
            this.button_refresh.Location = new System.Drawing.Point(12, 12);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(75, 23);
            this.button_refresh.TabIndex = 0;
            this.button_refresh.Text = "更新";
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);
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
            this.dataGridView1.Location = new System.Drawing.Point(0, 71);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(583, 294);
            this.dataGridView1.TabIndex = 6;
            // 
            // button_return_print
            // 
            this.button_return_print.Location = new System.Drawing.Point(175, 35);
            this.button_return_print.Name = "button_return_print";
            this.button_return_print.Size = new System.Drawing.Size(54, 23);
            this.button_return_print.TabIndex = 7;
            this.button_return_print.Text = "印刷";
            this.button_return_print.UseVisualStyleBackColor = true;
            this.button_return_print.Click += new System.EventHandler(this.button_print_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(295, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "全て印刷or行選択";
            // 
            // button_csv
            // 
            this.button_csv.Location = new System.Drawing.Point(420, 12);
            this.button_csv.Name = "button_csv";
            this.button_csv.Size = new System.Drawing.Size(93, 23);
            this.button_csv.TabIndex = 9;
            this.button_csv.Text = "表をcsv出力";
            this.button_csv.UseVisualStyleBackColor = true;
            this.button_csv.Click += new System.EventHandler(this.button_csv_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "返品リストを";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(235, 35);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(54, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "csv";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(131, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "明細を";
            // 
            // button_meisai_print
            // 
            this.button_meisai_print.Location = new System.Drawing.Point(175, 6);
            this.button_meisai_print.Name = "button_meisai_print";
            this.button_meisai_print.Size = new System.Drawing.Size(54, 23);
            this.button_meisai_print.TabIndex = 12;
            this.button_meisai_print.Text = "印刷";
            this.button_meisai_print.UseVisualStyleBackColor = true;
            this.button_meisai_print.Click += new System.EventHandler(this.button_print_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(235, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(54, 23);
            this.button3.TabIndex = 14;
            this.button3.Text = "csv";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // HenkinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 365);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_meisai_print);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_csv);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_return_print);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button_refresh);
            this.Name = "HenkinForm";
            this.Text = "返金・返品";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_refresh;
        private MyDataGridView dataGridView1;
        private System.Windows.Forms.Button button_return_print;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_csv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_meisai_print;
        private System.Windows.Forms.Button button3;
    }
}