namespace kaede2nd
{
    partial class Version
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Version));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_ver = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label_date = new System.Windows.Forms.Label();
            this.label_yukari = new System.Windows.Forms.Label();
            this.label_kaede = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "ゆかり姫萌え萌えソフトウェア\r\n  -\"Kaede\" 2nd Generation\r\n";
            this.label1.UseMnemonic = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(227, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "著作権および著作者人格権については、\r\n同梱の LICENSE.txt ファイルを参照してください";
            // 
            // label_ver
            // 
            this.label_ver.AutoSize = true;
            this.label_ver.Location = new System.Drawing.Point(70, 58);
            this.label_ver.Name = "label_ver";
            this.label_ver.Size = new System.Drawing.Size(23, 12);
            this.label_ver.TabIndex = 2;
            this.label_ver.Text = "svn";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(14, 216);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(232, 121);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // label_date
            // 
            this.label_date.AutoSize = true;
            this.label_date.Location = new System.Drawing.Point(12, 143);
            this.label_date.Name = "label_date";
            this.label_date.Size = new System.Drawing.Size(98, 12);
            this.label_date.TabIndex = 4;
            this.label_date.Text = "王国歴n年m月n日";
            // 
            // label_yukari
            // 
            this.label_yukari.AutoSize = true;
            this.label_yukari.Location = new System.Drawing.Point(12, 164);
            this.label_yukari.Name = "label_yukari";
            this.label_yukari.Size = new System.Drawing.Size(202, 12);
            this.label_yukari.TabIndex = 5;
            this.label_yukari.Text = "ゆかり姫の誕生日まで、あとn日と00:00:00";
            // 
            // label_kaede
            // 
            this.label_kaede.AutoSize = true;
            this.label_kaede.Location = new System.Drawing.Point(12, 185);
            this.label_kaede.Name = "label_kaede";
            this.label_kaede.Size = new System.Drawing.Size(201, 12);
            this.label_kaede.TabIndex = 6;
            this.label_kaede.Text = "楓ちゃんの誕生日まで、あとn日と00:00:00";
            // 
            // Version
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 349);
            this.Controls.Add(this.label_kaede);
            this.Controls.Add(this.label_yukari);
            this.Controls.Add(this.label_date);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label_ver);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Version";
            this.Text = "ゆかり姫萌え萌えソフトウェアについて";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_ver;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label_date;
        private System.Windows.Forms.Label label_yukari;
        private System.Windows.Forms.Label label_kaede;
    }
}