namespace kaede2nd
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.cSVで出力CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.楓ちゃん形式で出力KToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.最近追加された商品リストLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ページ設定UToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.タグ印刷ごとにダイアログを表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.選択中の票を印刷PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ログイン画面に戻るLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.新Receiptを追加UToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.最新の情報に更新RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.品番カウンタをセットしなおすToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.機能ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.売却ウィンドウSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.監査ウィンドウWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.オペレーターIDを管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.返金返品ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.各部門の返金額合算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.バージョン情報ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.label_company = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 31);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(103, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "新Receipt (F3)";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(236, 31);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(81, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "タグ印刷";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(337, 31);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(86, 23);
            this.button5.TabIndex = 6;
            this.button5.Text = "全ての商品";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 60);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(713, 329);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.機能ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(713, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.cSVで出力CToolStripMenuItem,
            this.楓ちゃん形式で出力KToolStripMenuItem,
            this.toolStripSeparator3,
            this.最近追加された商品リストLToolStripMenuItem,
            this.toolStripSeparator1,
            this.ページ設定UToolStripMenuItem,
            this.タグ印刷ごとにダイアログを表示ToolStripMenuItem,
            this.選択中の票を印刷PToolStripMenuItem,
            this.toolStripSeparator4,
            this.ログイン画面に戻るLToolStripMenuItem,
            this.終了XToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(66, 20);
            this.toolStripMenuItem1.Text = "ファイル(&F)";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.toolStripMenuItem3.Size = new System.Drawing.Size(246, 22);
            this.toolStripMenuItem3.Text = "バックアップを保存(&B)";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // cSVで出力CToolStripMenuItem
            // 
            this.cSVで出力CToolStripMenuItem.Name = "cSVで出力CToolStripMenuItem";
            this.cSVで出力CToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.cSVで出力CToolStripMenuItem.Text = "CSVで出力(&C)...";
            this.cSVで出力CToolStripMenuItem.Click += new System.EventHandler(this.cSVで出力CToolStripMenuItem_Click);
            // 
            // 楓ちゃん形式で出力KToolStripMenuItem
            // 
            this.楓ちゃん形式で出力KToolStripMenuItem.Name = "楓ちゃん形式で出力KToolStripMenuItem";
            this.楓ちゃん形式で出力KToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.楓ちゃん形式で出力KToolStripMenuItem.Text = "楓ちゃん形式で出力(&K)...";
            this.楓ちゃん形式で出力KToolStripMenuItem.Click += new System.EventHandler(this.kaedeOutput);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(243, 6);
            // 
            // 最近追加された商品リストLToolStripMenuItem
            // 
            this.最近追加された商品リストLToolStripMenuItem.Name = "最近追加された商品リストLToolStripMenuItem";
            this.最近追加された商品リストLToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.最近追加された商品リストLToolStripMenuItem.Text = "最近追加された商品リスト (&L)";
            this.最近追加された商品リストLToolStripMenuItem.Click += new System.EventHandler(this.最近追加された商品リストLToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(243, 6);
            // 
            // ページ設定UToolStripMenuItem
            // 
            this.ページ設定UToolStripMenuItem.Name = "ページ設定UToolStripMenuItem";
            this.ページ設定UToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.ページ設定UToolStripMenuItem.Text = "ページ設定(&U)...";
            this.ページ設定UToolStripMenuItem.Click += new System.EventHandler(this.ページ設定UToolStripMenuItem_Click);
            // 
            // タグ印刷ごとにダイアログを表示ToolStripMenuItem
            // 
            this.タグ印刷ごとにダイアログを表示ToolStripMenuItem.Checked = true;
            this.タグ印刷ごとにダイアログを表示ToolStripMenuItem.CheckOnClick = true;
            this.タグ印刷ごとにダイアログを表示ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.タグ印刷ごとにダイアログを表示ToolStripMenuItem.Name = "タグ印刷ごとにダイアログを表示ToolStripMenuItem";
            this.タグ印刷ごとにダイアログを表示ToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.タグ印刷ごとにダイアログを表示ToolStripMenuItem.Text = "タグ印刷ごとにダイアログを表示";
            this.タグ印刷ごとにダイアログを表示ToolStripMenuItem.Click += new System.EventHandler(this.タグ印刷ごとにダイアログを表示ToolStripMenuItem_Click);
            // 
            // 選択中の票を印刷PToolStripMenuItem
            // 
            this.選択中の票を印刷PToolStripMenuItem.Name = "選択中の票を印刷PToolStripMenuItem";
            this.選択中の票を印刷PToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.選択中の票を印刷PToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.選択中の票を印刷PToolStripMenuItem.Text = "選択中の票のタグを印刷(&P)...";
            this.選択中の票を印刷PToolStripMenuItem.Click += new System.EventHandler(this.選択中の票を印刷PToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(243, 6);
            // 
            // ログイン画面に戻るLToolStripMenuItem
            // 
            this.ログイン画面に戻るLToolStripMenuItem.Name = "ログイン画面に戻るLToolStripMenuItem";
            this.ログイン画面に戻るLToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.ログイン画面に戻るLToolStripMenuItem.Text = "ログイン画面に戻る(&L)";
            this.ログイン画面に戻るLToolStripMenuItem.Click += new System.EventHandler(this.ログイン画面に戻るLToolStripMenuItem_Click);
            // 
            // 終了XToolStripMenuItem
            // 
            this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
            this.終了XToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.終了XToolStripMenuItem.Text = "終了(&X)";
            this.終了XToolStripMenuItem.Click += new System.EventHandler(this.終了XToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新Receiptを追加UToolStripMenuItem,
            this.最新の情報に更新RToolStripMenuItem,
            this.toolStripSeparator2,
            this.品番カウンタをセットしなおすToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(61, 20);
            this.toolStripMenuItem2.Text = "データ(&D)";
            // 
            // 新Receiptを追加UToolStripMenuItem
            // 
            this.新Receiptを追加UToolStripMenuItem.Name = "新Receiptを追加UToolStripMenuItem";
            this.新Receiptを追加UToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.新Receiptを追加UToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.新Receiptを追加UToolStripMenuItem.Text = "新Receiptを追加... (&U)";
            this.新Receiptを追加UToolStripMenuItem.Click += new System.EventHandler(this.新Receiptを追加UToolStripMenuItem_Click);
            // 
            // 最新の情報に更新RToolStripMenuItem
            // 
            this.最新の情報に更新RToolStripMenuItem.Name = "最新の情報に更新RToolStripMenuItem";
            this.最新の情報に更新RToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.最新の情報に更新RToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.最新の情報に更新RToolStripMenuItem.Text = "最新の情報に更新 (&R)";
            this.最新の情報に更新RToolStripMenuItem.Click += new System.EventHandler(this.最新の情報に更新RToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(215, 6);
            // 
            // 品番カウンタをセットしなおすToolStripMenuItem
            // 
            this.品番カウンタをセットしなおすToolStripMenuItem.Name = "品番カウンタをセットしなおすToolStripMenuItem";
            this.品番カウンタをセットしなおすToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.品番カウンタをセットしなおすToolStripMenuItem.Text = "品番カウンタをセットしなおす(&C)";
            this.品番カウンタをセットしなおすToolStripMenuItem.Click += new System.EventHandler(this.品番カウンタをセットしなおすToolStripMenuItem_Click);
            // 
            // 機能ToolStripMenuItem
            // 
            this.機能ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.売却ウィンドウSToolStripMenuItem,
            this.監査ウィンドウWToolStripMenuItem,
            this.オペレーターIDを管理ToolStripMenuItem,
            this.toolStripSeparator5,
            this.返金返品ToolStripMenuItem,
            this.各部門の返金額合算ToolStripMenuItem,
            this.toolStripSeparator6,
            this.バージョン情報ToolStripMenuItem});
            this.機能ToolStripMenuItem.Name = "機能ToolStripMenuItem";
            this.機能ToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.機能ToolStripMenuItem.Text = "機能(&O)";
            // 
            // 売却ウィンドウSToolStripMenuItem
            // 
            this.売却ウィンドウSToolStripMenuItem.Name = "売却ウィンドウSToolStripMenuItem";
            this.売却ウィンドウSToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.売却ウィンドウSToolStripMenuItem.Text = "売却ウィンドウ (&S)";
            this.売却ウィンドウSToolStripMenuItem.Click += new System.EventHandler(this.売却ウィンドウSToolStripMenuItem_Click);
            // 
            // 監査ウィンドウWToolStripMenuItem
            // 
            this.監査ウィンドウWToolStripMenuItem.Name = "監査ウィンドウWToolStripMenuItem";
            this.監査ウィンドウWToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.監査ウィンドウWToolStripMenuItem.Text = "監査ウィンドウ (&W)";
            this.監査ウィンドウWToolStripMenuItem.Click += new System.EventHandler(this.監査ウィンドウWToolStripMenuItem_Click);
            // 
            // オペレーターIDを管理ToolStripMenuItem
            // 
            this.オペレーターIDを管理ToolStripMenuItem.Name = "オペレーターIDを管理ToolStripMenuItem";
            this.オペレーターIDを管理ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.オペレーターIDを管理ToolStripMenuItem.Text = "オペレーターIDを管理 (&O)";
            this.オペレーターIDを管理ToolStripMenuItem.Click += new System.EventHandler(this.オペレーターIDを管理ToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(193, 6);
            // 
            // 返金返品ToolStripMenuItem
            // 
            this.返金返品ToolStripMenuItem.Name = "返金返品ToolStripMenuItem";
            this.返金返品ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.返金返品ToolStripMenuItem.Text = "返金・返品 (&R)";
            this.返金返品ToolStripMenuItem.Click += new System.EventHandler(this.返金返品ToolStripMenuItem_Click);
            // 
            // 各部門の返金額合算ToolStripMenuItem
            // 
            this.各部門の返金額合算ToolStripMenuItem.Name = "各部門の返金額合算ToolStripMenuItem";
            this.各部門の返金額合算ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.各部門の返金額合算ToolStripMenuItem.Text = "各部門の返金額合算 (&G)";
            this.各部門の返金額合算ToolStripMenuItem.Click += new System.EventHandler(this.各部門の返金額合算ToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(193, 6);
            // 
            // バージョン情報ToolStripMenuItem
            // 
            this.バージョン情報ToolStripMenuItem.Name = "バージョン情報ToolStripMenuItem";
            this.バージョン情報ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.バージョン情報ToolStripMenuItem.Text = "バージョン情報 (&A)";
            this.バージョン情報ToolStripMenuItem.Click += new System.EventHandler(this.バージョン情報ToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(141, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "更新 (F5)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label_company
            // 
            this.label_company.AutoSize = true;
            this.label_company.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label_company.Location = new System.Drawing.Point(443, 36);
            this.label_company.Name = "label_company";
            this.label_company.Size = new System.Drawing.Size(81, 12);
            this.label_company.TabIndex = 11;
            this.label_company.Text = "CompanyName";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 392);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(713, 22);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 414);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label_company);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "ゆかり姫萌え萌えソフトウェア";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cSVで出力CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 楓ちゃん形式で出力KToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ログイン画面に戻るLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ページ設定UToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 選択中の票を印刷PToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem 品番カウンタをセットしなおすToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新Receiptを追加UToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 最新の情報に更新RToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 最近追加された商品リストLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 機能ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem バージョン情報ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 売却ウィンドウSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 監査ウィンドウWToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem オペレーターIDを管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem 返金返品ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 各部門の返金額合算ToolStripMenuItem;
        private System.Windows.Forms.Label label_company;
        private System.Windows.Forms.ToolStripMenuItem タグ印刷ごとにダイアログを表示ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
    }
}

