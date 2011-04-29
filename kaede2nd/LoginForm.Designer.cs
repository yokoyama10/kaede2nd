namespace kaede2nd
{
    partial class LoginForm
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
            this.text_host = new System.Windows.Forms.TextBox();
            this.text_port = new System.Windows.Forms.TextBox();
            this.text_user = new System.Windows.Forms.TextBox();
            this.text_pass = new System.Windows.Forms.TextBox();
            this.text_dbname = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox_MySQL_readonly = new System.Windows.Forms.CheckBox();
            this.label_backupdest = new System.Windows.Forms.Label();
            this.textBox_backupdest = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox_dbtype = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.button_SQLite_create = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBox_SQLite_readonly = new System.Windows.Forms.CheckBox();
            this.button_SQLite_open = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // text_host
            // 
            this.text_host.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.text_host.Location = new System.Drawing.Point(110, 43);
            this.text_host.Name = "text_host";
            this.text_host.Size = new System.Drawing.Size(100, 19);
            this.text_host.TabIndex = 2;
            this.text_host.Text = "hostname";
            // 
            // text_port
            // 
            this.text_port.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.text_port.Location = new System.Drawing.Point(110, 68);
            this.text_port.Name = "text_port";
            this.text_port.Size = new System.Drawing.Size(100, 19);
            this.text_port.TabIndex = 3;
            this.text_port.Text = "3306";
            // 
            // text_user
            // 
            this.text_user.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.text_user.Location = new System.Drawing.Point(110, 93);
            this.text_user.Name = "text_user";
            this.text_user.Size = new System.Drawing.Size(100, 19);
            this.text_user.TabIndex = 4;
            this.text_user.Text = "user";
            // 
            // text_pass
            // 
            this.text_pass.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.text_pass.Location = new System.Drawing.Point(110, 118);
            this.text_pass.Name = "text_pass";
            this.text_pass.Size = new System.Drawing.Size(100, 19);
            this.text_pass.TabIndex = 5;
            this.text_pass.Text = "pass";
            // 
            // text_dbname
            // 
            this.text_dbname.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.text_dbname.Location = new System.Drawing.Point(110, 143);
            this.text_dbname.Name = "text_dbname";
            this.text_dbname.Size = new System.Drawing.Size(100, 19);
            this.text_dbname.TabIndex = 6;
            this.text_dbname.Text = "ennichidb";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(135, 234);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(232, 366);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 100;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(18, 10);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "Host";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(75, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "User";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(75, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "Pass";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(76, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "Db";
            // 
            // checkBox_MySQL_readonly
            // 
            this.checkBox_MySQL_readonly.AutoSize = true;
            this.checkBox_MySQL_readonly.Location = new System.Drawing.Point(78, 212);
            this.checkBox_MySQL_readonly.Name = "checkBox_MySQL_readonly";
            this.checkBox_MySQL_readonly.Size = new System.Drawing.Size(91, 16);
            this.checkBox_MySQL_readonly.TabIndex = 8;
            this.checkBox_MySQL_readonly.Text = "読み取り専用";
            this.checkBox_MySQL_readonly.UseVisualStyleBackColor = true;
            // 
            // label_backupdest
            // 
            this.label_backupdest.AutoSize = true;
            this.label_backupdest.Location = new System.Drawing.Point(16, 271);
            this.label_backupdest.Name = "label_backupdest";
            this.label_backupdest.Size = new System.Drawing.Size(163, 12);
            this.label_backupdest.TabIndex = 15;
            this.label_backupdest.Text = "Ctrl+Sでバックアップを出力する先:";
            // 
            // textBox_backupdest
            // 
            this.textBox_backupdest.Location = new System.Drawing.Point(18, 288);
            this.textBox_backupdest.Name = "textBox_backupdest";
            this.textBox_backupdest.Size = new System.Drawing.Size(247, 19);
            this.textBox_backupdest.TabIndex = 10;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(296, 343);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.comboBox_dbtype);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Controls.Add(this.checkBox_MySQL_readonly);
            this.tabPage1.Controls.Add(this.textBox_backupdest);
            this.tabPage1.Controls.Add(this.text_host);
            this.tabPage1.Controls.Add(this.label_backupdest);
            this.tabPage1.Controls.Add(this.text_port);
            this.tabPage1.Controls.Add(this.text_user);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.text_pass);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.text_dbname);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(288, 317);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "サーバーに接続";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(76, 180);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 12);
            this.label10.TabIndex = 19;
            this.label10.Text = "Type";
            // 
            // comboBox_dbtype
            // 
            this.comboBox_dbtype.FormattingEnabled = true;
            this.comboBox_dbtype.Items.AddRange(new object[] {
            "MySQL",
            "MSSQL"});
            this.comboBox_dbtype.Location = new System.Drawing.Point(110, 177);
            this.comboBox_dbtype.Name = "comboBox_dbtype";
            this.comboBox_dbtype.Size = new System.Drawing.Size(100, 20);
            this.comboBox_dbtype.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label6.Location = new System.Drawing.Point(179, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "MySQL / MSSQL";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.button_SQLite_create);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.checkBox_SQLite_readonly);
            this.tabPage2.Controls.Add(this.button_SQLite_open);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(288, 317);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ファイルを開く";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(122, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 12);
            this.label9.TabIndex = 23;
            this.label9.Text = "（保存は自動です）";
            // 
            // button_SQLite_create
            // 
            this.button_SQLite_create.Location = new System.Drawing.Point(21, 242);
            this.button_SQLite_create.Name = "button_SQLite_create";
            this.button_SQLite_create.Size = new System.Drawing.Size(103, 23);
            this.button_SQLite_create.TabIndex = 3;
            this.button_SQLite_create.Text = "新規作成";
            this.button_SQLite_create.UseVisualStyleBackColor = true;
            this.button_SQLite_create.Click += new System.EventHandler(this.button_SQLite_create_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 12);
            this.label8.TabIndex = 21;
            this.label8.Text = "最近使用したファイル";
            // 
            // checkBox_SQLite_readonly
            // 
            this.checkBox_SQLite_readonly.AutoSize = true;
            this.checkBox_SQLite_readonly.Location = new System.Drawing.Point(180, 217);
            this.checkBox_SQLite_readonly.Name = "checkBox_SQLite_readonly";
            this.checkBox_SQLite_readonly.Size = new System.Drawing.Size(91, 16);
            this.checkBox_SQLite_readonly.TabIndex = 2;
            this.checkBox_SQLite_readonly.Text = "読み取り専用";
            this.checkBox_SQLite_readonly.UseVisualStyleBackColor = true;
            // 
            // button_SQLite_open
            // 
            this.button_SQLite_open.Location = new System.Drawing.Point(21, 35);
            this.button_SQLite_open.Name = "button_SQLite_open";
            this.button_SQLite_open.Size = new System.Drawing.Size(125, 23);
            this.button_SQLite_open.TabIndex = 1;
            this.button_SQLite_open.Text = "ファイルを開く...";
            this.button_SQLite_open.UseVisualStyleBackColor = true;
            this.button_SQLite_open.Click += new System.EventHandler(this.button_SQLite_open_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label7.Location = new System.Drawing.Point(232, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 12);
            this.label7.TabIndex = 18;
            this.label7.Text = "SQLite";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label11.Location = new System.Drawing.Point(10, 374);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(204, 12);
            this.label11.TabIndex = 18;
            this.label11.Text = "メインフォーム表示時Ctrl押下で拡張機能";
            // 
            // LoginForm
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(319, 401);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.Text = "部門の選択を";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LoginForm_FormClosed);
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox text_host;
        private System.Windows.Forms.TextBox text_port;
        private System.Windows.Forms.TextBox text_user;
        private System.Windows.Forms.TextBox text_pass;
        private System.Windows.Forms.TextBox text_dbname;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox_MySQL_readonly;
        private System.Windows.Forms.Label label_backupdest;
        private System.Windows.Forms.TextBox textBox_backupdest;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBox_SQLite_readonly;
        private System.Windows.Forms.Button button_SQLite_open;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button_SQLite_create;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox_dbtype;
        private System.Windows.Forms.Label label11;
    }
}