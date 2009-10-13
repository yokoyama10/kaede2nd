namespace kaede2nd
{
    partial class MyItemFormBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyItemFormBase));
            this.contextMenuStrip_rowHeader = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.行メニュー_toolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.まとめて設定toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.まとめて定価設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.まとめて返品設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.タグを印刷ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.アイテムを削除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rowHeader_toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.キャンセル_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_rowHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip_rowHeader
            // 
            this.contextMenuStrip_rowHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.行メニュー_toolStripLabel,
            this.まとめて設定toolStripMenuItem1,
            this.タグを印刷ToolStripMenuItem,
            this.アイテムを削除ToolStripMenuItem,
            this.rowHeader_toolStripSeparator1,
            this.キャンセル_toolStripMenuItem});
            this.contextMenuStrip_rowHeader.Name = "contextMenuStrip_rowHeader";
            this.contextMenuStrip_rowHeader.ShowImageMargin = false;
            this.contextMenuStrip_rowHeader.Size = new System.Drawing.Size(138, 135);
            // 
            // 行メニュー_toolStripLabel
            // 
            this.行メニュー_toolStripLabel.BackColor = System.Drawing.SystemColors.Control;
            this.行メニュー_toolStripLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.行メニュー_toolStripLabel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.行メニュー_toolStripLabel.Name = "行メニュー_toolStripLabel";
            this.行メニュー_toolStripLabel.Size = new System.Drawing.Size(97, 12);
            this.行メニュー_toolStripLabel.Text = "行メニュー (Ctrl+D)";
            // 
            // まとめて設定toolStripMenuItem1
            // 
            this.まとめて設定toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.まとめて定価設定ToolStripMenuItem,
            this.まとめて返品設定ToolStripMenuItem});
            this.まとめて設定toolStripMenuItem1.Name = "まとめて設定toolStripMenuItem1";
            this.まとめて設定toolStripMenuItem1.Size = new System.Drawing.Size(137, 22);
            this.まとめて設定toolStripMenuItem1.Text = "まとめて設定";
            // 
            // まとめて定価設定ToolStripMenuItem
            // 
            this.まとめて定価設定ToolStripMenuItem.Name = "まとめて定価設定ToolStripMenuItem";
            this.まとめて定価設定ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.まとめて定価設定ToolStripMenuItem.Text = "まとめて定価設定...";
            this.まとめて定価設定ToolStripMenuItem.Click += new System.EventHandler(this.まとめて定価設定ToolStripMenuItem_Click);
            // 
            // まとめて返品設定ToolStripMenuItem
            // 
            this.まとめて返品設定ToolStripMenuItem.Name = "まとめて返品設定ToolStripMenuItem";
            this.まとめて返品設定ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.まとめて返品設定ToolStripMenuItem.Text = "まとめて返品設定...";
            this.まとめて返品設定ToolStripMenuItem.Click += new System.EventHandler(this.まとめて返品設定ToolStripMenuItem_Click);
            // 
            // タグを印刷ToolStripMenuItem
            // 
            this.タグを印刷ToolStripMenuItem.Name = "タグを印刷ToolStripMenuItem";
            this.タグを印刷ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.タグを印刷ToolStripMenuItem.Text = "タグを印刷...";
            this.タグを印刷ToolStripMenuItem.Click += new System.EventHandler(this.タグを印刷ToolStripMenuItem_Click);
            // 
            // アイテムを削除ToolStripMenuItem
            // 
            this.アイテムを削除ToolStripMenuItem.Name = "アイテムを削除ToolStripMenuItem";
            this.アイテムを削除ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.アイテムを削除ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.アイテムを削除ToolStripMenuItem.Text = "アイテムを削除";
            this.アイテムを削除ToolStripMenuItem.Click += new System.EventHandler(this.アイテムを削除ToolStripMenuItem_Click);
            // 
            // rowHeader_toolStripSeparator1
            // 
            this.rowHeader_toolStripSeparator1.Name = "rowHeader_toolStripSeparator1";
            this.rowHeader_toolStripSeparator1.Size = new System.Drawing.Size(134, 6);
            // 
            // キャンセル_toolStripMenuItem
            // 
            this.キャンセル_toolStripMenuItem.Name = "キャンセル_toolStripMenuItem";
            this.キャンセル_toolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.キャンセル_toolStripMenuItem.Text = "キャンセル";
            // 
            // MyItemFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Name = "MyItemFormBase";
            this.Text = "MyItemFormBase";
            this.contextMenuStrip_rowHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.ContextMenuStrip contextMenuStrip_rowHeader;
        protected System.Windows.Forms.ToolStripLabel 行メニュー_toolStripLabel;
        protected System.Windows.Forms.ToolStripMenuItem まとめて設定toolStripMenuItem1;
        protected System.Windows.Forms.ToolStripMenuItem まとめて定価設定ToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem まとめて返品設定ToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem タグを印刷ToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem アイテムを削除ToolStripMenuItem;
        protected System.Windows.Forms.ToolStripSeparator rowHeader_toolStripSeparator1;
        protected System.Windows.Forms.ToolStripMenuItem キャンセル_toolStripMenuItem;
    }
}