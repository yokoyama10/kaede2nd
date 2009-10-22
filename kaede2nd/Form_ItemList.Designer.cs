namespace kaede2nd
{
    partial class Form_ItemList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ItemList));
            this.dataGridView1 = new kaede2nd.MyDataGridView();
            this.button_refresh = new System.Windows.Forms.Button();
            this.button_csv = new System.Windows.Forms.Button();
            this.button_search = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox_regex = new System.Windows.Forms.CheckBox();
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
            this.dataGridView1.Location = new System.Drawing.Point(0, 52);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(657, 295);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // button_refresh
            // 
            this.button_refresh.Location = new System.Drawing.Point(12, 12);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(75, 23);
            this.button_refresh.TabIndex = 4;
            this.button_refresh.Text = "更新";
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_csv
            // 
            this.button_csv.Location = new System.Drawing.Point(102, 12);
            this.button_csv.Name = "button_csv";
            this.button_csv.Size = new System.Drawing.Size(84, 23);
            this.button_csv.TabIndex = 6;
            this.button_csv.Text = "csv出力";
            this.button_csv.UseVisualStyleBackColor = true;
            this.button_csv.Click += new System.EventHandler(this.button2_Click);
            // 
            // button_search
            // 
            this.button_search.Location = new System.Drawing.Point(463, 15);
            this.button_search.Name = "button_search";
            this.button_search.Size = new System.Drawing.Size(72, 23);
            this.button_search.TabIndex = 7;
            this.button_search.Text = "検索";
            this.button_search.UseVisualStyleBackColor = true;
            this.button_search.Click += new System.EventHandler(this.button_search_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textBox1.Location = new System.Drawing.Point(309, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(148, 22);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "検索語";
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            // 
            // checkBox_regex
            // 
            this.checkBox_regex.AutoSize = true;
            this.checkBox_regex.Location = new System.Drawing.Point(541, 22);
            this.checkBox_regex.Name = "checkBox_regex";
            this.checkBox_regex.Size = new System.Drawing.Size(92, 16);
            this.checkBox_regex.TabIndex = 9;
            this.checkBox_regex.Text = "正規表現 (&R)";
            this.checkBox_regex.UseVisualStyleBackColor = true;
            // 
            // Form_ItemList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 347);
            this.Controls.Add(this.checkBox_regex);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_search);
            this.Controls.Add(this.button_csv);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button_refresh);
            this.KeyPreview = true;
            this.Name = "Form_ItemList";
            this.Text = "全商品リスト";
            this.Load += new System.EventHandler(this.Form_ItemList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_ItemList_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_refresh;
        private MyDataGridView dataGridView1;
        private System.Windows.Forms.Button button_csv;
        private System.Windows.Forms.Button button_search;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox_regex;

    }
}