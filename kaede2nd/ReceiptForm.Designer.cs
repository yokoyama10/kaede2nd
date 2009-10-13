namespace kaede2nd
{
    partial class ReceiptForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceiptForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new kaede2nd.MyDataGridView();
            this.text_rid = new System.Windows.Forms.TextBox();
            this.text_pass = new System.Windows.Forms.TextBox();
            this.radio_zaigaku = new System.Windows.Forms.RadioButton();
            this.radio_legacy = new System.Windows.Forms.RadioButton();
            this.radio_donate = new System.Windows.Forms.RadioButton();
            this.radio_external = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label_nen = new System.Windows.Forms.Label();
            this.text_zai_nen = new System.Windows.Forms.TextBox();
            this.label_kumi = new System.Windows.Forms.Label();
            this.text_zai_kumi = new System.Windows.Forms.TextBox();
            this.label_ban = new System.Windows.Forms.Label();
            this.text_zai_ban = new System.Windows.Forms.TextBox();
            this.label_external = new System.Windows.Forms.Label();
            this.label_sakini = new System.Windows.Forms.Label();
            this.check_payback = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.contextMenuStrip_temp = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.商品名でIMEオンToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.商品名編集Enterで右移動ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.最新の情報に更新RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.商品リストを表示LToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.受付票を印刷ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.text_external = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip_temp.SuspendLayout();
            this.SuspendLayout();
            // 
            // webc
            // 
            this.webc.Headers = ((System.Net.WebHeaderCollection)(resources.GetObject("webc.Headers")));
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "票番";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "パスワード(&P)";
            this.label2.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 70);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(672, 331);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridView1_UserDeletingRow);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // text_rid
            // 
            this.text_rid.Location = new System.Drawing.Point(83, 15);
            this.text_rid.Name = "text_rid";
            this.text_rid.ReadOnly = true;
            this.text_rid.Size = new System.Drawing.Size(46, 19);
            this.text_rid.TabIndex = 3;
            this.text_rid.TabStop = false;
            this.text_rid.Text = "R0000";
            this.text_rid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // text_pass
            // 
            this.text_pass.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.text_pass.Location = new System.Drawing.Point(83, 39);
            this.text_pass.Name = "text_pass";
            this.text_pass.Size = new System.Drawing.Size(69, 19);
            this.text_pass.TabIndex = 7;
            this.text_pass.Text = "password";
            this.text_pass.Visible = false;
            this.text_pass.Enter += new System.EventHandler(this.text_Enter_SelectAll);
            // 
            // radio_zaigaku
            // 
            this.radio_zaigaku.AutoSize = true;
            this.radio_zaigaku.Checked = true;
            this.radio_zaigaku.Location = new System.Drawing.Point(171, 16);
            this.radio_zaigaku.Name = "radio_zaigaku";
            this.radio_zaigaku.Size = new System.Drawing.Size(63, 16);
            this.radio_zaigaku.TabIndex = 0;
            this.radio_zaigaku.TabStop = true;
            this.radio_zaigaku.Text = "在学(&G)";
            this.radio_zaigaku.UseVisualStyleBackColor = true;
            this.radio_zaigaku.CheckedChanged += new System.EventHandler(this.SellerRadio_Changed);
            // 
            // radio_legacy
            // 
            this.radio_legacy.AutoSize = true;
            this.radio_legacy.Location = new System.Drawing.Point(318, 16);
            this.radio_legacy.Name = "radio_legacy";
            this.radio_legacy.Size = new System.Drawing.Size(61, 16);
            this.radio_legacy.TabIndex = 6;
            this.radio_legacy.Text = "遺産(&L)";
            this.radio_legacy.UseVisualStyleBackColor = true;
            this.radio_legacy.CheckedChanged += new System.EventHandler(this.SellerRadio_Changed);
            // 
            // radio_donate
            // 
            this.radio_donate.AutoSize = true;
            this.radio_donate.Location = new System.Drawing.Point(389, 16);
            this.radio_donate.Name = "radio_donate";
            this.radio_donate.Size = new System.Drawing.Size(63, 16);
            this.radio_donate.TabIndex = 77;
            this.radio_donate.Text = "寄付(&D)";
            this.radio_donate.UseVisualStyleBackColor = true;
            this.radio_donate.CheckedChanged += new System.EventHandler(this.SellerRadio_Changed);
            // 
            // radio_external
            // 
            this.radio_external.AutoSize = true;
            this.radio_external.Location = new System.Drawing.Point(244, 16);
            this.radio_external.Name = "radio_external";
            this.radio_external.Size = new System.Drawing.Size(62, 16);
            this.radio_external.TabIndex = 2;
            this.radio_external.Text = "外部(&E)";
            this.radio_external.UseVisualStyleBackColor = true;
            this.radio_external.CheckedChanged += new System.EventHandler(this.SellerRadio_Changed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(400, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 12);
            this.label3.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(592, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 49);
            this.button1.TabIndex = 12;
            this.button1.Text = "←確定(&A)\r\n(Ctrl+Enter)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label_nen
            // 
            this.label_nen.AutoSize = true;
            this.label_nen.Location = new System.Drawing.Point(196, 42);
            this.label_nen.Name = "label_nen";
            this.label_nen.Size = new System.Drawing.Size(17, 12);
            this.label_nen.TabIndex = 15;
            this.label_nen.Text = "年";
            // 
            // text_zai_nen
            // 
            this.text_zai_nen.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.text_zai_nen.Location = new System.Drawing.Point(171, 39);
            this.text_zai_nen.Name = "text_zai_nen";
            this.text_zai_nen.Size = new System.Drawing.Size(22, 19);
            this.text_zai_nen.TabIndex = 1;
            this.text_zai_nen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.text_zai_nen.Enter += new System.EventHandler(this.text_Enter_SelectAll);
            // 
            // label_kumi
            // 
            this.label_kumi.AutoSize = true;
            this.label_kumi.Location = new System.Drawing.Point(237, 42);
            this.label_kumi.Name = "label_kumi";
            this.label_kumi.Size = new System.Drawing.Size(17, 12);
            this.label_kumi.TabIndex = 17;
            this.label_kumi.Text = "組";
            // 
            // text_zai_kumi
            // 
            this.text_zai_kumi.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.text_zai_kumi.Location = new System.Drawing.Point(212, 39);
            this.text_zai_kumi.Name = "text_zai_kumi";
            this.text_zai_kumi.Size = new System.Drawing.Size(22, 19);
            this.text_zai_kumi.TabIndex = 2;
            this.text_zai_kumi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.text_zai_kumi.Enter += new System.EventHandler(this.text_Enter_SelectAll);
            // 
            // label_ban
            // 
            this.label_ban.AutoSize = true;
            this.label_ban.Location = new System.Drawing.Point(291, 42);
            this.label_ban.Name = "label_ban";
            this.label_ban.Size = new System.Drawing.Size(17, 12);
            this.label_ban.TabIndex = 19;
            this.label_ban.Text = "番";
            // 
            // text_zai_ban
            // 
            this.text_zai_ban.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.text_zai_ban.Location = new System.Drawing.Point(254, 39);
            this.text_zai_ban.Name = "text_zai_ban";
            this.text_zai_ban.Size = new System.Drawing.Size(33, 19);
            this.text_zai_ban.TabIndex = 3;
            this.text_zai_ban.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.text_zai_ban.Enter += new System.EventHandler(this.text_Enter_SelectAll);
            // 
            // label_external
            // 
            this.label_external.AutoSize = true;
            this.label_external.Location = new System.Drawing.Point(314, 42);
            this.label_external.Name = "label_external";
            this.label_external.Size = new System.Drawing.Size(29, 12);
            this.label_external.TabIndex = 78;
            this.label_external.Text = "名前";
            // 
            // label_sakini
            // 
            this.label_sakini.AutoSize = true;
            this.label_sakini.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label_sakini.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_sakini.ForeColor = System.Drawing.Color.Red;
            this.label_sakini.Location = new System.Drawing.Point(6, 79);
            this.label_sakini.Name = "label_sakini";
            this.label_sakini.Size = new System.Drawing.Size(486, 35);
            this.label_sakini.TabIndex = 79;
            this.label_sakini.Text = "先に↑の情報を確定させてください";
            // 
            // check_payback
            // 
            this.check_payback.AutoSize = true;
            this.check_payback.Location = new System.Drawing.Point(501, 17);
            this.check_payback.Name = "check_payback";
            this.check_payback.Size = new System.Drawing.Size(76, 16);
            this.check_payback.TabIndex = 10;
            this.check_payback.TabStop = false;
            this.check_payback.Text = "返金済(&B)";
            this.check_payback.ThreeState = true;
            this.check_payback.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(518, 39);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(59, 19);
            this.button2.TabIndex = 80;
            this.button2.Text = "メニュー";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // contextMenuStrip_temp
            // 
            this.contextMenuStrip_temp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.商品名でIMEオンToolStripMenuItem,
            this.商品名編集Enterで右移動ToolStripMenuItem,
            this.toolStripSeparator2,
            this.最新の情報に更新RToolStripMenuItem,
            this.商品リストを表示LToolStripMenuItem,
            this.受付票を印刷ToolStripMenuItem});
            this.contextMenuStrip_temp.Name = "contextMenuStrip_temp";
            this.contextMenuStrip_temp.ShowCheckMargin = true;
            this.contextMenuStrip_temp.ShowImageMargin = false;
            this.contextMenuStrip_temp.Size = new System.Drawing.Size(223, 120);
            // 
            // 商品名でIMEオンToolStripMenuItem
            // 
            this.商品名でIMEオンToolStripMenuItem.Checked = true;
            this.商品名でIMEオンToolStripMenuItem.CheckOnClick = true;
            this.商品名でIMEオンToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.商品名でIMEオンToolStripMenuItem.Name = "商品名でIMEオンToolStripMenuItem";
            this.商品名でIMEオンToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.商品名でIMEオンToolStripMenuItem.Text = "商品名でIMEオン (&I)";
            this.商品名でIMEオンToolStripMenuItem.Click += new System.EventHandler(this.商品名でIMEオンToolStripMenuItem_Click);
            // 
            // 商品名編集Enterで右移動ToolStripMenuItem
            // 
            this.商品名編集Enterで右移動ToolStripMenuItem.CheckOnClick = true;
            this.商品名編集Enterで右移動ToolStripMenuItem.Name = "商品名編集Enterで右移動ToolStripMenuItem";
            this.商品名編集Enterで右移動ToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.商品名編集Enterで右移動ToolStripMenuItem.Text = "商品名編集Enterで右移動 (&E)";
            this.商品名編集Enterで右移動ToolStripMenuItem.Click += new System.EventHandler(this.商品名編集Enterで右移動ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(219, 6);
            // 
            // 最新の情報に更新RToolStripMenuItem
            // 
            this.最新の情報に更新RToolStripMenuItem.Name = "最新の情報に更新RToolStripMenuItem";
            this.最新の情報に更新RToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.最新の情報に更新RToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.最新の情報に更新RToolStripMenuItem.Text = "最新の情報に更新 (&R)";
            this.最新の情報に更新RToolStripMenuItem.Click += new System.EventHandler(this.最新の情報に更新RToolStripMenuItem_Click);
            // 
            // 商品リストを表示LToolStripMenuItem
            // 
            this.商品リストを表示LToolStripMenuItem.Name = "商品リストを表示LToolStripMenuItem";
            this.商品リストを表示LToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.商品リストを表示LToolStripMenuItem.Text = "商品リストを表示 (&L)";
            this.商品リストを表示LToolStripMenuItem.Click += new System.EventHandler(this.商品リストを表示LToolStripMenuItem_Click);
            // 
            // 受付票を印刷ToolStripMenuItem
            // 
            this.受付票を印刷ToolStripMenuItem.Name = "受付票を印刷ToolStripMenuItem";
            this.受付票を印刷ToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.受付票を印刷ToolStripMenuItem.Text = "受付票を印刷 (&P)...";
            this.受付票を印刷ToolStripMenuItem.Click += new System.EventHandler(this.票印刷ToolStripMenuItem_Click);
            // 
            // text_external
            // 
            this.text_external.FormattingEnabled = true;
            this.text_external.Location = new System.Drawing.Point(349, 38);
            this.text_external.Name = "text_external";
            this.text_external.Size = new System.Drawing.Size(149, 20);
            this.text_external.TabIndex = 4;
            this.text_external.Enter += new System.EventHandler(this.text_Enter_SelectAll);
            // 
            // ReceiptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 401);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label_sakini);
            this.Controls.Add(this.text_external);
            this.Controls.Add(this.label_external);
            this.Controls.Add(this.label_ban);
            this.Controls.Add(this.text_zai_ban);
            this.Controls.Add(this.label_kumi);
            this.Controls.Add(this.text_zai_kumi);
            this.Controls.Add(this.label_nen);
            this.Controls.Add(this.text_zai_nen);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.check_payback);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.radio_external);
            this.Controls.Add(this.radio_donate);
            this.Controls.Add(this.radio_legacy);
            this.Controls.Add(this.radio_zaigaku);
            this.Controls.Add(this.text_pass);
            this.Controls.Add(this.text_rid);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Name = "ReceiptForm";
            this.Text = "ReceiptForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReceiptForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip_temp.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox text_rid;
        private System.Windows.Forms.TextBox text_pass;
        private System.Windows.Forms.RadioButton radio_zaigaku;
        private System.Windows.Forms.RadioButton radio_legacy;
        private System.Windows.Forms.RadioButton radio_donate;
        private System.Windows.Forms.RadioButton radio_external;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label_nen;
        private System.Windows.Forms.TextBox text_zai_nen;
        private System.Windows.Forms.Label label_kumi;
        private System.Windows.Forms.TextBox text_zai_kumi;
        private System.Windows.Forms.Label label_ban;
        private System.Windows.Forms.TextBox text_zai_ban;
        private System.Windows.Forms.Label label_external;
        private System.Windows.Forms.Label label_sakini;
        private MyDataGridView dataGridView1;
        private System.Windows.Forms.CheckBox check_payback;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_temp;
        private System.Windows.Forms.ToolStripMenuItem 商品名でIMEオンToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 商品名編集Enterで右移動ToolStripMenuItem;
        private System.Windows.Forms.ComboBox text_external;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 受付票を印刷ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 最新の情報に更新RToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 商品リストを表示LToolStripMenuItem;
    }
}