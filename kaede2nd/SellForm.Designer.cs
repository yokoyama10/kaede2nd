namespace kaede2nd
{
    partial class SellForm
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
            this.textBox_ban = new System.Windows.Forms.TextBox();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_baika = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_teika = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_nebiki = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label_sellzumi = new System.Windows.Forms.Label();
            this.button_mibai = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "品番orバーコード";
            // 
            // textBox_ban
            // 
            this.textBox_ban.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_ban.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_ban.Location = new System.Drawing.Point(122, 20);
            this.textBox_ban.Name = "textBox_ban";
            this.textBox_ban.Size = new System.Drawing.Size(100, 28);
            this.textBox_ban.TabIndex = 0;
            this.textBox_ban.Text = "品番";
            this.textBox_ban.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_ban.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_ban_KeyDown);
            this.textBox_ban.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // textBox_name
            // 
            this.textBox_name.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_name.Location = new System.Drawing.Point(86, 75);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.ReadOnly = true;
            this.textBox_name.Size = new System.Drawing.Size(249, 23);
            this.textBox_name.TabIndex = 2;
            this.textBox_name.TabStop = false;
            this.textBox_name.Text = "商品名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "商品名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "売価";
            // 
            // textBox_baika
            // 
            this.textBox_baika.Font = new System.Drawing.Font("MS UI Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_baika.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_baika.Location = new System.Drawing.Point(113, 179);
            this.textBox_baika.Name = "textBox_baika";
            this.textBox_baika.Size = new System.Drawing.Size(109, 34);
            this.textBox_baika.TabIndex = 1;
            this.textBox_baika.Text = "0";
            this.textBox_baika.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_baika.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_baika_KeyDown);
            this.textBox_baika.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(86, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "\\";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(241, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "入力してEnter";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(241, 195);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "入力してEnter";
            this.label6.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(68, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "\\";
            // 
            // textBox_teika
            // 
            this.textBox_teika.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_teika.Location = new System.Drawing.Point(86, 108);
            this.textBox_teika.Name = "textBox_teika";
            this.textBox_teika.ReadOnly = true;
            this.textBox_teika.Size = new System.Drawing.Size(63, 23);
            this.textBox_teika.TabIndex = 10;
            this.textBox_teika.TabStop = false;
            this.textBox_teika.Text = "0";
            this.textBox_teika.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 115);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 9;
            this.label8.Text = "定価";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(177, 115);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "値引";
            // 
            // textBox_nebiki
            // 
            this.textBox_nebiki.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_nebiki.Location = new System.Drawing.Point(212, 108);
            this.textBox_nebiki.Name = "textBox_nebiki";
            this.textBox_nebiki.ReadOnly = true;
            this.textBox_nebiki.Size = new System.Drawing.Size(50, 23);
            this.textBox_nebiki.TabIndex = 13;
            this.textBox_nebiki.TabStop = false;
            this.textBox_nebiki.Text = "不可";
            this.textBox_nebiki.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 255);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(169, 12);
            this.label10.TabIndex = 14;
            this.label10.Text = "最後の変更を取り消すには Ctrl+Z";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.label11.Location = new System.Drawing.Point(111, 219);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(152, 12);
            this.label11.TabIndex = 15;
            this.label11.Text = "定価通りの売却は \"-\" を入力";
            // 
            // label_sellzumi
            // 
            this.label_sellzumi.AutoSize = true;
            this.label_sellzumi.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_sellzumi.ForeColor = System.Drawing.Color.Red;
            this.label_sellzumi.Location = new System.Drawing.Point(19, 146);
            this.label_sellzumi.Name = "label_sellzumi";
            this.label_sellzumi.Size = new System.Drawing.Size(145, 19);
            this.label_sellzumi.TabIndex = 16;
            this.label_sellzumi.Text = "売却済 00,000円";
            this.label_sellzumi.Visible = false;
            // 
            // button_mibai
            // 
            this.button_mibai.Location = new System.Drawing.Point(203, 142);
            this.button_mibai.Name = "button_mibai";
            this.button_mibai.Size = new System.Drawing.Size(112, 23);
            this.button_mibai.TabIndex = 17;
            this.button_mibai.Text = "未売却にする (&M)";
            this.button_mibai.UseVisualStyleBackColor = true;
            this.button_mibai.Click += new System.EventHandler(this.button_mibai_Click);
            // 
            // SellForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 319);
            this.Controls.Add(this.button_mibai);
            this.Controls.Add(this.label_sellzumi);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox_nebiki);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox_teika);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_baika);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.textBox_ban);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SellForm";
            this.Text = "売却";
            this.Load += new System.EventHandler(this.SellForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SellForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_ban;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_baika;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_teika;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_nebiki;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label_sellzumi;
        private System.Windows.Forms.Button button_mibai;
    }
}