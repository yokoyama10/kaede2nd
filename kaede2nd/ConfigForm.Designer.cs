namespace kaede2nd
{
    partial class ConfigForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_bumon = new System.Windows.Forms.TextBox();
            this.textBox_company = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_barcode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button_color = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkBox_imeon = new System.Windows.Forms.CheckBox();
            this.checkBox_entertotab = new System.Windows.Forms.CheckBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button_color2 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(304, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "※この画面での設定は通常変更の必要はありません。\r\n設定は（PC一台ごとではなく）部門データに対して適用されます。";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "部門名";
            // 
            // textBox_bumon
            // 
            this.textBox_bumon.Location = new System.Drawing.Point(161, 89);
            this.textBox_bumon.Name = "textBox_bumon";
            this.textBox_bumon.Size = new System.Drawing.Size(231, 19);
            this.textBox_bumon.TabIndex = 0;
            this.textBox_bumon.Text = "bumonname";
            // 
            // textBox_company
            // 
            this.textBox_company.Location = new System.Drawing.Point(161, 123);
            this.textBox_company.Name = "textBox_company";
            this.textBox_company.Size = new System.Drawing.Size(231, 19);
            this.textBox_company.TabIndex = 1;
            this.textBox_company.Text = "companyname";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "長い部門名（タグ印字）";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 234);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "バーコード識別子";
            // 
            // textBox_barcode
            // 
            this.textBox_barcode.Location = new System.Drawing.Point(161, 231);
            this.textBox_barcode.MaxLength = 2;
            this.textBox_barcode.Name = "textBox_barcode";
            this.textBox_barcode.Size = new System.Drawing.Size(39, 19);
            this.textBox_barcode.TabIndex = 4;
            this.textBox_barcode.Text = "00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(206, 234);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "（数字二文字）";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label6.Location = new System.Drawing.Point(138, 253);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(280, 24);
            this.label6.TabIndex = 8;
            this.label6.Text = "変更すると以前のバーコードは読み取れなくなります\r\nガラクタ：期数 (ex. 58)、古本：期数-50 (ex. 08) など推奨";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "表の背景色";
            // 
            // button_color
            // 
            this.button_color.Location = new System.Drawing.Point(225, 157);
            this.button_color.Name = "button_color";
            this.button_color.Size = new System.Drawing.Size(45, 23);
            this.button_color.TabIndex = 2;
            this.button_color.Text = "変更";
            this.button_color.UseVisualStyleBackColor = true;
            this.button_color.Click += new System.EventHandler(this.button_color_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(161, 157);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(58, 23);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // checkBox_imeon
            // 
            this.checkBox_imeon.AutoSize = true;
            this.checkBox_imeon.Location = new System.Drawing.Point(14, 300);
            this.checkBox_imeon.Name = "checkBox_imeon";
            this.checkBox_imeon.Size = new System.Drawing.Size(156, 16);
            this.checkBox_imeon.TabIndex = 5;
            this.checkBox_imeon.Text = "商品名欄でIMEをオンにする";
            this.checkBox_imeon.UseVisualStyleBackColor = true;
            // 
            // checkBox_entertotab
            // 
            this.checkBox_entertotab.AutoSize = true;
            this.checkBox_entertotab.Location = new System.Drawing.Point(14, 322);
            this.checkBox_entertotab.Name = "checkBox_entertotab";
            this.checkBox_entertotab.Size = new System.Drawing.Size(196, 16);
            this.checkBox_entertotab.TabIndex = 6;
            this.checkBox_entertotab.Text = "商品名欄でのEnter押下で右に進む";
            this.checkBox_entertotab.UseVisualStyleBackColor = true;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(242, 367);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 12;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(330, 367);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 13;
            this.button_cancel.Text = "キャンセル";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Black;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(161, 194);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(58, 23);
            this.pictureBox2.TabIndex = 16;
            this.pictureBox2.TabStop = false;
            // 
            // button_color2
            // 
            this.button_color2.Location = new System.Drawing.Point(225, 194);
            this.button_color2.Name = "button_color2";
            this.button_color2.Size = new System.Drawing.Size(45, 23);
            this.button_color2.TabIndex = 3;
            this.button_color2.Text = "変更";
            this.button_color2.UseVisualStyleBackColor = true;
            this.button_color2.Click += new System.EventHandler(this.button_color2_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 199);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "タグの部門名印刷色";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label9.Location = new System.Drawing.Point(276, 199);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 12);
            this.label9.TabIndex = 17;
            this.label9.Text = "黒推奨（インク的に）";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 372);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(206, 12);
            this.label10.TabIndex = 18;
            this.label10.Text = "[OK] で完了後、部門選択画面に戻ります";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label11.Location = new System.Drawing.Point(223, 301);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(121, 12);
            this.label11.TabIndex = 19;
            this.label11.Text = "ガラクタon、古本off推奨";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label12.Location = new System.Drawing.Point(223, 323);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(122, 24);
            this.label12.TabIndex = 20;
            this.label12.Text = "ガラクタoff、古本on推奨\r\n（バーコードリーダー向け）\r\n";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label13.Location = new System.Drawing.Point(12, 50);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(367, 24);
            this.label13.TabIndex = 21;
            this.label13.Text = "この画面は、登録「受付票」 (Receipt) 数が0の場合には自動表示されます。\r\n設定が完了している場合は [キャンセル] で閉じてください。";
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(428, 404);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button_color2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.checkBox_entertotab);
            this.Controls.Add(this.checkBox_imeon);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_color);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_barcode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_company);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_bumon);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Tag = "";
            this.Text = "部門データの設定";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_bumon;
        private System.Windows.Forms.TextBox textBox_company;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_barcode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_color;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBox_imeon;
        private System.Windows.Forms.CheckBox checkBox_entertotab;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button_color2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
    }
}