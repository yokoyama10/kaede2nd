namespace kaede2nd
{
    partial class RecentItem
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecentItem));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button_print = new System.Windows.Forms.Button();
            this.text_kensuu = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_ext = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.最新の情報に更新RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.常に手前に表示AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.表示商品全てを印刷PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.選択した商品を印刷SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.最後に印刷した商品をリストに復活ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.選択した商品をリストから除去ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 39);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(412, 263);
            this.dataGridView1.TabIndex = 0;
            // 
            // button_print
            // 
            this.button_print.Location = new System.Drawing.Point(12, 6);
            this.button_print.Name = "button_print";
            this.button_print.Size = new System.Drawing.Size(208, 27);
            this.button_print.TabIndex = 1;
            this.button_print.Text = "最古n件を印刷 (Ctrl+P)\r";
            this.button_print.UseVisualStyleBackColor = true;
            this.button_print.Click += new System.EventHandler(this.button_print_Click);
            // 
            // text_kensuu
            // 
            this.text_kensuu.BackColor = System.Drawing.SystemColors.Window;
            this.text_kensuu.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.text_kensuu.Location = new System.Drawing.Point(335, 10);
            this.text_kensuu.Name = "text_kensuu";
            this.text_kensuu.ReadOnly = true;
            this.text_kensuu.Size = new System.Drawing.Size(41, 23);
            this.text_kensuu.TabIndex = 2;
            this.text_kensuu.Text = "0";
            this.text_kensuu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(382, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "件";
            // 
            // button_ext
            // 
            this.button_ext.Location = new System.Drawing.Point(243, 12);
            this.button_ext.Name = "button_ext";
            this.button_ext.Size = new System.Drawing.Size(72, 21);
            this.button_ext.TabIndex = 4;
            this.button_ext.Text = "その他(&O)";
            this.button_ext.UseVisualStyleBackColor = true;
            this.button_ext.Click += new System.EventHandler(this.button_ext_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.最新の情報に更新RToolStripMenuItem,
            this.常に手前に表示AToolStripMenuItem,
            this.toolStripSeparator1,
            this.表示商品全てを印刷PToolStripMenuItem,
            this.選択した商品を印刷SToolStripMenuItem,
            this.toolStripSeparator2,
            this.最後に印刷した商品をリストに復活ToolStripMenuItem,
            this.選択した商品をリストから除去ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(255, 148);
            // 
            // 最新の情報に更新RToolStripMenuItem
            // 
            this.最新の情報に更新RToolStripMenuItem.Name = "最新の情報に更新RToolStripMenuItem";
            this.最新の情報に更新RToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.最新の情報に更新RToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.最新の情報に更新RToolStripMenuItem.Text = "最新の情報に更新 (&R)";
            this.最新の情報に更新RToolStripMenuItem.Click += new System.EventHandler(this.最新の情報に更新RToolStripMenuItem_Click);
            // 
            // 常に手前に表示AToolStripMenuItem
            // 
            this.常に手前に表示AToolStripMenuItem.Checked = true;
            this.常に手前に表示AToolStripMenuItem.CheckOnClick = true;
            this.常に手前に表示AToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.常に手前に表示AToolStripMenuItem.Name = "常に手前に表示AToolStripMenuItem";
            this.常に手前に表示AToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.常に手前に表示AToolStripMenuItem.Text = "常に手前に表示 (&A)";
            this.常に手前に表示AToolStripMenuItem.Click += new System.EventHandler(this.常に手前に表示AToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(251, 6);
            // 
            // 表示商品全てを印刷PToolStripMenuItem
            // 
            this.表示商品全てを印刷PToolStripMenuItem.Name = "表示商品全てを印刷PToolStripMenuItem";
            this.表示商品全てを印刷PToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.表示商品全てを印刷PToolStripMenuItem.Text = "表示商品全てを印刷 (&P)...";
            this.表示商品全てを印刷PToolStripMenuItem.Click += new System.EventHandler(this.表示商品全てを印刷PToolStripMenuItem_Click);
            // 
            // 選択した商品を印刷SToolStripMenuItem
            // 
            this.選択した商品を印刷SToolStripMenuItem.Enabled = false;
            this.選択した商品を印刷SToolStripMenuItem.Name = "選択した商品を印刷SToolStripMenuItem";
            this.選択した商品を印刷SToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.選択した商品を印刷SToolStripMenuItem.Text = "選択した商品を印刷 (&S)...";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(251, 6);
            // 
            // 最後に印刷した商品をリストに復活ToolStripMenuItem
            // 
            this.最後に印刷した商品をリストに復活ToolStripMenuItem.Name = "最後に印刷した商品をリストに復活ToolStripMenuItem";
            this.最後に印刷した商品をリストに復活ToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.最後に印刷した商品をリストに復活ToolStripMenuItem.Text = "最後に印刷した商品をリストに復活 (&F)";
            this.最後に印刷した商品をリストに復活ToolStripMenuItem.Click += new System.EventHandler(this.最後に印刷した商品をリストに復活ToolStripMenuItem_Click);
            // 
            // 選択した商品をリストから除去ToolStripMenuItem
            // 
            this.選択した商品をリストから除去ToolStripMenuItem.Enabled = false;
            this.選択した商品をリストから除去ToolStripMenuItem.Name = "選択した商品をリストから除去ToolStripMenuItem";
            this.選択した商品をリストから除去ToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.選択した商品をリストから除去ToolStripMenuItem.Text = "選択した商品をリストから除去 (&D)";
            // 
            // RecentItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 302);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_ext);
            this.Controls.Add(this.text_kensuu);
            this.Controls.Add(this.button_print);
            this.Location = new System.Drawing.Point(700, 400);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RecentItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "最近追加された商品リスト";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.RecentItem_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RecentItem_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RecentItem_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_print;
        private System.Windows.Forms.TextBox text_kensuu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_ext;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 最新の情報に更新RToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 表示商品全てを印刷PToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 選択した商品を印刷SToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 常に手前に表示AToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 選択した商品をリストから除去ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 最後に印刷した商品をリストに復活ToolStripMenuItem;
    }
}