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
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // text_host
            // 
            this.text_host.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.text_host.Location = new System.Drawing.Point(104, 45);
            this.text_host.Name = "text_host";
            this.text_host.Size = new System.Drawing.Size(100, 19);
            this.text_host.TabIndex = 0;
            this.text_host.Text = "chihaya";
            // 
            // text_port
            // 
            this.text_port.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.text_port.Location = new System.Drawing.Point(104, 70);
            this.text_port.Name = "text_port";
            this.text_port.Size = new System.Drawing.Size(100, 19);
            this.text_port.TabIndex = 1;
            this.text_port.Text = "3306";
            // 
            // text_user
            // 
            this.text_user.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.text_user.Location = new System.Drawing.Point(104, 95);
            this.text_user.Name = "text_user";
            this.text_user.Size = new System.Drawing.Size(100, 19);
            this.text_user.TabIndex = 2;
            this.text_user.Text = "ennichi";
            // 
            // text_pass
            // 
            this.text_pass.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.text_pass.Location = new System.Drawing.Point(104, 120);
            this.text_pass.Name = "text_pass";
            this.text_pass.Size = new System.Drawing.Size(100, 19);
            this.text_pass.TabIndex = 3;
            this.text_pass.Text = "itsuki";
            // 
            // text_dbname
            // 
            this.text_dbname.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.text_dbname.Location = new System.Drawing.Point(104, 145);
            this.text_dbname.Name = "text_dbname";
            this.text_dbname.Size = new System.Drawing.Size(100, 19);
            this.text_dbname.TabIndex = 4;
            this.text_dbname.Text = "ennichidb";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(129, 185);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(129, 214);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "ガラクタ",
            "古本",
            "サンドボックスG",
            "-"});
            this.comboBox1.Location = new System.Drawing.Point(12, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(172, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(99, 20);
            this.button3.TabIndex = 8;
            this.button3.Text = "backupのほう";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.ControlBox = false;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.text_dbname);
            this.Controls.Add(this.text_pass);
            this.Controls.Add(this.text_user);
            this.Controls.Add(this.text_port);
            this.Controls.Add(this.text_host);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LoginForm";
            this.Text = "部門の選択を";
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
        private System.Windows.Forms.Button button3;
    }
}